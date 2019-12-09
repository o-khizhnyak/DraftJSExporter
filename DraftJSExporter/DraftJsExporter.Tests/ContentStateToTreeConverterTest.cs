using System.Text.Json;
using DraftJs.Exporter;
using DraftJs.Exporter.Models;
using Xunit;

namespace DraftJsExporter.Tests
{
    public class ContentStateToTreeConverterTest
    {
        [Fact]
        public void TestWithEmptyJson()
        {
            var tree = ContentStateToTreeConverter.Convert("");
            Assert.Null(tree);
        }

        [Fact]
        public void TestUnstyled()
        {
            var tree = ContentStateToTreeConverter.Convert(@"{
                ""entityMap"": {},
                ""blocks"": [
                    {
                        ""key"": ""w3rt5"",
                        ""text"": ""some text"",
                        ""type"": ""unstyled"",
                        ""depth"": 0,
                        ""inlineStyleRanges"": [],
                        ""entityRanges"": [],
                        ""data"": {}
                    }                       
                ]
            }");
            
            var child = Assert.Single(tree.Children);
            Assert.NotNull(child);
            var unstyled = Assert.IsType<UnstyledBlock>(child);
            Assert.Equal("some text", Assert.IsType<TextTreeNode>(Assert.Single(unstyled.Children)).Text);
            Assert.Equal(0, unstyled.Depth);
        }

        [Fact]
        public void TestHeaderOne()
        {
            var tree = ContentStateToTreeConverter.Convert(@"{
                ""entityMap"": {},
                ""blocks"": [
                    {
                        ""key"": ""w3rt5"",
                        ""text"": ""some text"",
                        ""type"": ""header-one"",
                        ""depth"": 0,
                        ""inlineStyleRanges"": [],
                        ""entityRanges"": [],
                        ""data"": {}
                    }                       
                ]
            }");

            var child = Assert.Single(tree.Children);
            Assert.NotNull(child);
            var header = Assert.IsType<HeaderOneBlock>(child);
            Assert.Equal("some text", Assert.IsType<TextTreeNode>(Assert.Single(header.Children)).Text);
            Assert.Equal(0, header.Depth);
        }

        [Fact]
        public void TestBlockWithUnderline1()
        {
            var tree = ContentStateToTreeConverter.Convert(@"{
                ""entityMap"": {},
                ""blocks"": [
                    {
                        ""key"": ""w3rt5"",
                        ""text"": ""underlined text"",
                        ""type"": ""unstyled"",
                        ""depth"": 0,
                        ""inlineStyleRanges"": [
                            {
                                ""offset"": 0,
                                ""length"": 10,
                                ""style"": ""UNDERLINE""
                            }
                        ],
                        ""entityRanges"": [],
                        ""data"": {}
                    }                       
                ]
            }");

            var child = Assert.Single(tree.Children);
            Assert.NotNull(child);
            var unstyled = Assert.IsType<UnstyledBlock>(child);
            Assert.Equal(0, unstyled.Depth);
            Assert.Equal(2, unstyled.Children.Count);
            var underline = Assert.IsType<UnderlineStyleTreeNode>(unstyled.Children[0]);
            Assert.Equal("underlined", Assert.IsType<TextTreeNode>(Assert.Single(underline.Children)).Text);
            var plain = Assert.IsType<TextTreeNode>(unstyled.Children[1]);
            Assert.Equal(" text", plain.Text);
            Assert.Empty(plain.Children);
        }

        [Fact]
        public void TestBlockWithUnderline2()
        {
            var tree = ContentStateToTreeConverter.Convert(@"{
                ""entityMap"": {},
                ""blocks"": [
                    {
                        ""key"": ""w3rt5"",
                        ""text"": ""text with underlined word"",
                        ""type"": ""unstyled"",
                        ""depth"": 0,
                        ""inlineStyleRanges"": [
                            {
                                ""offset"": 10,
                                ""length"": 10,
                                ""style"": ""UNDERLINE""
                            }
                        ],
                        ""entityRanges"": [],
                        ""data"": {}
                    }                       
                ]
            }");
            
            var child = Assert.Single(tree.Children);
            Assert.NotNull(child);
            var unstyled = Assert.IsType<UnstyledBlock>(child);
            Assert.Equal(0, unstyled.Depth);
            Assert.Equal(3, unstyled.Children.Count);
            
            var first = Assert.IsType<TextTreeNode>(unstyled.Children[0]);
            Assert.Equal("text with ", first.Text);
            Assert.Empty(first.Children);
            
            var second = Assert.IsType<UnderlineStyleTreeNode>(unstyled.Children[1]);
            Assert.Equal("underlined", Assert.IsType<TextTreeNode>(Assert.Single(second.Children)).Text);

            var third = Assert.IsType<TextTreeNode>(unstyled.Children[2]);
            Assert.Equal(" word", third.Text);
            Assert.Empty(third.Children);
        }

