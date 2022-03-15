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
        private Gameboard board;
        private List<Node> children;

        public Node(int level, Gameboard board)
        {
            this.level = level;
            this.board = board;
        }

        public void addChild(Gameboard childBoard)
        {
            children.Add(new Node(++this.level, childBoard));
        }
    }
}
