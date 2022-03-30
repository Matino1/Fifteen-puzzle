/*using System.Diagnostics;
using System.Collections;

namespace pietnastka
{
    internal class Astar : SearchingAlgorithm
    {
        public Astar() : base()
        {
        }

        public override bool result(int [,] rootBoard, string algorithm)
        {
            Stopwatch stopWatch = Stopwatch.StartNew();

            Node rootNode = new Node(0, rootBoard);

            PriorityQueue<Node, int> priorityQueue = new PriorityQueue<Node, int>();

            if (algorithm == "manh")
            {
                priorityQueue.Enqueue(rootNode, rootNode.ManhattanDistance);
            }
            else if (algorithm == "ham")
            {
                priorityQueue.Enqueue(rootNode, rootNode.HammingDistance);
            }
            else
            {
                return false;
            }
            

            bool isFinished = false;
            Node node;
            HashSet<ulong> visitedBoards = new HashSet<ulong>();

            visitedBoards.Add(rootNode.getBoardHash());

            while (priorityQueue.Count != 0)
            {
                node = priorityQueue.Dequeue();

                //Console.WriteLine("Manh: " + node.getGameboard().manhattanDistance);
                //Console.WriteLine("Level: " + node.level);
                //Console.WriteLine();

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
                            if (*//*visitedBoards.Add(node.getNextMoveHash(move)) &&*//* move != node.getReversePreviousMove())
                            {
                                moves.Add(move);
                            }
                        }
                    }
                    
                    node.addChildren(moves);

                    List<Node> nodes = node.getChildren();
                    foreach (Node child in nodes)
                    {
                        if (algorithm == "manh")
                        {
                            child.findFullManhatanDistance();
                            priorityQueue.Enqueue(child, child.getGameboard().manhattanDistance);
                        }
                        else
                        {
                            child.findFullHammingDistance();
                            priorityQueue.Enqueue(child, child.getGameboard().hammingDistance);
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
}*/