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

            Node rootNode = new Node(1, rootBoard);
            TreeNode rootTreeNode = new TreeNode();
            rootTreeNode.gameNode = rootNode;

            BinaryTree tree = new BinaryTree();
            tree.Add(rootTreeNode);

            bool isFinished = false;
            Node node;
            HashSet<ulong> visitedBoards = new HashSet<ulong>();
            while (tree.root is not null)
            {
                node = tree.Remove().gameNode;
                nodesProcessed++;

                //if (!visitedBoards.Contains(node.getBoardHash()))
                //{
                if (node.getPreviousMoves().Count == 0)
                {
                    visitedBoards.Add(node.getBoardHash());
                }
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
                        if (visitedBoards.Add(node.getGameboard().nextMove(move)))
                        {
                            moves.Add(move);
                            nodesVisited++;
                        }
                    }
                }
                if (node.level < maxLevel)
                    node.addChildren(moves);
                //Console.WriteLine(nodesVisited);
                //Console.WriteLine(tree.root is null);
                List<Node> nodes = node.getChildren();
                foreach (Node child in nodes)
                {
                    if (child.level <= maxLevel)
                    {
                        /*if (algorithm == "manh")
                        {
                            list.Add(rootNode.getGameboard().manhattanDistance, rootNode);
                        }
                        else if (algorithm == "hamm")
                        {
                            list.Add(rootNode.getGameboard().hammingDistance, rootNode);
                        }*/
                        //list.Add(child);
                        TreeNode childTreeNode = new TreeNode();
                        childTreeNode.gameNode = child;
                        tree.Add(childTreeNode);
                    }
                        //list.Add(child);
                }
                /*if (algorithm == "hamm")
                {
                    list.Sort((p, q) => p.getGameboard().hammingDistance.CompareTo(q.getGameboard().hammingDistance));
                }
                else if (algorithm == "manh")
                {
                    list.Sort((p, q) => p.getGameboard().manhattanDistance.CompareTo(q.getGameboard().manhattanDistance));
                }*/

                //nodesProcessed = visitedBoards.Count - 1;
                
                
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