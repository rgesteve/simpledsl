using System;

namespace simpledsl
{
    public abstract class Node : ICloneable
    {
        public abstract object Clone();

        public virtual int ChildCount { get {return 0;}}
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

    /// <summary>
    /// A subtree consisting of a parent and a single child, meant to
    /// denote a unary operator, where the parent is the operator and the child
    /// the argument.
    /// </summary>
    public abstract class UnNode : Node
    {
        Node child;
        public UnNode(Node n)
        {
            child = n;
        }
        public UnNode()
        {
            child = NilNode.Nil;
        }

        public override int ChildCount
        {
            get { return 1; }
        }
    }

    /// <summary>
    /// A subtree consisting of a parent with two children, meant to
    /// denote a binary operator, encoded in the parent, with a left and right 
    /// arguments (the children).
    /// </summary>
    public abstract class BiNode : Node
    {
        Node left, right;
        public BiNode(Node _left, Node _right)
        {
            left = _left;
            right = _right;
        }

        public BiNode()
        {
            left = right = NilNode.Nil;
        }

        public Node Left 
        {
            get { return left; }
            set { left = value; }
        }

        public Node Right 
        {
            get { return right; }
            set { right = value; }
        }

        public override int ChildCount
        {
            get { return 2; }
        }
    }

    /// <summary>
    /// We use the same node to represent addition and subtraction (but not
    /// multiplication and division, or other binary ops) to facilitate the 
    /// encoding of association rules (multiplication and division "bind" or have
    /// higher associative priority than addition and subtraction).
    /// </summary>
    public class AddSubNode : BiNode
    {
        bool addop; // keeps track of whether this is an addition or a subtraction
        public AddSubNode(Node _left, Node _right, bool isadd) : base(_left, _right)
        {
            IsAddition = isadd;
            // TODO: Basic reorg to keep Literals on the left operator
        }

        public bool IsAddition
        {
            get { return addop; }
            set { addop = value; }
        }
        public override object Clone()
        {
            return new AddSubNode((Node)Left.Clone(), (Node)Right.Clone(), IsAddition);
        }

        public override string MLIR()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return $"({Left})";
        }
        public override void Accept(NodeVisitor v)
        {
            v.VisitAddSub(this);
        }

    }
}
