using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace JackAnalyzer
{
    class VMWriter
    {
        StreamWriter sr;
        Dictionary<string, string> commandDictionnary = new Dictionary<string, string>();
        SymbolTable subroutineSymbolTable = new SymbolTable();
        SymbolTable classSymbolTable = new SymbolTable();

        public VMWriter(string fileIn)
        {
            sr = new StreamWriter(fileIn);
            buildCommandDictionnary();
        }
        public void setCurrentClassTable(SymbolTable ClassTable)
        {
            classSymbolTable = ClassTable;
        }
        public void delCurrentClassTable()
        {
            classSymbolTable = new SymbolTable();
        }


        public void setCurrentSubroutineTable(SymbolTable SubroutineTable)
        {
            subroutineSymbolTable = SubroutineTable;
        }

        public void delCurrentSubroutineTable()
        {
            subroutineSymbolTable = new SymbolTable();
        }


        public void WritePush(string Segment, string index)
        {
            string instr = $"push {Segment} {index}";
            sr.WriteLine(instr);
        }

        public void WritePop(string Segment, int index)
        {
            
            string instr = $"pop {Segment} {index}";
            sr.WriteLine(instr);
        }

        public void WritePopValue(string value)
        {
            value = value.Replace("<identifier>", "").Replace("</identifier>", "").Trim();
            string segment = getKind(value, subroutineSymbolTable, classSymbolTable);
            string counter = getCounter(value, subroutineSymbolTable, classSymbolTable);

            WritePop(segment, Convert.ToInt32(counter));

        }
        public void WritePushValue(string value)
        {
            value = value.Replace("<identifier>", "").Replace("</identifier>", "").Trim();
            string segment = getKind(value, subroutineSymbolTable, classSymbolTable);
            string counter = getCounter(value, subroutineSymbolTable, classSymbolTable);

            WritePush(segment, counter);

        }



        public void WriteArithmetic(string command)
        {
            command = command.Replace("</symbol>","").Replace("<symbol>","").Trim();

            string instr = $"{commandDictionnary[command]}";
            sr.WriteLine(instr);
        }

        public void WriteStringConstant(string expression)
        {
            expression = expression.Replace(" </stringConstant>", "").Replace("<stringConstant> ", "");
            WritePush("constant", expression.Length.ToString());
            writeCall("String.new", 1);
            foreach (char chr in expression)
            {
                int charVal = (int)chr;
                WritePush("constant", charVal.ToString());
                writeCall("String.appendChar", 2);

            }
        }


        public void WriteLabel(string label)
        {
            string instr = $"label {label}";
            sr.WriteLine(instr);
        }

        public void WriteGoto(string label)
        {
            string instr = $"goto {label}";
            sr.WriteLine(instr);
        }

        public void WriteIf(string label)
        {
            string instr = $"if-goto {label}";
            sr.WriteLine(instr);
        }

        public void writeFunction(string className ,string name, int nLocals)
        {
            className = className.Replace("<identifier>", "").Replace("</identifier>", "").Trim();
            name = name.Replace("<identifier>","").Replace("</identifier>","").Trim();
            string instr = $"function {className}.{name} {nLocals}";
            sr.WriteLine(instr);
        }

        public void writeCall(string name, int nbArguments)
        {
            name = name.Replace("<identifier>", "").Replace("</identifier>", "").Trim();
            string instr = $"call {name} {nbArguments}";
            sr.WriteLine(instr);
        }

        public void writeReturn()
        {
            string instr = $"return";
            sr.WriteLine(instr);
        }

        public void close()
        {
            sr.Close();
        }

        public string getKind(string value, SymbolTable subroutineSymbolTable, SymbolTable classSymbolTable)
        {
            if (value.Contains("<integerConstant>"))
            {
                return "constant";
            }

            if (subroutineSymbolTable.entryExists(value))
            {
                return subroutineSymbolTable.KindOf(value);
            }
            if (classSymbolTable.entryExists(value))
            {
                return classSymbolTable.KindOf(value);
            }
            if (value == "null")
            {
                return "constant";
            }
            else return "error";

        }

        public string getCounter(string value, SymbolTable subroutineSymbolTable, SymbolTable classSymbolTable)
        {
            if (value.Contains("<integerConstant>"))
            {
                return value.Replace("<integerConstant>", "").Replace("</integerConstant>", "").Trim();
            }
            if (subroutineSymbolTable.entryExists(value))
            {
                return subroutineSymbolTable.IndexOf(value);
            }
            if (classSymbolTable.entryExists(value))
            {
                return classSymbolTable.IndexOf(value);
            }
            if (value == "null")
            {
                return "0";
            }
            else return "error";

        }

        public string getType(string value)
        {
            if (value.Contains("<integerConstant>"))
            {
                return value.Replace("<integerConstant>", "").Replace("</integerConstant>", "").Trim();
            }
            if (subroutineSymbolTable.entryExists(value))
            {
                return subroutineSymbolTable.TypeOf(value);
            }
            if (classSymbolTable.entryExists(value))
            {
                return classSymbolTable.TypeOf(value);
            }
            if (value == "null")
            {
                return "0";
            }
            else return "error";

        }

        public bool entryExists(string name)
        {
            if(subroutineSymbolTable.entryExists(name) == false && classSymbolTable.entryExists(name) == false)
            {
                return false;
            }
            else
            {
                return true;
            }
            
        }

        public void codeWrite(string expression,SymbolTable subroutineSymbolTable, SymbolTable classSymbolTable)
        {
            string express_temp = expression;

            if(int.TryParse(expression,out int ignoreMe))
            {
                WritePush(getKind(expression, subroutineSymbolTable, classSymbolTable), getCounter(expression, subroutineSymbolTable, classSymbolTable));
            }


        }


        public void buildCommandDictionnary()
        {
            commandDictionnary.Add("*", "call Math.multiply 2");
            commandDictionnary.Add("+", "add");
            commandDictionnary.Add("-", "sub");
            commandDictionnary.Add("&lt;", "lt"); 
            commandDictionnary.Add("&gt;", "gt"); 
            commandDictionnary.Add("&amp;", "and"); 
            commandDictionnary.Add("/", "call Math.divide 2");
            commandDictionnary.Add("not", "not");
            commandDictionnary.Add("=", "eq");
            commandDictionnary.Add("neg", "neg");

        }

    }
}
