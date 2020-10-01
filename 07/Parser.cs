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



        public Parser(string inputFile)
        {
            sr = new StreamReader(inputFile);
        }


        public void advance()
        {
            currentLine = sr.ReadLine();
            hasMoreCommands = !sr.EndOfStream;
            commandType = getCommandType();

            if (hasMoreCommands)
            {    
                getArg1();
                getArg2();
            }
        }

        public string getCommandType()
        {
            string cdType = currentLine.Split(' ')[0].Trim();

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

            if (cdType.Trim() == "push") { return "C_PUSH"; };
            if (cdType.Trim() == "pop") { return "C_POP"; };
            if ( arithmCds.Contains( cdType.Trim()) ) { return "Arithm"; }
            else
            {
                return "null";
            }
        }

        public void getArg1()
        {
            if(commandType == "C_PUSH" || commandType == "C_POP")
            {
                arg1 = currentLine.Split(' ')[1].Trim();
            }
        }

        public void getArg2()
        {
            if (commandType == "C_PUSH" || commandType == "C_POP")
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
