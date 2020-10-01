using System;
using System.Collections.Generic;
using System.IO;

namespace Assembler
{
    class Program
    {

        public static Dictionary<string, string> destDict = new Dictionary<string, string>();
        public static Dictionary<string, string> jmpDict = new Dictionary<string, string>();
        public static Dictionary<string, string> instrDict = new Dictionary<string, string>();
        public static Dictionary<string, string> destinationDict = new Dictionary<string, string>();
        // public static Dictionary<string, string> labelDict = new Dictionary<string, string>();

        public static int freeMemCounter = 16;


        static void Main(string[] args)
        {
            createDictionnaryDest();
            createDictionnaryJmp();
            createDictionnaryInstr();
            dictLabelMemory();

            Console.WriteLine("Let's compile !");

            string file1 = @"C:\Users\jonat\Dropbox\CS\3-Core Systems\From Nand to Tetris\nand2tetris\projects\06\pong\Pong.asm";

            string file2 = firstPass(file1);

            addVars(file2);

            secondPass(file2);

            Console.WriteLine("done");

        }

        private static string firstPass(string file1)
        {
            int lineNb = 0;
            StreamReader sr = new StreamReader(file1);
            string file2 = file1.Replace(".asm", "-out1.asm");
            string output = "";
            File.Delete(file2);

            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                //Console.WriteLine($"{line}");

                if (!ignoreLine(line,lineNb))
                {
                    output = cleanLine(line);
                    //replaceNewVarInitiation(output);
                    //Console.WriteLine($"{output}");
                    File.AppendAllText(file2, output + Environment.NewLine);
                    lineNb++;
                }
            }
            //Console.ReadLine();
            sr.Close();

            return file2;

        }

        private static void addVars(string file2)
        {
            StreamReader sr2 = new StreamReader(file2);
            int lineNb = 0;
            while (!sr2.EndOfStream)
            {
                string line = sr2.ReadLine();
                //Console.WriteLine($"{line}");

                if (!ignoreLine(line, lineNb))
                {
                    replaceNewVarInitiation(line);
                    //Console.WriteLine($"{output}");
                    lineNb++;
                }
            }
            //Console.ReadLine();
            sr2.Close();
        }


        private static void replaceNewVarInitiation(string inStr)
        {
            if (inStr.Substring(0, 1) == "@")
            {
                string varName = inStr.Replace("@", "");
                if (isDigitString(varName))
                {

                }
                else
                {
                    if (destinationDict.ContainsKey(varName))
                    {
                    }
                    else
                    {
                        allocateNewSpaceForVar(varName);
                    }
                }

            }
            else
            {
            }
        }

        private static void secondPass(string file2)
        {
            string file3 = file2.Replace("-out1.asm", "-out2.hack");
            File.Delete(file3);
            int lineNb = 0;
            using (StreamReader s2 = new StreamReader(file2))
            {
                while (!s2.EndOfStream)
                {
                    string line = s2.ReadLine();
                    //Console.WriteLine($"{line}");

                    if (!ignoreLine(line,lineNb))
                    {
                        line = cleanLine(line);
                        string output = replaceInstruction(line);
                        //Console.WriteLine($"{output}");
                        File.AppendAllText(file3, output + Environment.NewLine);
                        
                        lineNb++;

                    }
                }
                //Console.ReadLine();
            }
        }

        private static bool ignoreLine(string line, int lineNb)
        {
            bool ignore = false;
            if (line.Trim().Length < 1)
            {
                ignore = true;
            }
            else if(line.Substring(0, 1) == "(")
            {
                ignore = true;
                destinationDict.Add(line.Replace("(","").Replace(")", ""),numberToBinary(lineNb
                    ));
            }
            else if (line.Substring(0, 2) == "//")
            {
                ignore = true;
            }
            return ignore;
        }

        private static string cleanLine(string line)
        {
            string newLine = line.Split(new string[] { "//" }, StringSplitOptions.None)[0];
            newLine = newLine.Trim();
            return newLine;

        }



        private static string replaceInstruction(string input)
        {
            string output="";
            if (input.Substring(0,1) == "@")
            {
                output = replaceAInstruction(input);
            }
            else
            {
                output = replaceCInstruction(input);
            }


            return output;

        }

        public static string numberToBinary(int nb)
        {
            string outf = Convert.ToString(Convert.ToInt32(nb), 2);
            outf= outf.PadLeft(15, '0');

            return outf;
        }

        private static string replaceAInstruction(string input)
        {
            string destination = input.Substring(1, input.Length-1);
            string destinationBin;

            bool isDigit = isDigitString(destination);

            if (isDigit)
            {
                destinationBin = Convert.ToString(Convert.ToInt32(destination), 2);
                destinationBin =destinationBin.PadLeft(15, '0');
            }
            else
            {
                destinationBin = destinationDict[destination];
            }


            string output = $"0{destinationBin}";

            return output;
        }

