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
        private readonly int prime1 = 11;
        private readonly int prime2 = 3;
        private readonly int prime3 = 7;
        private SearchingAlgorithm searchingAlgorithm;
        public int hammingDistance { get; set; }
        public int manhattanDistance { get; set; }

        private int[,] board = new int[4, 4];


        public Gameboard(string path)
        {
            readBoardFromFile(path);
            this.hammingDistance = 0;
            this.manhattanDistance = 0;
            findHammingDistance();
            findManhattanDistance();
        }

        public void setAlgorithm(SearchingAlgorithm algorithm)
        {
            this.searchingAlgorithm = algorithm;
        }

        public string getSolution()
        {
            searchingAlgorithm.result(this);
            return searchingAlgorithm.getSolutionMoves();
        }
        public string getSolution(string algorithm)
        {
            searchingAlgorithm.result(this, algorithm);
            return searchingAlgorithm.getSolutionMoves();
        }

        private int[] findPosition(int number)
        {
            int[,] finishedBoard = new int[this.board.GetLength(0), this.board.GetLength(1)];
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    finishedBoard[i, j] = i * board.GetLength(0) + j + 1;
                    if (i == board.GetLength(0) - 1 && j == board.GetLength(1) - 1)
                    {
                        finishedBoard[i, j] = 0;
                    }
                    if (finishedBoard[i, j] == number)
                    {
                        return new int[2] { i, j };
                    }
                }
            }
            return null;
        }

        private void findHammingDistance()
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j =  0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] != i * 4 + j + 1)
                    {
                        this.hammingDistance += 1;
                    }
                }
            }
        }

        private void findManhattanDistance()
        {
            int [] position = new int[2];
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    position = findPosition(this.board[i, j]);
                    this.manhattanDistance += Math.Abs(i - position[0]) + Math.Abs(j - position[1]);
                }
            }
        }

        public Gameboard(int[,] board)
        {
            this.board = board;
            findHammingDistance();
            findManhattanDistance();
        }

        public ulong nextMove(char move)
        {
            int [,] copiedBoard = copyBoard();
            Gameboard newGame = new Gameboard(copiedBoard);
            if (newGame.isMoveLegal(move))
                newGame.moveZero(move);

            return newGame.getBoardHash();
        }

        public void readBoardFromFile(string fileName)
        {
            StreamReader file = new StreamReader(Environment.CurrentDirectory + @"\" + fileName);
            String line;
            int j = 0;
            try
            {
                line = file.ReadLine();
                if (line != null)
                    this.board = new int[Int32.Parse(line.Split(' ')[0]), Int32.Parse(line.Split(' ')[1])];
                
                line = file.ReadLine();
                while (line != null)
                {
                    int [] row = new int[board.GetLength(1)];
                    for (int i = 0; i < row.Length; i++)
                    {
                        this.board[j, i] = Int32.Parse(line.Split(' ')[i]);
                    }
                    j++;
                    line = file.ReadLine();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                if (file != null)
                {
                    file.Close();
                }
            }
        }
        public void saveSolutionToFile(string fileName)
        {
            StreamWriter file = new StreamWriter(Environment.CurrentDirectory + @"\" + fileName);
            try
            {
                if (getSolution() == "")
                {
                    file.Write("-1");
                }
                else
                {
                    file.WriteLine(getSolution().Length);
                    file.Write(getSolution());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                if (file != null)
                {
                    file.Close();
                }
            }
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
            findHammingDistance();
            findManhattanDistance();
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
                    if (i == board.GetLength(0) - 1 && j == board.GetLength(1) - 1)
                    {
                        return board[i, j] == 0 ? true : false;
                    } 
                    else
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
            }
            return true;
        }
        public ulong getBoardHash()
        {
            ulong hash = (ulong) prime1;
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    hash = hash * (ulong) prime2 + (ulong) board[i, j];
                }
            }
            return hash;
        }
    }
}
