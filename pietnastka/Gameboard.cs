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
        public static int instances = 0;
        private readonly int prime2 = 3;
        private readonly int prime3 = 7;
        private SearchingAlgorithm searchingAlgorithm;

        private int[,] board = new int[4, 4];


        public Gameboard(string path)
        {
            readBoardFromFile(path);
            this.hammingDistance = 0;
            this.manhattanDistance = 0;
            instances++;
            //findHammingDistance();
            //findManhattanDistance();
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
        public Gameboard(int[,] board)
        {
            this.board = board;
            this.hammingDistance = 0;
            this.manhattanDistance = 0;
            instances++;
            //findHammingDistance();
            //findManhattanDistance();
        }

        public void readBoardFromFile(string fileName)
        {
            //StreamReader file = new StreamReader(Environment.CurrentDirectory + @"\" + fileName);
            StreamReader file = new StreamReader(fileName);
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
        

        public Gameboard(int[,] board, char move)
        {
            this.board = CopyExistingBoard(board);
            moveZero(move);
            instances++;
            this.hammingDistance = 0;
            this.manhattanDistance = 0;
            //findHammingDistance();
            //findManhattanDistance();
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
                        if (i == board.GetLength(0) - 1 && j == board.GetLength(1) - 1)
                        {
                            return board[i, j] == 0 ? true : false;
                        }
                        return false;
                    }
                }
            }
            return true;
        }

        
        

        public bool CompareTo(Gameboard anoterBoard)
        {
            if (anoterBoard is null)
            {
                return false;
            }

            // Optimization for a common success case.
            else if (Object.ReferenceEquals(this, anoterBoard))
            {
                return true;
            }

            // If run-time types are not exactly the same, return false.
            else if (this.GetType() != anoterBoard.GetType())
            {
                return false;
            }

            else if (board.GetLength(0) != anoterBoard.board.GetLength(0) || board.GetLength(1) != anoterBoard.board.GetLength(1))
            {
                return false;
            }

            else
            {
                for (int i = 0; i < board.GetLength(0) - 1; i++)
                {
                    for (int j = 0; j < board.GetLength(1) - 1; j++)
                    {
                        if (board[i, j] != anoterBoard.board[i, j])
                        {
                            return false;
                        }
                    }
                }
                return true;
            }

        }
    }
}
