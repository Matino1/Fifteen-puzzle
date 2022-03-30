using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pietnastka
{
    internal class Node
    {
        public int level { get; set; }
        private static readonly ulong prime = 7;
        private static readonly ulong prime1 = 31;
        //private Gameboard board;
        private List<Node> children = new List<Node>();
        private char[] possibleMoves = new char[4] { 'U', 'D', 'L', 'R' };
        private List<char> previousMoves = new List<char>();
        public int[,] Board { get; set; } = new int[4, 4];
        public int HammingDistance { get; set; }
        public int ManhattanDistance { get; set; }
        public int[] ZeroPosition { get; set; } = new int[2];
        //private int[,] board = new int[4, 4];

        public Node(int level, int[,] board)
        {
            this.level = level;
            this.Board = board;
            ZeroPosition = findZeroPosition(board);
        }
        public Node(int level, int[,] board, List<char> previousMoves, int[] zeroPosition)
        {
            this.level = level;
            this.Board = board;
            MoveZero(this.Board, previousMoves[previousMoves.Count - 1]);
            this.previousMoves = new List<char>(previousMoves);
            switch(previousMoves[previousMoves.Count - 1])
            {
                case 'L':
                    this.ZeroPosition[0] = zeroPosition[0];
                    this.ZeroPosition[1] = zeroPosition[1] - 1;
                    break;

                case 'R':
                    this.ZeroPosition[0] = zeroPosition[0];
                    this.ZeroPosition[1] = zeroPosition[1] + 1;
                    break;

                case 'U':
                    this.ZeroPosition[0] = zeroPosition[0] - 1;
                    this.ZeroPosition[1] = zeroPosition[1];
                    break;

                case 'D':
                    this.ZeroPosition[0] = zeroPosition[0] + 1;
                    this.ZeroPosition[1] = zeroPosition[1];
                    break;

                default:
                    Console.WriteLine("Wrong zero position");
                    break;
            }
        }

        public char[] getPossibleMoves()
        {
            return possibleMoves;
        }

        public int[,] NextMoveBoard(char move)
        {
            int[,] copiedBoard = CopyBoard(this.Board);
            MoveZero(copiedBoard, move);

            return copiedBoard;
        }

        public List<char> getPreviousMoves()
        {
            return previousMoves;
        }

        public void addPreviousMove(char move)
        {
            previousMoves.Add(move);
        }
        private ulong power(ulong x, int y)
        {
            ulong result = 1;
            for (int i = 0; i < y; i++)
            {
                result *= x;
            }
            return result;
        }
        public ulong getBoardHash()
        {
            ulong hash = 0;
            int bSize = Board.Length;
            for (int i = 0; i < Board.GetLength(0); i++)
            {
                for (int j = 0; j < Board.GetLength(1); j++)
                {
                    hash += power(prime1, --bSize) * (ulong)Board[i, j];
                }
            }
            return hash + (ulong)level * prime;
        }

        //public int [,] getNextMoveBoard(char move)
        //{
        //    int[,] copiedBoard = copyBoard();
        //    Gameboard newGame = new Gameboard(copiedBoard);
        //    if (newGame.isMoveLegal(move))
        //    {
        //        newGame.moveZero(move);
        //    }

        //    return newGame;
        //}
        public void findHammingDistance()
        {
            this.HammingDistance = 0;
            int x = 1;
            for (int i = 0; i < Board.GetLength(0); i++)
            {
                for (int j = 0; j < Board.GetLength(1); j++)
                {
                    if (Board[i, j] != x++)
                    {
                        this.HammingDistance += 1;
                    }
                }
            }
            if (Board[Board.GetLength(0) - 1, Board.GetLength(1) - 1] == 0)
            {
                this.HammingDistance--;
            }
            this.HammingDistance += this.level;
        }
        private int[] FindPosition(int [,] board, int number)
        {
            return number == 0 ? new int[2] { board.GetLength(0) - 1, board.GetLength(1) - 1 } : new int[2] { (number - 1) / board.GetLength(0), (number - 1) % board.GetLength(0) };
        }

        public void findManhattanDistance()
        {
            //this.manhattanDistance = 0;
            int[] position = new int[2];
            int x = 1;
            for (int i = 0; i < Board.GetLength(0); i++)
            {
                for (int j = 0; j < Board.GetLength(1); j++)
                {
                    position = FindPosition(this.Board, this.Board[i, j]);
                    this.ManhattanDistance += Math.Abs(i - position[0]) + Math.Abs(j - position[1]);
                }
            }
            this.ManhattanDistance += this.level;
        }
        public bool isLegal()
        {
            List<int> numbers = new List<int>();
            int zeros = 0;

            for (int i = 0; i < Board.GetLength(0); i++)
            {
                for (int j = 0; j < Board.GetLength(1); j++)
                {
                    numbers.Add(this.Board[i, j]);
                    if (this.Board[i, j] == 0)
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
        private int[] findZeroPosition(int[,] board)
        {
            int[] zeroPosition = new int[2];
            if (!isLegal())
            {
                //Console.WriteLine("Board is not legal");
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
        public void MoveZero(int [,] board, char move)
        {
            int[] zero = findZeroPosition(board);
            int x = zero[0];
            int y = zero[1];
            int temp = 0;
            switch (move)
            {
                case 'L':
                    temp = board[x, y];
                    board[x, y] = board[x, y - 1];
                    board[x, y - 1] = temp;
                    break;

                case 'R':
                    temp = board[x, y];
                    board[x, y] = board[x, y + 1];
                    board[x, y + 1] = temp;
                    break;

                case 'U':
                    temp = board[x, y];
                    board[x, y] = board[x - 1, y];
                    board[x - 1, y] = temp;
                    break;

                case 'D':
                    temp = board[x, y];
                    board[x, y] = board[x + 1, y];
                    board[x + 1, y] = temp;
                    break;

                default:
                    Console.WriteLine("Wrong move");
                    break;
            }
        }

        public int[,] CopyBoard(int[,] board)
        {
            int[,] newboard = new int[board.GetLength(0), board.GetLength(1)];
            for (int i = 0; i < this.Board.GetLength(0); i++)
            {
                for (int j = 0; j < this.Board.GetLength(1); j++)
                {
                    newboard[i, j] = board[i, j];
                }
            }
            return newboard;
        }

        public void AddExistingChild(Node child)
        {
            children.Add(child);
        }

        public List<Node> getChildren()
        {
            return children;
        }

        public char getReversePreviousMove()
        {
            
            if(this.previousMoves.Count > 0)
            {
                switch (this.previousMoves[previousMoves.Count - 1])
                {
                    case 'L':
                        return 'R';
                        break;

                    case 'R':
                        return 'L';
                        break;

                    case 'U':
                        return 'D';
                        break;

                    case 'D':
                        return 'U';
                        break;
                }
            }
            return 'N';
        }
    }
}
