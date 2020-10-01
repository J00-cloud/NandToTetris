using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace JackAnalyzer
{
    class Parser
    {
        public List<string> finalInstructions = new List<string>();
        public int currentIndex = 0;
        public Tokenizer fileBeingRead;
        public VMWriter currentVMWriter;
        public SymbolTable currentClassTable;
        public SymbolTable currentSubroutineTable;
        string currentFunctionType;
        int whileCount = 0;
        int ifCount = 0;
        string currentClass = "";
        public Parser(string inFile)
        {
            fileBeingRead = new Tokenizer();
            currentVMWriter = new VMWriter(inFile.Replace(".jack",".vm"));
            fileBeingRead.buildWordList(inFile);
        }



        public string printAndAdvance(string value) {
            currentIndex++;
            if (value == "XML")
            {
                return fileBeingRead.getXMLValueAt(currentIndex - 1);
            }
            else
            {
                return fileBeingRead.getElementValueAt(currentIndex - 1);
            }
        }

        internal void printToFile(string outFile)
        {
            currentVMWriter.close();
            int totalSpaces = 0;
            string printedSpaces = "";

            using(StreamWriter sw = new StreamWriter(outFile))
            {
                foreach (string line in finalInstructions)
                {
                    if (line.StartsWith("</"))
                    {
                        totalSpaces = totalSpaces - 2;
                    }

                    printedSpaces = generateSpaces( totalSpaces);
                    sw.WriteLine(printedSpaces + line);
                    
                    if (!line.Contains("</"))
                    {
                        totalSpaces = totalSpaces + 2;
                    }

                }

            }

        }

        private string generateSpaces(int nbSpaces)
        {
            string spaces = "";
            for (int i = 0; i < nbSpaces; i++)
            {
                spaces = $"{spaces} ";
            }
            return spaces;
        }

        public void Advance()
        {
            currentIndex++;
        }

        public void compileClass()
        {
           
            string currentInstruction;
            string classType;
            

            finalInstructions.Add("<class>");
            currentClassTable = new SymbolTable();
            currentClassTable.startSubroutine();
            currentVMWriter.setCurrentClassTable(currentClassTable);

            currentInstruction = printAndAdvance("XML");
            finalInstructions.Add(currentInstruction); // class
            currentInstruction = printAndAdvance("XML");
            finalInstructions.Add(currentInstruction); // point

            classType = currentInstruction;
            currentClass = classType.Replace("<identifier>", "").Replace("</identifier>", "").Trim();

            currentInstruction = printAndAdvance("XML");
            finalInstructions.Add(currentInstruction); // {

            compileSubroutineDec(classType);
            
            currentInstruction = printAndAdvance("XML");
            finalInstructions.Add(currentInstruction); // }
            finalInstructions.Add("</class>");

            currentClassTable.clear();
            currentVMWriter.delCurrentClassTable();
        }
        public int compileClassVarDec(int countVars = 0) {

            if (isNextItemClassVarDec())
            {
                finalInstructions.Add("<classVarDec>");
                string currentInstruction;
                currentInstruction = printAndAdvance("XML");
                finalInstructions.Add(currentInstruction); // var
                string kind = currentInstruction;
                currentInstruction = printAndAdvance("XML");
                finalInstructions.Add(currentInstruction); // Array
                string type = currentInstruction;
                currentInstruction = printAndAdvance("XML");
                finalInstructions.Add(currentInstruction); // a
                string name = currentInstruction;
                currentClassTable.Define(name, type, kind);
                countVars++;
                while (nextChar() == ",")
                {
                    currentInstruction = printAndAdvance("XML");
                    finalInstructions.Add(currentInstruction); // ,
                    currentInstruction = printAndAdvance("XML");
                    finalInstructions.Add(currentInstruction); // b
                    name = currentInstruction;
                    currentClassTable.Define(name, type, kind);
                    countVars++;
                }

                currentInstruction = printAndAdvance("XML");
                finalInstructions.Add(currentInstruction); // ;
                if (isNextItemClassVarDec())
                {
                    countVars = compileClassVarDec(countVars);
                }
                finalInstructions.Add("</classVarDec>");
            }
            return countVars;

        }
        public void compileSubroutineDec(string classType) {
            string currentInstruction;
            string functionType;
            int countVars= 0;
                while (nextChar() != "}")
                {
                    string test = nextChar();
                    if (nextChar() == "field" || nextChar() == "static")
                    {
                        countVars = compileClassVarDec();
                    }
                    else if (nextChar() == "function" || nextChar() == "subroutine" || nextChar() == "method" || nextChar() == "constructor")
                    {
                        functionType = nextChar();
                        currentSubroutineTable = new SymbolTable();
                        currentSubroutineTable.startSubroutine();
                        currentVMWriter.setCurrentSubroutineTable(currentSubroutineTable);

                        if (nextChar() == "method")
                        {
                            currentSubroutineTable.Define("this", classType, "argument");

                    }
                        finalInstructions.Add("<subroutineDec>");
                        currentInstruction = printAndAdvance("XML");
                        finalInstructions.Add(currentInstruction); // method
                        currentInstruction = printAndAdvance("XML");
                        finalInstructions.Add(currentInstruction); // int

                        currentFunctionType = currentInstruction;

                        currentInstruction = printAndAdvance("XML");
                        finalInstructions.Add(currentInstruction); // getx

                        string functionName = currentInstruction;

                        currentInstruction = printAndAdvance("XML");
                        finalInstructions.Add(currentInstruction); // (
                        finalInstructions.Add("<parameterList>");

                        int countParameters = compileParameterList();

                        finalInstructions.Add("</parameterList>");
                        currentInstruction = printAndAdvance("XML");



                        finalInstructions.Add(currentInstruction); // )

                        compileSubroutineBody(classType, functionName,functionType,countVars);

                        

                        finalInstructions.Add("</subroutineDec>");
                        


                    //currentVMWriter.writeReturn();

                        currentSubroutineTable.clear();
                        currentVMWriter.delCurrentSubroutineTable();
                    ifCount = 0;
                    whileCount = 0;
                }
                    


                }


        }
        public int compileParameterList(int countParameters = 0) {
            string currentInstruction;
            if (nextChar() != ")")
            {
                currentInstruction = printAndAdvance("XML");
                finalInstructions.Add(currentInstruction); // int
                string type = currentInstruction;
                currentInstruction = printAndAdvance("XML");
                finalInstructions.Add(currentInstruction); // val
                string name = currentInstruction;
                currentSubroutineTable.Define(name, type, "argument");
                countParameters++;
            }
            if (nextChar() == ",")
            {
                currentInstruction = printAndAdvance("XML");
                finalInstructions.Add(currentInstruction); // ,
                //countParameters++;
                countParameters = compileParameterList(countParameters);
            }
            return countParameters;

        }

        public void compileSubroutineBody(string classType, string functionName,string functionType, int countParameters) {
            finalInstructions.Add("<subroutineBody>");
            string currentInstruction;

            currentInstruction = printAndAdvance("XML");
            finalInstructions.Add(currentInstruction); // {
            
            int varDecs = compileVarDec();

            currentVMWriter.writeFunction(classType, functionName, varDecs);
            if (functionType == "constructor")
            {
                currentVMWriter.WritePush("constant", countParameters.ToString());
                currentVMWriter.writeCall("Memory.alloc", 1);
                currentVMWriter.WritePop("pointer", 0);
            }
            if (functionType == "method")
            {
                currentVMWriter.WritePush("argument", "0");
                currentVMWriter.WritePop("pointer", 0);
            }
            compileStatements();
            
            currentInstruction = printAndAdvance("XML");
            finalInstructions.Add(currentInstruction); // }
            finalInstructions.Add("</subroutineBody>");

        }
        public int compileVarDec(int countVars = 0) {

            
            if (isNextItemVarDev())
            {
                finalInstructions.Add("<varDec>");
                string currentInstruction;
                currentInstruction = printAndAdvance("XML");
                finalInstructions.Add(currentInstruction); // var
                
                currentInstruction = printAndAdvance("XML");
                finalInstructions.Add(currentInstruction); // Array
                string type = currentInstruction;
                
                currentInstruction = printAndAdvance("XML");
                finalInstructions.Add(currentInstruction); // a
                string name = currentInstruction;

                currentSubroutineTable.Define(name, type, "local");

                string nextCharA = nextChar();
                countVars++;

                while ( nextCharA == ",") {
                    
                    currentInstruction = printAndAdvance("XML");
                    finalInstructions.Add(currentInstruction); // ,
                    currentInstruction = printAndAdvance("XML");
                    finalInstructions.Add(currentInstruction); // sum
                    name = currentInstruction;

                    currentSubroutineTable.Define(name, type, "local");

                    nextCharA = nextChar();
                    countVars++;
                }

                currentInstruction = printAndAdvance("XML");
                finalInstructions.Add(currentInstruction); // ;
                finalInstructions.Add("</varDec>");

                if (isNextItemVarDev())
                {
                    countVars = compileVarDec(countVars);
                }

            }
            return countVars;
        
        }

        public void compileStatements()
        {
            finalInstructions.Add("<statements>");

            while (isNextLineStatement(fileBeingRead.getElementValueAt(currentIndex)))
            {
                if (fileBeingRead.getElementValueAt(currentIndex) == "if")
                {
                    compileIfStatement();
                }
                else if (fileBeingRead.getElementValueAt(currentIndex) == "while")
                {
                    compileWhileStatement();
                }
                else if (fileBeingRead.getElementValueAt(currentIndex) == "let")
                {
                    compileLetStatement();
                }
                else if (fileBeingRead.getElementValueAt(currentIndex) == "do")
                {
                    compileDo();
                }
                else if (fileBeingRead.getElementValueAt(currentIndex) == "return")
                {
                    compileReturn();
                }
            }
            finalInstructions.Add("</statements>");

        }

        public void compileIfStatement()
        {
            string currentInstruction;
            finalInstructions.Add("<ifStatement>");
            currentInstruction = printAndAdvance("XML");
            finalInstructions.Add(currentInstruction); // if
            currentInstruction = printAndAdvance("XML");
            finalInstructions.Add(currentInstruction); // (
            List<string> expressionTermList = new List<string>();
            finalInstructions.Add("<expression>");
            compileTerm();
            finalInstructions.Add("</expression>");
            currentInstruction = printAndAdvance("XML");
            finalInstructions.Add(currentInstruction); // )

            currentVMWriter.WriteIf($"IF_TRUE{ifCount}");
            currentVMWriter.WriteGoto($"IF_FALSE{ifCount}");
            currentVMWriter.WriteLabel($"IF_TRUE{ifCount}");

            int currentIfCount = ifCount;
            ifCount++;

            currentInstruction = printAndAdvance("XML");
            finalInstructions.Add(currentInstruction); // {
            compileStatements();
            currentInstruction = printAndAdvance("XML");
            finalInstructions.Add(currentInstruction); // }

            //ifCount--;



            if (nextChar() == "else")
            {
                currentVMWriter.WriteGoto($"IF_END{currentIfCount}");
                currentVMWriter.WriteLabel($"IF_FALSE{currentIfCount}");
                currentInstruction = printAndAdvance("XML");
                finalInstructions.Add(currentInstruction); // else
                currentInstruction = printAndAdvance("XML");
                finalInstructions.Add(currentInstruction); // {
                compileStatements();
                currentInstruction = printAndAdvance("XML");
                finalInstructions.Add(currentInstruction); // }
                currentVMWriter.WriteLabel($"IF_END{currentIfCount}");
            }
            else
            {
                currentVMWriter.WriteLabel($"IF_FALSE{currentIfCount}");
            }

            finalInstructions.Add("</ifStatement>");
            

        }
        public void compileLetStatement()
        {
            bool array = false;

            string currentInstruction;
            finalInstructions.Add("<letStatement>");
            currentInstruction = printAndAdvance("XML");
            finalInstructions.Add(currentInstruction); // let
            currentInstruction = printAndAdvance("XML");
            finalInstructions.Add(currentInstruction); // count
            string valtoPop = currentInstruction;
            if (nextChar() == "[")
            {
                array = true;
                
                currentInstruction = printAndAdvance("XML");
                finalInstructions.Add(currentInstruction); // [
                
                finalInstructions.Add("<expression>");
                compileExpression();
                finalInstructions.Add("</expression>");
                currentInstruction = printAndAdvance("XML");
                finalInstructions.Add(currentInstruction); // ]

                currentVMWriter.WritePushValue(valtoPop);
                currentVMWriter.WriteArithmetic("+");
            }
            currentInstruction = printAndAdvance("XML");
            finalInstructions.Add(currentInstruction); // =
            
            finalInstructions.Add("<expression>");
            compileExpression();
            finalInstructions.Add("</expression>");
            currentInstruction = printAndAdvance("XML");

            if (array== true)
            {
                currentVMWriter.WritePop("temp", 0);
                currentVMWriter.WritePop("pointer", 1);
                currentVMWriter.WritePush("temp", "0");
                currentVMWriter.WritePop("that", 0);
            }
            else
            {
                currentVMWriter.WritePopValue(valtoPop);
            }

            finalInstructions.Add(currentInstruction); // ;
            finalInstructions.Add("</letStatement>");
        }
        public void compileWhileStatement()
        {

            string currentInstruction;
            finalInstructions.Add("<whileStatement>");
            currentInstruction = printAndAdvance("XML");
            finalInstructions.Add(currentInstruction); //while

            currentVMWriter.WriteLabel($"WHILE_EXP{whileCount}");

            currentInstruction = printAndAdvance("XML");
            finalInstructions.Add(currentInstruction); // (
            finalInstructions.Add("<expression>");
            compileExpression();
            finalInstructions.Add("</expression>");
            currentInstruction = printAndAdvance("XML");
            finalInstructions.Add(currentInstruction); // )

            currentVMWriter.WriteArithmetic("not");
            currentVMWriter.WriteIf($"WHILE_END{whileCount}");
            int currentWhile = whileCount;
            whileCount++;

            currentInstruction = printAndAdvance("XML");
            finalInstructions.Add(currentInstruction); // {
            compileStatements();
            currentInstruction = printAndAdvance("XML");
            finalInstructions.Add(currentInstruction); // }

            //whileCount--;

            currentVMWriter.WriteGoto($"WHILE_EXP{currentWhile}");
            currentVMWriter.WriteLabel($"WHILE_END{currentWhile}");

            finalInstructions.Add("</whileStatement>");

            

            return;
        }
        public void compileTerm()
        {
            string v1 = nextChar();
            string v2 = fileBeingRead.getElementValueAt(currentIndex + 1);
            string v3 = fileBeingRead.getElementValueAt(currentIndex + 2);

            string termType = getTermType(v1,v2 ,v3 );
            string currentInstruction;

            finalInstructions.Add("<term>");

            if (termType == "arrayType")
            {
                currentInstruction = printAndAdvance("XML");
                finalInstructions.Add(currentInstruction); // VarName

                string arrayName = currentInstruction;

                currentInstruction = printAndAdvance("XML");
                finalInstructions.Add(currentInstruction); // [
                finalInstructions.Add("<expression>");
                compileExpression();
                finalInstructions.Add("</expression>");
                currentInstruction = printAndAdvance("XML");
                finalInstructions.Add(currentInstruction); // ]

                currentVMWriter.WritePushValue(arrayName);
                currentVMWriter.WriteArithmetic("+");
                currentVMWriter.WritePop("pointer", 1);
                currentVMWriter.WritePush("that", "0");


            }
            else if (termType == "parenthesis")
            {
                currentInstruction = printAndAdvance("XML");
                finalInstructions.Add(currentInstruction); // (
                finalInstructions.Add("<expression>");
                compileExpression();
                finalInstructions.Add("</expression>");
                currentInstruction = printAndAdvance("XML");
                finalInstructions.Add(currentInstruction); // )
            }
            else if (termType == "subroutine1")
            {
                currentInstruction = printAndAdvance("XML");
                finalInstructions.Add(currentInstruction); // array
                string className = currentInstruction.Replace("<identifier>", "").Replace("</identifier>", "").Trim();
                currentInstruction = printAndAdvance("XML");
                finalInstructions.Add(currentInstruction); // .
                currentInstruction = printAndAdvance("XML");
                finalInstructions.Add(currentInstruction); // new
                string methodName = currentInstruction.Replace("<identifier>", "").Replace("</identifier>", "").Trim();
                currentInstruction = printAndAdvance("XML");
                finalInstructions.Add(currentInstruction); // (
                
                finalInstructions.Add("<expressionList>");
                int expreCount = compileExpressionList();
                finalInstructions.Add("</expressionList>");

                // if className (as value) is in classTable or subroutine table == method
                // if method, push local 0 and replace method name by kind
                // finally, add 1 to expreCount

                if (currentVMWriter.entryExists(className) && (methodName != "new"))
                {
                    //currentVMWriter.WritePushValue(className);
                    expreCount++;
                    className = currentVMWriter.getType(className).Trim();
                }

                currentVMWriter.writeCall(className + "." + methodName, expreCount);

                currentInstruction = printAndAdvance("XML");
                finalInstructions.Add(currentInstruction); // )
            }
            else if (termType == "subroutine2")
            {
                currentInstruction = printAndAdvance("XML");
                finalInstructions.Add(currentInstruction); // circle
                string functionCalled = currentInstruction.Trim();
                currentInstruction = printAndAdvance("XML");
                finalInstructions.Add(currentInstruction); // (
                finalInstructions.Add("<expressionList>");
                int nbExpressions = compileExpressionList();
                finalInstructions.Add("</expressionList>");
                currentInstruction = printAndAdvance("XML");
                finalInstructions.Add(currentInstruction); // )

                currentVMWriter.writeCall(functionCalled, nbExpressions); // call circle
            }
            else if (termType == "negType")
            {
                currentInstruction = printAndAdvance("XML");
                finalInstructions.Add(currentInstruction); // -
                compileTerm();
                currentVMWriter.WriteArithmetic("neg"); // push -
            }
            else if (termType == "notType")
            {
                currentInstruction = printAndAdvance("XML");
                finalInstructions.Add(currentInstruction); // ~
                if(nextChar() == "(")
                {
                    currentInstruction = printAndAdvance("XML");
                    finalInstructions.Add(currentInstruction); // (
                    finalInstructions.Add("<expressionList>");
                    compileExpressionList();
                    finalInstructions.Add("</expressionList>");
                    currentInstruction = printAndAdvance("XML");
                    finalInstructions.Add(currentInstruction); // )
                    
                }
                else
                {
                    
                    compileExpression();
                    
                }
                currentVMWriter.WriteArithmetic("not");

            }
            else {
                currentInstruction = printAndAdvance("XML");
                finalInstructions.Add(currentInstruction); // a

                if (currentInstruction.Contains("<stringConstant>"))
                {
                    currentVMWriter.WriteStringConstant(currentInstruction);
                }
                else if(currentInstruction.Contains("<keyword>") && currentInstruction.Contains("true"))
                {
                    currentVMWriter.WritePush("constant", "0");
                    currentVMWriter.WriteArithmetic("not");
                }
                else if (currentInstruction.Contains("<keyword>") && currentInstruction.Contains("false"))
                {
                    currentVMWriter.WritePush("constant", "0");
                }
                else if (currentInstruction.Contains("<keyword>") && currentInstruction.Contains("null"))
                {
                    currentVMWriter.WritePush("constant", "0");
                }
                else if (currentInstruction.Contains("<keyword>") && currentInstruction.Contains("this"))
                {
                    currentVMWriter.WritePush("pointer", "0");
                }

                else 
                {
                    //currentVMWriter.WritePush(currentVMWriter.getKind(currentInstruction, currentClassTable, currentSubroutineTable), currentVMWriter.getCounter(currentInstruction, currentClassTable, currentSubroutineTable)); // push a
                    currentVMWriter.WritePushValue(currentInstruction);
                }

            }


            finalInstructions.Add("</term>");

            if (isOp(fileBeingRead.getElementValueAt(currentIndex)))
            {
                currentInstruction = printAndAdvance("XML");
                finalInstructions.Add(currentInstruction); // +
                string operat = currentInstruction;
                compileTerm();
                currentVMWriter.WriteArithmetic(operat); // push +
            }
        }

        private string getTermType(string v1, string v2, string v3)
        {
            if(v1 == "~")
            {
                return "notType";
            }
            if (v1 == "-")
            {
                return "negType";
            }
            if (v2 == "[")
            {
                return "arrayType";
            }
            if (v1 == "(")
            {
                return "parenthesis";
            }
            if(v2=="." )
            {
                return "subroutine1";
            }
            if(v2 == "(")
            {
                return "subroutine2";
            }

            return fileBeingRead.getType(v1);

        }

        public string nextChar(bool xml = false)
        {
            if (xml == false)
            {
                return fileBeingRead.getElementValueAt(currentIndex);
            }
            else
            {
                return fileBeingRead.getXMLValueAt(currentIndex);
            }
        }

        public void compileDo() {

            string currentInstruction;
            int expressionListCount = 0;
            string expressName = "";

            finalInstructions.Add("<doStatement>");
            currentInstruction = printAndAdvance("XML");
            finalInstructions.Add(currentInstruction); // do
            currentInstruction = printAndAdvance("XML");
            finalInstructions.Add(currentInstruction); // output
            expressName = currentInstruction.Replace("<identifier>", "").Replace("</identifier>", "").Trim();

            if (nextChar() == "(")
            {
                currentInstruction = printAndAdvance("XML");
                finalInstructions.Add(currentInstruction); // (
                finalInstructions.Add("<expressionList>");
                expressionListCount = compileExpressionList();
                finalInstructions.Add("</expressionList>");
                currentInstruction = printAndAdvance("XML");
                finalInstructions.Add(currentInstruction); // )
                currentInstruction = printAndAdvance("XML");
                finalInstructions.Add(currentInstruction); // ;
                expressName = currentClass + "." + expressName;
                expressionListCount++;

                // if current sub = method or constructor
                currentVMWriter.WritePush("pointer", "0");

            }
            else
            {
                bool isObject = false;
                if (currentVMWriter.entryExists(expressName))
                {
                    //expreCount++;
                    currentVMWriter.WritePushValue(expressName);
                    expressName = currentVMWriter.getType(expressName);
                    isObject = true;
                    
                    // push getType + getCounter of ExpressName
                }
                
                currentInstruction = printAndAdvance("XML");
                finalInstructions.Add(currentInstruction); // .
                currentInstruction = printAndAdvance("XML");
                finalInstructions.Add(currentInstruction); // println
                expressName = expressName.Trim() + "." + currentInstruction.Replace("<identifier>", "").Replace("</identifier>", "").Trim();
                currentInstruction = printAndAdvance("XML");
                finalInstructions.Add(currentInstruction); // (
                finalInstructions.Add("<expressionList>");
                expressionListCount = compileExpressionList();
                finalInstructions.Add("</expressionList>");
                currentInstruction = printAndAdvance("XML");
                finalInstructions.Add(currentInstruction); // )
                currentInstruction = printAndAdvance("XML");
                finalInstructions.Add(currentInstruction); // ;
                if (isObject)
                {
                    expressionListCount++;
                }
            }



            currentVMWriter.writeCall(expressName, expressionListCount);
            finalInstructions.Add("</doStatement>");
            currentVMWriter.WritePop("temp", 0);


        }
        public void compileReturn() {
            string currentInstruction;

            finalInstructions.Add("<returnStatement>");
            currentInstruction = printAndAdvance("XML");
            finalInstructions.Add(currentInstruction); // return
            if(nextChar() != ";")
            {
                
                finalInstructions.Add("<expression>");
                compileExpression();
                finalInstructions.Add("</expression>");
            }
            currentInstruction = printAndAdvance("XML");
            finalInstructions.Add(currentInstruction); // ;

            if (currentFunctionType.Contains("void"))
            {
                currentVMWriter.WritePush("constant", "0");
            }
            else
            {
                //currentVMWriter.WritePush("local", "0");
            }

            currentVMWriter.writeReturn();
            
            finalInstructions.Add("</returnStatement>");

        }
        public int compileExpressionList() {

            int nbExpressions = 0;

            if (isSpeChar2(nextChar()))
            {
                return nbExpressions;
            }

            finalInstructions.Add("<expression>");
            compileExpression();
            nbExpressions++;
            finalInstructions.Add("</expression>");
            while (isSpeChar(nextChar()))
            {
                string currentInstruction;
                currentInstruction = printAndAdvance("XML");
                finalInstructions.Add(currentInstruction); // , or + , etc
                finalInstructions.Add("<expression>");
                do
                {
                    compileExpression();
                    nbExpressions++;
                } 
                while (isSameExpressionChar(nextChar()));
                finalInstructions.Add("</expression>");
            }

            return nbExpressions;

        }
        public void compileExpression() {

                        
            compileTerm();
            
            string currentInstruction;
        }

        private bool isNextLineStatement(string value)
        {
            if (value == "if" || value == "let" || value == "while" || value == "do" || value == "return")
            {
                return true;
            }
            return false;
        }
        private bool isNextItemVarDev()
        {
            string val = nextChar();
            if (val == "var")
            {
                return true;
            }
            return false;
        }

        private bool isSameExpressionChar(string item)
        {
                string[] ops = { "+", "-", "<", ">", "&lt;", "&gt;", "/", "*", "|", "&amp;", "&" };
                if (ops.Contains(item))
                {
                    return true;
                }
                return false;
        }

        private bool isNextItemClassVarDec()
        {
            string val = nextChar();
            if (val == "field" || val == "static")
            {
                return true;
            }
            return false;


        }


        public bool isOp(string item)
        {
            string[] ops = { "+", "=", "-", "<", ">", "&lt;", "&gt;", "/", "*", "|", "&amp;", "&" };
            if (ops.Contains(item))
            {
                return true;
            }
            return false;
        }

        public bool isSpeChar(string item)
        {
            string[] ops = { "+", "=", "-", "<", ">", "&lt;", "&gt;", "/", "*", ",", "&amp;", "&" };
            if (ops.Contains(item))
            {
                return true;
            }
            return false;
        }
        public bool isSpeChar2(string item)
        {
            string[] ops = { "+", "=", "-", "<", ">", "&lt;", "&gt;", "/", "*", ",", ")", "&amp;", "&" };
            if (ops.Contains(item))
            {
                return true;
            }
            return false;
        }
    }
}
