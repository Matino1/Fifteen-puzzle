// See https://aka.ms/new-console-template for more information
using pietnastka;
using System.Threading;
using System.Diagnostics;


Node RootNode = new Node(0, args[2]);

/*Node RootNodeBfs = new Node(0, args[2]);
Node RootNodeDfs = new Node(0, args[2]);
Node RootNodeAstr = new Node(0, args[2]);

RootNodeBfs.setAlgorithm(new BFS());
RootNodeDfs.setAlgorithm(new DFS());
RootNodeAstr.setAlgorithm(new Astar());*/

switch (args[0].ToLower())
{
    case "bfs":
        RootNode.setAlgorithm(new BFS());
        break;
    case "dfs":
        RootNode.setAlgorithm(new DFS());
        break;
    case "astr":
        RootNode.setAlgorithm(new Astar());
        break;
}

if (args[0].ToLower() == "bfs" || args[0].ToLower() == "dfs")
{
    RootNode.setMovesOrder(args[1].ToCharArray());
    RootNode.getSolution();
}
else if (args[0].ToLower() == "astr")
{
    RootNode.getSolution(args[1].ToLower());
}

RootNode.saveSolutionToFile(args[3]);
RootNode.saveAdditionalInfoToFile(args[4]);













//Gameboard gameboard = new Gameboard(game);
//gameboard.printBoard();
/*SearchingAlgorithm bfs = new BFS();
SearchingAlgorithm dfs = new DFS();
SearchingAlgorithm astar = new Astar();
SearchingAlgorithm astar2 = new Astar();
Node RootNode = new Node(0, "starting_board.txt");
Stopwatch watch = new Stopwatch();*/

/*RootNode.setAlgorithm(astar);
Console.Write("Solution A* manhattan: ");
Console.WriteLine(RootNode.getSolution("manh"));
Console.WriteLine("Solution depth: " + astar.depth);
Console.WriteLine("Nodes visited: " + astar.nodesVisited);
Console.WriteLine("Nodes processed: " + astar.nodesProcessed);
Console.WriteLine("Result lenght: " + astar.resultLenght);
Console.WriteLine("Time: " + astar.resultTime);
Console.WriteLine();

astar.Reset();
RootNode.setAlgorithm(astar);
Console.Write("Solution A* hamming: ");
Console.WriteLine(RootNode.getSolution("ham"));
Console.WriteLine("Solution depth: " + astar.depth);
Console.WriteLine("Nodes visited: " + astar.nodesVisited);
Console.WriteLine("Nodes processed: " + astar.nodesProcessed);
Console.WriteLine("Result lenght: " + astar.resultLenght);
Console.WriteLine("Time: " + astar.resultTime);
Console.WriteLine();*/

/*
RootNode.setAlgorithm(dfs);
Console.Write("Solution DFS: ");
Console.WriteLine(RootNode.getSolution());
Console.WriteLine("Solution depth: " + dfs.depth);
Console.WriteLine("Nodes visited: " + dfs.nodesVisited);
Console.WriteLine("Nodes processed: " + dfs.nodesProcessed);
Console.WriteLine("Result lenght: " + dfs.resultLenght);
Console.WriteLine("Time: " + dfs.resultTime);
Console.WriteLine();*/

/*RootNode.setAlgorithm(bfs);
Console.Write("Solution BFS: ");
Console.WriteLine(RootNode.getSolution());
Console.WriteLine("Solution depth: " + bfs.depth);
Console.WriteLine("Nodes visited: " + bfs.nodesVisited);
Console.WriteLine("Nodes processed: " + bfs.nodesProcessed);
Console.WriteLine("Result lenght: " + bfs.resultLenght);
Console.WriteLine("Time: " + bfs.resultTime);*/

//Thread.Sleep(10000);

/*Gameboard gameboard2 = new Gameboard(game2);



bfs.result(gameboard2);
Console.WriteLine("Solution depth: " + bfs.depth);
Console.WriteLine("Nodes visited: " + bfs.nodesVisited);
Console.WriteLine("Nodes processed: " + bfs.nodesProcessed);
Console.WriteLine("Result lenght: " + bfs.resultLenght);
Console.WriteLine("Time: " + bfs.resultTime);
Console.Write("Solution: ");
bfs.solutionMoves.ForEach(move => Console.Write(move));
Console.WriteLine();
Console.WriteLine();
Console.WriteLine();*/

//SearchingAlgorithm dfs = new DFS();


/*Thread myThread = new System.Threading.Thread(new
   System.Threading.ThreadStart(bfs.result(gameboard)));
*/


//dfs.result(gameboard);
//Console.WriteLine("Solution depth: " + dfs.depth);
//Console.WriteLine("Nodes visited: " + dfs.nodesVisited);
//Console.WriteLine("Nodes processed: " + dfs.nodesProcessed);
//Console.WriteLine("Result lenght: " + dfs.resultLenght);
//Console.WriteLine("Time: " + dfs.resultTime);
//Console.Write("Solution: ");
//dfs.solutionMoves.ForEach(move => Console.Write(move));
