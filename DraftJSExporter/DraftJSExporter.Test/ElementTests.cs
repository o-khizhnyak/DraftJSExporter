using System;
using System.Collections.Generic;
using Xunit;

namespace DraftJSExporter.Test
{
    public class ElementTests
    {
        [Fact]
        public void RenderTest()
        {
            var element1 = new Element("body", new Dictionary<string, string>());
            element1.AppendChild(new Element("div", new Dictionary<string, string>
            {
                {"class", "qwe"}
            }));
            
            Assert.Equal(
                @"<body>
    <div class=""qwe"">
    </div>
</body>", 
                element1.Render());
            
            var element2 = new Element("div", new Dictionary<string, string>());
            var paragraph = new Element("p", new Dictionary<string, string>());
            element2.AppendChild(paragraph);
            paragraph.AppendChild(new Element("b", new Dictionary<string, string>
            {
                {"foo", "bar"}
            }));
            element2.AppendChild(new Element("span", new Dictionary<string, string>()));
            
            Assert.Equal(
                @"<div>
    <p>
        <b foo=""bar"">
        </b>
    </p>
    <span>
    </span>
</div>", 
                element2.Render());
            
        }
    }
}