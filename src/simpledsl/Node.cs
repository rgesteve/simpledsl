using System;

namespace simpledsl
{
    public abstract class Node : ICloneable
    {
        public abstract object Clone();

        public int ChildCount { get {return 0;}}

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
        public static LiteralNode One = new LiteralNode(1);
        public static LiteralNode Zero = new LiteralNode(0);
    }
}
