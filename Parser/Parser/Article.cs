using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser
{
    class Article
    {
        private string number;
        private string name;
        private string text;

        public Article(string name, string number) {
            this.name = name;
            this.number = number;
            text = "";
        }

        public void addText(string text) {
            this.text += text+ "\n";
        }
    }
}
