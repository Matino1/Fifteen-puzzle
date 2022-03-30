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

        public override bool result(int[,] rootBoard)
        {
            Stopwatch stopWatch = Stopwatch.StartNew();

            Node rootNode = new Node(0, rootBoard);

            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(rootNode);

            bool isFinished = false;
            Node node;

            HashSet<ulong> visitedBoards = new HashSet<ulong>();
            visitedBoards.Add(rootNode.getBoardHash());

            while (queue.Any())
            {
                node = queue.Dequeue();

                nodesProcessed++;

                if (node.IsFinished())
                {
                    depth = node.level;
                    solutionMoves = node.getPreviousMoves();
                    isFinished = true;
                    break;
                }

                char lastMove = node.getReversePreviousMove();
                char[] possibleMoves = node.getPossibleMoves();
                for (int i = possibleMoves.Length - 1; i >= 0; i--)
                {
                    char move = possibleMoves[i];
                    if (node.isMoveLegal(move) && move != lastMove)
                    {
                        nodesVisited++;
                        Node child = new Node(node.level + 1, node.CopyBoard(), node.getPreviousMoves(), node.ZeroPosition, move);
                        if (visitedBoards.Add(child.getBoardHash()))
                        {
                            queue.Enqueue(child);
                        }
                        else
                        {
                            //TODO
                        }
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
