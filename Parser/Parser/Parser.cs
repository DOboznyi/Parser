using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser
{
    class Parser
    {
        private string path;
        private string outPath;
        private string[] keys = new string[] { "<span class=rvts23>", "<span class=rvts15>" , "<span class=rvts9>" };
        private string currTitle;
        private string currChapter;
        private string currArticle;
        public Parser(string path, string outPath) {
            this.path = path;
            this.outPath = outPath;
            this.outPath = mkdir(DateTime.Now.ToString("h_mm_ss"),outPath);
            Parse();
        }
        private void Parse() {
            try
            {
                string text = "";
                using (StreamReader sr = new StreamReader(path, System.Text.Encoding.GetEncoding(1251)))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line.Contains("<span class=rvts23>")) {
                            string nameTitle = makeTitle(line);
                            parseTitle(sr,nameTitle);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private Title parseTitle(StreamReader sr, string name)
        {
            Title title = new Title(name);
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                if (line.Contains("<span class=rvts15>"))
                {
                    int numberChapter = makeNumberChapter(line);
                    line = sr.ReadLine();
                    string nameChapter = makeNameChapter(line);
                    parseChapter(sr, nameChapter, numberChapter);
                }
            }
            return title;
        }

        private string makeNameChapter(string line)
        {
            line = removeStr(line, "<br><span class=rvts15>");
            line = removeStr(line, "</span></p>");
            return line;
        }

        private int makeNumberChapter(string line)
        {
            line = removeStr(line, "<span class=rvts15>Розділ ");
            line = removeStr(line, " </span>");
            int num = RomanToInteger(line);
            return num;
        }

        private Chapter parseChapter(StreamReader sr, string name, int number)
        {
            throw new NotImplementedException();
        }

        private string mkdir(string name, string path) {
            string path1 = outPath+"\\"+name;
            try
            {
                // Determine whether the directory exists.
                if (Directory.Exists(path1))
                {
                    Console.WriteLine("That path exists already.");
                    return null;
                }

                // Try to create the directory.
                DirectoryInfo di = Directory.CreateDirectory(path1);
                //Console.WriteLine("The directory was created successfully at {0}.", Directory.GetCreationTime(path));

                // Delete the directory.
                //di.Delete();
                //Console.WriteLine("The directory was deleted successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
            finally { }
            return path1;
        }

        public string makeTitle(string line) {
            line = removeStr(line, "<span class=rvts23>");
            line = removeStr(line, "</span></p>");
            return line;
        }

        public string removeStr(string sourceString, string removeString) {
            int index = sourceString.IndexOf(removeString);
            string cleanPath = (index < 0)
                ? sourceString
                : sourceString.Remove(index, removeString.Length);
            return cleanPath;
        }

        private static Dictionary<char, int> RomanMap = new Dictionary<char, int>()
        {
            { 'I', 1},
            { 'V', 5},
            { 'X', 10},
            { 'L', 50},
            { 'C', 100},
            { 'D', 500},
            { 'M', 1000}
        };
        public static int RomanToInteger(string roman)
        {
            int number = 0;
            for (int i = 0; i < roman.Length; i++)
            {
                if (i + 1 < roman.Length && RomanMap[roman[i]] < RomanMap[roman[i + 1]])
                {
                    number -= RomanMap[roman[i]];
                }
                else
                {
                    number += RomanMap[roman[i]];
                }
            }
            return number;
        }
    }
}
