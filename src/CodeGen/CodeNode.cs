using System;
using System.Collections.Generic;
using System.Collections.ObjectModel; // ReadOnlyCollection

namespace CodeGen
{
    class CodeNode : CodeTree
    {
        public string Value { get; }
        public CodeNode(string value) : base() => Value = value;
//        public CodeNode Add(CodeNode node) { children.Add(node); return this; }
        public new CodeNode Add(CodeNode node) { children.Add(node); return this; }
    }
}
