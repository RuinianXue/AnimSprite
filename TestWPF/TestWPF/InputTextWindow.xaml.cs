using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace TestWPF
{
    /// <summary>
    /// InputTextWindow.xaml 的交互逻辑
    /// </summary>
    public partial class InputTextWindow : Window
    {
        public string Result { get; set; }
        public InputTextWindow()
        {
            InitializeComponent();
        }

        private void InputBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                //string filePath = "path/to/text/file.txt";
                //string fileContents = File.ReadAllText(filePath);
                // Perform processing on fileContents here
                Result = InputBox.Text;
                Close();
            }
        }
        private void InputBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Result = InputBox.Text;
        }


        public string GetInputText()
        {
            return InputBox.Text;
        }

        private void ReplyBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        /*
        private void OK_Click(object sender, RoutedEventArgs e)
        {
            // 设置返回值并关闭窗口
            //Result = InputBox.Text;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            // 关闭窗口
            Close();
        }*/
    }
}
