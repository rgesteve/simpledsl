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
    }
}