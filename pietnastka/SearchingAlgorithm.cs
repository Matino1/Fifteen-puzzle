using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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



        public List<char> solutionMoves { get; set; }

        public SearchingAlgorithm()
        {
            solutionMoves = new();
            maxLevel = 20;
        }

        public void saveElapsedTime(Stopwatch stopwatch)
        {
            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;

            string elapsedTime = String.Format("{0:00}.{1:000}s",
            ts.Seconds + (ts.Minutes * 60), ts.Milliseconds);

            resultTime = elapsedTime;
        }

        public virtual bool result(Gameboard rootBoard)
        {
            return true;
        }

    }
}
