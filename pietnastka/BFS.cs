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

            List<KeyValuePair<ulong, Node>> visitedBoardsWithHash = new List<KeyValuePair<ulong, Node>>();
            visitedBoardsWithHash.Add(new KeyValuePair<ulong, Node>(rootNode.getBoardHash(), rootNode));
            //list.Add(new KeyValuePair<string, string>("Name1", "Phone Num1"));
            //foreach (KeyValuePair >< string, string> item in list)
            //{
            //    Console.Write(item.Key + "=>" + item.Value);
            //}
            HashSet<ulong> visitedBoards = new HashSet<ulong>();
            visitedBoards.Add(rootNode.getBoardHash());

            while (queue.Any())
            {
                node = queue.Dequeue();


                nodesProcessed++;

                if (node.getGameboard().IsFinished())
                {
                    depth = node.level;
                    maxLevel = node.level;
                    solutionMoves = node.getPreviousMoves();
                    isFinished = true;
                    break;
                }

                if (node.level < maxLevel)
                {
                    List<char> moves = new List<char>();
                    foreach (char move in node.getPossibleMoves())
                    {

                        if (node.getGameboard().isMoveLegal(move))
                        {
                            nodesVisited++;
                            if (visitedBoards.Add(node.getNextMoveHash(move)) && move != node.getReversePreviousMove())
                            {
                                moves.Add(move);
                            }
                            else
                            {
                                //TODO
                            }
                        }
                    }

                    node.addChildren(moves);

                    foreach (Node child in node.getChildren())
                    {
                        queue.Enqueue(child);
                    }
                }
            }

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
