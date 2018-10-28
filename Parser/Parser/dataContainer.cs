using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser
{
    public class dataContainer
    {
        private List<Title> titles;
        public dataContainer(List<Title>titles) {
            this.titles = titles;
        }

        public dataContainer()
        {
            titles = new List<Title>();
        }

        public List<Title> getTitles() {
            return titles;
        }

        public virtual int getCount() {
            return titles.Count;
        }
    }
}
