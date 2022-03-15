// See https://aka.ms/new-console-template for more information
using pietnastka;

int[,] game = new int[2, 2];
int x = 1;
for (int i = 0; i < 2; i++)
{
    for (int j = 0; j < 2; j++)
    {
        game[i, j] = x*2;
        x++;
    }
}

Gameboard gameboard = new Gameboard(game);
Console.WriteLine(gameboard.IsFinished());
Console.WriteLine();
Console.WriteLine();
