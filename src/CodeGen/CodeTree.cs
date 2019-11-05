using System;
using System.Collections.Generic;
using System.Collections.ObjectModel; // ReadOnlyCollection

namespace CodeGen
{
    class CodeTree
    {
        protected IList<CodeNode> children;
        public IReadOnlyList<CodeNode> Children { get; }
        public CodeTree()
        {
            children = new List<CodeNode>();
            Children = new ReadOnlyCollection<CodeNode>(children);
        }
        public CodeTree Add(CodeNode node) { children.Add(node); return this; }
    }
}
