using System.Collections.Generic;
using DraftJs.Exporter.Html;
using Xunit;

namespace DraftJsExporter.Tests
{
    public class HtmlBuilderTest
    {
        [Fact]
        public void TestBuilder()
        {
            var builder = new HtmlBuilder();
            builder.AddOpeningTag("div", null, false, false);
            
            Assert.Equal(
                @"<div>
    ", builder.ToString());
            
            builder.AddText("text ");
            
            Assert.Equal(
                @"<div>
    text ", builder.ToString());
            
            builder.AddOpeningTag("b", null, true, false);
            
            Assert.Equal(
                @"<div>
    text <b>", builder.ToString());
            
            builder.AddText("bold");
            
            Assert.Equal(
                @"<div>
    text <b>bold", builder.ToString());
            
            builder.AddClosingTag("b", true);
            
            Assert.Equal(
                @"<div>
    text <b>bold</b>", builder.ToString());
            
            builder.AddClosingTag("div", false);
            
            Assert.Equal(
                @"<div>
    text <b>bold</b>
</div>", builder.ToString());
            
            builder.AddOpeningTag("div", new Dictionary<string, string>
            {
                {"class", "paragraph"}
            }, false, false);
            
            Assert.Equal(
                @"<div>
    text <b>bold</b>
</div>
<div class=""paragraph"">
    ", builder.ToString());
            
            builder.AddOpeningTag("p", null, false, false);
            
            Assert.Equal(
                @"<div>
    text <b>bold</b>
</div>
<div class=""paragraph"">
    <p>
        ", builder.ToString());
            
            builder.AddText("qwe");
            
            Assert.Equal(
                @"<div>
    text <b>bold</b>
</div>
<div class=""paragraph"">
    <p>
        qwe", builder.ToString());
            
            builder.AddClosingTag("p", false);
            
            Assert.Equal(
                @"<div>
    text <b>bold</b>
</div>
<div class=""paragraph"">
    <p>
        qwe
    </p>", builder.ToString());
            
            builder.AddOpeningTag("li", null, false, false);
            
            Assert.Equal(
                @"<div>
    text <b>bold</b>
</div>
<div class=""paragraph"">
    <p>
        qwe
    </p>
    <li>
        ", builder.ToString());
            
            builder.AddText("list item");
            
            Assert.Equal(
                @"<div>
    text <b>bold</b>
</div>
<div class=""paragraph"">
    <p>
        qwe
    </p>
    <li>
        list item", builder.ToString());
            
            builder.AddClosingTag("li", false);
            
            Assert.Equal(
                @"<div>
    text <b>bold</b>
</div>
<div class=""paragraph"">
    <p>
        qwe
    </p>
    <li>
        list item
    </li>", builder.ToString());
            
            builder.AddClosingTag("div", false);
            
            Assert.Equal(
                @"<div>
    text <b>bold</b>
</div>
<div class=""paragraph"">
    <p>
        qwe
    </p>
    <li>
        list item
    </li>
</div>", builder.ToString());
            
            builder.AddOpeningTag("p", null, false, false);
            builder.AddOpeningTag(null, null, true, false);
            builder.AddText("some ");
            builder.AddClosingTag(null, true);
            builder.AddOpeningTag("u", null, true, false);
            builder.AddText("underlined");
            builder.AddClosingTag("u", true);
            builder.AddOpeningTag(null, null, true, false);
            builder.AddText(" text");
            builder.AddClosingTag(null, true);
            builder.AddOpeningTag("br", null, true, true);
            builder.CloseTag(true);
            builder.AddClosingTag("p", false);
            
            Assert.Equal(
                @"<div>
    text <b>bold</b>
</div>
<div class=""paragraph"">
    <p>
        qwe
    </p>
    <li>
        list item
    </li>
</div>
<p>
    some <u>underlined</u> text<br /> 
</p>", builder.ToString());
        }
    }
}