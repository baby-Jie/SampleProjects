using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStructorSamples.Models;

namespace DataStructorSamples.CommonTools
{
    public static class TreeUtil
    {
        #region 计算树的高度

        public static int CalculatetHeight(Tree tree)
        {
            if (tree == null)
            {
                return 0;
            }

            return Math.Max(CalculatetHeight(tree.left), CalculatetHeight(tree.right)) + 1;
        }

        #endregion 计算树的高度	

        #region Tranverse

        #region PreTraverse


        public static void PreTraverse(Tree node)
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


        public static void MiddleTraverse(Tree node)
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


        public static void AfterTraverse(Tree node)
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
