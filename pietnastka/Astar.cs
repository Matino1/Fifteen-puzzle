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

            HashSet<Node> list = new HashSet<Node>();
            list.Add(rootNode);

            bool isFinished = false;
            Node node;
            HashSet<ulong> visitedBoards = new HashSet<ulong>();

            visitedBoards.Add(rootNode.getBoardHash());

            while (list.Any()) //tree.root is not null)
            {
                //node = tree.Remove().gameNode;

                node = list.MinBy(x => x.getGameboard().manhattanDistance);
                list.Remove(node);

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
                        //TreeNode childTreeNode = new TreeNode();
                        //childTreeNode.gameNode = child;
                        //tree.Add(childTreeNode);
                        list.Add(child);
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