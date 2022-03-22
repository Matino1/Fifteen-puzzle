// See https://aka.ms/new-console-template for more information
using pietnastka;
using System.Threading;

int[,] game = new int[4, 4]{ {2, 3, 4, 8},
                             {1, 10, 6, 7},
                             {5, 11, 12, 0 },
                             {9, 13, 14, 15 } };

int[,] game2 = new int[4, 4]{ {1, 2, 3, 4},
                              {5, 6, 7, 8},
                              {9, 0, 11, 12},
                              {13, 10, 14, 15 } };

int[,] game3 = new int[4, 4]{ {0, 1, 2, 7},
                              {8, 9, 12, 10},
                              {13, 3, 6, 4},
                              {15, 14, 11, 5 } };

Gameboard gameboard = new Gameboard("starting_board");
gameboard.printBoard();
SearchingAlgorithm bfs = new BFS();
gameboard.setAlgorithm(bfs);
Console.WriteLine(gameboard.getSolution());
Console.WriteLine("Solution depth: " + bfs.depth);
Console.WriteLine("Nodes visited: " + bfs.nodesVisited);
Console.WriteLine("Nodes processed: " + bfs.nodesProcessed);
Console.WriteLine("Result lenght: " + bfs.resultLenght);
Console.WriteLine("Time: " + bfs.resultTime);
Console.Write("Solution: ");
gameboard.saveSolutionToFile("solution");
//Gameboard gameboard = new Gameboard(game2);



//bfs.result(gameboard);
//Console.WriteLine("Solution depth: " + bfs.depth);
//Console.WriteLine("Nodes visited: " + bfs.nodesVisited);
//Console.WriteLine("Nodes processed: " + bfs.nodesProcessed);
//Console.WriteLine("Result lenght: " + bfs.resultLenght);
//Console.WriteLine("Time: " + bfs.resultTime);
//Console.Write("Solution: ");
//bfs.solutionMoves.ForEach(move => Console.Write(move));
//Console.WriteLine();
//Console.WriteLine();
//Console.WriteLine();

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
