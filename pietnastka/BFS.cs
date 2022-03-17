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
        public int nodesVisited { get; set; }
        public int depth { get; set; }
        public int resultTime { get; set; }
        public int nodeProcessed { get; set; }

        private List<char> solutionMoves = new();

        public BFS()
        {

        }

        public List<char> getSolution()
        {
            return solutionMoves;   
        }

        public bool result(Gameboard rootBoard)
        {
            Node rootNode = new Node(0, rootBoard);

            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(rootNode);

            Node node;
            List<long> visitedBoards = new List<long>();
            while (queue.Any())
            {
                node = queue.Dequeue();
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
                        //return true;
                    }

                    List<char> moves = new List<char>();
                    foreach (char move in node.getPossibleMoves())
                    {
                        if (node.getGameboard().isMoveLegal(move))
                            if (!visitedBoards.Contains(node.getGameboard().nextMove(move)))
                                moves.Add(move);
                    }

                    if (node.level < 7)
                        node.addChildren(moves);
                }
                else
                {
                    nodesVisited--;
                }

                if (node.level < 7)
                {
                    foreach (Node child in node.getChildren())
                    {
                        queue.Enqueue(child);
                    }
                }

            }
            nodesVisited = visitedBoards.Count;
            return false;
        }
    }
}
