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

        private bool isLegal()
        {
            List<int> numbers = new List<int>();
            int zeros = 0;

            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    numbers.Add(this.board[i, j]);
                    if (this.board[i, j] == 0)
                    {
                        zeros++;
                    }
                }
            }
            numbers.Sort();

            if (zeros == 1)
                numbers.RemoveAt(0);
            else
                return false;

            for (int i = 1; i < numbers.Count - 1; i++)
            {
                if (numbers[i] != numbers[i - 1] + 1)
                {
                    return false;
                }
            }
            return true;
            //return numbers.Distinct().Count() == numbers.Count() ? true : false;
        }

        private int [] findZeroPosition(int [,] board)
        {
            int[] zeroPosition = new int[2];
            if (!isLegal())
            {
                Console.WriteLine("Board is not legal");
                return zeroPosition;
            }
            
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] == 0)
                    {
                        zeroPosition[0] = i;
                        zeroPosition[1] = j;
                        return zeroPosition;
                    }
                }
            }
            return zeroPosition;
        }

        public int [,] copyBoard()
        {
            int[,] copiedBoard = new int[this.board.GetLength(0), this.board.GetLength(1)];
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    copiedBoard[i, j] = this.board[i, j];
                }
            }
            return copiedBoard;
        }

        public bool isMoveLegal(char move)
        {
            int[] zero = findZeroPosition(this.board);
            switch (move)
            {
                case 'L':
                    return zero[1] == 0 ? false : true;

                case 'R':
                    return zero[1] == board.GetLength(1) - 1 ? false : true;

                case 'U':
                    return zero[0] == 0 ? false : true;

                case 'D':
                    return zero[0] == board.GetLength(0) - 1 ? false : true;

                default:
                    Console.WriteLine("Wrong move");
                    return false;

            }
        }

        public void printBoard()
        {
            for (int i = 0; i < this.board.GetLength(0); i++)
            {
                for (int j = 0; j < this.board.GetLength(1); j++)
                {
                    Console.Write(this.board[i, j]);
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
        }
        public void moveZero(char move)
        {
            int[] zero = findZeroPosition(this.board);
            int x = zero[0];
            int y = zero[1];
            int temp = 0;
            switch (move)
            {
                case 'L':
                    temp = this.board[x, y]; 
                    this.board[x, y] = this.board[x, y - 1];
                    this.board[x, y - 1] = temp;
                    break;

                case 'R':
                    temp = this.board[x, y];
                    this.board[x, y] = this.board[x, y + 1];
                    this.board[x, y + 1] = temp;
                    break;

                case 'U':
                    temp = this.board[x, y];
                    this.board[x, y] = this.board[x - 1, y];
                    this.board[x - 1, y] = temp;
                    break;

                case 'D':
                    temp = this.board[x, y];
                    this.board[x, y] = this.board[x + 1, y];
                    this.board[x + 1, y] = temp;
                    break;

                default:
                    Console.WriteLine("Wrong move");
                    break;
            }
        }

        public Gameboard(int[,] board, char move)
        {
            this.board = board;
            moveZero(move);
        }

        public int[,] GetBoard()
        {
            return this.board;
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
        public string getBoardString()
        {
            string boardString = "";
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    boardString += this.board[i, j].ToString();
                }
            }
            return boardString;
        }
    }
}
