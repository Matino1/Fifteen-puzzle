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
        private List<Node> children = new List<Node>();
        private char[] moves = new char[4] { 'L', 'R', 'U', 'D' };

        public Node(int level, Gameboard board)
        {
            this.level = level;
            this.board = board;
        }

        public string getStringBoardCode()
        {
            return board.getBoardString();
        }

        public Gameboard getBoard()
        {
            return board;
        }

        public List<Node> getChildren()
        {
            return children;
        }

        public Gameboard getGameboard()
        {
            return board;
        }
        public void addChild(char move)
        {
            Gameboard tempBoard = new Gameboard(this.board.copyBoard(), move);
            Node tempNode = new Node(this.level + 1, tempBoard);
            this.children.Add(tempNode);
        }

        public void addChildren()
        {
            List<int> legalMoves = new List<int>();
            for (int i = 0; i < moves.GetLength(0); i++)
            {
                if (board.isMoveLegal(moves[i]))
                {
                    legalMoves.Add(i);
                }
            }
            foreach (int x in legalMoves)
            {
                addChild(moves[x]);
            }
        }

        public string getBoardString()
        {
            return board.getBoardString();
        }
    }
}
