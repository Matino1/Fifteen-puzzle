namespace pietnastka
{
    internal class Node
    {
        public int level { get; set; }
        private static readonly ulong prime = 7;
        private static readonly ulong prime1 = 31;
        private List<Node> children = new List<Node>();
        public static char[] possibleMoves = new char[4] { 'L', 'R', 'U', 'D' };
        private List<char> previousMoves = new List<char>();
        public int[,] Board { get; set; } = new int[4, 4];
        public int HammingDistance { get; set; }
        public int ManhattanDistance { get; set; }
        public int[] ZeroPosition { get; set; } = new int[2];

        public SearchingAlgorithm SearchingAlgorithm { get; set; }

        public Node(int level, int[,] board)
        {
            this.level = level;
            this.Board = board;
            ZeroPosition = FindZeroPosition(board);
        }

        public Node(int level, string fileName)
        {
            this.level = level;
            readBoardFromFile(fileName);
            ZeroPosition = FindZeroPosition(this.Board);
        }

        public Node(int level, int[,] board, List<char> previousMoves, int[] zeroPosition, char move, char[] possibleMoves)
        {
            this.level = level;
            this.Board = board;
            MoveZero(this.Board, move);
            UpdateZeroPosition(move);
            Node.possibleMoves = possibleMoves;
            this.previousMoves = new List<char>(previousMoves);
            addPreviousMove(move);
        }

        public void setMovesOrder(char[] moves)
        { 
            for (int i = 0; i < moves.Length; i++)
            {
                Node.possibleMoves[i] = Char.ToUpper(moves[i]);
            }
        }

        private void UpdateZeroPosition(char move)
        {
            switch (move)
            {
                case 'L':
                    this.ZeroPosition[0] = this.ZeroPosition[0];
                    this.ZeroPosition[1] = this.ZeroPosition[1] - 1;
                    break;

                case 'R':
                    this.ZeroPosition[0] = this.ZeroPosition[0];
                    this.ZeroPosition[1] = this.ZeroPosition[1] + 1;
                    break;

                case 'U':
                    this.ZeroPosition[0] = this.ZeroPosition[0] - 1;
                    this.ZeroPosition[1] = this.ZeroPosition[1];
                    break;

                case 'D':
                    this.ZeroPosition[0] = this.ZeroPosition[0] + 1;
                    this.ZeroPosition[1] = this.ZeroPosition[1];
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
            int[,] copiedBoard = CopyBoard();
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
            int bSize = Board.Length - 1;
            for (int i = 0; i < Board.GetLength(0); i++)
            {
                for (int j = 0; j < Board.GetLength(1); j++)
                {
                    hash += power(prime1, bSize--) * (ulong)Board[i, j];
                }
            }
            return hash + (ulong)level * prime;
        }

        public bool CompareTo(int[,] anoterBoard)
        {
            if (anoterBoard is null)
            {
                return false;
            }
            else if (Object.ReferenceEquals(this, anoterBoard))
            {
                return true;
            }
            else if (this.GetType() != anoterBoard.GetType())
            {
                return false;
            }
            else if (Board.GetLength(0) != anoterBoard.GetLength(0) || Board.GetLength(1) != anoterBoard.GetLength(1))
            {
                return false;
            }
            else
            {
                for (int i = 0; i < Board.GetLength(0) - 1; i++)
                {
                    for (int j = 0; j < Board.GetLength(1) - 1; j++)
                    {
                        if (Board[i, j] != anoterBoard[i, j])
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
        }

        public bool IsFinished()
        {
            if (Board[Board.GetLength(0) - 1, Board.GetLength(1) - 1] != 0)
            {
                return false;
            }

            int x = 1;
            for (int i = 0; i < Board.GetLength(0); i++)
            {
                for (int j = 0; j < Board.GetLength(1); j++)
                {
                    if (Board[i, j] == x)
                    {
                        x++;
                    }
                    else
                    {
                        if (x == Board.GetLength(0) * Board.GetLength(1))
                        {
                            return true;
                        }
                        return false;
                    }
                }
            }
            return true;
        }

        public void printBoard()
        {
            for (int i = 0; i < this.Board.GetLength(0); i++)
            {
                for (int j = 0; j < this.Board.GetLength(1); j++)
                {
                    Console.Write(this.Board[i, j]);
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
        }

        public bool isMoveLegal(char move)
        {
            int[] zero = FindZeroPosition(this.Board);
            switch (move)
            {
                case 'L':
                    return zero[1] == 0 ? false : true;

                case 'R':
                    return zero[1] == this.Board.GetLength(1) - 1 ? false : true;

                case 'U':
                    return zero[0] == 0 ? false : true;

                case 'D':
                    return zero[0] == this.Board.GetLength(0) - 1 ? false : true;

                default:
                    Console.WriteLine("Wrong move");
                    return false;
            }
        }

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

        private int[] FindPosition(int[,] board, int number)
        {
            return number == 0 ? new int[2] { board.GetLength(0) - 1, board.GetLength(1) - 1 } : new int[2] { (number - 1) / board.GetLength(0), (number - 1) % board.GetLength(0) };
        }

        public void setAlgorithm(SearchingAlgorithm algorithm)
        {
            this.SearchingAlgorithm = algorithm;
        }

        public string getSolution()
        {
            SearchingAlgorithm.result(this.Board);
            return SearchingAlgorithm.getSolutionMoves();
        }

        public string getSolution(string algorithm)
        {
            SearchingAlgorithm.result(this.Board, algorithm);
            return SearchingAlgorithm.getSolutionMoves();
        }

        public void saveSolutionToFile(string fileName)
        {
            StreamWriter file = new StreamWriter(Environment.CurrentDirectory + @"\" + fileName);
            try
            {
                file.WriteLine(SearchingAlgorithm.resultLenght);
                foreach (char move in SearchingAlgorithm.solutionMoves)
                {
                    file.Write(move);
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

        public void readBoardFromFile(string fileName)
        {
            StreamReader file = new StreamReader(fileName);
            String line;
            int j = 0;
            try
            {
                line = file.ReadLine();
                if (line != null)
                    this.Board = new int[Int32.Parse(line.Split(' ')[0]), Int32.Parse(line.Split(' ')[1])];

                line = file.ReadLine();
                while (line != null)
                {
                    int[] row = new int[Board.GetLength(1)];
                    for (int i = 0; i < row.Length; i++)
                    {
                        this.Board[j, i] = Int32.Parse(line.Split(' ')[i]);
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

        public void findManhattanDistance()
        {
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
            this.ManhattanDistance -= Math.Abs(ZeroPosition[0] - Board.GetLength(0)) + Math.Abs(ZeroPosition[1] - Board.GetLength(0));

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
        }

        private int[] FindZeroPosition(int[,] board)
        {
            int[] zeroPosition = new int[2];
            if (!isLegal())
            {
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

        public void MoveZero(int[,] board, char move)
        {
            int[] zero = FindZeroPosition(board);
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

        public int[,] CopyBoard()
        {
            int[,] newboard = new int[this.Board.GetLength(0), this.Board.GetLength(1)];
            for (int i = 0; i < this.Board.GetLength(0); i++)
            {
                for (int j = 0; j < this.Board.GetLength(1); j++)
                {
                    newboard[i, j] = this.Board[i, j];
                }
            }
            return newboard;
        }

        public char getReversePreviousMove()
        {
            if (this.previousMoves.Count > 0)
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

        public void saveAdditionalInfoToFile(string fileName)
        {
            StreamWriter file = new StreamWriter(Environment.CurrentDirectory + @"\" + fileName);
            try
            {
                file.WriteLine(SearchingAlgorithm.resultLenght);
                file.WriteLine(SearchingAlgorithm.nodesVisited);
                file.WriteLine(SearchingAlgorithm.nodesProcessed);
                file.WriteLine(SearchingAlgorithm.depth);
                file.WriteLine(SearchingAlgorithm.resultTime);
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
    }
}