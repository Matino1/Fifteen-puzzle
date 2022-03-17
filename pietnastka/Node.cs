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
        private char[] possibleMoves = new char[4] { 'L', 'R', 'U', 'D' };
        private List<char> previousMoves = new List<char>();
        public char[] getPossibleMoves()
        {
            return possibleMoves;
        }

        public List<char> getPreviousMoves()
        {
            return previousMoves;
        }

        public void addPreviousMove(char move)
        {
            previousMoves.Add(move);
        }
        public Node(int level, Gameboard board)
        {
            this.level = level;
            this.board = board;
        }
        public Node(int level, Gameboard board, List<char> previousMoves)
        {
            this.level = level;
            this.board = board;
            foreach (char move in previousMoves)
                this.previousMoves.Add(move);
        }   

        public string getStringBoardCode()
        {
            return board.getBoardString();
        }

        public Gameboard getGameboard()
        {
            return board;
        }

        public List<Node> getChildren()
        {
            return children;
        }

        public void addChild(char move)
        {
            Gameboard tempBoard = new Gameboard(this.board.copyBoard(), move);
            Node tempNode = new Node(this.level + 1, tempBoard, this.previousMoves);
            tempNode.addPreviousMove(move);
            this.children.Add(tempNode);
        }

        public void addChildren(List<char> moves)
        {
            //List<int> legalMoves = new List<int>();
            //for (int i = 0; i < moves.Count; i++)
            //{
            //    if (board.isMoveLegal(moves[i]))
            //    {
            //        legalMoves.Add(i);
            //    }
            //}
            foreach (char x in moves)
            {
                //if (moves.Contains(this.possibleMoves[x]))
                //    addChild(moves[x]);
                foreach (char move in possibleMoves)
                    if (x == move)
                        addChild(move);
            }
        }

        public string getBoardString()
        {
            return board.getBoardString();
        }
    }
}
