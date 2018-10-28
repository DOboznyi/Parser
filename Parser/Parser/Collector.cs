using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser
{
    public class Collector
    {
        private dataContainer data;
        private string outPath;
        public Collector(dataContainer data, string outPath) {
            this.data = data;
            this.outPath = outPath;
            mkTree();
        }

        private bool CheckContainerZero(dataContainer data) {
            if (data.getCount() == 0) {
                return true;
            }
            return false;
        }

        public string mkTree(){
            if (CheckContainerZero(data)) return null;
            string outLine = mkdir(DateTime.Now.ToString("h_mm_ss"), outPath);
            for (int i = 0; i < data.getTitles().Count(); i++)
            {
                Title currTitle = data.getTitles().ElementAt(i);
                string outTitle = mkdir(currTitle.getName(), outLine);
                int countChapters = currTitle.getChapters().Count();
                for (int j = 0; j < countChapters; j++)
                {
                    Chapter currChapter = currTitle.getChapters().ElementAt(j);
                    string outChapter = mkdir(currChapter.getFullName(), outTitle);
                    int countArticles = currChapter.getArticles().Count();
                    for (int k = 0; k < countArticles; k++)
                    {
                        Article currArticle = currChapter.getArticles().ElementAt(k); 
                        File.WriteAllText(outChapter + "\\" + currArticle.getFullName()+".md", currArticle.getText());
                    }
                }
            }
            return outLine;
        }

        private string mkdir(string name, string path)
        {
            string path1 = path + "\\" + name;
            try
            {
                if (Directory.Exists(path1))
                {
                    Console.WriteLine("That path exists already.");
                    return null;
                }
                
                DirectoryInfo di = Directory.CreateDirectory(path1);
               
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
            return path1;
        }
    }
}
