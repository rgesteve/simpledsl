using System;

using simpledsl;

// This is a sample of a very basic internal DSL

namespace simpledsl.sample
{
    class Program
    {
        static void Main(string[] args)
        {
            LiteralNode n = LiteralNode.One;
            Console.WriteLine($"Checking that the new child has {n.ChildCount} children, with value {n}!");
            var nil = NilNode.Nil;
            Console.WriteLine($"The node nil is {nil}.");
            var sum = n + (Node)6;
            Console.WriteLine($"The node sum is {sum}");
            var sub = nil - (Node)6;
            Console.WriteLine($"The node sum is {sub}");
            NodeVisitor v = new PrettyPrinterVisitor();
            n.Accept(v);
            Console.WriteLine("Done!");
        }
    }
}
