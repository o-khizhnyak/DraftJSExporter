using System;
using System.Collections.Generic;
using Newtonsoft.Json.Serialization;
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
            
            var element3 = new Element("span", null, "some text", true);
            Assert.Equal("<span>some text</span>", element3.Render());
            
            var element4 = new Element("p", null, "some text");
            Assert.Equal(
                @"<p>
    some text
</p>",
                element4.Render());
            
            var element5 = new Element(null, null, "plain text", true);
            Assert.Equal("plain text", element5.Render());
            
            var element6 = new Element("p", null, "paragraph text ");
            var element6Child = new Element("i", null, "italic text", true);
            element6.AppendChild(element6Child);
            Assert.Equal(
                @"<p>
    paragraph text 
    <i>italic text</i>
</p>",
                element6.Render());
            
            var element7 = new Element("p");
            var element7Child1 = new Element(null, null, "paragraph text ", true);
            var element7Child2 = new Element("i", null, "italic text", true);
            element7.AppendChild(element7Child1);
            element7.AppendChild(element7Child2);
            Assert.Equal(
                @"<p>
    paragraph text <i>italic text</i>
</p>",
                element7.Render());
            
            var element8 = new Element("p");
            var element8Child1 = new Element(null, null, "text with ", true);
            var element8Child2 = new Element("u", null, "underline", true);
            var element8Child3 = new Element(null, null, " style", true);
            element8.AppendChild(element8Child1);
            element8.AppendChild(element8Child2);
            element8.AppendChild(element8Child3);
            Assert.Equal(
                @"<p>
    text with <u>underline</u> style
</p>",
                element8.Render());
        }
    }
}