        [Fact]
        public void TestBlockWithMultipleStyles()
        {
            var tree = ContentStateToTreeConverter.Convert(@"{
                ""entityMap"": {},
                ""blocks"": [
                    {
                        ""key"": ""w3rt5"",
                        ""text"": ""text with multiple styles"",
                        ""type"": ""unstyled"",
                        ""depth"": 0,
                        ""inlineStyleRanges"": [
                            {
                                ""offset"": 5,
                                ""length"": 13,
                                ""style"": ""UNDERLINE""
                            },
                            {
                                ""offset"": 10,
                                ""length"": 12,
                                ""style"": ""BOLD""
                            }
                        ],
                        ""entityRanges"": [],
                        ""data"": {}
                    }                       
                ]
            }");
            
            var child = Assert.Single(tree.Children);
            Assert.NotNull(child);
            var unstyled = Assert.IsType<UnstyledBlock>(child);
            Assert.Equal(0, unstyled.Depth);
            Assert.Equal(5, unstyled.Children.Count);

            var plain1 = Assert.IsType<TextTreeNode>(unstyled.Children[0]);
            Assert.Equal("text ", plain1.Text);
            Assert.Empty(plain1.Children);

            var underline1 = Assert.IsType<UnderlineStyleTreeNode>(unstyled.Children[1]);
            Assert.Equal("with ", Assert.IsType<TextTreeNode>(Assert.Single(underline1.Children)).Text);

            var underline2 = Assert.IsType<UnderlineStyleTreeNode>(unstyled.Children[2]);
            Assert.Single(underline2.Children);

            var bold1 = Assert.IsType<BoldStyleTreeNode>(underline2.Children[0]);
            Assert.Equal("multiple", Assert.IsType<TextTreeNode>(Assert.Single(bold1.Children)).Text);

            var bold2 = Assert.IsType<BoldStyleTreeNode>(unstyled.Children[3]);
            Assert.Equal(" sty", Assert.IsType<TextTreeNode>(Assert.Single(bold2.Children)).Text);

            var plain2 = Assert.IsType<TextTreeNode>(unstyled.Children[4]);
            Assert.Equal("les", plain2.Text);
            Assert.Empty(plain2.Children);
        }

        [Fact]
        public void TestBlockWithEntity()
        {
            var tree = ContentStateToTreeConverter.Convert(@"{
                ""entityMap"": {
                    ""0"": {
                        ""type"": ""LINK"",
                        ""mutability"": ""MUTABLE"",
                        ""data"": {
                            ""href"": ""http://example.com""
                        }
                    },
                    ""1"": {
                        ""type"": ""IMAGE"",
                        ""mutability"": ""MUTABLE"",
                        ""data"": {
                            ""src"": ""http://site.com""
                        }
                    }
                },
                ""blocks"": [
                    {
                        ""key"": ""w3rt5"",
                        ""text"": ""link to example"",
                        ""type"": ""unstyled"",
                        ""depth"": 0,
                        ""inlineStyleRanges"": [],
                        ""entityRanges"": [
                            {
                                ""key"": 0,
                                ""offset"": 0,
                                ""length"": 15
                            }
                        ],
                        ""data"": {}
                    },
                    {
                        ""key"": ""w3rt5"",
                        ""text"": ""link to example"",
                        ""type"": ""unstyled"",
                        ""depth"": 0,
                        ""inlineStyleRanges"": [],
                        ""entityRanges"": [
                            {
                                ""key"": 0,
                                ""offset"": 2,
                                ""length"": 5
                            }
                        ],
                        ""data"": {}
                    },
                    {
                        ""key"": ""w3rt5"",
                        ""text"": "" "",
                        ""type"": ""atomic"",
                        ""depth"": 0,
                        ""inlineStyleRanges"": [],
                        ""entityRanges"": [
                            {
                                ""key"": 1,
                                ""offset"": 0,
                                ""length"": 1
                            }
                        ],
                        ""data"": {}
                    }
                ]
            }");
            
            Assert.Equal(3, tree.Children.Count);
            
            var block1 = Assert.IsType<UnstyledBlock>(tree.Children[0]);
            var block1Child = Assert.Single(block1.Children);
            
            var entity1 = Assert.IsType<EntityTreeNode>(block1Child);
            Assert.Equal("LINK", entity1.Type);
            Assert.Equal("http://example.com", StringFromJsonElement(entity1.Data["href"]));
            Assert.Equal("link to example", Assert.IsType<TextTreeNode>(Assert.Single(entity1.Children)).Text);
            
            var block2 = Assert.IsType<UnstyledBlock>(tree.Children[1]);
            Assert.Equal(3, block2.Children.Count);

            var block2Child1 = Assert.IsType<TextTreeNode>(block2.Children[0]);
            Assert.Equal("li", block2Child1.Text);

            var block2Child2 = Assert.IsType<EntityTreeNode>(block2.Children[1]);
            Assert.Equal("nk to", Assert.IsType<TextTreeNode>(Assert.Single(block2Child2.Children)).Text);
            Assert.Equal("http://example.com", StringFromJsonElement(block2Child2.Data["href"]));
            
            var block2Child3 = Assert.IsType<TextTreeNode>(block2.Children[2]);
            Assert.Equal(" example", block2Child3.Text);

            var block3 = Assert.IsType<AtomicBlock>(tree.Children[2]);
            var block3Child = Assert.Single(block3.Children);
            var image = Assert.IsType<EntityTreeNode>(block3Child);
            Assert.Equal(" ", Assert.IsType<TextTreeNode>(Assert.Single(image.Children)).Text);
            Assert.Equal("IMAGE", image.Type);
            Assert.Equal("http://site.com", StringFromJsonElement(image.Data["src"]));
        }


