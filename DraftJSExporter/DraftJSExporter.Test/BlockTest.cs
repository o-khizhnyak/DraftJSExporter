using System;
using System.Collections.Generic;
using DraftJSExporter.Defaults;
using Xunit;

namespace DraftJSExporter.Test
{
    public class BlockTest
    {
        [Fact]
        public void TestBlockWithoutStyleOrEntity()
        {
            var config = new ExporterConfig();
            var entityMap = new Dictionary<int, Entity>();
            
            var block = new Block
            {
                Text = "some text",
                Type = "unstyled",
                Depth = 0,
                InlineStyleRanges = new List<InlineStyleRange>(),
                EntityRanges = new List<EntityRange>()
            };

            var element = block.ConvertToElement(config, entityMap, null, -1);
            
            Assert.Equal("div", element.Type);
            Assert.Equal(0, element.Attributes.Count);
            Assert.Equal("some text", element.Text);
            Assert.Equal(false, element.Inline);
            Assert.Equal(0, element.Children.Count);
            
            block = new Block
            {
                Text = "some text",
                Type = "header-one",
                Depth = 0,
                InlineStyleRanges = new List<InlineStyleRange>(),
                EntityRanges = new List<EntityRange>()
            };

            element = block.ConvertToElement(config, entityMap, null, -1);
            
            Assert.Equal("h1", element.Type);
            Assert.Equal(0, element.Attributes.Count);
            Assert.Equal("some text", element.Text);
            Assert.Equal(false, element.Inline);
            Assert.Equal(0, element.Children.Count);
        }