        private static bool isDigitString(string inStr)
        {
            bool isDigit = true;

            foreach (Char letter in inStr)
            {
                if (!Char.IsDigit(letter)){
                    isDigit = false;
                }
            }
            return isDigit;
        }

        private static string replaceCInstruction(string input)
        {
            string txt_dest;
            if (input.Contains("="))
            {
                txt_dest = input.Split("=")[0];
                txt_dest = txt_dest.Trim();
            }
            else
            {
                txt_dest = "null";
            }

            string txt_instr = input;
            if (txt_instr.Contains("="))
            {
                txt_instr = (input.Split("=")[1]);
            }

            if (txt_instr.Contains(";"))
            {
                txt_instr = txt_instr.Split(";")[0];
            }
            txt_instr = txt_instr.Trim();

            string txt_jmp;
            if (input.Contains(";"))
            {
                txt_jmp = input.Split(";")[1];
                txt_jmp = txt_jmp.Trim();
            }
            else
            {
                txt_jmp = "null";
            }

            string jmp = jmpDict[txt_jmp];
            string dest = destDict[txt_dest];
            bool aORmBool = txt_instr.Contains("M");
            string aORm = (aORmBool ? 1 : 0).ToString();
            string instrct = instrDict[txt_instr];


            return $"111{aORm}{instrct}{dest}{jmp}";
        }

        private static void allocateNewSpaceForVar(string VarName)
        {
            string address = numberToBinary(freeMemCounter);
            destinationDict.Add(VarName, address);
            freeMemCounter = freeMemCounter + 1;
        }


        private static void createDictionnaryDest()
        {
            destDict.Add("null", "000");
            destDict.Add("M","001");
            destDict.Add("D", "010");
            destDict.Add("MD", "011");
            destDict.Add("A", "100");
            destDict.Add("AM", "101");
            destDict.Add("AD", "110");
            destDict.Add("AMD", "111");
        }

        private static void dictLabelMemory()
        {
            destinationDict.Add("R0", "000000000000000");
            destinationDict.Add("R1", "000000000000001");
            destinationDict.Add("R2", "000000000000010");
            destinationDict.Add("R3", "000000000000011");
            destinationDict.Add("R4", "000000000000100");
            destinationDict.Add("R5", "000000000000101");
            destinationDict.Add("R6", "000000000000110");
            destinationDict.Add("R7", "000000000000111");
            destinationDict.Add("R8", "000000000001000");
            destinationDict.Add("R9",  "000000000001001");
            destinationDict.Add("R10", "000000000001010");
            destinationDict.Add("R11", "000000000001011");
            destinationDict.Add("R12", "000000000001100");
            destinationDict.Add("R13", "000000000001101");
            destinationDict.Add("R14", "000000000001110");
            destinationDict.Add("R15", "000000000001111");
            destinationDict.Add("SP",  "000000000000000");
            destinationDict.Add("LCL", "000000000000001");
            destinationDict.Add("ARG", "000000000000010");
            destinationDict.Add("THIS","000000000000011");
            destinationDict.Add("THAT",  "000000000000100");
            destinationDict.Add("SCREEN","100000000000000");
            destinationDict.Add("KBD", "110000000000000");

        }


        private static void createDictionnaryJmp()
        {
            jmpDict.Add("null", "000");
            jmpDict.Add("JGT","001");
            jmpDict.Add("JEQ", "010");
            jmpDict.Add("JGE", "011");
            jmpDict.Add("JLT", "100");
            jmpDict.Add("JNE", "101");
            jmpDict.Add("JLE", "110");
            jmpDict.Add("JMP", "111");
        }

        private static void createDictionnaryInstr()
        {
            instrDict.Add("0", "101010");
            instrDict.Add("1", "111111");
            instrDict.Add("-1", "111010");
            instrDict.Add("D", "001100");
            instrDict.Add("A", "110000");
            instrDict.Add("!D", "001101");
            instrDict.Add("!A", "110001");
            instrDict.Add("-D", "001111");
            instrDict.Add("-A", "110011");
            instrDict.Add("D+1", "011111");
            instrDict.Add("A+1", "110111");
            instrDict.Add("D-1", "001110");
            instrDict.Add("A-1", "110010");
            instrDict.Add("D+A", "000010");
            instrDict.Add("D-A", "010011");
            instrDict.Add("A-D", "000111");
            instrDict.Add("D&A", "000000");
            instrDict.Add("D|A", "010101");
            instrDict.Add("M", "110000");
            instrDict.Add("!M", "110001");
            instrDict.Add("-M", "110011");
            instrDict.Add("M+1", "110111");
            instrDict.Add("M-1", "110010");
            instrDict.Add("D+M", "000010");
            instrDict.Add("D-M", "010011");
            instrDict.Add("M-D", "000111");
            instrDict.Add("D&M", "000000");
            instrDict.Add("D|M", "010101");


        }

    }
}
