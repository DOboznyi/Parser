using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser
{
    class Collector
    {
        private dataContainer data;
        private string outPath;
        public Collector(dataContainer data, string outPath) {
            this.data = data;
            this.outPath = outPath;
            mkTree();
        }

        public string mkTree(){
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
    }
}
