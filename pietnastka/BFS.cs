using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pietnastka
{
    internal class BFS
    {
        public int resultLenght { get; set; }
        public int nodeVisited { get; set; }
        public int recursionDepth { get; set; }
        public int resultTime { get; set; }
        public int nodeProcessed { get; set; }


        public BFS()
        {

        }

        public bool result(Gameboard rootBoard)
        {
            Node rootNode = new Node(0, rootBoard);

            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(rootNode);

            Node node;
            List<string> visitedBoards = new List<string>();
            while(!queue.Any())
            {
                node = queue.Dequeue();

                if(!node.isVisited)
                {
                    node.isVisited = true;
                    if (node.getBoard().IsFinished())
                    {
                        return true;
                    } 
                    else
                    {
                        node.addChildren();
                    }
                }

                foreach (Node child in node.getChildren())
                {
                    if(!child.isVisited)
                    {
                        queue.Enqueue(child);
                    }
                }
            }

            for(int level = 0; level < 22; level++)
            {
                rootNode.addChildren();
                
            }



            return false;
        }
    }
}
