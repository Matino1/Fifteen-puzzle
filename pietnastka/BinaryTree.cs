using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pietnastka
{
    internal class BinaryTree
    {
        public TreeNode Root { get; set; }

        public bool Add(TreeNode value)
        {
            TreeNode before = null;
            TreeNode after = this.Root;

            while (after != null)
            {
                before = after;
                if (value.gameNode.getGameboard().manhattanDistance < after.gameNode.getGameboard().manhattanDistance) //Is new node in left tree? 
                    after = after.LeftNode;
                else if (value.gameNode.getGameboard().manhattanDistance >= after.gameNode.getGameboard().manhattanDistance) //Is new node in right tree?
                    after = after.RightNode;
                else
                {
                    //Exist same value
                    return false;
                }
            }

            TreeNode newNode = new TreeNode();
            newNode.gameNode = value.gameNode;

            if (this.Root == null)//Tree ise empty
                this.Root = newNode;
            else
            {
                if (value.gameNode.getGameboard().manhattanDistance < before.gameNode.getGameboard().manhattanDistance)
                    before.LeftNode = newNode;
                else
                    before.RightNode = newNode;
            }

            return true;
        }

        public void Remove(Node value)
        {
            this.Root = Remove(this.Root, value);
        }

        private TreeNode Remove(TreeNode parent, Node key)
        {
            if (parent == null) return parent;

            if (key.getGameboard().manhattanDistance < parent.gameNode.getGameboard().manhattanDistance) parent.LeftNode = Remove(parent.LeftNode, key);
            else if (key.getGameboard().manhattanDistance > parent.gameNode.getGameboard().manhattanDistance)
                parent.RightNode = Remove(parent.RightNode, key);

            // if value is same as parent's value, then this is the node to be deleted  
            else
            {
                // node with only one child or no child  
                if (parent.LeftNode == null)
                    return parent.RightNode;
                else if (parent.RightNode == null)
                    return parent.LeftNode;

                // node with two children: Get the inorder successor (smallest in the right subtree)  
                parent.gameNode.getGameboard().manhattanDistance = MinValue(parent.RightNode);

                // Delete the inorder successor  
                parent.RightNode = Remove(parent.RightNode, parent.gameNode);
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
            return this.Find(value, this.Root);
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
            return this.GetTreeDepth(this.Root);
        }

        private int GetTreeDepth(TreeNode parent)
        {
            return parent == null ? 0 : Math.Max(GetTreeDepth(parent.LeftNode), GetTreeDepth(parent.RightNode)) + 1;
        }
    }
}
