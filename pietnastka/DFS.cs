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

        public override bool result(Gameboard rootBoard)
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

                if (node.getGameboard().IsFinished())
                {
                    depth = node.level;
                    maxLevel = node.level;
                    solutionMoves = node.getPreviousMoves();
                    isFinished = true;
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
                            } else
                            {
                                //TODO
                            }
                        }
                    }

                    node.addChildren(moves);
                
                    List<Node> nodes = node.getChildren();
                    nodes.Reverse();

                    foreach (Node child in nodes)
                    {
                        stack.Push(child);
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
