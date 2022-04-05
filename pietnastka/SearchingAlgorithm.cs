using System.Diagnostics;

namespace pietnastka
{
    internal class SearchingAlgorithm
    {
        public int maxLevel { get; set; }
        public int resultLenght { get; set; }
        public int nodesVisited { get; set; }
        public int depth { get; set; }
        public string resultTime { get; set; }
        public int nodesProcessed { get; set; }
        public char prevMove { get; set; }

        private readonly int MAX_LEVEL = 20;

        public List<char> solutionMoves { get; set; }

        public void Reset()
        {
            maxLevel = MAX_LEVEL;
            resultLenght = 0;
            nodesVisited = 0;
            depth = 0;
            nodesProcessed = 0;
            prevMove = '\0';
            resultTime = "";
        }

        public SearchingAlgorithm()
        {
            solutionMoves = new();
            maxLevel = MAX_LEVEL;
        }

        public void saveElapsedTime(Stopwatch stopwatch)
        {
            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;

            double msRounded = Math.Round(ts.TotalMilliseconds, 3);

            string elapsedTime = msRounded.ToString();

            resultTime = elapsedTime;
        }

        public virtual bool result(int[,] rootBoard)
        {
            return true;
        }

        public string getSolutionMoves()
        {
            string solutionMoves = "";
            foreach (char move in this.solutionMoves)
            {
                solutionMoves += move;
            }
            return solutionMoves;
        }

        public virtual bool result(int[,] rootBoard, string algorithm)
        {
            return false;
        }
    }
}