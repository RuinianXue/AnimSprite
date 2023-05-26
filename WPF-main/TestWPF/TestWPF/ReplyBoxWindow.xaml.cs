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

namespace TestWPF.Resources
{
    /// <summary>
    /// ReplyBoxWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ReplyBoxWindow : Window
    {
        public ReplyBoxWindow()
        {
            InitializeComponent();
        }
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e) //与那边的对应
        {
            if (e.WidthChanged)
            {
                // 根据宽度和比例计算高度，并设置窗口的高度
                this.Height = this.Width;
            }
            // 如果窗口的高度变化了
            if (e.HeightChanged)
            {
                // 根据高度和比例计算宽度，并设置窗口的宽度
                this.Width = this.Height;
            }
            double width = e.NewSize.Width;
            double height = e.NewSize.Height;
            ReplyBoxView.Height = width - 50;
            ReplyBoxView.Width = height - 50;

            /*
            BubbleImage.Width = this.Width;
            BubbleImage.Height = this.Height;
            TestControlButton.Width = this.Width;
            TestControlButton.Height = this.Height;
            BubbleButton.Width = this.Width;
            BubbleButton.Height = this.Height;
            Arrow.Width = this.Width;
            Arrow.Height = this.Height;
            //            InputText.Width = this.Width;
            //          InputText.Height = this.Height;*/
        }
        private void Reply_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

    }
}
