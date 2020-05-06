using System;

namespace simpledsl
{
    public abstract class Node : ICloneable
    {
        public abstract object Clone();

        public int ChildCount { get {return 0;}}
        // from https://mlir.llvm.org/docs/LangRef
        public abstract string MLIR();

        public static explicit operator Node(float f)
        {
            return new LiteralNode(f);
        }

        public virtual void Accept(NodeVisitor n) 
        {
            Console.WriteLine("In Node::Accept for NodeVisitor"); 
            n.VisitNode(this); 
        }

    }

    /// <summary>
    /// A Node for a constant value
    /// </summary>
    public class LiteralNode : Node
    {
        private float literal;
        public float Value {
            get { return literal; }
            set { literal = value; }
        }
        public LiteralNode(float v) { Value = v; }
        public override string ToString() {
            return $"{Value}";
        }

        public override object Clone() {
            return new LiteralNode(Value);
        }

        public override string MLIR() {
            return $"Literal<1> [{Value}]";
        }

        public override void Accept(NodeVisitor n) {
            n.VisitLiteral(this);
        }
        public static LiteralNode One = new LiteralNode(1);
        public static LiteralNode Zero = new LiteralNode(0);
    }

    public class NilNode : Node
    {
        public static NilNode Nil = new NilNode();

        private NilNode()
        {
            /* empty -- use `Nil` instead */
        }
        public override object Clone()
        {
            return this;
        }

        public override void Accept(NodeVisitor n)
        {
            Console.WriteLine("In PrettyPrint accept for nilnode");
            n.VisitNil(this);
        }

        public override string MLIR()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return "NILNODE";
        }
    }
}