        [Fact]
        public void TestBlockWithStyles()
        {
            var config = new ExporterConfig();
            var entityMap = new Dictionary<int, Entity>();
            
            var block = new Block
            {
                Text = "underlined text",
                Type = "unstyled",
                Depth = 0,
                InlineStyleRanges = new List<InlineStyleRange>
                {
                    new InlineStyleRange
                    {
                        Offset = 0,
                        Length = 10,
                        Style = "UNDERLINE"
                    }
                },
                EntityRanges = new List<EntityRange>()
            };
            
            var element = block.ConvertToElement(config, entityMap, null, -1);
            
            Assert.Equal("div", element.Type);
            Assert.Equal(0, element.Attributes.Count);
            Assert.Equal(null, element.Text);
            Assert.Equal(false, element.Inline);
            Assert.Equal(2, element.Children.Count);
            Assert.Equal("u", element.Children[0].Type);
            Assert.Equal("underlined", element.Children[0].Text);
            Assert.Equal(0, element.Children[0].Attributes.Count);
            Assert.Equal(true, element.Children[0].Inline);
            Assert.Equal(0, element.Children[0].Children.Count);
            Assert.Equal(null, element.Children[1].Type);
            Assert.Equal(" text", element.Children[1].Text);
            Assert.Equal(0, element.Children[1].Attributes.Count);
            Assert.Equal(true, element.Children[1].Inline);
            Assert.Equal(0, element.Children[1].Children.Count);
            
            block = new Block
            {
                Text = "text with underlined word",
                Type = "unstyled",
                Depth = 0,
                InlineStyleRanges = new List<InlineStyleRange>
                {
                    new InlineStyleRange
                    {
                        Offset = 10,
                        Length = 10,
                        Style = "UNDERLINE"
                    }
                },
                EntityRanges = new List<EntityRange>()
            };
            
            element = block.ConvertToElement(config, entityMap, null, -1);
            
            Assert.Equal("div", element.Type);
            Assert.Equal(0, element.Attributes.Count);
            Assert.Equal(null, element.Text);
            Assert.Equal(false, element.Inline);
            Assert.Equal(3, element.Children.Count);
            
            Assert.Equal(null, element.Children[0].Type);
            Assert.Equal(0, element.Children[0].Attributes.Count);
            Assert.Equal("text with ", element.Children[0].Text);
            Assert.Equal(true, element.Children[0].Inline);
            Assert.Equal(0, element.Children[0].Children.Count);
            
            Assert.Equal("u", element.Children[1].Type);
            Assert.Equal(0, element.Children[1].Attributes.Count);
            Assert.Equal("underlined", element.Children[1].Text);
            Assert.Equal(true, element.Children[1].Inline);
            Assert.Equal(0, element.Children[1].Children.Count);
            
            Assert.Equal(null, element.Children[2].Type);
            Assert.Equal(0, element.Children[2].Attributes.Count);
            Assert.Equal(" word", element.Children[2].Text);
            Assert.Equal(true, element.Children[2].Inline);
            Assert.Equal(0, element.Children[2].Children.Count);
            
            block = new Block
            {
                Text = "text with multiple styles",
                Type = "unstyled",
                Depth = 0,
                InlineStyleRanges = new List<InlineStyleRange>
                {
                    new InlineStyleRange
                    {
                        Offset = 5,
                        Length = 13,
                        Style = "UNDERLINE"
                    },
                    new InlineStyleRange
                    {
                        Offset = 10,
                        Length = 12,
                        Style = "BOLD"
                    }
                },
                EntityRanges = new List<EntityRange>()
            };
            
            element = block.ConvertToElement(config, entityMap, null, -1);
            
            Assert.Equal("div", element.Type);
            Assert.Equal(0, element.Attributes.Count);
            Assert.Equal(null, element.Text);
            Assert.Equal(false, element.Inline);
            Assert.Equal(5, element.Children.Count);
            
            Assert.Equal(null, element.Children[0].Type);
            Assert.Equal(0, element.Children[0].Attributes.Count);
            Assert.Equal("text ", element.Children[0].Text);
            Assert.Equal(true, element.Children[0].Inline);
            Assert.Equal(0, element.Children[0].Children.Count);
            
            Assert.Equal("u", element.Children[1].Type);
            Assert.Equal(0, element.Children[1].Attributes.Count);
            Assert.Equal("with ", element.Children[1].Text);
            Assert.Equal(true, element.Children[1].Inline);
            Assert.Equal(0, element.Children[1].Children.Count);
            
            Assert.Equal("u", element.Children[2].Type);
            Assert.Equal(0, element.Children[2].Attributes.Count);
            Assert.Equal(null, element.Children[2].Text);
            Assert.Equal(true, element.Children[2].Inline);
            Assert.Equal(1, element.Children[2].Children.Count);
            
            Assert.Equal("strong", element.Children[2].Children[0].Type);
            Assert.Equal(0, element.Children[2].Children[0].Attributes.Count);
            Assert.Equal("multiple", element.Children[2].Children[0].Text);
            Assert.Equal(true, element.Children[2].Children[0].Inline);
            Assert.Equal(0, element.Children[2].Children[0].Children.Count);
            
            Assert.Equal("strong", element.Children[3].Type);
            Assert.Equal(0, element.Children[3].Attributes.Count);
            Assert.Equal(" sty", element.Children[3].Text);
            Assert.Equal(true, element.Children[3].Inline);
            Assert.Equal(0, element.Children[3].Children.Count);
            
            Assert.Equal(null, element.Children[4].Type);
            Assert.Equal(0, element.Children[4].Attributes.Count);
            Assert.Equal("les", element.Children[4].Text);
            Assert.Equal(true, element.Children[4].Inline);
            Assert.Equal(0, element.Children[4].Children.Count);
        }

