using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace pietnastka
{
    internal class Gameboard
    {
        private int[,] board = new int[4, 4];

        public Gameboard(int[,] board)
        {
            this.board = board;
        }

        public int[,] GetBoard()
        {
            return board;
        }

        public bool IsFinished()
        {
            int x = 1;
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] == x)
                    {
                        x++;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
