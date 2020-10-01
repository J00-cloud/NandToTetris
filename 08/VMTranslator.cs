using System;
using System.Collections.Generic;
using System.IO;

namespace VMTranslator
{
    class VMTranslator
    {
        static void Main(string[] args)
        {

            //string file1 = @"C:\Users\jonat\Dropbox\CS\3-Core Systems\From Nand to Tetris\nand2tetris\projects\08\FunctionCalls\NestedCall\Sys.vm";
            bool folder = true;
            //string directory = @"C:\Users\jonat\Dropbox\CS\3-Core Systems\From Nand to Tetris\nand2tetris\projects\08\FunctionCalls\StaticsTest\";
            //string directory = @"C:\Users\jonat\Dropbox\CS\3-Core Systems\From Nand to Tetris\nand2tetris\projects\08\FunctionCalls\FibonacciElement";
            //string directory = @"C:\Users\jonat\Dropbox\CS\3-Core Systems\From Nand to Tetris\nand2tetris\projects\08\FunctionCalls\NestedCall\Sys.vm";
            string directory = @args[0];

            if (directory.EndsWith(".asm")|| directory.EndsWith(".vm"))
            {
                folder = false;
            }

            List<string> filesIn = convertFiles(directory);

            //string folderName = (directory).Split("\\")[(directory).Split("\\").Length - 2];
            string folderName = "";
            string fileout = "";

            if (!folder)
            {
                folderName = Path.GetFileName(Path.GetDirectoryName(directory));
                string currName = Path.GetFileName(directory);
                fileout = directory.Replace($"{currName}", $"{folderName}.asm");
            }
            else
            {
                folderName = Path.GetFileName(Path.GetDirectoryName(directory + Path.DirectorySeparatorChar));
                fileout = $"{directory}{Path.DirectorySeparatorChar}{folderName}.asm";
            }
            //string 

            if (File.Exists(fileout))
            {
                File.Delete(fileout);
            }

            
            CodeWriter cw = new CodeWriter(fileout);


            foreach (string fileIn in filesIn)
            {

                string fileName = Path.GetFileNameWithoutExtension(fileIn);
                cw.filename = fileName;

                // get all files in folder
                // order by sys.vm first

                Parser newFile = new Parser(fileIn);

                if (folder)
                {
                    //cw.writePushPop("C_PUSH", "constant", 256);
                    //cw.writePushPop("C_POP", "SP", -1);
                    
                    cw.writeFile("@256");
                    cw.writeFile("D=A");
                    cw.writeFile("@SP");
                    cw.writeFile("M=D");
                    cw.writeFile("A=D");
                    cw.functionCall("Sys.init", 0);
                    folder = false;
                }

                while (newFile.hasMoreCommands)
                {
                    newFile.advance();
                    if (newFile.commandType != "null")
                    {

                        if (newFile.commandType == "C_PUSH" || newFile.commandType == "C_POP")
                        {
                            //Console.WriteLine("pushOrPop");
                            cw.writePushPop(newFile.commandType, newFile.arg1, Convert.ToInt32(newFile.arg2));
                        }
                        else if (newFile.commandType == "Arithm")
                        {
                            //Console.WriteLine("arithm");
                            //cw.writeArithmetic(newFile.currentLine);
                            cw.writeArithmetic(newFile.inAr);
                        }
                        else if (newFile.commandType == "C_LABEL" || newFile.commandType == "C_IFGOTO" || newFile.commandType == "C_GOTO")
                        {
                            //Console.WriteLine("label");
                            cw.writeFlow(newFile.commandType, newFile.arg1);
                        }
                        else if (newFile.commandType == "C_FUNC")
                        {
                            cw.functionStart(newFile.arg1, Convert.ToInt32(newFile.arg2));

                        }
                        else if (newFile.commandType == "C_CALL")
                        {
                            cw.functionCall(newFile.arg1, Convert.ToInt32(newFile.arg2));
                        }
                        else if (newFile.commandType == "C_RETURN")
                        {
                            cw.functionReturn();
                        }

                    }


                }
                newFile.close();

            }


        }




        public static List<string> convertFiles(string inFolder)
        {
            // get all the files in a folder with .vm
            List<string> strList = new List<string>();
            if (inFolder.EndsWith(".vm"))
            {
                strList.Add(inFolder);
                return strList;
            }


            string[] files = Directory.GetFiles(inFolder, "*.vm");
            List<string> listOfFiles = new List<string>(files);
            
            

            // sort the files
            if (files.Length == 1)
            {
                //File.Delete($"{inFolder}{folderName}.vm");
                //File.Copy(files[0], $"{inFolder}{folderName}.vm");
                return listOfFiles;
            }
            else
            {
                //File.Delete($"{inFolder}{folderName}.vm");
                // do sys first
                string toRemove = "";
                bool rem = false;
                foreach (var file in listOfFiles)
                {
                    if(Path.GetFileName(file) == "Sys.vm")
                    {
                        //File.AppendAllLines($"{inFolder}{folderName}.vm", File.ReadAllLines(file));
                        strList.Add(file);
                        toRemove = file;
                        rem = true;
                    }
                    
                }
                if (rem)
                {
                    listOfFiles.Remove(toRemove);

                }

                foreach (var file in listOfFiles)
                {
                    strList.Add(file);
                    //File.AppendAllLines($"{inFolder}{folderName}.vm", File.ReadAllLines(file));
                }
            }

            return strList;
            

        }


    }
}
