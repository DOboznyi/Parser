using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser
{ 

    class Chapter
    {
        private int number;
        private string name;
        private List<Article> articles;

        public Chapter(string name,int number) {
            this.name = name;
            this.number = number;
            articles = new List<Article>();
        }

        public void addArticle(Article article) {
            articles.Add(article);
        }

    }
}
