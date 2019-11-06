using System;
using System.Text;

namespace CodeGen
{
    class CodeGenerator
    {
        public string Indent { get; set; } = "    ";
        private StringBuilder builder = new StringBuilder();
        public string Generate(CodeTree tree)
        {
            builder.Clear();
            foreach(CodeNode node in tree.Children) { _Generate(node, 0); }
            return builder.ToString().TrimStart('\n'); // 先頭の改行を1つ削除
        }
        private void _Generate(CodeNode node, int indent)
        {
            builder.Append("\n");
            builder.Insert(builder.Length, Indent, indent).Append(node.Value);
            // ブロック開始
            BlockStart(node, indent);
            if (0 < node.Children.Count)
            {
                indent++;
                foreach(CodeNode n in node.Children) { _Generate(n, indent); }
                indent--;
            }
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
    }
}
