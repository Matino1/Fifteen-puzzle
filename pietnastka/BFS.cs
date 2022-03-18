using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace pietnastka
{
    internal class BFS : SearchingAlgorithm
    {
       /* public readonly int maxLevel = 7;
        public int resultLenght { get; set; }
        public int nodesVisited { get; set; }
        public int depth { get; set; }
        public string resultTime { get; set; }
        public int nodeProcessed { get; set; }

        private List<char> solutionMoves = new();*/

        public BFS(): base()
        {
          
        }

        public override bool result(Gameboard rootBoard, int maxLevel)
        {
            this.maxLevel = maxLevel;
            Stopwatch stopWatch = Stopwatch.StartNew();

            Node rootNode = new Node(0, rootBoard);

            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(rootNode);

            Node node = null;
            List<long> visitedBoards = new List<long>();
            while (queue.Any())
            {
                node = queue.Dequeue();
                //Console.WriteLine(queue.Count);
                //Console.WriteLine(node.level);

                //Console.WriteLine(node.getStringBoardCode());
                //Console.WriteLine("level: " + node.level);
                //Console.WriteLine("visited: " + visitedBoards.Count);
                //Console.WriteLine("visited: " + (visitedBoards.Distinct().Count() == visitedBoards.Count));
                //node.getGameboard().printBoard();


                if (!visitedBoards.Contains(node.getBoardHash()))
                {
                    visitedBoards.Add(node.getBoardHash());
                    if (node.getGameboard().IsFinished())
                    {
                        depth = node.level;
                        solutionMoves = node.getPreviousMoves();
                        if (node.getPreviousMoves().Count == 0)
                            Console.WriteLine("Game already finished");
                        //return true;
                        
                    }
                    
                    List<char> moves = new List<char>();
                    foreach (char move in node.getPossibleMoves())
                    {
                        if (node.getGameboard().isMoveLegal(move))
                            if (!visitedBoards.Contains(node.getGameboard().nextMove(move)))
                                moves.Add(move);
                    }
                    if (node.level < maxLevel)
                        node.addChildren(moves);
                }
                else
                {
                    nodesVisited--;
                }

                foreach (Node child in node.getChildren())
                {
                    if (child.level <= maxLevel)
                        queue.Enqueue(child);
                }

            }
            depth = node.level;
            nodesVisited = visitedBoards.Count - 1;
            saveElapsedTime(stopWatch);
            return false;
        }
    }
}
