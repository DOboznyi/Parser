using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser
{
    public class Title
    {
        private string name;
        private List<Chapter> chapters;
        public Title(string name) {
            this.name = name;
            chapters = new List<Chapter>();
        }

        public void addChapter(Chapter chapter) {
            chapters.Add(chapter);
        }

        public string getName() {
            return name;
        }

        public List<Chapter> getChapters() {
            return chapters;
        }
    }
}
