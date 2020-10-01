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
        public Parser(string inFile)
        {
            fileBeingRead = new Tokenizer();
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
            finalInstructions.Add("<class>");
            currentInstruction = printAndAdvance("XML");
            finalInstructions.Add(currentInstruction); // class
            currentInstruction = printAndAdvance("XML");
            finalInstructions.Add(currentInstruction); // point
            currentInstruction = printAndAdvance("XML");
            finalInstructions.Add(currentInstruction); // {
            compileSubroutineDec();
            currentInstruction = printAndAdvance("XML");
            finalInstructions.Add(currentInstruction); // }
            finalInstructions.Add("</class>");
        }
        public void compileClassVarDec() {
            if (isNextItemClassVarDec())
            {
                finalInstructions.Add("<classVarDec>");
                string currentInstruction;
                currentInstruction = printAndAdvance("XML");
                finalInstructions.Add(currentInstruction); // var
                currentInstruction = printAndAdvance("XML");
                finalInstructions.Add(currentInstruction); // Array
                currentInstruction = printAndAdvance("XML");
                finalInstructions.Add(currentInstruction); // a
                while (nextChar() == ",")
                {
                    currentInstruction = printAndAdvance("XML");
                    finalInstructions.Add(currentInstruction); // ,
                    currentInstruction = printAndAdvance("XML");
                    finalInstructions.Add(currentInstruction); // b
                }
                if (isNextItemClassVarDec())
                {
                    compileClassVarDec();
                }
                currentInstruction = printAndAdvance("XML");
                finalInstructions.Add(currentInstruction); // ;
                finalInstructions.Add("</classVarDec>");
            }

        }
        public void compileSubroutineDec() {
            string currentInstruction;

                while (nextChar() != "}")
                {
                    string test = nextChar();
                    if (nextChar() == "field" || nextChar() == "static")
                    {
                        compileClassVarDec();
                    }
                    else if (nextChar() == "function" || nextChar() == "subroutine" || nextChar() == "method" || nextChar() == "constructor")
                    {
                        finalInstructions.Add("<subroutineDec>");
                        currentInstruction = printAndAdvance("XML");
                        finalInstructions.Add(currentInstruction); // method
                        currentInstruction = printAndAdvance("XML");
                        finalInstructions.Add(currentInstruction); // int
                        currentInstruction = printAndAdvance("XML");
                        finalInstructions.Add(currentInstruction); // getx
                        currentInstruction = printAndAdvance("XML");
                        finalInstructions.Add(currentInstruction); // (
                        finalInstructions.Add("<parameterList>");
                        compileParameterList();
                        finalInstructions.Add("</parameterList>");
                        currentInstruction = printAndAdvance("XML");
                        finalInstructions.Add(currentInstruction); // )
                        compileSubroutineBody();
                        finalInstructions.Add("</subroutineDec>");
                    }

                    if(currentIndex == 264)
                    {
                        break;
                    }

                }


        }
        public void compileParameterList() {
            string currentInstruction;

            if(nextChar() != ")")
            {
                currentInstruction = printAndAdvance("XML");
                finalInstructions.Add(currentInstruction); // int
                currentInstruction = printAndAdvance("XML");
                finalInstructions.Add(currentInstruction); // val
            }
            if (nextChar() == ",")
            {
                currentInstruction = printAndAdvance("XML");
                finalInstructions.Add(currentInstruction); // ,
                compileParameterList();
            }

        }

        public void compileSubroutineBody() {
            finalInstructions.Add("<subroutineBody>");
            string currentInstruction;

            currentInstruction = printAndAdvance("XML");
            finalInstructions.Add(currentInstruction); // {
            
            compileVarDec();

            compileStatements();
            
            currentInstruction = printAndAdvance("XML");
            finalInstructions.Add(currentInstruction); // }
            finalInstructions.Add("</subroutineBody>");

        }
        public void compileVarDec() {
            if (isNextItemVarDev())
            {
                finalInstructions.Add("<varDec>");
                string currentInstruction;
                currentInstruction = printAndAdvance("XML");
                finalInstructions.Add(currentInstruction); // var
                currentInstruction = printAndAdvance("XML");
                finalInstructions.Add(currentInstruction); // Array
                currentInstruction = printAndAdvance("XML");
                finalInstructions.Add(currentInstruction); // a
                
                string nextCharA = nextChar();
                while ( nextCharA == ",") {
                    
                    currentInstruction = printAndAdvance("XML");
                    finalInstructions.Add(currentInstruction); // ,
                    currentInstruction = printAndAdvance("XML");
                    finalInstructions.Add(currentInstruction); // sum
                    nextCharA = nextChar();
                }

                currentInstruction = printAndAdvance("XML");
                finalInstructions.Add(currentInstruction); // ;
                finalInstructions.Add("</varDec>");

                if (isNextItemVarDev())
                {
                    compileVarDec();
                }
            }
        
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
            finalInstructions.Add("<expression>");
            compileTerm();
            finalInstructions.Add("</expression>");
            currentInstruction = printAndAdvance("XML");
            finalInstructions.Add(currentInstruction); // )
            currentInstruction = printAndAdvance("XML");
            finalInstructions.Add(currentInstruction); // {
            compileStatements();
            currentInstruction = printAndAdvance("XML");
            finalInstructions.Add(currentInstruction); // }

            if (nextChar() == "else")
            {
                currentInstruction = printAndAdvance("XML");
                finalInstructions.Add(currentInstruction); // else
                currentInstruction = printAndAdvance("XML");
                finalInstructions.Add(currentInstruction); // {
                compileStatements();
                currentInstruction = printAndAdvance("XML");
                finalInstructions.Add(currentInstruction); // }
            }
            finalInstructions.Add("</ifStatement>");
            //while (isNextLineStatement(fileBeingRead.getElementValueAt(currentIndex)))
            //{
            //    compileStatements();

            //}

        }
        public void compileLetStatement()
        {
            string currentInstruction;
            finalInstructions.Add("<letStatement>");
            currentInstruction = printAndAdvance("XML");
            finalInstructions.Add(currentInstruction); // let
            currentInstruction = printAndAdvance("XML");
            finalInstructions.Add(currentInstruction); // count
            if (nextChar() == "[")
            {
                currentInstruction = printAndAdvance("XML");
                finalInstructions.Add(currentInstruction); // [
                finalInstructions.Add("<expression>");
                compileExpression();
                finalInstructions.Add("</expression>");
                currentInstruction = printAndAdvance("XML");
                finalInstructions.Add(currentInstruction); // ]
            }
            currentInstruction = printAndAdvance("XML");
            finalInstructions.Add(currentInstruction); // =
            finalInstructions.Add("<expression>");
            compileTerm();
            finalInstructions.Add("</expression>");
            currentInstruction = printAndAdvance("XML");
            finalInstructions.Add(currentInstruction); // ;
            finalInstructions.Add("</letStatement>");
        }
        public void compileWhileStatement()
        {

            string currentInstruction;
            finalInstructions.Add("<whileStatement>");
            currentInstruction = printAndAdvance("XML");
            finalInstructions.Add(currentInstruction); //while
            currentInstruction = printAndAdvance("XML");
            finalInstructions.Add(currentInstruction); // (
            finalInstructions.Add("<expression>");
            compileExpression();
            finalInstructions.Add("</expression>");
            currentInstruction = printAndAdvance("XML");
            finalInstructions.Add(currentInstruction); // )
            currentInstruction = printAndAdvance("XML");
            finalInstructions.Add(currentInstruction); // {
            compileStatements();
            currentInstruction = printAndAdvance("XML");
            finalInstructions.Add(currentInstruction); // }

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
                currentInstruction = printAndAdvance("XML");
                finalInstructions.Add(currentInstruction); // [
                finalInstructions.Add("<expression>");
                compileExpression();
                finalInstructions.Add("</expression>");
                currentInstruction = printAndAdvance("XML");
                finalInstructions.Add(currentInstruction); // ]

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
                currentInstruction = printAndAdvance("XML");
                finalInstructions.Add(currentInstruction); // .
                currentInstruction = printAndAdvance("XML");
                finalInstructions.Add(currentInstruction); // new
                currentInstruction = printAndAdvance("XML");
                finalInstructions.Add(currentInstruction); // (
                
                finalInstructions.Add("<expressionList>");
                compileExpressionList();
                finalInstructions.Add("</expressionList>");

                currentInstruction = printAndAdvance("XML");
                finalInstructions.Add(currentInstruction); // )
            }
            else if (termType == "subroutine2")
            {
                currentInstruction = printAndAdvance("XML");
                finalInstructions.Add(currentInstruction); // circle
                currentInstruction = printAndAdvance("XML");
                finalInstructions.Add(currentInstruction); // (
                finalInstructions.Add("<expressionList>");
                compileExpressionList();
                finalInstructions.Add("</expressionList>");
                currentInstruction = printAndAdvance("XML");
                finalInstructions.Add(currentInstruction); // )
            }
            else if (termType == "negType")
            {
                currentInstruction = printAndAdvance("XML");
                finalInstructions.Add(currentInstruction); // -
                compileTerm();
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
                    //finalInstructions.Add("<expression>");
                    compileExpression();
                    //finalInstructions.Add("</expression>");
                }

            }
            else {
                currentInstruction = printAndAdvance("XML");
                finalInstructions.Add(currentInstruction); // count
            }


            finalInstructions.Add("</term>");

            if (isOp(fileBeingRead.getElementValueAt(currentIndex)))
            {
                currentInstruction = printAndAdvance("XML");
                finalInstructions.Add(currentInstruction); // =
                compileTerm();
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

            finalInstructions.Add("<doStatement>");
            currentInstruction = printAndAdvance("XML");
            finalInstructions.Add(currentInstruction); // do
            currentInstruction = printAndAdvance("XML");
            finalInstructions.Add(currentInstruction); // output
            if (nextChar() == "(")
            {
                currentInstruction = printAndAdvance("XML");
                finalInstructions.Add(currentInstruction); // (
                finalInstructions.Add("<expressionList>");
                compileExpressionList();
                finalInstructions.Add("</expressionList>");
                currentInstruction = printAndAdvance("XML");
                finalInstructions.Add(currentInstruction); // )
                currentInstruction = printAndAdvance("XML");
                finalInstructions.Add(currentInstruction); // ;
            }
            else
            {
                currentInstruction = printAndAdvance("XML");
                finalInstructions.Add(currentInstruction); // .
                currentInstruction = printAndAdvance("XML");
                finalInstructions.Add(currentInstruction); // println
                currentInstruction = printAndAdvance("XML");
                finalInstructions.Add(currentInstruction); // (
                finalInstructions.Add("<expressionList>");
                compileExpressionList();
                finalInstructions.Add("</expressionList>");
                currentInstruction = printAndAdvance("XML");
                finalInstructions.Add(currentInstruction); // )
                currentInstruction = printAndAdvance("XML");
                finalInstructions.Add(currentInstruction); // ;
            }
            finalInstructions.Add("</doStatement>");


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
            finalInstructions.Add("</returnStatement>");

        }
        public void compileExpressionList() {
            if (isSpeChar2(nextChar()))
            {
                return;
            }
            finalInstructions.Add("<expression>");
            compileExpression();
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
                } 
                while (isSameExpressionChar(nextChar()));
                finalInstructions.Add("</expression>");
            }
            
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
