using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser
{
    class Article
    {
        private int number;
        private string name;
        private string text;

        public Article(string name, int number, string text) {
            this.name = name;
            this.number = number;
            this.text = text;
        }
    }
}
