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
        public int nodeProcessed { get; set; }

        public List<char> solutionMoves { get;}

        public SearchingAlgorithm()
        {
            solutionMoves = new();
        }

        public void configureElapsedTime(Stopwatch stopwatch)
        {
            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;

            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:000}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds);

            resultTime = elapsedTime;
        }

        public virtual bool result(Gameboard rootBoard)
        {
            return true;
        }

    }
}
