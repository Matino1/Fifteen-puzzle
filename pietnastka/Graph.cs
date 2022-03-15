using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pietnastka
{
    internal class Graph
    {
        private List<List<Node>> nodes;

        public Graph()
        {
            nodes = new List<List<Node>>();
        }

        public void addNode(Node node)
        {
            nodes[node.level].Add(node);
        }

        public void addEdge(Node praentNode, Node childrenNode)
        {
            nodes[].Add(childrenNode);
        }
    }
}
