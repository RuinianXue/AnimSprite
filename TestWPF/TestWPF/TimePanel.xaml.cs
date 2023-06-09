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

namespace TestWPF
{
    /// <summary>
    /// TimePanel.xaml 的交互逻辑
    /// </summary>
    public partial class TimePanel : Window
    {
        public DateTime RecorededTime {  get; set; }
        public TimePanel()
        {
            InitializeComponent();
        }
        private void CalendarWithClock_Confirmed()
        {
            RecorededTime = (DateTime)nowTimePanel.SelectedDateTime;
            //这里是需要写入的时间

            //Console.WriteLine(RecorededTime.ToString()); //2023/6/9 15:44:09
            Close();
        }
    }
}
