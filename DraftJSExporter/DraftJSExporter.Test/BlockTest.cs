using System;
using System.Collections.Generic;
using DraftJSExporter.Defaults;
using Xunit;

namespace DraftJSExporter.Test
{
    public class BlockTest
    {
//        [Fact]
//        public void TestBlockWithoutStyleOrEntity()
//        {
//            var config = new DraftJsExporterConfig();
//            var entityMap = new Dictionary<int, Entity>();
//            
//            var block = new Block
//            {
//                Text = "some text",
//                Type = "unstyled",
//                Depth = 0,
//                InlineStyleRanges = new List<InlineStyleRange>(),
//                EntityRanges = new List<EntityRange>()
//            };
//
//            var element = block.ConvertToTreeNode(config, entityMap, null, -1);
//            
//            Assert.Equal("div", element.Name);
//            Assert.Equal(0, element.Attributes.Count);
//            Assert.Equal("some text", element.Text);
//            Assert.False(element.Inline);
//            Assert.Empty(element.Children);
//            
//            block = new Block
//            {
//                Text = "some text",
//                Type = "header-one",
//                Depth = 0,
//                InlineStyleRanges = new List<InlineStyleRange>(),
//                EntityRanges = new List<EntityRange>()
//            };
//
//            element = block.ConvertToTreeNode(config, entityMap, null, -1);
//            
//            Assert.Equal("h1", element.Name);
//            Assert.Equal(0, element.Attributes.Count);
//            Assert.Equal("some text", element.Text);
//            Assert.False(element.Inline);
//            Assert.Empty(element.Children);
//        }
//
//        [Fact]
//        public void TestBlockWithStyles()
//        {
//            var config = new DraftJsExporterConfig();
//            var entityMap = new Dictionary<int, Entity>();
//            
//            var block = new Block
//            {
//                Text = "underlined text",
//                Type = "unstyled",
//                Depth = 0,
//                InlineStyleRanges = new List<InlineStyleRange>
//                {
//                    new InlineStyleRange
//                    {
//                        Offset = 0,
//                        Length = 10,
//                        Style = "UNDERLINE"
//                    }
//                },
//                EntityRanges = new List<EntityRange>()
//            };
//            
//            var element = block.ConvertToTreeNode(config, entityMap, null, -1);
//            
//            Assert.Equal("div", element.Name);
//            Assert.Equal(0, element.Attributes.Count);
//            Assert.Null(element.Text);
//            Assert.False(element.Inline);
//            Assert.Equal(2, element.Children.Count);
//            Assert.Equal("u", element.Children[0].Name);
//            Assert.Equal("underlined", element.Children[0].Text);
//            Assert.Equal(0, element.Children[0].Attributes.Count);
//            Assert.True(element.Children[0].Inline);
//            Assert.Empty(element.Children[0].Children);
//            Assert.Null(element.Children[1].Name);
//            Assert.Equal(" text", element.Children[1].Text);
//            Assert.Equal(0, element.Children[1].Attributes.Count);
//            Assert.True(element.Children[1].Inline);
//            Assert.Empty(element.Children[1].Children);
//            
//            block = new Block
//            {
//                Text = "text with underlined word",
//                Type = "unstyled",
//                Depth = 0,
//                InlineStyleRanges = new List<InlineStyleRange>
//                {
//                    new InlineStyleRange
//                    {
//                        Offset = 10,
//                        Length = 10,
//                        Style = "UNDERLINE"
//                    }
//                },
//                EntityRanges = new List<EntityRange>()
//            };
//            
//            element = block.ConvertToTreeNode(config, entityMap, null, -1);
//            
//            Assert.Equal("div", element.Name);
//            Assert.Equal(0, element.Attributes.Count);
//            Assert.Null(element.Text);
//            Assert.False(element.Inline);
//            Assert.Equal(3, element.Children.Count);
//            
//            Assert.Null(element.Children[0].Name);
//            Assert.Equal(0, element.Children[0].Attributes.Count);
//            Assert.Equal("text with ", element.Children[0].Text);
//            Assert.True(element.Children[0].Inline);
//            Assert.Empty(element.Children[0].Children);
//            
//            Assert.Equal("u", element.Children[1].Name);
//            Assert.Equal(0, element.Children[1].Attributes.Count);
//            Assert.Equal("underlined", element.Children[1].Text);
//            Assert.True(element.Children[1].Inline);
//            Assert.Empty(element.Children[1].Children);
//            
//            Assert.Null(element.Children[2].Name);
//            Assert.Equal(0, element.Children[2].Attributes.Count);
//            Assert.Equal(" word", element.Children[2].Text);
//            Assert.True(element.Children[2].Inline);
//            Assert.Empty(element.Children[2].Children);
//            
//            block = new Block
//            {
//                Text = "text with multiple styles",
//                Type = "unstyled",
//                Depth = 0,
//                InlineStyleRanges = new List<InlineStyleRange>
//                {
//                    new InlineStyleRange
//                    {
//                        Offset = 5,
//                        Length = 13,
//                        Style = "UNDERLINE"
//                    },
//                    new InlineStyleRange
//                    {
//                        Offset = 10,
//                        Length = 12,
//                        Style = "BOLD"
//                    }
//                },
//                EntityRanges = new List<EntityRange>()
//            };
//            
//            element = block.ConvertToTreeNode(config, entityMap, null, -1);
//            
//            Assert.Equal("div", element.Name);
//            Assert.Equal(0, element.Attributes.Count);
//            Assert.Null(element.Text);
//            Assert.False(element.Inline);
//            Assert.Equal(5, element.Children.Count);
//            
//            Assert.Null(element.Children[0].Name);
//            Assert.Equal(0, element.Children[0].Attributes.Count);
//            Assert.Equal("text ", element.Children[0].Text);
//            Assert.True(element.Children[0].Inline);
//            Assert.Empty(element.Children[0].Children);
//            
//            Assert.Equal("u", element.Children[1].Name);
//            Assert.Equal(0, element.Children[1].Attributes.Count);
//            Assert.Equal("with ", element.Children[1].Text);
//            Assert.True(element.Children[1].Inline);
//            Assert.Empty(element.Children[1].Children);
//            
//            Assert.Equal("u", element.Children[2].Name);
//            Assert.Equal(0, element.Children[2].Attributes.Count);
//            Assert.Null(element.Children[2].Text);
//            Assert.True(element.Children[2].Inline);
//            Assert.Single(element.Children[2].Children);
//            
//            Assert.Equal("strong", element.Children[2].Children[0].Name);
//            Assert.Equal(0, element.Children[2].Children[0].Attributes.Count);
//            Assert.Equal("multiple", element.Children[2].Children[0].Text);
//            Assert.True(element.Children[2].Children[0].Inline);
//            Assert.Empty(element.Children[2].Children[0].Children);
//            
//            Assert.Equal("strong", element.Children[3].Name);
//            Assert.Equal(0, element.Children[3].Attributes.Count);
//            Assert.Equal(" sty", element.Children[3].Text);
//            Assert.True(element.Children[3].Inline);
//            Assert.Empty(element.Children[3].Children);
//            
//            Assert.Null(element.Children[4].Name);
//            Assert.Equal(0, element.Children[4].Attributes.Count);
//            Assert.Equal("les", element.Children[4].Text);
//            Assert.True(element.Children[4].Inline);
//            Assert.Empty(element.Children[4].Children);
//        }
//
//        [Fact]
//        public void TestBlockWithEntity()
//        {
//            var config = new DraftJsExporterConfig
//            {
//                EntityDecorators = new Dictionary<string, Func<IReadOnlyDictionary<string, string>, HtmlElement>>
//                {
//                    {"LINK", pairs => new HtmlElement("a", pairs, null, true)},
//                    {"IMAGE", pairs => new HtmlElement("img", pairs, null, false, false, true)}
//                }
//            };
//            
//            var entityMap = new Dictionary<int, Entity>
//            {
//                {
//                    0, new Entity
//                    {
//                        Type = "LINK",
//                        Mutability = "MUTABLE",
//                        Data = new Dictionary<string, string>
//                        {
//                            {"href", "http://example.com"}
//                        }    
//                    }
//                },
//                {
//                    1, new Entity
//                    {
//                        Type = "IMAGE",
//                        Mutability = "MUTABLE",
//                        Data = new Dictionary<string, string>
//                        {
//                            {"src", "http://site.com"}
//                        }
//                    }
//                }
//            };
//            
//            var block = new Block
//            {
//                Text = "link to example",
//                Depth = 0,
//                Type = "unstyled",
//                InlineStyleRanges = new List<InlineStyleRange>(),
//                EntityRanges = new List<EntityRange>
//                {
//                    new EntityRange
//                    {
//                        Key = 0,
//                        Offset = 0,
//                        Length = 15
//                    }
//                }
//            };
//
//            var element = block.ConvertToTreeNode(config, entityMap, null, -1);
//            
//            Assert.Equal("div", element.Name);
//            Assert.Null(element.Text);
//            Assert.False(element.Inline);
//            Assert.Equal(0, element.Attributes.Count);
//            Assert.Single(element.Children);
//            
//            Assert.Equal("a", element.Children[0].Name);
//            Assert.Null(element.Children[0].Text);
//            Assert.Equal(1, element.Children[0].Attributes.Count);
//            Assert.True(element.Children[0].Attributes.ContainsKey("href"));
//            Assert.Equal("http://example.com", element.Children[0].Attributes["href"]);
//            Assert.Single(element.Children[0].Children);
//            Assert.True(element.Children[0].Inline);
//            
//            Assert.Null(element.Children[0].Children[0].Name);
//            Assert.Equal("link to example", element.Children[0].Children[0].Text);
//            Assert.Equal(0, element.Children[0].Children[0].Attributes.Count);
//            Assert.Empty(element.Children[0].Children[0].Children);
//            Assert.True(element.Children[0].Children[0].Inline);
//            
//            block = new Block
//            {
//                Text = "link to example",
//                Depth = 0,
//                Type = "unstyled",
//                InlineStyleRanges = new List<InlineStyleRange>(),
//                EntityRanges = new List<EntityRange>
//                {
//                    new EntityRange
//                    {
//                        Key = 0,
//                        Offset = 2,
//                        Length = 5
//                    }
//                }
//            };
//            
//            element = block.ConvertToTreeNode(config, entityMap, null, -1);
//            
//            Assert.Equal("div", element.Name);
//            Assert.Null(element.Text);
//            Assert.False(element.Inline);
//            Assert.Equal(0, element.Attributes.Count);
//            Assert.Equal(3, element.Children.Count);
//            
//            Assert.Null(element.Children[0].Name);
//            Assert.Equal("li", element.Children[0].Text);
//            Assert.True(element.Children[0].Inline);
//            Assert.Equal(0, element.Children[0].Attributes.Count);
//            Assert.Empty(element.Children[0].Children);
//            
//            Assert.Equal("a", element.Children[1].Name);
//            Assert.Null(element.Children[1].Text);
//            Assert.True(element.Children[1].Inline);
//            Assert.Equal(1, element.Children[1].Attributes.Count);
//            Assert.True(element.Children[1].Attributes.ContainsKey("href"));
//            Assert.Equal("http://example.com", element.Children[1].Attributes["href"]);
//            Assert.Single(element.Children[1].Children);
//            
//            Assert.Null(element.Children[1].Children[0].Name);
//            Assert.Equal("nk to", element.Children[1].Children[0].Text);
//            Assert.True(element.Children[1].Children[0].Inline);
//            Assert.Equal(0, element.Children[1].Children[0].Attributes.Count);
//            Assert.Empty(element.Children[1].Children[0].Children);
//            
//            Assert.Null(element.Children[2].Name);
//            Assert.Equal(" example", element.Children[2].Text);
//            Assert.True(element.Children[2].Inline);
//            Assert.Equal(0, element.Children[2].Attributes.Count);
//            Assert.Empty(element.Children[2].Children);
//            
//            block = new Block
//            {
//                Text = " ",
//                Depth = 0,
//                Type = "atomic",
//                InlineStyleRanges = new List<InlineStyleRange>(),
//                EntityRanges = new List<EntityRange>
//                {
//                    new EntityRange
//                    {
//                        Key = 1,
//                        Length = 1,
//                        Offset = 0
//                    }
//                }
//            };
//            
//            element = block.ConvertToTreeNode(config, entityMap, null, -1);
//            
//            Assert.Null(element.Name);
//            Assert.Null(element.Text);
//            Assert.False(element.Inline);
//            Assert.Single(element.Children);
//            Assert.Equal(0, element.Attributes.Count);
//           
//            Assert.Equal("img", element.Children[0].Name);
//            Assert.Null(element.Children[0].Text);
//            Assert.False(element.Children[0].Inline);
//            Assert.Equal(1, element.Children[0].Attributes.Count);
//            Assert.True(element.Children[0].Attributes.ContainsKey("src"));
//            Assert.Equal("http://site.com", element.Children[0].Attributes["src"]);
//            Assert.Empty(element.Children[0].Children);
//        }
//
//        [Fact]
//        public void TestBlockWithStyleAndEntity()
//        {
//            var config = new DraftJsExporterConfig
//            {
//                EntityDecorators = new Dictionary<string, Func<IReadOnlyDictionary<string, string>, HtmlElement>>
//                {
//                    {"LINK", pairs => new HtmlElement("a", pairs, null, true)}
//                }
//            };
//            
//            var entityMap = new Dictionary<int, Entity>
//            {
//                {
//                    0, new Entity
//                    {
//                        Type = "LINK",
//                        Mutability = "MUTABLE",
//                        Data = new Dictionary<string, string>
//                        {
//                            {"href", "http://example.com"}
//                        }
//                    }
//                }
//            };
//            
//            var block = new Block
//            {
//                Text = "some text with styles and entity",
//                Depth = 0,
//                Type = "unstyled",
//                InlineStyleRanges = new List<InlineStyleRange>
//                {
//                    new InlineStyleRange
//                    {
//                        Offset = 10,
//                        Length = 4,
//                        Style = "BOLD"
//                    },
//                    new InlineStyleRange
//                    {
//                        Offset = 22,
//                        Length = 3,
//                        Style = "ITALIC"
//                    }
//                },
//                EntityRanges = new List<EntityRange>
//                {
//                    new EntityRange
//                    {
//                        Key = 0,
//                        Offset = 10,
//                        Length = 11
//                    }
//                }
//            };
//
//            var element = block.ConvertToTreeNode(config, entityMap, null, -1);
//            
//            Assert.Equal("div", element.Name);
//            Assert.Null(element.Text);
//            Assert.Equal(0, element.Attributes.Count);
//            Assert.False(element.Inline);
//            Assert.Equal(5, element.Children.Count);
//            
//            Assert.Null(element.Children[0].Name);
//            Assert.Equal("some text ", element.Children[0].Text);
//            Assert.Equal(0, element.Children[0].Attributes.Count);
//            Assert.True(element.Children[0].Inline);
//            Assert.Empty(element.Children[0].Children);
//            
//            Assert.Equal("a", element.Children[1].Name);
//            Assert.Null(element.Children[1].Text);
//            Assert.Equal(1, element.Children[1].Attributes.Count);
//            Assert.True(element.Children[1].Attributes.ContainsKey("href"));
//            Assert.Equal("http://example.com", element.Children[1].Attributes["href"]);
//            Assert.True(element.Children[1].Inline);
//            Assert.Equal(2, element.Children[1].Children.Count);
//            
//            Assert.Equal("strong", element.Children[1].Children[0].Name);
//            Assert.Equal("with", element.Children[1].Children[0].Text);
//            Assert.Equal(0, element.Children[1].Children[0].Attributes.Count);
//            Assert.True(element.Children[1].Children[0].Inline);
//            Assert.Empty(element.Children[1].Children[0].Children);
//            
//            Assert.Null(element.Children[1].Children[1].Name);
//            Assert.Equal(" styles", element.Children[1].Children[1].Text);
//            Assert.Equal(0, element.Children[1].Children[1].Attributes.Count);
//            Assert.True(element.Children[1].Children[1].Inline);
//            Assert.Empty(element.Children[1].Children[1].Children);
//            
//            Assert.Null(element.Children[2].Name);
//            Assert.Equal(" ", element.Children[2].Text);
//            Assert.Equal(0, element.Children[2].Attributes.Count);
//            Assert.True(element.Children[2].Inline);
//            Assert.Empty(element.Children[2].Children);
//            
//            Assert.Equal("em", element.Children[3].Name);
//            Assert.Equal("and", element.Children[3].Text);
//            Assert.Equal(0, element.Children[3].Attributes.Count);
//            Assert.True(element.Children[3].Inline);
//            Assert.Empty(element.Children[3].Children);
//            
//            Assert.Null(element.Children[4].Name);
//            Assert.Equal(" entity", element.Children[4].Text);
//            Assert.Equal(0, element.Children[4].Attributes.Count);
//            Assert.True(element.Children[4].Inline);
//            Assert.Empty(element.Children[4].Children);
//        }
    }
}
