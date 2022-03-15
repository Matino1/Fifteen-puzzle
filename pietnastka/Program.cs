// See https://aka.ms/new-console-template for more information
using pietnastka;

int[,] game = new int[2, 2];
int x = 1;
for (int i = 0; i < 2; i++)
{
    for (int j = 0; j < 2; j++)
    {
        game[i, j] = x;
        x++;
    }
}
game[1,0] = 0;
Gameboard gameboard = new Gameboard(game);
Console.WriteLine(gameboard.isMoveLegal('U'));
gameboard.printBoard();
gameboard.moveZero('U');
gameboard.printBoard();
Gameboard newGameboard = new Gameboard(game, 'D');
newGameboard.printBoard();


//Console.WriteLine(gameboard.IsFinished());
//Console.WriteLine(gameboard.isMoveLegal);
