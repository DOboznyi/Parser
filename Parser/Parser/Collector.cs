using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Parser
{
    public class Collector
    {
        private dataContainer data;
        private dataContainer oldData;
        private string outPath;
        public Collector(dataContainer data, string outPath) {
            this.data = data;
            this.outPath = outPath;
            oldData = DeSerializeObject<dataContainer>(outPath + "data.xml");
            //mkTree();
            comparison();
        }

        private bool CheckContainerZero(dataContainer data) {
            if (data.getCount() == 0) {
                return true;
            }
            return false;
        }

        public string comparison() {
            if (CheckContainerZero(data)) return null;
            string outLine = outPath;
            foreach (Title title in data.getTitles()) {
                Title oldTitle = checkTitle(title);
                if (oldTitle != null)
                {
                    foreach (Chapter chapter in title.getChapters()) {
                        Chapter oldChapter = checkChapter(chapter, oldTitle);
                        if (oldChapter != null)
                        {
                            foreach (Article article in chapter.getArticles()) {
                                File.WriteAllText(title.getName() + "\\" + outLine + "\\" + chapter.getFullName() + "\\" + article.getFullName() + ".md", article.getText());
                            }
                        }
                        else {
                            string outChapter = mkdir(chapter.getFullName(), title.getName()+"\\"+outLine);
                            int countArticles = chapter.getArticles().Count();
                            for (int k = 0; k < countArticles; k++)
                            {
                                Article currArticle = chapter.getArticles().ElementAt(k);
                                File.WriteAllText(outChapter + "\\" + currArticle.getFullName() + ".md", currArticle.getText());
                            }
                        }
                    }
                }
                else {
                    string outTitle = mkdir(title.getName(), outLine);
                    int countChapters = title.getChapters().Count();
                    for (int j = 0; j < countChapters; j++)
                    {
                        Chapter currChapter = title.getChapters().ElementAt(j);
                        string outChapter = mkdir(currChapter.getFullName(), outTitle);
                        int countArticles = currChapter.getArticles().Count();
                        for (int k = 0; k < countArticles; k++)
                        {
                            Article currArticle = currChapter.getArticles().ElementAt(k);
                            File.WriteAllText(outChapter + "\\" + currArticle.getFullName() + ".md", currArticle.getText());
                        }
                    }
                }
            }
            SerializeObject<dataContainer>(data, outPath + "data.xml");
            return outLine;
        }

        private Chapter checkChapter(Chapter chapter, Title oldTitle)
        {
            foreach (Chapter oldChapter in oldTitle.getChapters())
            {
                if (chapter.getFullName() == oldChapter.getFullName())
                {
                    return oldChapter;
                }
            }
            return null;
        }

        private Title checkTitle(Title title)
        {
            foreach (Title oldTitle in oldData.getTitles()) {
                if (title.getName() == oldTitle.getName()) {
                    return oldTitle;
                }
            }
            return null;
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

        public void moveDir(string sourceDirectory, string destinationDirectory) {
            try
            {
                Directory.Move(sourceDirectory, destinationDirectory);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
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

        public void SerializeObject<T>(T serializableObject, string fileName)
        {
            if (serializableObject == null) { return; }

            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                XmlSerializer serializer = new XmlSerializer(serializableObject.GetType());
                using (MemoryStream stream = new MemoryStream())
                {
                    serializer.Serialize(stream, serializableObject);
                    stream.Position = 0;
                    xmlDocument.Load(stream);
                    xmlDocument.Save(fileName);
                }
            }
            catch (Exception) { }
        }

        public T DeSerializeObject<T>(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) { return default(T); }
            T objectOut = default(T);
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(fileName);
                string xmlstring = xmlDocument.OuterXml;

                using (StringReader read = new StringReader(xmlstring))
                {
                    Type outType = typeof(T);

                    XmlSerializer serializer = new XmlSerializer(outType);
                    using (XmlReader reader = new XmlTextReader(read))
                    {
                        objectOut = (T)serializer.Deserialize(reader);
                    }
                }
            }
            catch (Exception) { }
            return objectOut;
        }
    }
}
