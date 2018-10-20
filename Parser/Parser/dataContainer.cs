using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser
{
    class dataContainer
    {
        private List<Title> titles;
        public dataContainer(List<Title>titles) {
            this.titles = titles;
        }

        public List<Title> getTitles() {
            return titles;
        }
    }
}
