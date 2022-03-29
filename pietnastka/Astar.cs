using System.Diagnostics;
using System.Collections;

namespace pietnastka
{
    internal class Astar : SearchingAlgorithm
    {
        public Astar() : base()
        {
        }

        public override bool result(Gameboard rootBoard, string algorithm)
        {
            Stopwatch stopWatch = Stopwatch.StartNew();

            Node rootNode = new Node(0, rootBoard);
            //TreeNode rootTreeNode = new TreeNode();
            //rootTreeNode.gameNode = rootNode;

            //BinaryTree tree = new BinaryTree();
            //tree.Add(rootTreeNode);

            PriorityQueue<Node, int> list = new PriorityQueue<Node, int>();
            list.Enqueue(rootNode, rootNode.getGameboard().manhattanDistance);

            bool isFinished = false;
            Node node;
            HashSet<ulong> visitedBoards = new HashSet<ulong>();

            visitedBoards.Add(rootNode.getBoardHash());

            while (list.Count != 0) //tree.root is not null)
            {
                //node = tree.Remove().gameNode;

                node = list.Dequeue();

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
                            }
                        }
                    }
                    
                    node.addChildren(moves);

                    List<Node> nodes = node.getChildren();
                    foreach (Node child in nodes)
                    {    
                        list.Enqueue(child, child.getGameboard().manhattanDistance);
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