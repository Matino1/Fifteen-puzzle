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

        public BFS() : base()
        {
          
        }

        public override bool result(Gameboard rootBoard)
        {
            Stopwatch stopWatch = Stopwatch.StartNew();

            Node rootNode = new Node(0, rootBoard);

            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(rootNode);

            bool isFinished = false;
            Node node;
            //List<long> visitedBoards = new List<long>();
            HashSet<ulong> visitedBoards = new HashSet<ulong>();
            while (queue.Any())
            {
                
                node = queue.Dequeue();;
                if (node.getPreviousMoves().Count == 0)
                {
                    visitedBoards.Add(node.getBoardHash());
                }
                nodesProcessed++;

                //if (visitedBoards.Add(node.getBoardHash()))
                //{
                    //visitedBoards.Add(node.getBoardHash());
                    if (node.getGameboard().IsFinished())
                    {
                        depth = node.level;
                        maxLevel = node.level;
                        solutionMoves = node.getPreviousMoves();
                        
                        if (node.getPreviousMoves().Count == 0)
                            Console.WriteLine("Game already finished");
                        isFinished = true;
                        
                    }
                    
                    List<char> moves = new List<char>();
                    foreach (char move in node.getPossibleMoves())
                    {
                        if (node.getGameboard().isMoveLegal(move))
                        {
                            if (visitedBoards.Add(node.getNextMoveHash(move)))
                            {
                                moves.Add(move);
                                nodesVisited++;
                            }
                        }
                    }
                                                           
                    if (node.level < maxLevel)
                        node.addChildren(moves);
                //}
                //Console.WriteLine(nodesVisited);

                foreach (Node child in node.getChildren())
                {
                    if (child.level <= maxLevel)
                        queue.Enqueue(child);
                }
            }

            //nodesProcessed = visitedBoards.Count - 1;
            resultLenght = solutionMoves.Count;
            saveElapsedTime(stopWatch);

            if (isFinished)
            {
                return true;
            } 
            else
            {
                resultLenght = -1;
                return false;
            }
        }
    }
}
