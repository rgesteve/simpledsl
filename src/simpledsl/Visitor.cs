using System;

namespace simpledsl
{
    public interface NodeVisitor
    {
        void VisitNode(Node n);
        void VisitNil(NilNode n);
        void VisitLiteral(LiteralNode n);
        /*
        void VisitSymbol(SymbolNode n);
        */
        void VisitAddSub(AddSubNode n);
        /*
        void VisitMulDiv(MulDivNode n);
        void VisitPow(PowNode n);
        void VisitNegate(NegateNode n);
        void VisitFx(FxNode n);
        void VisitRelational(RelationalNode n);
        void VisitLogic(LogicNode n);
        void VisitLogicNot(LogicNotNode n);
        */
    }

    public class NodeVisitorImpl : NodeVisitor
    {
        public void VisitNode(Node n) { /* empty */ }
        public void VisitNil(NilNode n) { /* empty */ }
        public void VisitLiteral(LiteralNode n) { /* empty */ }
        public void VisitAddSub(AddSubNode n) { /* empty */ }

        // Could be implemented directly on the interface, but I'd have
        // to switch to C# 8.0
        public void ChildAccept(Node n)
        {
            for (int i = 0; i < n.ChildCount; i++) {
                n.Accept(this); // should really have a children enumerator
            }
        }
    }

    public class PrettyPrinterVisitor : NodeVisitor
    {
        public void VisitLiteral(LiteralNode n)
        {
            Console.WriteLine($"Literal: {n}");
        }

        public void VisitNil(NilNode n)
        {
            Console.WriteLine($"Nil node: {n}");
        }

        public void VisitAddSub(AddSubNode n)
        {
            Console.WriteLine($"An addition/subtraction operator");       
        }

        public void VisitNode(Node n)
        {
            throw new NotImplementedException(); // shouldn't be called
        }        
    }
}
