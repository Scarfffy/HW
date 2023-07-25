using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw4
{
    public class BinaryTree
    {
        private TreeNode root;
        private int count;

        public BinaryTree()
        {
            root = null;
            count = 0;
        }

        public void Add(int value)
        {
            if (root == null)
            {
                root = new TreeNode(value);
            }
            else
            {
                AddToNode(root, value);
            }

            count++;
        }

        private void AddToNode(TreeNode node, int value)
        {
            if (value < node.Value)
            {
                if (node.Left == null)
                {
                    node.Left = new TreeNode(value);
                }
                else
                {
                    AddToNode(node.Left, value);
                }
            }
            else if (value > node.Value)
            {
                if (node.Right == null)
                {
                    node.Right = new TreeNode(value);
                }
                else
                {
                    AddToNode(node.Right, value);
                }
            }
        }

        public bool Contains(int value)
        {
            return FindNode(root, value) != null;
        }

        private TreeNode FindNode(TreeNode node, int value)
        {
            if (node == null || node.Value == value)
            {
                return node;
            }

            if (value < node.Value)
            {
                return FindNode(node.Left, value);
            }

            return FindNode(node.Right, value);
        }

        public void Clear()
        {
            root = null;
            count = 0;
        }

        public int[] ToArray()
        {
            int[] array = new int[count];
            int index = 0;
            TraverseInOrder(root, ref array, ref index);
            return array;
        }

        private void TraverseInOrder(TreeNode node, ref int[] array, ref int index)
        {
            if (node != null)
            {
                TraverseInOrder(node.Left, ref array, ref index);
                array[index] = node.Value;
                index++;
                TraverseInOrder(node.Right, ref array, ref index);
            }
        }
    }

    public class TreeNode
    {
        public int Value { get; set; }
        public TreeNode Left { get; set; }
        public TreeNode Right { get; set; }

        public TreeNode(int value)
        {
            Value = value;
            Left = null;
            Right = null;
        }
    }

}
