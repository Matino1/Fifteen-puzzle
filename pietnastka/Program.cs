// See https://aka.ms/new-console-template for more information
using pietnastka;

int[,] game = new int[4, 4]{ {2,0, 3, 4},
                             {1, 6, 7, 8},
                             {5, 10, 11, 12},
                             {9, 13, 14, 15 } };
/*int x = 1;
for (int i = 0; i < 2; i++)
{
    for (int j = 0; j < 2; j++)
    {
        game[i, j] = x*2;
        x++;
    }
}*/

BFS bfs = new BFS();

Gameboard gameboard = new Gameboard(game);
Console.WriteLine(gameboard.IsFinished());
Console.WriteLine(bfs.result(gameboard));
Console.WriteLine();
