using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace VMTranslator
{
    class Parser
    {

        private StreamReader sr;

        public string currentLine;
        public bool hasMoreCommands = true;
        public string commandType;
        public string arg1;
        public string arg2;
        public string inAr;


        public Parser(string inputFile)
        {
            sr = new StreamReader(inputFile);
        }


        public void advance()
        {
            currentLine = sr.ReadLine();
            currentLine = clean(currentLine);
            hasMoreCommands = !sr.EndOfStream;
            commandType = getCommandType();
            getArg1();
            getArg2();

            if (hasMoreCommands)
            {    
            }

            if (currentLine == "push local 0")
            {
                Console.WriteLine($" args : {arg1}, {arg2}");
            }

        }
        public string clean(string instr)
        {
            string outstr = instr.Replace("\t", " ");
            return outstr;
        }

        public string getCommandType()
        {
            if (currentLine.StartsWith("lt"))
            {
                
                int e =1;
            }
            string cdType = currentLine.Split(' ')[0].Trim();
            inAr = cdType;

            List<string> arithmCds = new List<string>();
            arithmCds.Add("add");
            arithmCds.Add("sub");
            arithmCds.Add("neg");
            arithmCds.Add("eq");
            arithmCds.Add("gt");
            arithmCds.Add("lt");
            arithmCds.Add("and");
            arithmCds.Add("or");
            arithmCds.Add("not");

            if (cdType.Trim() == "push") { return "C_PUSH"; }
            else if (cdType.Trim() == "pop") { return "C_POP"; }
            else if ( arithmCds.Contains( cdType.Trim()) ) { return "Arithm"; }
            else if (cdType.Trim() == "label") { return "C_LABEL"; }
            else if (cdType.Trim() == "if-goto") { return "C_IFGOTO"; }
            else if (cdType.Trim() == "goto") { return "C_GOTO"; }
            else if (cdType.Trim() == "function") { return "C_FUNC"; }
            else if (cdType.Trim() == "return") { return "C_RETURN"; }
            else if (cdType.Trim() == "call") { return "C_CALL"; }
            else
            {
                return "null";
            }
        }

        public void getArg1()
        {
            if(commandType == "C_PUSH" || commandType == "C_POP" || commandType == "C_LABEL" || commandType == "C_IFGOTO" || commandType == "C_GOTO" || commandType == "C_FUNC" || commandType == "C_CALL")
            {
                //Console.WriteLine("push");
                arg1 = currentLine.Split(' ')[1].Trim();
            }


        }

        public void getArg2()
        {
            if (commandType == "C_PUSH" || commandType == "C_POP" || commandType == "C_FUNC" || commandType == "C_CALL")
            {
                arg2 = currentLine.Split(' ')[2].Trim();
            }
        }

        public void close()
        {
            sr.Close();
        }




    }
}
