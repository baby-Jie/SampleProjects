using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DataStructorSamples.Models
{
    public class AVLTreeNode: BinaryTreeNode
    {

        //public AVLTreeNode(int val):base(val)
        //{

        //}
        ////public override void AddNode(int val)
        ////{

        ////}

        //public AVLTreeNode AddNode(int val)
        //{
        //    return InsertdNode(this, val);
        //}

        //public AVLTreeNode InsertdNode(AVLTreeNode root, int val)
        //{
        //    if (root == null)
        //    {
        //        root = new AVLTreeNode(val);
        //    }

        //    var value = root.val.Value;

        //   if (val < value)
        //    {
        //        if (root.left != null)
        //        {
        //            // 插入左子树
        //            root.left = this.InsertdNode(root.left, val);

        //            // 调整
        //        }
        //        else
        //        {
        //            BinaryTreeNode node = new BinaryTreeNode(val);
        //            root.left = node;
        //        }
        //    }
        //    else
        //    {
        //        if (root.right != null)
        //        {
        //            AddNode(root.right, val);

        //            // 调整
        //        }
        //        else
        //        {
        //            BinaryTreeNode node = new BinaryTreeNode(val);
        //            root.right = node;
        //        }
        //    }

        //    // 调整

        //    return root;
        //}
    }
}
