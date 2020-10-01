using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace JackAnalyzer
{
    class Tokenizer
    {

        List<string> xmlWords = new List<string>();
        List<string> symbols = new List<string>();
        List<string> keyword = new List<string>();
        List<string> fetchedItems = new List<string>();

        public Tokenizer()
        {
            buildDictionnaries();
        }
        public void buildWordList(string inputFile)
        {
            //string fullText = File.ReadAllText(inputFile);
            string fullText = deleteComments(inputFile);
            int fullTextLength = fullText.Length;
            int start = 0;
            int end = 1;
            char currentChar = inputFile[end];

            //string test = fullText.Substring(1190, fullText.Length-1190-1);

            while( end < fullTextLength)
            {
                if (end > 1190)
                {
                    int a;
                }
                if (end < (fullTextLength -1 )) {

                    if (isSpaceOrLine(fullText[end]) || isSymbol(fullText[end]) || isSymbol(fullText[start]) )
                    {
                        string addedString = fullText.Substring(start, (end - start));
                        addedString = addedString.TrimStart();
                        if (addedString.Length > 0)
                        {
                            if (addedString[0].ToString() == "\"")
                            {
                                end++;
                                addedString = fullText.Substring(start, (end - start));
                                while (addedString[addedString.Length - 1].ToString() != "\"")
                                {
                                    addedString = fullText.Substring(start, (end - start));
                                    end++;

                                }
                                end--;
                            }

                        }

                        fetchedItems.Add(addedString.Trim());
                        start = end;
                    }
                }
                else
                {

                    if (isSymbol(fullText[end]))
                    {

                        fetchedItems.Add(fullText.Substring(end, 1).Trim());
                        start = end;
                    }

                    
                    string teste = fullText.Substring((end-1), fullText.Length - (end - 1) - 1);
                    fetchedItems.Add(teste);

                }

                end++;
                
            }
            fetchedItems = fetchedItems.Where(s => !string.IsNullOrWhiteSpace(s)).ToList();

            //xmlWords.Add("<tokens>");
            foreach (string item  in fetchedItems)
            {
                string type = getType(item);
                string usedItem = isReplaceToHtml(item);
                if (type == "stringConstant")
                {
                    usedItem = usedItem.Replace("\"","");
                }
                xmlWords.Add($"<{type}> {usedItem} </{type}>");
            }
            //xmlWords.Add("</tokens>");

        }

        public string getElementValueAt(int position) {

            return fetchedItems[position];
        }
        public string getElementTypeAt(int position)
        {

            return getType(fetchedItems[position]);
        }
        public string getXMLValueAt(int position)
        {
            return xmlWords[position];
        }

        private static bool isSpaceOrLine(char inChar)
        {
            if (inChar == '\r'){
                return true;
            }
            if (inChar == ' ')
            {
                return true;
            }
            if (inChar == '\n')
            {
                return true;
            }

            return false;
        }
        private bool isSymbol(char inChar)
        {
            if (symbols.Contains(inChar.ToString()))
            {
                return true;
            }
            return false;
        }
        private string isReplaceToHtml(string inStr)
        {
            if (inStr == ">")
            {
                inStr = "&gt;";
            }
            if (inStr == "<")
            {
                inStr = "&lt;";
            }
            if (inStr == "&")
            {
                inStr = "&amp;";
            }

            return inStr;
        }
        private void printList()
        {

            foreach (string listItem in xmlWords)
            {
                Console.WriteLine(listItem);
            }
        }
        public int getMaxLength()
        {
            return fetchedItems.Count();
        }

        public string getType(string word)
        {
            if (symbols.Contains(word))
            {
                return "symbol";
            }
            else if (keyword.Contains(word))
            {
                return "keyword";
            }
            else if (word.StartsWith("\""))
            {
                return "stringConstant";
            }
            else if (int.TryParse(word, out _))
            {
                return "integerConstant";
            }
            return "identifier";

        }

        private string deleteComments(string inputFile)
        {
            string concatenatedFile = "";
            using(StreamReader sr = new StreamReader(inputFile))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    if (line.Contains("//"))
                    {
                        int indexOfComment = line.IndexOf("//");
                        line = line.Substring(0, indexOfComment);
                    }
                    if (line.Contains("/**"))
                    {
                        int indexOfComment = line.IndexOf("/**");
                        line = line.Substring(0, indexOfComment);
                    }
                    if(line.StartsWith(" *") || line.StartsWith(" *")|| line.TrimStart().StartsWith("*"))
                    {
                        line = line.Substring(0, 0);
                    }
                    concatenatedFile = concatenatedFile + '\r' + line;
                }
            }
            return concatenatedFile;
        }


        private void buildDictionnaries()
        {

            symbols.Add("{");
            symbols.Add("}");
            symbols.Add("[");
            symbols.Add("]");
            symbols.Add("(");
            symbols.Add(")");
            symbols.Add(".");
            symbols.Add(",");
            symbols.Add(";");
            symbols.Add("+");
            symbols.Add("-");
            symbols.Add("*");
            symbols.Add("/");
            symbols.Add("&");
            symbols.Add("|");
            symbols.Add("<");
            symbols.Add(">");
            symbols.Add("=");
            symbols.Add("~");

            keyword.Add("class");
            keyword.Add("constructor");
            keyword.Add("function");
            keyword.Add("method");
            keyword.Add("field");
            keyword.Add("static");
            keyword.Add("var");
            keyword.Add("int");
            keyword.Add("char");
            keyword.Add("boolean");
            keyword.Add("void");
            keyword.Add("true");
            keyword.Add("false");
            keyword.Add("return");
            keyword.Add("let");
            keyword.Add("do");
            keyword.Add("if");
            keyword.Add("else");
            keyword.Add("while");
            keyword.Add("this");

        }
    }
}
