using System;

using simpledsl;

// This is a sample of a very basic internal DSL

namespace simpledsl.sample
{
    class Program
    {
        static void Main(string[] args)
        {
            Node n = new Node();
            Console.WriteLine($"Checking that the new child has {n.ChildCount} children!");
        }
    }
}