        [Fact]
        public void TestBlockWithEntity()
        {
            var config = new ExporterConfig
            {
                EntityDecorators = new Dictionary<string, Func<IReadOnlyDictionary<string, string>, Element>>
                {
                    {"LINK", pairs => new Element("a", pairs, null, true)}
                }
            };
            
            var entityMap = new Dictionary<int, Entity>
            {
                {
                    0, new Entity
                    {
                        Type = "LINK",
                        Mutability = "MUTABLE",
                        Data = new Dictionary<string, string>
                        {
                            {"href", "http://example.com"}
                        }
                    }
                }
            };
            
            var block = new Block
            {
                Text = "link to example",
                Depth = 0,
                Type = "unstyled",
                InlineStyleRanges = new List<InlineStyleRange>(),
                EntityRanges = new List<EntityRange>
                {
                    new EntityRange
                    {
                        Key = 0,
                        Offset = 0,
                        Length = 15
                    }
                }
            };

            var element = block.ConvertToElement(config, entityMap, null, -1);
            
            Assert.Equal("div", element.Type);
            Assert.Equal(null, element.Text);
            Assert.Equal(false, element.Inline);
            Assert.Equal(0, element.Attributes.Count);
            Assert.Equal(1, element.Children.Count);
            
            Assert.Equal("a", element.Children[0].Type);
            Assert.Equal(null, element.Children[0].Text);
            Assert.Equal(1, element.Children[0].Attributes.Count);
            Assert.Equal(true, element.Children[0].Attributes.ContainsKey("href"));
            Assert.Equal("http://example.com", element.Children[0].Attributes["href"]);
            Assert.Equal(1, element.Children[0].Children.Count);
            Assert.Equal(true, element.Children[0].Inline);
            
            Assert.Equal(null, element.Children[0].Children[0].Type);
            Assert.Equal("link to example", element.Children[0].Children[0].Text);
            Assert.Equal(0, element.Children[0].Children[0].Attributes.Count);
            Assert.Equal(0, element.Children[0].Children[0].Children.Count);
            Assert.Equal(true, element.Children[0].Children[0].Inline);
            
            block = new Block
            {
                Text = "link to example",
                Depth = 0,
                Type = "unstyled",
                InlineStyleRanges = new List<InlineStyleRange>(),
                EntityRanges = new List<EntityRange>
                {
                    new EntityRange
                    {
                        Key = 0,
                        Offset = 2,
                        Length = 5
                    }
                }
            };
            
            element = block.ConvertToElement(config, entityMap, null, -1);
            
            Assert.Equal("div", element.Type);
            Assert.Equal(null, element.Text);
            Assert.Equal(false, element.Inline);
            Assert.Equal(0, element.Attributes.Count);
            Assert.Equal(3, element.Children.Count);
            
            Assert.Equal(null, element.Children[0].Type);
            Assert.Equal("li", element.Children[0].Text);
            Assert.Equal(true, element.Children[0].Inline);
            Assert.Equal(0, element.Children[0].Attributes.Count);
            Assert.Equal(0, element.Children[0].Children.Count);
            
            Assert.Equal("a", element.Children[1].Type);
            Assert.Equal(null, element.Children[1].Text);
            Assert.Equal(true, element.Children[1].Inline);
            Assert.Equal(1, element.Children[1].Attributes.Count);
            Assert.Equal(true, element.Children[1].Attributes.ContainsKey("href"));
            Assert.Equal("http://example.com", element.Children[1].Attributes["href"]);
            Assert.Equal(1, element.Children[1].Children.Count);
            
            Assert.Equal(null, element.Children[1].Children[0].Type);
            Assert.Equal("nk to", element.Children[1].Children[0].Text);
            Assert.Equal(true, element.Children[1].Children[0].Inline);
            Assert.Equal(0, element.Children[1].Children[0].Attributes.Count);
            Assert.Equal(0, element.Children[1].Children[0].Children.Count);
            
            Assert.Equal(null, element.Children[2].Type);
            Assert.Equal(" example", element.Children[2].Text);
            Assert.Equal(true, element.Children[2].Inline);
            Assert.Equal(0, element.Children[2].Attributes.Count);
            Assert.Equal(0, element.Children[2].Children.Count);
        }

