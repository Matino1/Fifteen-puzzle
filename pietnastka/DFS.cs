﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace pietnastka
{
    internal class DFS : SearchingAlgorithm
    {
        public DFS(): base()
        {

        }

        public override bool result(Gameboard rootBoard)
        {
            Stopwatch stopWatch = Stopwatch.StartNew();

            Node rootNode = new Node(0, rootBoard);

            Stack<Node> stack = new Stack<Node>();
            stack.Push(rootNode);

            bool isFinished = false;
            Node node;
            List<long> visitedBoards = new List<long>();
            int k = 0;
            while (stack.Any())
            {
                node = stack.Pop();

                nodesVisited++;

                if (!visitedBoards.Contains(node.getBoardHash()))
                {
                    visitedBoards.Add(node.getBoardHash());
                    if (node.getGameboard().IsFinished())
                    {
                        depth = node.level;
                        maxLevel = node.level;
                        solutionMoves = node.getPreviousMoves();
                        //Console.WriteLine("Game already finished");
                        /*if (node.getPreviousMoves().Count == 0)
                            Console.WriteLine("Game already finished");*/
                        isFinished = true;
                    }

                    List<char> moves = new List<char>();
                    foreach (char move in node.getPossibleMoves())
                    {
                        if (node.getGameboard().isMoveLegal(move))
                            if (!visitedBoards.Contains(node.getGameboard().nextMove(move)))
                                moves.Add(move);
                    }

                    if (node.level < maxLevel)
                    {
                        node.addChildren(moves);
                       /* foreach (Node child in node.getChildren())
                        {
                            stack.Push(child);
                        }*/
                    }
                }
                //Console.WriteLine("Stack:" + stack.Count);
                //Console.WriteLine("Level:" + node.level);

                List<Node> nodes = node.getChildren();
                nodes.Reverse();

                foreach (Node child in nodes)
                {
                    if (child.level <= maxLevel)
                    {
                        stack.Push(child);
                    }
                }
            }

            nodesProcessed = visitedBoards.Count - 1;
            resultLenght = solutionMoves.Count;
            saveElapsedTime(stopWatch);

            if (isFinished)
            {
                return true;
            }
            else
            {
                resultLenght = -1;
                return false;
            }
        }
    }
}
