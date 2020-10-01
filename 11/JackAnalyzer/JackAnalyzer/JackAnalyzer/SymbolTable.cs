using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JackAnalyzer
{
    class SymbolTable
    {
        // tracks variable names and places in memory ( variable a = static 1 )
        //DataTable currentSymbolTable;
        List<string[]> listSymbolTable;
        public SymbolTable() 
        {
            //currentSymbolTable = new DataTable();
            listSymbolTable = new List<string[]>();
        }

        public void startSubroutine()
        {
            //currentSymbolTable.Clear();
            listSymbolTable = new List<string[]>();
            //currentSymbolTable.Columns.Add("name");
            //currentSymbolTable.Columns.Add("type");
            //currentSymbolTable.Columns.Add("kind");
            //currentSymbolTable.Columns.Add("index");
        }


        public void Define(string name, string type, string kind)
        {
            //DataRow dr = currentSymbolTable.NewRow();

            string nameIn = name.Replace("<identifier>", "").Replace("</identifier>", "").Replace("<keyword>", "").Replace("</keyword>", "").Trim();
            string typeIn = type.Replace("<identifier>", "").Replace("</identifier>", "").Replace("<keyword>", "").Replace("</keyword>", "").Trim();
            string kindIn = kind.Replace("<identifier>", "").Replace("</identifier>", "").Replace("<keyword>", "").Replace("</keyword>", "").Replace("field", "this").Trim();
            string countIn = VarCount(kindIn).ToString();

            //dr["name"] = nameIn;            
            //dr["type"] = typeIn;
            //dr["kind"] = kindIn;
            //dr["index"] = countIn; 

            //currentSymbolTable.Rows.Add(dr);

            string[] varsIn = {nameIn,typeIn,kindIn,countIn};

            listSymbolTable.Add(varsIn);


        }

        public int VarCount(string type)
        {
            //int count = currentSymbolTable.Select($"[kind] ='{type}'").Count();

            int countb = 0;

            foreach (string[] entries in listSymbolTable)
            {
                if(entries[2] == type)
                {
                    countb++;
                }
            }

            return countb; 
        }

        public string KindOf(string KindName)
        {
            //DataRow dr = currentSymbolTable.Select($"[name] ='{KindName}'").FirstOrDefault();

            string kind = "error";

            foreach (string[] entries in listSymbolTable)
            {
                if (entries[0] == KindName)
                {
                    return entries[2];
                }
            }

            return kind;
            //return dr["kind"].ToString();
        }

        public string TypeOf(string name)
        {
            //DataRow dr = currentSymbolTable.Select($"[name] ='{name}'").FirstOrDefault();

            //return dr["type"].ToString();

            string type = "error";

            foreach (string[] entries in listSymbolTable)
            {
                if (entries[0] == name)
                {
                    return entries[1];
                }
            }

            return type;
        }

        public string IndexOf(string IndexName)
        {
            //DataRow dr = currentSymbolTable.Select($"[name] = '{IndexName}'").FirstOrDefault();

            //return dr["index"].ToString();

            string index = "error";

            foreach (string[] entries in listSymbolTable)
            {
                if (entries[0] == IndexName)
                {
                    return entries[3];
                }
            }

            return index;


        }

        public bool entryExists(string name)
        {
            //int countEntries = currentSymbolTable.Select($"[name] = '{name}'").Count();
            //if (countEntries > 0)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}


            foreach (string[] entries in listSymbolTable)
            {
                if (entries[0] == name)
                {
                    return true;
                }
            }

            return false;

        }

        public bool typeExists(string name)
        {
            //int countEntries = currentSymbolTable.Select($"[type] = '{name}'").Count();
            //if (countEntries > 0)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}


            foreach (string[] entries in listSymbolTable)
            {
                if (entries[1] == name)
                {
                    return true;
                }
            }

            return false;

        }

        public void clear()
        {
            //currentSymbolTable.Clear();
            listSymbolTable = new List<string[]>();

        }



    }
}
