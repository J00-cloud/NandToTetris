using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace VMTranslator
{
    class CodeWriter
    {
        public string fileWrite;
        private Dictionary<string, string> registerDictionary;
        private int checkEndEq = 0;
        private int checkEndLt = 0;
        private int checkEndGt = 0;
        private int endIf = 0;
        public string filename = "";
        private int retNb = 1;

        public CodeWriter(string fileWriteIN)
        {
            //StreamWriter sr2 = new File.apppend;
            fileWrite = fileWriteIN;
            updateRegisterDictionnary();
        }

        public void writeArithmetic(string command)
        {
            List<string> commands = new List<string>();
            commands.Add($"// {command}");

            if (command.Trim() == "add")
            {
                // saves SP in D, gets SP-1 in M
                commands.Add($"@SP"); //=@0
                commands.Add($"M=M-1");
                commands.Add($"A=M");
                commands.Add($"D=M");
                commands.Add($"A=A-1");

                commands.Add($"M=D+M");
                commands.Add($"D=A");
                commands.Add($"@SP");
                commands.Add($"M=D");
                commands.Add($"@SP"); //=@0
                commands.Add($"M=M+1");
            }
            if (command.Trim() == "sub")
            {
                // saves SP in D, gets SP-1 in M
                commands.Add($"@SP"); //=@0
                commands.Add($"M=M-1");
                commands.Add($"A=M");
                commands.Add($"D=M");
                commands.Add($"A=A-1");

                commands.Add($"M=M-D");
                commands.Add($"D=A");
                commands.Add($"@SP");
                commands.Add($"M=D");
                commands.Add($"@SP"); //=@0
                commands.Add($"M=M+1");
            }
            if (command.Trim() == "neg")
            {
                commands.Add($"@SP"); //=@0
                commands.Add($"A=M-1");
                commands.Add($"M=-M");
            }
            if (command.Trim() == "not")
            {
                commands.Add($"@SP"); //=@0
                commands.Add($"A=M-1");
                commands.Add($"M=!M");
            }
            if (command.Trim() == "and")
            {
                // saves SP in D, gets SP-1 in M
                commands.Add($"@SP"); //=@0
                commands.Add($"M=M-1");
                commands.Add($"A=M");
                commands.Add($"D=M");
                commands.Add($"A=A-1");

                commands.Add($"M=M&D");
                commands.Add($"D=A");
                commands.Add($"@SP");
                commands.Add($"M=D");
                commands.Add($"@SP"); //=@0
                commands.Add($"M=M+1");
            }
            if (command.Trim() == "or")
            {
                // saves SP in D, gets SP-1 in M
                commands.Add($"@SP"); //=@0
                commands.Add($"M=M-1");
                commands.Add($"A=M");
                commands.Add($"D=M");
                commands.Add($"A=A-1");

                commands.Add($"M=M|D");
                commands.Add($"D=A");
                commands.Add($"@SP");
                commands.Add($"M=D");
                commands.Add($"@SP"); //=@0
                commands.Add($"M=M+1");
            }
            if (command.Trim() == "eq")
            {
                // saves SP in D, gets SP-1 in M
                commands.Add($"@SP"); //=@0
                commands.Add($"M=M-1");
                commands.Add($"A=M");
                commands.Add($"D=M");
                commands.Add($"A=A-1");

                //commands.Add($"M=0"); // Makes as true
                commands.Add($"D=D-M"); // jump if not equal 0
                commands.Add($"@end.checkEq{checkEndEq}"); // Makes as true
                commands.Add($"D;JNE"); // jump if not equal 0
                                        // write -1
                commands.Add($"@SP");
                commands.Add($"M=M-1");
                commands.Add($"A=M");
                commands.Add($"M=-1");
                //go to end
                commands.Add($"@end.endIf{endIf}");
                commands.Add($"0;JEQ");
                //path 2
                commands.Add($"(end.checkEq{checkEndEq})");
                // write 0
                commands.Add($"@SP");
                commands.Add($"M=M-1");
                commands.Add($"A=M");
                commands.Add($"M=0");

                //go to end
                commands.Add($"@end.endIf{endIf}");
                commands.Add($"0;JEQ");

                commands.Add($"(end.endIf{endIf})");
                commands.Add($"@SP"); //=@0
                commands.Add($"M=M+1");

                checkEndEq++;
                endIf++;

            }
            if (command.Trim() == "lt")
            {
                // saves SP in D, gets SP-1 in M
                commands.Add($"@SP"); //=@0
                commands.Add($"M=M-1");
                commands.Add($"A=M");
                commands.Add($"D=M");
                commands.Add($"A=A-1");

                commands.Add($"D=M-D"); // jump if not equal 0
                commands.Add($"@end.checkLt{checkEndLt}"); // Makes as true
                commands.Add($"D;JLT"); // jump if not equal 0
                                        // write -1
                commands.Add($"@SP");
                commands.Add($"M=M-1");
                commands.Add($"A=M");
                commands.Add($"M=0");
                //go to end
                commands.Add($"@end.endIf{endIf}");
                commands.Add($"0;JEQ");

                //path 2
                commands.Add($"(end.checkLt{checkEndLt})");
                // write -1
                commands.Add($"@SP");
                commands.Add($"M=M-1");
                commands.Add($"A=M");
                commands.Add($"M=-1");
                //go to end
                commands.Add($"@end.endIf{endIf}");
                commands.Add($"0;JEQ");

                commands.Add($"(end.endIf{endIf})");
                commands.Add($"@SP"); //=@0
                commands.Add($"M=M+1");
                checkEndLt++;
                endIf++;

            }
            if (command.Trim() == "gt")
            {            // saves SP in D, gets SP-1 in M
                commands.Add($"@SP"); //=@0
                commands.Add($"M=M-1");
                commands.Add($"A=M");
                commands.Add($"D=M");
                commands.Add($"A=A-1");

                commands.Add($"D=M-D"); // jump if not equal 0
                commands.Add($"@end.checkGt{checkEndGt}"); // Makes as true
                commands.Add($"D;JGT"); // jump if not equal 0
                                        // write -1
                commands.Add($"@SP");
                commands.Add($"M=M-1");
                commands.Add($"A=M");
                commands.Add($"M=0");
                //go to end
                commands.Add($"@end.endIf{endIf}");
                commands.Add($"0;JEQ");

                //path 2
                commands.Add($"(end.checkGt{checkEndGt})");
                // write -1
                commands.Add($"@SP");
                commands.Add($"M=M-1");
                commands.Add($"A=M");
                commands.Add($"M=-1");
                //go to end
                commands.Add($"@end.endIf{endIf}");
                commands.Add($"0;JEQ");

                commands.Add($"(end.endIf{endIf})");
                commands.Add($"@SP"); //=@0
                commands.Add($"M=M+1");
                checkEndGt++;
                endIf++;
            }


            using (StreamWriter sw = File.AppendText(fileWrite))
            {
                foreach (string comm in commands)
                {
                    sw.WriteLine(comm);
                }
            }
        }

        public void writePushPop(string commandType, string arg1, int arg2)
        {
            List<string> commands = new List<string>();
            commands.Add($"// {commandType} {arg1} {arg2}");

            if (commandType == "C_PUSH")
            {
                if (arg2 == -1)
                {
                    bool inDictionnary = registerDictionary.ContainsKey(arg1);
                    if (inDictionnary)
                    {
                        string regName = registerDictionary[arg1];
                        commands.Add($"@{regName}");
                        commands.Add($"D=M");
                    }
                    else
                    {
                        commands.Add($"@{arg1}");
                        commands.Add($"D=A");
                    }

                    // if regName not in dictionnary
                    // @regname // will create a new variable regname
                    // D=A
                }
                else if (arg1 == "constant")
                {
                    commands.Add($"@{arg2}");
                    commands.Add($"D=A");         

                }
                else
                {
                    string regName = registerDictionary[arg1];
                    if (regName == "pointer" && arg2 == 0)
                    {
                        regName = "THIS";
                        commands.Add($"@{regName}");
                        commands.Add($"D=A");
                        commands.Add($"D=M");
                    }
                    else if (regName == "pointer" && arg2 == 1)
                    {
                        regName = "THAT";
                        commands.Add($"@{regName}");
                        commands.Add($"D=A");
                        commands.Add($"D=M");
                    }
                    else if (regName == "5")
                    {
                        commands.Add($"@{regName}");
                        commands.Add($"D=A");
                        commands.Add($"@{arg2}");
                        commands.Add($"D=D+A");
                        commands.Add($"A=D");
                        commands.Add($"D=M");
                    }
                    else if (regName == "static")
                    {
                        commands.Add($"@{filename}.{arg2}");
                        commands.Add($"D=M");
                    }
                    else
                    {
                        commands.Add($"@{regName}");
                        commands.Add($"D=M");
                        commands.Add($"@{arg2}");
                        commands.Add($"D=D+A");
                        commands.Add($"A=D");
                        commands.Add($"D=M");
                    }

                }
                //Value *SP = D
                commands.Add($"@SP");
                commands.Add($"A=M");
                commands.Add($"M=D");
                //SP++
                commands.Add($"@SP");
                commands.Add($"M=M+1");

            }
            if (commandType == "C_POP")
            {
                string regName = registerDictionary[arg1];
                if (regName == "pointer" && arg2 == 0)
                {
                    regName = "THIS";
                    commands.Add($"@{regName}");
                    commands.Add($"D=A");
                }
                else if (regName == "pointer" && arg2 == 1)
                {
                    regName = "THAT";
                    commands.Add($"@{regName}");
                    commands.Add($"D=A");
                }
                else if (regName == "5")
                {
                    commands.Add($"@{regName}");
                    commands.Add($"D=A");
                    commands.Add($"@{arg2}");
                    commands.Add($"A=D+A");
                    commands.Add($"D=A");
                }
                else if (regName == "static")
                {
                    commands.Add($"@{filename}.{arg2}");
                    commands.Add($"D=A");
                }
                else
                {
                    commands.Add($"@{regName}");
                    commands.Add($"D=M");
                    commands.Add($"@{arg2}");
                    commands.Add($"A=D+A");
                    commands.Add($"D=A");
                }


                commands.Add($"@SP");
                commands.Add($"M=M-1");
                commands.Add($"A=M");
                commands.Add($"D=D+M");
                commands.Add($"M=M-D");
                commands.Add($"M=-M");
                commands.Add($"D=D-M");
                commands.Add($"A=M");
                commands.Add($"M=D");

                if (arg2 == -1)
                {
                    commands.Add($"// {commandType} {arg1} {arg2}");
                    commands.Clear();
                    commands.Add($"@SP");
                    commands.Add($"M=M-1");
                    commands.Add($"A=M");
                    commands.Add($"D=M");
                    commands.Add($"@{regName}");
                    commands.Add($"M=D");
                }


            }


            using (StreamWriter sw = File.AppendText(fileWrite))
            {
                foreach (string command in commands)
                {
                    sw.WriteLine(command);
                }
            }

        }

        public void functionStart(string arg1, int arg2)
        {
            writeFile($"({arg1})");


            // push 0 nvars times
            for (int i = 0; i < arg2; i++)
            {
                writePushPop("C_PUSH", "constant", 0);
            }
            writeFile("// Stop initialisation");

        }

        public void functionCall(string arg1, int arg2)
        {

            writeFile("// start function declaration");

            // --- Saved Variables ---//
            //push returnAddress
            //writePushPop("C_PUSH", "SP", -1);
            //writePushPop("C_PUSH", "constant", arg2);
            //writeArithmetic("sub");
            //should be
            writePushPop("C_PUSH", $"Return_Address{retNb}", -1);
            //retNb++;

            //push LCL
            writePushPop("C_PUSH", "local", -1);

            //push ARG
            writePushPop("C_PUSH", "argument", -1);

            //push THIS
            writePushPop("C_PUSH", "this", -1);

            //push THAT
            writePushPop("C_PUSH", "that", -1);

            // --- Current Variables ---//
            // LCL = current SP
            writePushPop("C_PUSH", "SP", -1);
            writePushPop("C_POP", "local", -1);


            //ARG = LCL-5
            writePushPop("C_PUSH", "local", -1);
            writePushPop("C_PUSH", "constant", 5);
            writePushPop("C_PUSH", "constant", arg2);
            writeArithmetic("add");
            writeArithmetic("sub");
            writePushPop("C_POP", "argument", -1);

            // go to function name
            writeFlow("C_GOTO", arg1);

            writeFile($"(Return_Address{retNb})");
            retNb++;

        }

        public void functionReturn()
        {
            writeFile("// Start return");

            writePushPop("C_POP", "temp", 4);
            // save the result in t4

            //save the return adress in t3
            writePushPop("C_PUSH", "local", -1);
            writePushPop("C_PUSH", "constant", 4);
            writeArithmetic("sub");
            writePushPop("C_POP", "SP", -1);
            writePushPop("C_POP", "temp", 3);


            writePushPop("C_PUSH", "argument", -1);
            writePushPop("C_POP", "temp", 2);

            // move to local
            writePushPop("C_PUSH", "local", -1);
            writePushPop("C_POP", "SP", -1);

            //pop values
            writePushPop("C_POP", "that", -1);
            writePushPop("C_POP", "this", -1);
            writePushPop("C_POP", "argument", -1);
            writePushPop("C_POP", "local", -1);
            
            // reset SP
            writePushPop("C_PUSH", "temp", 2);
            writePushPop("C_POP", "SP", -1);


            //save the result
            writePushPop("C_PUSH", "temp", 4); // a mettre apres les 2 prochaines

            // go to return addr (t3)
            writeFile($"@8");
            writeFile($"A=M"); //required ?
            writeFile($"0;JMP");
        }

        public void writeFlow(string commandType, string arg1)
        {
            List<string> commands = new List<string>();
            commands.Add($"// {commandType} {arg1} ");

            if(commandType=="C_LABEL") {
                commands.Add($"({arg1})");
            }
            else if (commandType == "C_GOTO")
            {
                commands.Add($"@{arg1}");
                commands.Add($"0;JMP");
            }
            else if (commandType == "C_IFGOTO")
            {
                // get value from SP-1
                commands.Add($"@SP");
                commands.Add($"M=M-1");
                commands.Add($"A=M");
                commands.Add($"D=M");

                commands.Add($"@{arg1}");
                //commands.Add($"D;JGE"); //JNE
                commands.Add($"D;JNE"); //JNE


            }
            using (StreamWriter sw = File.AppendText(fileWrite))
            {
                foreach (string command in commands)
                {
                    sw.WriteLine(command);
                }
            }


        }

        public void updateRegisterDictionnary()
        {
            registerDictionary = new Dictionary<string, string>();

            registerDictionary.Add("SP", "SP");
            registerDictionary.Add("local", "LCL");
            registerDictionary.Add("argument", "ARG");
            registerDictionary.Add("constant", "CST");
            registerDictionary.Add("this", "THIS");
            registerDictionary.Add("that", "THAT");
            registerDictionary.Add("temp", "5");
            registerDictionary.Add("static", "static");
            registerDictionary.Add("pointer", "pointer");

        }


        public void writeFile(string text)
        {
            using (StreamWriter sw = File.AppendText(fileWrite))
            {

                    sw.WriteLine(text);
                
            }
        }

    }
}
