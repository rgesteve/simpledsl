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
    }
}