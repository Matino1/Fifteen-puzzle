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
            while (queue.Any())
            {
                node = queue.Dequeue();
                Console.WriteLine(node.getStringBoardCode());
                Console.WriteLine("level: " + node.level);
                Console.WriteLine("visited: " + visitedBoards.Count);
                Console.WriteLine("visited: " + (visitedBoards.Distinct().Count() == visitedBoards.Count));


                if (!visitedBoards.Contains(node.getStringBoardCode()))
                {
                    visitedBoards.Add(node.getStringBoardCode());
                    if (node.getBoard().IsFinished())
                    {
                        recursionDepth = node.level;
                        nodeVisited = visitedBoards.Count;
                        //return true;
                    }
                    else
                    {
                        node.addChildren();
                    }
                }

                if (node.level < 7)
                {
                    foreach (Node child in node.getChildren())
                    {
                        if (!visitedBoards.Contains(child.getStringBoardCode()))
                        {
                            queue.Enqueue(child);
                        }
                    }
                }

            }

            return false;
        }
    }
}
