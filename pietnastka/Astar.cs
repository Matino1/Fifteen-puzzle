using System.Diagnostics;

namespace pietnastka
{
    internal class Astar : SearchingAlgorithm
    {
        public Astar()
        {
        }

        public override bool result(Gameboard rootBoard, string algorithm)
        {
            Stopwatch stopWatch = Stopwatch.StartNew();

            Node rootNode = new Node(0, rootBoard);

            List<Node> list = new List<Node>();
            list.Add(rootNode);

            bool isFinished = false;
            Node node;
            //List<ulong> visitedBoards = new List<ulong>();
            HashSet<ulong> visitedBoards = new HashSet<ulong>();
            while (list.Any())
            {
                node = list[0];
                list.RemoveAt(0);

                nodesProcessed++;

                //if (!visitedBoards.Contains(node.getBoardHash()))
                //{
                if (node.getPreviousMoves().Count == 0)
                {
                    visitedBoards.Add(node.getGameboard().getBoardHash());
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
                List<Node> nodes = node.getChildren();
                foreach (Node child in nodes)
                {
                    if (child.level <= maxLevel)
                        list.Add(child);
                }
                if (algorithm == "hamm")
                {
                    list.Sort((p, q) => p.getGameboard().hammingDistance.CompareTo(q.getGameboard().hammingDistance));
                }
                else if (algorithm == "manh")
                {
                    list.Sort((p, q) => p.getGameboard().manhattanDistance.CompareTo(q.getGameboard().manhattanDistance));
                }

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