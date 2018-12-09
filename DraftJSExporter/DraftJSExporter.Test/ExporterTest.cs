using System;
using System.Collections.Generic;
using Xunit;

namespace DraftJSExporter.Test
{
    public class ExporterTest
    {
        [Fact]
        public void TestExporter()
        {
            var exporter = new Exporter(new ExporterConfig
            {
                EntityDecorators = new Dictionary<string, Func<IReadOnlyDictionary<string, string>, Element>>
                {
                    {"LINK", pairs => new Element("a", pairs, null, true)}
                }
            });

            var result = exporter.Render(
                @"
{
    ""entityMap"": {
        ""0"": {
            ""type"": ""LINK"",
            ""mutability"": ""MUTABLE"",
            ""data"": {
                ""href"": ""http://some-site.com""
            }
        }
    },
    ""blocks"": [
        {
            ""key"": ""w3rt5"",
            ""text"": ""Title"",
            ""type"": ""header-one"",
            ""depth"": 0,
            ""inlineStyleRanges"": [],
            ""entityRanges"": [],
            ""data"": {}
        },
        {
            ""key"": ""4k6d9"",
            ""text"": ""Paragraph text"",
            ""type"": ""unstyled"",
            ""depth"": 0,
            ""inlineStyleRanges"": [],
            ""entityRanges"": [],
            ""data"": {}
        },
        {
            ""key"": ""4g2s"",
            ""text"": ""Text with different styles"",
            ""type"": ""unstyled"",
            ""depth"": 0,
            ""inlineStyleRanges"": [{
                ""offset"": 1,
                ""length"": 2,
                ""style"": ""BOLD""   
            }, {
                ""offset"": 5,
                ""length"": 4,
                ""style"": ""ITALIC""
            }, {
                ""offset"": 10,
                ""length"": 12,
                ""style"": ""UNDERLINE""
            }, {
                ""offset"": 20,
                ""length"": 6,
                ""style"": ""CODE""
            }],
            ""entityRanges"": [],
            ""data"": {}
        }, 
        {
            ""key"": ""g5khh"",
            ""text"": ""Text which contains link to something"",
            ""type"": ""unstyled"",
            ""depth"": 0,
            ""inlineStyleRanges"": [],
            ""entityRanges"": [{
                ""offset"": 20,
                ""length"": 4,
                ""key"": 0
            }],
            ""data"": {}
        },
        {
            ""key"": ""g5khh"",
            ""text"": ""Text which contains styled link to something"",
            ""type"": ""unstyled"",
            ""depth"": 0,
            ""inlineStyleRanges"": [{
                ""offset"": 20,
                ""length"": 11,
                ""style"": ""UNDERLINE""
            }],
            ""entityRanges"": [{
                ""offset"": 20,
                ""length"": 11,
                ""key"": 0
            }],
            ""data"": {}
        },
        {
            ""key"": ""g5khh"",
            ""text"": ""Text which contains styled link to something"",
            ""type"": ""unstyled"",
            ""depth"": 0,
            ""inlineStyleRanges"": [{
                ""offset"": 20,
                ""length"": 3,
                ""style"": ""BOLD""
            }],
            ""entityRanges"": [{
                ""offset"": 20,
                ""length"": 11,
                ""key"": 0
            }],
            ""data"": {}
        },
        {
            ""key"": ""g5khh"",
            ""text"": ""Text which contains styled link to something"",
            ""type"": ""unstyled"",
            ""depth"": 0,
            ""inlineStyleRanges"": [{
                ""offset"": 25,
                ""length"": 6,
                ""style"": ""BOLD""
            }],
            ""entityRanges"": [{
                ""offset"": 20,
                ""length"": 11,
                ""key"": 0
            }],
            ""data"": {}
        },
        {
            ""key"": ""fn5sv"",
            ""text"": ""List item 1"",
            ""type"": ""unordered-list-item"",
            ""depth"": 0,
            ""inlineStyleRanges"": [],
            ""entityRanges"": [],
            ""data"": {}
        },
        {
            ""key"": ""vfj32"",
            ""text"": ""List item 2"",
            ""type"": ""unordered-list-item"",
            ""depth"": 0,
            ""inlineStyleRanges"": [],
            ""entityRanges"": [],
            ""data"": {}
        },
        {
            ""key"": ""4d5h"",
            ""text"": ""Plain text"",
            ""type"": ""unstyled"",
            ""depth"": 0,
            ""inlineStyleRanges"": [],
            ""entityRanges"": [],
            ""data"": {}
        }
    ]
}
");
            
            Assert.Equal(
                @"<h1>
    Title
</h1>
<div>
    Paragraph text
</div>
<div>
    T<strong>ex</strong>t <em>with</em> <u>different </u><u><code>st</code></u><code>yles</code>
</div>
<div>
    Text which contains <a href=""http://some-site.com"">link</a> to something
</div>
<div>
    Text which contains <a href=""http://some-site.com""><u>styled link</u></a> to something
</div>
<div>
    Text which contains <a href=""http://some-site.com""><strong>sty</strong>led link</a> to something
</div>
<div>
    Text which contains <a href=""http://some-site.com"">style<strong>d link</strong></a> to something
</div>
<ul>
    <li class=""list-item--depth-0"">
        List item 1
    </li>
    <li class=""list-item--depth-0"">
        List item 2
    </li>
</ul>
<div>
    Plain text
</div>", result);
        }
    }
}