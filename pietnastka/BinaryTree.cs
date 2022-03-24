using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pietnastka
{
    internal class BinaryTree
    {
        public TreeNode root { get; set; }

        public bool Add(TreeNode node)
        {
            if (root is null)
            {
                root = node;
                return true;
            }

            TreeNode parent = null;
            TreeNode current = this.root;
            while (current is not null)
            {
                parent = current;
                
                if (node.gameNode.getGameboard().manhattanDistance <= current.gameNode.getGameboard().manhattanDistance)
                {
                    current = current.LeftNode;
                    if (current is not null && current.gameNode.getGameboard().manhattanDistance <= node.gameNode.getGameboard().manhattanDistance)
                    {
                        parent.LeftNode = node;
                        node.LeftNode = current;
                        return true;
                    }
                }
                else if (node.gameNode.getGameboard().manhattanDistance > current.gameNode.getGameboard().manhattanDistance)
                {
                    current = current.RightNode;
                    if (current is not null && current.gameNode.getGameboard().manhattanDistance > node.gameNode.getGameboard().manhattanDistance)
                    {
                        parent.RightNode = node;
                        node.RightNode = current;
                        return true;
                    }
                }
            }

            if (node.gameNode.getGameboard().manhattanDistance <= parent.gameNode.getGameboard().manhattanDistance)
            {
                parent.LeftNode = node;
            }
            else
            {
                parent.RightNode = node;
            }

            return true;
        }

        public TreeNode Remove()
        {
            TreeNode parent = this.root;
            if (parent is null)
            {
                return parent;
            }
            TreeNode previous = new TreeNode();
            if (parent.LeftNode is not null)
            {
                while (parent.LeftNode is not null)
                {
                    previous = parent;
                    parent = parent.LeftNode;
                }
                previous.LeftNode = null;
                return parent;
            } 
            else if (parent.RightNode is null || parent.RightNode.gameNode.getGameboard().manhattanDistance >= parent.gameNode.getGameboard().manhattanDistance)
            {
                this.root = parent.RightNode;
                return parent;
            }
            
            return parent;
        }

        public int MinValue(TreeNode node)
        {
            int minv = node.gameNode.getGameboard().manhattanDistance;

            while (node.LeftNode != null)
            {
                minv = node.LeftNode.gameNode.getGameboard().manhattanDistance;
                node = node.LeftNode;
            }

            return minv;
        }
        public TreeNode Find(int value)
        {
            return this.Find(value, this.root);
        }
        private TreeNode Find(int value, TreeNode parent)
        {
            if (parent != null)
            {
                if (value == parent.gameNode.getGameboard().manhattanDistance) return parent;
                if (value < parent.gameNode.getGameboard().manhattanDistance)
                    return Find(value, parent.LeftNode);
                else
                    return Find(value, parent.RightNode);
            }
            return null;
        }
        public int GetTreeDepth()
        {
            return this.GetTreeDepth(this.root);
        }

        private int GetTreeDepth(TreeNode parent)
        {
            return parent == null ? 0 : Math.Max(GetTreeDepth(parent.LeftNode), GetTreeDepth(parent.RightNode)) + 1;
        }
    }
}
