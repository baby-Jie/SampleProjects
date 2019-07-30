using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DataStructorSamples.Models
{
    public class BinaryTreeNode
    {
        public int? val = null;

        public BinaryTreeNode left = null;

        public BinaryTreeNode right = null;

        public BinaryTreeNode()
        {

        }

        public BinaryTreeNode(int val)
        {
            this.val = val;
        }

        public BinaryTreeNode(int val, BinaryTreeNode left, BinaryTreeNode right)
        {
            this.val = val;
            this.left = left;
            this.right = right;
        }

        #region Delete Node

        public BinaryTreeNode DeleteNode(int val)
        {
            return DeleteNode(this, val);
        }

        public BinaryTreeNode DeleteNode(BinaryTreeNode node, int val)
        {
            if (node == null || !node.val.HasValue)
            {
                return null;
            }

            var value = node.val.Value;

            if (value == val)
            {
                // 删除

                // node为叶子节点  直接删除
                if (node.left == null && node.right == null)
                {
                    return null;
                }

                // node为单子节点  子节点替代
                if (node.left == null || node.right == null)
                {
                    return node.left ?? node.right;
                }

                var minNode = node.right;

                while (minNode.left != null)
                {
                    minNode = minNode.left;
                }

                var retNode = DeleteNode(node.right, minNode.val.Value);

                node.right = retNode;

                node.val = minNode.val.Value;

                // node为双子节点
                //BinaryTreeNode minNode = null;
                //DeleteMinNode(node.right, ref minNode);

                //if (minNode != null)
                //{
                //    minNode.left = node.left;
                //    minNode.right = node.right;
                //}

                //return minNode;

            }
            else if (val < value)
            {
                if (node.left != null)
                {
                    node.left = DeleteNode(node.left, val);
                }
            }
            else
            {
                if (node.right != null)
                {
                    node.right = DeleteNode(node.right, val);
                }
            }

            return node;
        }

        public BinaryTreeNode DeleteMinNode(BinaryTreeNode node, ref BinaryTreeNode minNode)
        {
            minNode = null;

            if (node == null)
            {
                return null;
            }

            if (node.left == null)
            {
                minNode = node;
                var ret = node.right;
                node.right = null;
                return ret;
            }
            else
            {
                DeleteMinNode(node.left, ref minNode);
            }

            return null;
        }

        #endregion Delete Node	

        #region Add Node

        public void AddNode(int val)
        {
            AddNode(this, val);
        }

        public void AddNode(BinaryTreeNode root, int val)
        {
            if (root == null || !root.val.HasValue)
            {
                return;
            }

            var value = root.val.Value;

            if (value == val)
            {
                return;
            }
            else if (val < value)
            {
                if (root.left != null)
                {
                    AddNode(root.left, val);
                }
                else
                {
                    BinaryTreeNode node = new BinaryTreeNode(val);
                    root.left = node;
                }
            }
            else
            {
                if (root.right != null)
                {
                    AddNode(root.right, val);
                }
                else
                {
                    BinaryTreeNode node = new BinaryTreeNode(val);
                    root.right = node;
                }
            }
        }

        #endregion Add Node	

        #region Tranverse

        #region PreTraverse

        public void PreTraverseSelf()
        {
            PreTraverse(this);
        }

        public void PreTraverse(BinaryTreeNode node)
        {
            if (node == null)
            {
                return;
            }

            Console.WriteLine($"{node.val} ");

            PreTraverse(node.left);

            PreTraverse(node.right);
        }

        #endregion PreTraverse	

        #region MiddleTraverse

        public void MiddleTraverseSelf()
        {
            MiddleTraverse(this);
        }

        public void MiddleTraverse(BinaryTreeNode node)
        {
            if (node == null)
            {
                return;
            }

            MiddleTraverse(node.left);

            Console.WriteLine($"{node.val} ");

            MiddleTraverse(node.right);
        }

        #endregion MiddleTraverse	

        #region AfterTraverse

        public void AfterTraverseSelf()
        {
            AfterTraverse(this);
        }

        public void AfterTraverse(BinaryTreeNode node)
        {
            if (node == null)
            {
                return;
            }

            AfterTraverse(node.left);

            AfterTraverse(node.right);

            Console.WriteLine($"{node.val} ");
        }

        #endregion MiddleTraverse	

        #endregion Tranverse	
    }
}
