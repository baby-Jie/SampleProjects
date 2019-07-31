using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;
using DataStructorSamples.CommonTools;

namespace DataStructorSamples.Models
{
    public class AVLTree:Tree
    {
        #region Constructors

        public AVLTree(int value) : this(value, null, null)
        {

        }

        public AVLTree(int value, Tree left, Tree right)
        {
            this.val = value;
            this.left = left;
            this.right = right;
        }

        #endregion Constructors

        #region  Fields
        #endregion Fields

        #region Properties
        #endregion Properties

        #region Methods
        #endregion Methods

        #region EventHandlers

        #region Windows
        #endregion Windows

        #endregion EventHandlers


        public override Tree InsertNode(Tree root, int val)
        {
            if (root == null)
            {
                root = new AVLTree(val);
            }

            int rootValue = root.val.Value;

            if (val < rootValue)
            {
                // 插入左子树
                root.left = InsertNode(root.left, val);

                //  因为是插入左子树  所以只可能是左子树高度高于右子树或者等于右子树 调整左子树
                if (GetHeight(root.left) - GetHeight(root.right) == 2)
                {
                    // 判断是在左子树的左边还是右边
                    if (val < root.left.val)
                    {
                        // 左左情况  需要右单旋
                        root = RightSingleRotate(root);
                    }
                    else if (val > root.left.val)
                    {
                        // 左右情况 需要左右双旋转
                        root = LeftRightDoubleRotate(root);
                    }
                }
            }
            else if (val > rootValue)
            {
                // 插入右子树
                root.right = InsertNode(root.right, val);

                //  因为是插入右子树  所以只可能是右子树高度高于右子树或者等于左子树 调整右子树
                if (GetHeight(root.right) - GetHeight(root.left) == 2)
                {
                    // 判断是在右子树的左边还是右边
                    if (val > root.right.val)
                    {
                        // 右右情况  需要左单旋
                        root = LeftSingleRotate(root);
                    }
                    else if (val < root.right.val)
                    {
                        // 右左情况 需要右左双旋转
                        root = RightLeftDoubleRotate(root);
                    }
                }
            }

            return root;
        }


        #region Rotate Tree

        private Tree RightSingleRotate(Tree root)
        {
            if (root == null)
                return null;

            var leftTree = root.left;
            root.left = leftTree.right;
            leftTree.right = root;

            return leftTree;
        }
        private Tree LeftRightDoubleRotate(Tree root)
        {
            // 左子树左旋
            root.left = LeftSingleRotate(root.left);

            // 后右旋
            root = RightSingleRotate(root);

            return root;
        }

        private Tree LeftSingleRotate(Tree root)
        {
            if (root == null)
            {
                return null;
            }

            var rightTree = root.right;
            root.right = rightTree.left;
            rightTree.left = root;

            return rightTree;
        }

        private Tree RightLeftDoubleRotate(Tree root)
        {
            // 先右子树右旋
            root.right = RightSingleRotate(root.right);

            // 再左旋
            root = LeftSingleRotate(root);

            return root;
        }

        #endregion Rotate Tree	

        public int GetHeight(Tree node)
        {
            return TreeUtil.CalculatetHeight(node);
        }

    }
}
