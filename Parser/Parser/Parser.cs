using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Parser
{
    class Parser
    {
        private string path;
        private string lastLine;
        private List<Title> titles;
        public Parser(string path) {
            this.path = path;
            Parse();
        }
        private void Parse() {
            titles = new List<Title>();
            try
            {
                string text = "";
                using (StreamReader sr = new StreamReader(path, System.Text.Encoding.GetEncoding(1251)))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line.Contains("<span class=rvts23>")) {
                            do
                            {
                                string nameTitle = makeTitle(line);
                                titles.Add(parseTitle(sr, nameTitle));
                                line = lastLine;
                            } while (lastLine.Contains("<span class=rvts23>"));
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
                    do
                    {
                        string numberChapter = makeNumberChapter(line);
                        line = sr.ReadLine();
                        string nameChapter = makeNameChapter(line);
                        title.addChapter(parseChapter(sr, nameChapter, numberChapter));
                        line = lastLine;
                    } while (lastLine.Contains("<span class=rvts15>"));
                    if (lastLine.Contains("<span class=rvts23>") ||lastLine.Contains("<div class=rvps8>"))
                    {
                        break;
                    }
                }
                lastLine = line;
            }
            return title;
        }

        private string makeNameChapter(string line)
        {
            line = removeHTML(line);
            if (line == "") line = null;
            return line;
        }

        private string makeNumberChapter(string line)
        {
            line = removeStr(line, "<span class=rvts15>Розділ ");
            line = removeStr(line, " </span>");
            line = removeHTML(line);
            string num = line;
            return num;
        }

        private Chapter parseChapter(StreamReader sr, string name, string number)
        {
            Chapter chapter = new Chapter(name, number);
            string line;
            if (name != null)
            {
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.Contains("<span class=rvts9>"))
                    {
                        do
                        {
                            string numberArticle = makeNumberArticle(line);
                            string nameArticle = makeNameArticle(line);
                            chapter.addArticle(parseArticle(sr, nameArticle, numberArticle));
                            line = lastLine;
                        } while (lastLine.Contains("<span class=rvts9>"));
                        if (lastLine.Contains("<span class=rvts15>Розділ") || lastLine.Contains("<span class=rvts23>"))
                        {
                            break;
                        }
                    }
                }
            }
            else {
                chapter.addArticle(parseArticle(sr, null, null));
            }
            return chapter;
        }

        private Article parseArticle(StreamReader sr, string nameArticle, string numberArticle)
        {
            Article article = new Article(nameArticle,numberArticle);
            string line;
            if (nameArticle == null && numberArticle == null) { line = parseText(sr); article.addText(line); }
            while ((line = sr.ReadLine()) != null)
            {
                if (line.Contains("<p class=rvps2>"))
                {
                    line = sr.ReadLine();
                    if (line.Contains("<span class=rvts9>Стаття") || line.Contains("<span class=rvts15>Розділ")|| line.Contains("<span class=rvts23>"))
                    {
                        lastLine = line;
                        break;
                    }
                    line = removeHTML(line);
                    article.addText(line);
                }
                if (line.Contains("<span class=rvts9>Стаття") || line.Contains("<span class=rvts15>Розділ")|| line.Contains("<span class=rvts23>")||line.Contains("<div class=rvps8>"))
                {
                    lastLine = line;
                    break;
                }
                if (line.Contains("<span class=rvts9>")) {
                    line = removeHTML(line);
                    article.addText(line);
                }
            }
            return article;
        }

        private string parseText(StreamReader sr) {
            string text;
            text = sr.ReadLine();
            text = removeHTML(text);
            return text;
        }

        public string removeHTML(string line) {
            return Regex.Replace(line, "<.*?>", String.Empty);
        }

        private string makeNameArticle(string line)
        {
            string str = line;
            str = removeHTML(str);
            int index = str.IndexOf(".");
                string cleanPath = (index < 0)
                    ? str
                    : str.Remove(0, index+2);
                str = cleanPath;
            return str;
        }

        private string makeNumberArticle(string line)
        {
            string str = line;
            str = removeStr(str, "Стаття ");
            str = removeHTML(str);
            int index = str.IndexOf(".");
            string cleanPath = (index < 0)
                ? str
                : str.Remove(index, str.Length-index);            
            return cleanPath;
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

        public dataContainer getData() {
            dataContainer data = new dataContainer(titles);
            return data;
        }
    }
}
