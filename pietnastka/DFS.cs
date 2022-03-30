using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace pietnastka
{
    internal class DFS : SearchingAlgorithm
    {
        public DFS() : base()
        {

        }
        public static int instances = 0;

        public override bool result(int[,] rootBoard)
        {
            Stopwatch stopWatch = Stopwatch.StartNew();

            Node rootNode = new Node(0, rootBoard);

            Stack<Node> stack = new Stack<Node>();
            stack.Push(rootNode);
            bool isFinished = false;
            Node node;

            HashSet<ulong> visitedBoards = new HashSet<ulong>();
            visitedBoards.Add(rootNode.getBoardHash());

            while (stack.Any())
            {
                node = stack.Pop();

                if (node.level > maxLevel)
                    continue;

                nodesProcessed++;

                if (node.IsFinished())
                {
                    depth = node.level;
                    maxLevel = node.level;
                    solutionMoves = node.getPreviousMoves();
                    isFinished = true;
                    break;
                }

                if (node.level < maxLevel)
                {
                    //List<Node> nodes = new ();
                    List<char> moves = new List<char>();
                    int[,] nodeBoard = node.Board;
                    char lastMove = node.getReversePreviousMove();
                    char[] possibleMoves = node.getPossibleMoves();
                    for (int i = possibleMoves.Length - 1; i >= 0; i--)
                    {
                        char move = possibleMoves[i];
                        if (node.isMoveLegal(move))
                        {
                            nodesVisited++;
                            Node child = new Node(node.level + 1, node.CopyBoard(), node.getPreviousMoves(), node.ZeroPosition, move);
                            if (visitedBoards.Add(child.getBoardHash()) && move != lastMove)
                            {
                                stack.Push(child);
                            }
                            else
                            {
                                //TODO
                            }
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
