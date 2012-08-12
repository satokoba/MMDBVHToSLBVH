using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;

namespace WpfApplication1
{
    /// <summary>
    /// Window1.xaml の相互作用ロジック
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();

            bvhFrom.Offset = new System.Windows.Media.Media3D.Vector3D(0, -10, 0);
            bvhTo.Scale = 0.25;

            menuItemFrame900.IsChecked = true;
            menuItemFrame600.IsChecked = false;
            menuItemFrame450.IsChecked = false;
            menuItemFrame300.IsChecked = false;
        }

        private void OpenMenuItem_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog dialog = new System.Windows.Forms.OpenFileDialog();
            dialog.DefaultExt = ".bvh";
            dialog.Filter = "BVHファイル(*.bvh)|*.bvh";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                BVH bvh = new BVH();
                bvh.Load(dialog.OpenFile());
                bvhFrom.BVH = bvh;
                bvhTo.BVH = bvh.Convert();
                menuItemUseAll.IsChecked = false;

                menuItemFrame900.IsChecked = true;
                menuItemFrame600.IsChecked = false;
                menuItemFrame450.IsChecked = false;
                menuItemFrame300.IsChecked = false;
                bvhTo.BVH.FramesPerFile = 900;
            }
        }

        private void SaveAsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.SaveFileDialog dialog = new System.Windows.Forms.SaveFileDialog();
            dialog.DefaultExt = ".bvh";
            dialog.Filter = "BVHファイル(*.bvh)|*.bvh";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                bvhTo.BVH.Save(dialog.FileName);
            }
        }

        private void UseAll_Checked(object sender, RoutedEventArgs e)
        {
            foreach (CompositeElement joint in bvhTo.BVH.JointList)
            {
                JointFrame jf = bvhTo.BVH.FrameList[0].GetJointFrame(joint.Name);
                jf.SetValue("Xrotation", 0.1);
            }
        }

        private void UseAll_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach(CompositeElement joint in bvhTo.BVH.JointList){
                JointFrame jf = bvhTo.BVH.FrameList[0].GetJointFrame(joint.Name);
                jf.SetValue("Xrotation", 0.0);
            }
        }

        private void SelectFramesPerFile(object sender, EventArgs e)
        {
            if (sender == menuItemFrame900)
            {
                menuItemFrame900.IsChecked = true;
                menuItemFrame600.IsChecked = false;
                menuItemFrame450.IsChecked = false;
                menuItemFrame300.IsChecked = false;
                if (bvhTo.BVH != null)
                {
                    bvhTo.BVH.FramesPerFile = 900;
                }
            }
            else if (sender == menuItemFrame600)
            {
                menuItemFrame900.IsChecked = false;
                menuItemFrame600.IsChecked = true;
                menuItemFrame450.IsChecked = false;
                menuItemFrame300.IsChecked = false;
                if (bvhTo.BVH != null)
                {
                    bvhTo.BVH.FramesPerFile = 600;
                }
            }
            else if (sender == menuItemFrame450)
            {
                menuItemFrame900.IsChecked = false;
                menuItemFrame600.IsChecked = false;
                menuItemFrame450.IsChecked = true;
                menuItemFrame300.IsChecked = false;
                if (bvhTo.BVH != null)
                {
                    bvhTo.BVH.FramesPerFile = 450;
                }
            }
            else if (sender == menuItemFrame300)
            {
                menuItemFrame900.IsChecked = false;
                menuItemFrame600.IsChecked = false;
                menuItemFrame450.IsChecked = false;
                menuItemFrame300.IsChecked = true;
                if (bvhTo.BVH != null)
                {
                    bvhTo.BVH.FramesPerFile = 300;
                }
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