        [Fact]
        public void TestBlockWithStylesAndEntity()
        {
            var tree = ContentStateToTreeConverter.Convert(@"{
                ""entityMap"": {
                    ""0"": {
                        ""type"": ""LINK"",
                        ""mutability"": ""MUTABLE"",
                        ""data"": {
                            ""href"": ""http://example.com""
                        }
                    }
                },
                ""blocks"": [
                    {
                        ""key"": ""w3rt5"",
                        ""text"": ""some text with styles and entity"",
                        ""type"": ""unstyled"",
                        ""depth"": 0,
                        ""inlineStyleRanges"": [
                            {
                                ""offset"": 10,
                                ""length"": 4,
                                ""style"": ""BOLD""
                            },
                            {
                                ""offset"": 22,
                                ""length"": 3,
                                ""style"": ""ITALIC""
                            }
                        ],
                        ""entityRanges"": [
                            {
                                ""key"": 0,
                                ""offset"": 10,
                                ""length"": 11
                            }
                        ],
                        ""data"": {}
                    }
                ]
            }");

            var root = Assert.Single(tree.Children);
            var unstyled = Assert.IsType<UnstyledBlock>(root);
            Assert.Equal(5, unstyled.Children.Count);

            var plain1 = Assert.IsType<TextTreeNode>(unstyled.Children[0]);
            Assert.Equal("some text ", plain1.Text);
            Assert.Empty(plain1.Children);

            var entity = Assert.IsType<EntityTreeNode>(unstyled.Children[1]);
            Assert.Equal("LINK", entity.Type);
            Assert.Equal("http://example.com", StringFromJsonElement(entity.Data["href"]));
            Assert.Equal(2, entity.Children.Count);

            var entityChild1 = Assert.IsType<BoldStyleTreeNode>(entity.Children[0]);
            Assert.Equal("with", Assert.IsType<TextTreeNode>(Assert.Single(entityChild1.Children)).Text);

            var entityChild2 = Assert.IsType<TextTreeNode>(entity.Children[1]);
            Assert.Equal(" styles", entityChild2.Text);
            Assert.Empty(entityChild2.Children);

            var plain2 = Assert.IsType<TextTreeNode>(unstyled.Children[2]);
            Assert.Equal(" ", plain2.Text);
            Assert.Empty(plain2.Children);

            var italic = Assert.IsType<ItalicStyleTreeNode>(unstyled.Children[3]);
            Assert.Equal("and", Assert.IsType<TextTreeNode>(Assert.Single(italic.Children)).Text);

            var plain3 = Assert.IsType<TextTreeNode>(unstyled.Children[4]);
            Assert.Equal(" entity", plain3.Text);
            Assert.Empty(plain3.Children);
        }
        
        
        private static string StringFromJsonElement(object el) => Assert.IsType<JsonElement>(el).GetString();

    }
}