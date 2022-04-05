﻿using System.Diagnostics;
using System.Collections;

namespace pietnastka
{
    internal class Astar : SearchingAlgorithm
    {
        public Astar() : base()
        {
        }

        public override bool result(int[,] rootBoard, string algorithm)
        {
            Stopwatch stopWatch = Stopwatch.StartNew();

            Node rootNode = new Node(0, rootBoard);

            PriorityQueue<Node, int> priorityQueue = new PriorityQueue<Node, int>();

            bool isFinished = false;
            Node node = rootNode;
            HashSet<ulong> visitedBoards = new HashSet<ulong>();

            visitedBoards.Add(rootNode.getBoardHash());

            do
            {
                nodesProcessed++;

                if (node.level > this.depth)
                {
                    this.depth = node.level;
                }

                if (node.IsFinished())
                {
                    solutionMoves = node.getPreviousMoves();
                    isFinished = true;
                    break;
                }

                char lastMove = node.getReversePreviousMove();
                char[] possibleMoves = node.getPossibleMoves();
                for (int i = possibleMoves.Length - 1; i >= 0; i--)
                {
                    char move = possibleMoves[i];
                    if (node.isMoveLegal(move) && move != lastMove)
                    {
                        nodesVisited++;
                        Node child = new Node(node.level + 1, node.CopyBoard(), node.getPreviousMoves(), node.ZeroPosition, move);
                        if (algorithm == "manh")
                        {
                            child.findManhattanDistance();
                            priorityQueue.Enqueue(child, child.ManhattanDistance);
                        }
                        else
                        {
                            child.findHammingDistance();
                            priorityQueue.Enqueue(child, child.HammingDistance);
                        }
                    }
                }
                node = priorityQueue.Dequeue();

            } while (priorityQueue.Count != 0);


            /*while (priorityQueue.Count != 0)
            {
                node = priorityQueue.Dequeue();

                nodesProcessed++;

                if (node.IsFinished())
                {
                    depth = node.level;
                    maxLevel = node.level;
                    solutionMoves = node.getPreviousMoves();
                    isFinished = true;
                    break;
                }

                //if (node.level < maxLevel)
                //{
                    char lastMove = node.getReversePreviousMove();
                    char[] possibleMoves = node.getPossibleMoves();
                    for (int i = possibleMoves.Length - 1; i >= 0; i--)
                    {
                        char move = possibleMoves[i];
                        if (node.isMoveLegal(move) && move != lastMove)
                        {
                            nodesVisited++;
                            Node child = new Node(node.level + 1, node.CopyBoard(), node.getPreviousMoves(), node.ZeroPosition, move);
                            //if (visitedBoards.Add(child.getBoardHash()))
                            //{
                                if (algorithm == "manh")
                                {
                                    child.findManhattanDistance();
                                    priorityQueue.Enqueue(child, child.ManhattanDistance);
                                }
                                else
                                {
                                    child.findHammingDistance();
                                    priorityQueue.Enqueue(child, child.HammingDistance);
                                }
                            //}
                            //else
                            //{
                                //TODO
                           // }
                        }
                    }
                //}
            }*/
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