        [Fact]
        public void TestBlockWithStyleAndEntity()
        {
            var config = new ExporterConfig
            {
                EntityDecorators = new Dictionary<string, Func<IReadOnlyDictionary<string, string>, Element>>
                {
                    {"LINK", pairs => new Element("a", pairs, null, true)}
                }
            };
            
            var entityMap = new Dictionary<int, Entity>
            {
                {
                    0, new Entity
                    {
                        Type = "LINK",
                        Mutability = "MUTABLE",
                        Data = new Dictionary<string, string>
                        {
                            {"href", "http://example.com"}
                        }
                    }
                }
            };
            
            var block = new Block
            {
                Text = "some text with styles and entity",
                Depth = 0,
                Type = "unstyled",
                InlineStyleRanges = new List<InlineStyleRange>
                {
                    new InlineStyleRange
                    {
                        Offset = 10,
                        Length = 4,
                        Style = "BOLD"
                    },
                    new InlineStyleRange
                    {
                        Offset = 22,
                        Length = 3,
                        Style = "ITALIC"
                    }
                },
                EntityRanges = new List<EntityRange>
                {
                    new EntityRange
                    {
                        Key = 0,
                        Offset = 10,
                        Length = 11
                    }
                }
            };

            var element = block.ConvertToElement(config, entityMap, null, -1);
            
            Assert.Equal("div", element.Type);
            Assert.Equal(null, element.Text);
            Assert.Equal(0, element.Attributes.Count);
            Assert.Equal(false, element.Inline);
            Assert.Equal(5, element.Children.Count);
            
            Assert.Equal(null, element.Children[0].Type);
            Assert.Equal("some text ", element.Children[0].Text);
            Assert.Equal(0, element.Children[0].Attributes.Count);
            Assert.Equal(true, element.Children[0].Inline);
            Assert.Equal(0, element.Children[0].Children.Count);
            
            Assert.Equal("a", element.Children[1].Type);
            Assert.Equal(null, element.Children[1].Text);
            Assert.Equal(1, element.Children[1].Attributes.Count);
            Assert.Equal(true, element.Children[1].Attributes.ContainsKey("href"));
            Assert.Equal("http://example.com", element.Children[1].Attributes["href"]);
            Assert.Equal(true, element.Children[1].Inline);
            Assert.Equal(2, element.Children[1].Children.Count);
            
            Assert.Equal("strong", element.Children[1].Children[0].Type);
            Assert.Equal("with", element.Children[1].Children[0].Text);
            Assert.Equal(0, element.Children[1].Children[0].Attributes.Count);
            Assert.Equal(true, element.Children[1].Children[0].Inline);
            Assert.Equal(0, element.Children[1].Children[0].Children.Count);
            
            Assert.Equal(null, element.Children[1].Children[1].Type);
            Assert.Equal(" styles", element.Children[1].Children[1].Text);
            Assert.Equal(0, element.Children[1].Children[1].Attributes.Count);
            Assert.Equal(true, element.Children[1].Children[1].Inline);
            Assert.Equal(0, element.Children[1].Children[1].Children.Count);
            
            Assert.Equal(null, element.Children[2].Type);
            Assert.Equal(" ", element.Children[2].Text);
            Assert.Equal(0, element.Children[2].Attributes.Count);
            Assert.Equal(true, element.Children[2].Inline);
            Assert.Equal(0, element.Children[2].Children.Count);
            
            Assert.Equal("em", element.Children[3].Type);
            Assert.Equal("and", element.Children[3].Text);
            Assert.Equal(0, element.Children[3].Attributes.Count);
            Assert.Equal(true, element.Children[3].Inline);
            Assert.Equal(0, element.Children[3].Children.Count);
            
            Assert.Equal(null, element.Children[4].Type);
            Assert.Equal(" entity", element.Children[4].Text);
            Assert.Equal(0, element.Children[4].Attributes.Count);
            Assert.Equal(true, element.Children[4].Inline);
            Assert.Equal(0, element.Children[4].Children.Count);
        }
    }
}
