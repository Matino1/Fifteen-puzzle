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

            Dictionary<ulong, int> hashDepth = new Dictionary<ulong, int>();
            hashDepth.Add(rootNode.getBoardHash(), 0);
            HashSet<ulong> visitedBoards = new HashSet<ulong>();
            visitedBoards.Add(rootNode.getBoardHash());

            while (stack.Any())
            {
                node = stack.Pop();

                nodesProcessed++;

                if (node.level > this.depth)
                {
                    this.depth = node.level;
                }

                if (node.IsFinished())
                {
                    maxLevel = node.level;
                    solutionMoves = node.getPreviousMoves();
                    isFinished = true;
                    break;
                }

                if (node.level < maxLevel)
                {
                    char lastMove = node.getReversePreviousMove();
                    char[] possibleMoves = node.getPossibleMoves();
                    for (int i = possibleMoves.Length - 1; i >= 0; i--)
                    {
                        char move = possibleMoves[i];
                        if (node.isMoveLegal(move) && move != lastMove)
                        {
                            nodesVisited++;
                            Node child = new Node(node.level + 1, node.CopyBoard(), node.getPreviousMoves(), node.ZeroPosition, move, Node.possibleMoves);
                            ulong boardHash = child.getBoardHash();
                            if (visitedBoards.Add(boardHash))
                            {
                                hashDepth.Add(boardHash, child.level);
                                stack.Push(child);
                            }
                            else
                            {
                                int hashedBoardLevel = hashDepth.GetValueOrDefault(boardHash);
                                if (hashedBoardLevel > child.level)
                                {
                                    hashDepth[boardHash] = child.level;
                                    stack.Push(child);
                                }
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