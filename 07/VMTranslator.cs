using System;
using System.Collections.Generic;
using System.IO;

namespace VMTranslator
{
    class VMTranslator
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("VM translator !");

            string file1 = args[0];
            string fileName = Path.GetFileNameWithoutExtension(file1);
            string fileout = file1.Replace(".vm",".asm");

            if (File.Exists(fileout))
            {
                File.Delete(fileout);
            }

            Parser newFile = new Parser(file1);
            CodeWriter cw = new CodeWriter(fileout);
            cw.filename = fileName;

            while (newFile.hasMoreCommands)
            {
                newFile.advance();
                if(newFile.commandType != "null")
                {
                    
                    //Console.WriteLine(newFile.currentLine);

                    if(newFile.commandType == "C_PUSH" || newFile.commandType == "C_POP")
                    {
                        //Console.WriteLine("pushOrPop");
                        cw.writePushPop(newFile.commandType, newFile.arg1, Convert.ToInt32(newFile.arg2));
                    }
                    else if (newFile.commandType== "Arithm")
                    {
                        //Console.WriteLine("arithm");
                        cw.writeArithmetic(newFile.currentLine);
                    }

                    //Console.ReadLine();

                }


            }
            newFile.close();


        }


    }
}
