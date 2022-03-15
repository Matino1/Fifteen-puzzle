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
game[0, 0] = 0;
Gameboard gameboard = new Gameboard(game);
gameboard.printBoard();

Node node = new Node(0, gameboard);
node.addChildren();
node.getChildren()[0].getGameboard().printBoard();
node.getChildren()[1].getGameboard().printBoard();

Thread.Sleep(100000);

//Console.WriteLine(gameboard.IsFinished());
//Console.WriteLine(gameboard.isMoveLegal);
