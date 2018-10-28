using NUnit.Framework;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Tests
{
    [TestFixture]
    public class Class1
    {
        [Test]
        public void Add_text_no_name_no_number_article()
        {
            var article = new Article(null, null);
            string expected = "";
            Assert.AreEqual(expected, article.getText());
            string text = "text";
            article.addText(text);
            expected = "text\n";
            Assert.AreEqual(expected, article.getText());
        }
        [Test]
        public void Add_text_no_name()
        {
            var article = new Article(null, "0");
            string expected = "Cтаття 0\n====\n";
            Assert.AreEqual(expected, article.getText());
            string text = "text";
            expected = "Cтаття 0\n====\ntext\n";
            article.addText(text);
            Assert.AreEqual(expected, article.getText());
        }

        [Test]
        public void Check_zero_data()
        {
            Mock<dataContainer> mockData = new Mock<dataContainer>();
            mockData.Setup(p => p.getCount()).Returns(0);
            Collector mockCollector = new Collector(mockData.Object,"text");
            var result = mockCollector.mkTree();
            Assert.That(result, Is.EqualTo(null));
        }
    }
}
