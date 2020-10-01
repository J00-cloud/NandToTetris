using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace JackAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            string inFile = args[0];

            //string inFile = @"C:\Users\jonat\Dropbox\CS\3-Core Systems\From Nand to Tetris\nand2tetris\projects\11\Square";


            string folder;

            if (inFile.EndsWith(".jack"))
            {
                string outFile = inFile.Replace(".jack", ".xml");

                Parser newParser = new Parser(inFile);
                newParser.compileClass();
                newParser.printToFile(outFile);
            }
            else
            {
                folder = inFile;
                string[] files = Directory.GetFiles(folder, "*.jack");
                foreach (string file in files)
                {
                    string outFile = file.Replace(".jack", ".xml");

                    Parser newParser = new Parser(file);
                    newParser.compileClass();
                    newParser.printToFile(outFile);
                }
            }





            Console.WriteLine("done");
            Console.ReadLine();
        }
    }
}
