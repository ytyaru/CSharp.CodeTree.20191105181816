using System;

namespace CodeGen
{
    class Program
    {
        static void Main(string[] args)
        {
            var gen = new CodeGenerator();
//            var node = new CodeNode("")
            var tree = new CodeTree()
                .Add(new CodeNode("using System;"))
                .Add(new CodeNode(""))
                .Add(new BlockCodeNode("namespace MyNamespace")
                    .Add(new BlockCodeNode("class MyClass")
                        .Add(new BlockCodeNode("static void Main(string[] args)")
                            .Add(new CodeNode(@"Console.WriteLine(""Hello world"");")))));
//            Console.WriteLine(gen.Generate(node));
            Console.WriteLine(gen.Generate(tree));
        }
    }
}
