using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


        public override Tree InsertNode(Tree node, int val)
        {
            if (node == null)
            {
                node = new AVLTree();
            }

            return null;
        }
    }
}
