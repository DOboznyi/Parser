using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser
{
    public class Article
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

        public string getFullName() {
            string line = null;
            if (number != null) {
                line = "Стаття " + number;
            }
            else line = "text";
            return line;
        }

        public string getText() {
            if (number != null) {
                if (name != null) return "Cтаття " + number + "\n====\n" + name + "\n----\n" + text;
                return "Cтаття " + number+ "\n====\n" + text;
            }
            return text;
        }
    }
}
