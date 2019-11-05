using System;
using System.IO;
using Microsoft.CSharp;
using System.CodeDom.Compiler;

namespace CodeGen
{
    class BlockCodeNode : CodeNode
    {
        public enum StyleType { None, Minimum, Space, NewLine };
        public StyleType Style { get; private set; }
        public BlockCodeNode(string value, StyleType style=StyleType.NewLine) : base(value) => Style = style;
        public string BlockStart { get => GetBlock("{"); }
        public string BlockEnd { get => GetBlock("}"); }
        private string GetBlock(string keyword) => Style switch {
            StyleType.None => "",
            StyleType.Minimum => keyword,
            StyleType.Space => $" {keyword}",
            StyleType.NewLine => $"\n{keyword}",
    //        NewLine => $"{Environment.NewLine}{keyword}",
            _ => "",
        };
    }
}
