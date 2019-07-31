using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStructorSamples.CommonTools;

namespace DataStructorSamples.Models
{
    public abstract class Tree
    {
        public int? val = null;


        public Tree left = null;

        public Tree right = null;

        public int Height { get; protected set; }


        #region 计算树的高度

        public int CalculateHeight()
        {
            return TreeUtil.CalculatetHeight(this);
        }

        #endregion 计算树的高度	

        public abstract Tree InsertNode(Tree node, int val);
    }
}
