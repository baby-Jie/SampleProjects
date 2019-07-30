using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DataStructorSamples.Models;

namespace DataStructorSamples
{
    /// <summary>
    /// TreeSamples.xaml 的交互逻辑
    /// </summary>
    public partial class TreeSamples : Window
    {
        #region Constructors

        public TreeSamples()
        {
            InitializeComponent();
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

        #region 二叉搜索树

        private void TestBinarySerachTreeBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var root = CreateBinarySerachTree();

            root.DeleteNode(7);

            Console.WriteLine("先序遍历---");
            root.PreTraverseSelf();

            Console.WriteLine("中序遍历---");
            root.MiddleTraverseSelf();

            Console.WriteLine("后序遍历");
            root.AfterTraverseSelf();
        }

        private BinaryTreeNode CreateBinarySerachTree()
        {
            BinaryTreeNode root = new BinaryTreeNode(10);

            root.AddNode(7);
            root.AddNode(4);
            root.AddNode(3);
            root.AddNode(8);
            root.AddNode(9);
            root.AddNode(15);
            root.AddNode(17);

            return root;
        }


        #endregion 二叉搜索树	


    }
}
