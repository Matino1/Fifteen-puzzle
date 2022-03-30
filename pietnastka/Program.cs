// See https://aka.ms/new-console-template for more information
using pietnastka;
using System.Threading;
using System.Diagnostics;

int[,] game = new int[4, 4]{ {2, 3, 4, 0},
                             {6, 7, 12, 8},
                             {1, 5, 10, 11 },
                             {9, 13, 14, 15 } };

int[,] game2 = new int[4, 4]{ {1, 2, 0, 3},
                              {5, 7, 8, 4},
                              {10, 6, 12, 15},
                              {9, 11, 13, 14 } };

int[,] game3 = new int[4, 4]{ {0, 1, 2, 7},
                              {8, 9, 12, 10},
                              {13, 3, 6, 4},
                              {15, 14, 11, 5 } };

int[,] game4 = new int[4, 4]{ { 1, 6, 11, 8 },
                              { 7, 12, 13, 3 },
                              { 2, 4, 0, 10 },
                              { 9, 5, 14, 15 } };

Gameboard gameboard = new Gameboard(game4);
//gameboard.printBoard();
SearchingAlgorithm bfs = new BFS();
SearchingAlgorithm dfs = new DFS();
SearchingAlgorithm astar = new Astar();

/*for (int i = 0; i < 412; i++)
{
    string fileName = @"C:\Users\Kurogami\Desktop\test\file_" + (i + 1).ToString();
    gameboard = new Gameboard(fileName);
    gameboard.setAlgorithm(bfs);
    if (!gameboard.isLegal())
    {
        Console.Write(fileName);
    }
    Console.Write("Solution: ");
    Console.WriteLine(gameboard.getSolution());
    Console.WriteLine("Solution depth: " + bfs.depth);
    Console.WriteLine("Nodes visited: " + bfs.nodesVisited);
    Console.WriteLine("Nodes processed: " + bfs.nodesProcessed);
    Console.WriteLine("Result lenght: " + bfs.resultLenght);
    Console.WriteLine("Time: " + bfs.resultTime);
    bfs.Reset();
}*/
gameboard.setAlgorithm(astar);
Console.Write("Solution A* manhattan: ");
Console.WriteLine(gameboard.getSolution("manh"));
Console.WriteLine("Solution depth: " + astar.depth);
Console.WriteLine("Nodes visited: " + astar.nodesVisited);
Console.WriteLine("Nodes processed: " + astar.nodesProcessed);
Console.WriteLine("Result lenght: " + astar.resultLenght);
Console.WriteLine("Time: " + astar.resultTime);

astar.Reset();
gameboard.setAlgorithm(astar);
Console.Write("Solution A* hamming: ");
Console.WriteLine(gameboard.getSolution("ham"));
Console.WriteLine("Solution depth: " + astar.depth);
Console.WriteLine("Nodes visited: " + astar.nodesVisited);
Console.WriteLine("Nodes processed: " + astar.nodesProcessed);
Console.WriteLine("Result lenght: " + astar.resultLenght);
Console.WriteLine("Time: " + astar.resultTime);


gameboard.setAlgorithm(dfs);
Console.Write("Solution DFS: ");
Console.WriteLine(gameboard.getSolution());
Console.WriteLine("Solution depth: " + dfs.depth);
Console.WriteLine("Nodes visited: " + dfs.nodesVisited);
Console.WriteLine("Nodes processed: " + dfs.nodesProcessed);
Console.WriteLine("Result lenght: " + dfs.resultLenght);
Console.WriteLine("Time: " + dfs.resultTime);

gameboard.setAlgorithm(bfs);
Console.Write("Solution BFS: ");
Console.WriteLine(gameboard.getSolution());
Console.WriteLine("Solution depth: " + bfs.depth);
Console.WriteLine("Nodes visited: " + bfs.nodesVisited);
Console.WriteLine("Nodes processed: " + bfs.nodesProcessed);
Console.WriteLine("Result lenght: " + bfs.resultLenght);
Console.WriteLine("Time: " + bfs.resultTime);

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
