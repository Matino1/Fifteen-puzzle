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

        public void newNode(Node node)
        {
            nodes.Add(new List<Node>());
        }

        public void addEdge(Node praentNode, Node childrenNode)
        {
            //nodes[praentNode.id].Add(childrenNode);
        }
    }
}
