using System;
using System.Text;

namespace CodeGen
{
    class CodeGenerator
    {
        public string Indent { get; set; } = "    ";
        private StringBuilder builder = new StringBuilder();
        private bool isLastNewLineDelete;
        public string Generate(CodeTree tree)
        {
            builder.Clear();
            isLastNewLineDelete = false;
            foreach(CodeNode node in tree.Children) { _Generate(node, 0); }
            return builder.ToString();
        }
        /*
        public string Generate(CodeNode node)
        {
            builder.Clear();
            _Generate(node, 0);
            return builder.ToString();
        }
        */
        private void _Generate(CodeNode node, int indent)
        {
            builder.Insert(builder.Length, Indent, indent).Append(node.Value);
            // ブロック開始
            BlockStart(node, indent);
            builder.Append("\n");
            if (0 < node.Children.Count)
            {
                indent++;
                foreach(CodeNode n in node.Children) { _Generate(n, indent); }
                indent--;
            }
            // 最後の改行コードを削除
//            if (!isLastNewLineDelete) { builder.Remove(builder.Length - 1, 1); isLastNewLineDelete=true; }
            if (!isLastNewLineDelete) { builder.Remove(builder.Length - 2, 2); isLastNewLineDelete=true; }
            // ブロック終了
            BlockEnd(node, indent);
        }
        // 改行コードの前でインデントする。これを全改行コードに対して行う
        private void BlockStart(CodeNode node, int indent) => Block(node, "{", indent);
        private void BlockEnd(CodeNode node, int indent) => Block(node, "}", indent);
        private void Block(CodeNode node, string keyword, int indent)
        {
            if (node is BlockCodeNode n) {
                switch (n.Style) {
                    case BlockCodeNode.StyleType.None: return;
                    case BlockCodeNode.StyleType.Minimum: builder.Append(keyword); return;
                    case BlockCodeNode.StyleType.Space: builder.Append($" {keyword}"); return;
                    case BlockCodeNode.StyleType.NewLine: builder.AppendLine().Insert(builder.Length, Indent, indent).Append(keyword); return;
                    default: return;
                }
            }
        }
        /*
        private string Block(BlockCodeNode node, string keyword, int indent) => node.Style switch
        {
            BlockCodeNode.StyleType.None => return, // 何もしないときを表現できない……
            BlockCodeNode.StyleType.Minimum => builder.Append(keyword),
            BlockCodeNode.StyleType.Space => builder.Append($" {keyword}")",
            BlockCodeNode.StyleType.NewLine => builder.Insert(builder.Length, Indent, indent).Append(keyword),
            _ => "",
        };
        */
    }
}
