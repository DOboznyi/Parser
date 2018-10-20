using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser
{ 

    class Chapter
    {
        private string number;
        private string name;
        private List<Article> articles;

        public Chapter(string name,string number) {
            this.name = name;
            this.number = number;
            articles = new List<Article>();
        }

        public void addArticle(Article article) {
            articles.Add(article);
        }

        public string getFullName() {
            string line = "Розділ "+number;
            if (name!=null)
            {
                line += ". " + name;
            }
            return line;
        }

        public List<Article> getArticles() {
            return articles;
        }
    }
}
