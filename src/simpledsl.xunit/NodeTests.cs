using System;
using Xunit;

using simpledsl;

namespace simpledsl.xunit
{
    public class Nodes
    {
        [Fact]
        public void LiteralNodeTest() {
            int a = 1;
            LiteralNode n = new LiteralNode(a);
            Assert.Equal(a, n.Value);
        }

        [Fact]
        public void ExplicitLiteralTest() {
            float f = 5.0F;
            var n = (Node)f;
            Assert.IsType<LiteralNode>(n);
        }

        [Fact]
        public void NilTest() {
            var n = NilNode.Nil;
            Assert.IsType<NilNode>(n);
        }

        [Fact]
        public void SimpleAddSubNodeTest() {
            var n = new AddSubNode(NilNode.Nil, NilNode.Nil, true);
            Assert.Equal(2, n.ChildCount);
        }

        [Fact]
        public void AddSubNodeFromOperator() {
            var n = (Node)3 + (Node)4;
            Assert.IsType<AddSubNode>(n);
        }

        [Fact]
        public void AddSubRightOp() {
            AddSubNode n = (AddSubNode)((Node)3 - NilNode.Nil);
            Assert.False(n.IsAddition);
        }
    }
}