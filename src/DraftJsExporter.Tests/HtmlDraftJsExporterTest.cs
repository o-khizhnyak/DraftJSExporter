using System.Collections.Generic;
using System.Text.Json;
using DraftJs.Exporter.Html;
using DraftJs.Exporter.Html.Models;
using HtmlTags;
using Xunit;

namespace DraftJsExporter.Tests
{
    public class HtmlDraftJsExporterTest
    {
        [Fact]
        public void TestExporter()
        {
            HtmlTag Picture(IReadOnlyDictionary<string, object> data)
            {
                var picture = new HtmlTag("picture");
                var alt = ((JsonElement) data["alt"]).GetString();
                var title = ((JsonElement) data["title"]).GetString();
                var src = ((JsonElement) data["src"]).GetString();
                picture.Append("img", img => img.Attr("src", src).Attr("title", title).Attr("alt", alt));

                foreach (var sourceElem in ((JsonElement) data["sources"]).EnumerateArray())
                {
                    var src1X = sourceElem.GetProperty("src1x").GetString();
                    var src2X = sourceElem.GetProperty("src2x").GetString();
                    var media = sourceElem.GetProperty("media").GetString();
                    picture.Append("source", configuration: source =>
                    {
                        source.Attr("srcset", $"{src1X} 1x, {src2X} 2x");
                        source.Attr("media", media);
                    });
                }

                return picture;
            }

            var exporter = new HtmlDraftJsExporter(new HtmlDraftJsExporterConfig
            {
                EntityDecorators = new Dictionary<string, CreateTagFromEntityData>
                {
                    {"LINK", data => new HtmlTag("a", t => t.ConfigureAttributesFromEntityData(data))},
                    {"IMAGE", data => new HtmlTag("img", t => t.ConfigureAttributesFromEntityData(data))},
                    { "PICTURE", Picture }
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
        },
        ""1"": {
            ""type"": ""LINK"",
            ""mutability"": ""MUTABLE"",
            ""data"": {
                ""href"": ""http://www.google.com/""
            }
        },
        ""2"": {
            ""type"": ""IMAGE"",
            ""mutability"": ""MUTABLE"",
            ""data"": {
                ""src"": ""http://some-site-1.com""
            }
        },
        ""3"": {
            ""type"": ""PICTURE"",
            ""mutability"": ""MUTABLE"",
            ""data"": {
                ""alt"": ""a"",
                ""title"": ""t"",
                ""src"": ""https://example.com/fallback.png"",
                ""sources"": [{
                    ""src1x"": ""https://example.com/s1_1000.png"",
                    ""src2x"": ""https://example.com/s1_2000.png"",
                    ""media"": ""(min-width: 600px)""
                }, {
                    ""src1x"": ""https://example.com/s2_1000.png"",
                    ""src2x"": ""https://example.com/s2_2000.png"",
                    ""media"": ""(min-width: 400px)""
                }]
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
            ""key"": ""ef4jk"",
            ""text"": ""List item 2.1"",
            ""type"": ""unordered-list-item"",
            ""depth"": 1,
            ""inlineStyleRanges"": [],
            ""entityRanges"": [],
            ""data"": {}
        },
        {
            ""key"": ""m3dhs"",
            ""text"": ""List item 2.1.1"",
            ""type"": ""unordered-list-item"",
            ""depth"": 2,
            ""inlineStyleRanges"": [],
            ""entityRanges"": [],
            ""data"": {}
        },
        {
            ""key"": ""ev4jk"",
            ""text"": ""List item 2.2"",
            ""type"": ""unordered-list-item"",
            ""depth"": 1,
            ""inlineStyleRanges"": [],
            ""entityRanges"": [],
            ""data"": {}
        },
        {
            ""key"": ""gfh5w"",
            ""text"": ""List item 3 with a styled link to something"",
            ""type"": ""unordered-list-item"",
            ""depth"": 0,
            ""inlineStyleRanges"": [{
                ""offset"": 24,
                ""length"": 6,
                ""style"": ""BOLD""
            }],
            ""entityRanges"": [{
                ""offset"": 19,
                ""length"": 11,
                ""key"": 1
            }],
            ""data"": {}
        },
        {
            ""key"": ""dd92v"",
            ""text"": "" "",
            ""type"": ""atomic"",
            ""depth"": 0,
            ""inlineStyleRanges"": [],
            ""entityRanges"": [{
                ""offset"": 0,
                ""length"": 1,
                ""key"": 2
            }],
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
        },
        {
            ""key"": ""adasef13qwe"",
            ""text"": "" "",
            ""type"": ""atomic"",
            ""depth"": 0,
            ""inlineStyleRanges"": [],
            ""entityRanges"": [{
                ""offset"": 0,
                ""length"": 1,
                ""key"": 3
            }],
            ""data"": {}
        }
    ]
}
");
            
            Assert.Equal(
                "<h1>Title</h1>" +
                "<div>Paragraph text</div>" +
                "<div>T<strong>ex</strong>t <em>with</em> <u>different </u><u><code>st</code></u><code>yles</code></div>" +
                "<div>Text which contains <a href=\"http://some-site.com\">link</a> to something</div>" +
                "<div>Text which contains <a href=\"http://some-site.com\"><u>styled link</u></a> to something</div>" +
                "<div>Text which contains <a href=\"http://some-site.com\"><strong>sty</strong>led link</a> to something</div>" +
                "<div>Text which contains <a href=\"http://some-site.com\">style<strong>d link</strong></a> to something</div>" +
                "<ul>" +
                "<li class=\"list-item--depth-0 list-item--reset\">List item 1</li>" +
                "<li class=\"list-item--depth-0\">List item 2</li>" +
                "<li class=\"list-item--depth-1 list-item--reset\">List item 2.1</li>" +
                "<li class=\"list-item--depth-2 list-item--reset\">List item 2.1.1</li>" +
                "<li class=\"list-item--depth-1\">List item 2.2</li>" +
                "<li class=\"list-item--depth-0\">List item 3 with a <a href=\"http://www.google.com/\">style<strong>d link</strong></a> to something</li>" +
                "</ul>" +
                "<img src=\"http://some-site-1.com\">" +
                "<div>Plain text</div>" +
                "<picture>" +
                "<img src=\"https://example.com/fallback.png\" title=\"t\" alt=\"a\">" +
                "<source srcset=\"https://example.com/s1_1000.png 1x, https://example.com/s1_2000.png 2x\" media=\"(min-width: 600px)\">" +
                "<source srcset=\"https://example.com/s2_1000.png 1x, https://example.com/s2_2000.png 2x\" media=\"(min-width: 400px)\"> " +
                "</picture>"
                , 
                result);
        }

        [Fact]
        public void TestExporter_EmptyContent()
        {
            var exporter = new HtmlDraftJsExporter(new HtmlDraftJsExporterConfig());

            var result = exporter.Render("{}");
            Assert.Equal(string.Empty, result);
        }
    }
}