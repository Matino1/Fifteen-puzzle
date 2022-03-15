// See https://aka.ms/new-console-template for more information
using pietnastka;

int[,] game = new int[2, 2];
int x = 2;
for (int i = 0; i < 2; i++)
{
    for (int j = 0; j < 2; j++)
    {
        game[i, j] = x++;
    }
}

int[,] game2 = new int[2, 2] { { 4, 5 }, { 0, 3 } };

game[0, 0] = 0;


Gameboard gameboard2 = new Gameboard(game2);
Gameboard gameboard3 = new Gameboard(game);

gameboard2.printBoard();
gameboard3.printBoard();
Console.WriteLine(gameboard2.getBoardString());
Console.WriteLine(gameboard3.getBoardString());



//Console.WriteLine(gameboard.IsFinished());
//Console.WriteLine(gameboard.isMoveLegal);
