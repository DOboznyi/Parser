using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Parser;

namespace Parser.Tests
{
    public class Class1
    {
        [Fact]
        public void Add_text_no_name_no_number_article()
        {
            var article = new Article(null, null);
            string expected = "";
            Assert.Equal(expected, article.getText());
            string text = "text";
            article.addText(text);
            expected = "text\n";
            Assert.Equal(expected, article.getText());
        }

        [Fact]
        public void Add_text_no_name()
        {
            var article = new Article(null, "0");
            string expected = "Cтаття 0\n====\n";
            Assert.Equal(expected, article.getText());
            string text = "text";
            expected = "Cтаття 0\n====\ntext\n";
            article.addText(text);
            Assert.Equal(expected, article.getText());
        }               
    }
}
