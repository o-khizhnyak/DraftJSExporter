using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using Newtonsoft.Json.Serialization;
using Xunit;

namespace DraftJSExporter.Test
{
    public class ElementTests
    {
//        [Fact]
//        public void RenderTest()
//        {
//            var element1 = new HtmlElement("body", new Dictionary<string, string>());
//            element1.AppendChild(new HtmlElement("div", new Dictionary<string, string>
//            {
//                {"class", "qwe"}
//            }));
//            
//            Assert.Equal(
//                @"<body>
//    <div class=""qwe"">
//    </div>
//</body>", 
//                element1.Render());
//            
//            var element2 = new HtmlElement("div", new Dictionary<string, string>());
//            var paragraph = new HtmlElement("p", new Dictionary<string, string>());
//            element2.AppendChild(paragraph);
//            paragraph.AppendChild(new HtmlElement("b", new Dictionary<string, string>
//            {
//                {"foo", "bar"}
//            }));
//            element2.AppendChild(new HtmlElement("span", new Dictionary<string, string>()));
//            
//            Assert.Equal(
//                @"<div>
//    <p>
//        <b foo=""bar"">
//        </b>
//    </p>
//    <span>
//    </span>
//</div>", 
//                element2.Render());
//            
//            var element3 = new HtmlElement("span", null, "some text", true);
//            Assert.Equal("<span>some text</span>", element3.Render());
//            
//            var element4 = new HtmlElement("p", null, "some text");
//            Assert.Equal(
//                @"<p>
//    some text
//</p>",
//                element4.Render());
//            
//            var element5 = new HtmlElement(null, null, "plain text", true);
//            Assert.Equal("plain text", element5.Render());
//            
//            var element6 = new HtmlElement("p");
//            element6.AppendChild(new HtmlElement(null, null, "paragraph text ", true));
//            element6.AppendChild(new HtmlElement("i", null, "italic text", true));
//            Assert.Equal(
//                @"<p>
//    paragraph text <i>italic text</i>
//</p>",
//                element6.Render());
//            
//            var element7 = new HtmlElement("p");
//            var element7Child1 = new HtmlElement(null, null, "paragraph text ", true);
//            var element7Child2 = new HtmlElement("i", null, "italic text", true);
//            element7.AppendChild(element7Child1);
//            element7.AppendChild(element7Child2);
//            Assert.Equal(
//                @"<p>
//    paragraph text <i>italic text</i>
//</p>",
//                element7.Render());
//            
//            var element8 = new HtmlElement("p");
//            var element8Child1 = new HtmlElement(null, null, "text with ", true);
//            var element8Child2 = new HtmlElement("u", null, "underline", true);
//            var element8Child3 = new HtmlElement(null, null, " style", true);
//            element8.AppendChild(element8Child1);
//            element8.AppendChild(element8Child2);
//            element8.AppendChild(element8Child3);
//            Assert.Equal(
//                @"<p>
//    text with <u>underline</u> style
//</p>",
//                element8.Render());
//            
//            var element9 = new HtmlElement("p", new Dictionary<string, string>
//            {
//                {"attr", "value"}
//            });
//            var element9Child = new HtmlElement("div");
//            var element9ChildChild = new HtmlElement("p", null, "some text");
//            element9.AppendChild(element9Child);
//            element9Child.AppendChild(element9ChildChild);
//            Assert.Equal(
//                @"<p attr=""value"">
//    <div>
//        <p>
//            some text
//        </p>
//    </div>
//</p>",
//                element9.Render());
//            
//            var element10 = new HtmlElement("div");
//            var element10Child1 = new HtmlElement("div", null, "item 1");
//            var element10Child2 = new HtmlElement("div", null, "item 2");
//            var element10Child3 = new HtmlElement("div", null, "item 3");
//            element10.AppendChild(element10Child1);
//            element10.AppendChild(element10Child2);
//            element10.AppendChild(element10Child3);
//            Assert.Equal(
//                @"<div>
//    <div>
//        item 1
//    </div>
//    <div>
//        item 2
//    </div>
//    <div>
//        item 3
//    </div>
//</div>",
//                element10.Render());
//            
//            var element11 = new HtmlElement();
//            element11.AppendChild(new HtmlElement("h1", null, "Header"));
//            
//            Assert.Equal(
//                @"<h1>
//    Header
//</h1>",
//                element11.Render());
//            
//            var element12 = new HtmlElement("div");
//            var element12Child = new HtmlElement();
//            element12Child.AppendChild(new HtmlElement("p", null, "text"));
//            element12.AppendChild(element12Child);
//            
//            Assert.Equal(
//                @"<div>
//    <p>
//        text
//    </p>
//</div>", 
//                element12.Render());
//            
//            var element13 = new HtmlElement("ul");
//            element13.AppendChild(new HtmlElement("li", null, "item 1"));
//            element13.AppendChild(new HtmlElement("li", null, "item 2"));
//            element13.AppendChild(new HtmlElement("li", null, "item 3"));
//            
//            Assert.Equal(
//                @"<ul>
//    <li>
//        item 1
//    </li>
//    <li>
//        item 2
//    </li>
//    <li>
//        item 3
//    </li>
//</ul>",
//                element13.Render());
//            
//            var element14 = new HtmlElement("br", null, null, true, false, true);
//            element14.AppendChild(new HtmlElement());
//            
//            Assert.Equal("<br />", element14.Render());
//            
//            var element15 = new HtmlElement("img", new Dictionary<string, string>
//            {
//                {"src", "qwe"}
//            }, null, true, false, true);
//            
//            Assert.Equal(@"<img src=""qwe"" />", element15.Render());
//        }
    }
}