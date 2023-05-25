using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using WpfAnimatedGif;

namespace TestWPF
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public event Func<string> PageDownloaded; 
        public MainWindow()
        {
            InitializeComponent();
            LoadGifImage("Walk_left");//主界面可视化
            if (!bubbleVisible)
            {
                setBubbleTimer();//创建计时器线程，重复ShowBubble();
            }
        //   setChatTimer();
        //  ShowBubble();
        }
        public void HideRequestButton()
        {
            ChatBubble.Visibility = Visibility.Collapsed;
            RequestBubble.Visibility = Visibility.Visible;
            RequestButton.Visibility = Visibility.Visible;
            ChatButton.Visibility = Visibility.Collapsed;
        }
        public void HideChatButton()
        {
            ChatBubble.Visibility = Visibility.Visible;
            RequestBubble.Visibility = Visibility.Collapsed;
            
        }


        public void setBubbleTimer()
        {
            bubbleTimer = new DispatcherTimer();
            bubbleTimer.Interval = TimeSpan.FromSeconds(10);
            bubbleTimer.Tick += BubbleTimer_Tick;
            bubbleTimer.Start();
            bubbleVisible = true;
        }
        public void setChatTimer()
        {
            chatTimer = new DispatcherTimer();
            chatTimer.Interval = TimeSpan.FromSeconds(10);
            chatTimer.Tick += ChatTimer_Tick;
            chatTimer.Start();
        }

        //大小固定比例
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e) //与那边的对应
        {
            if (e.WidthChanged)
            {
                // 根据宽度和比例计算高度，并设置窗口的高度
                this.Height = this.Width ;
            }
            // 如果窗口的高度变化了
            if (e.HeightChanged)
            {
                // 根据高度和比例计算宽度，并设置窗口的宽度
                this.Width = this.Height ;
            }
            GifImage.Width = this.Width;
            GifImage.Height = this.Height;
            double width = e.NewSize.Width;
            double height = e.NewSize.Height;

            BubbleImage.Width = this.Width;
            BubbleImage.Height = this.Height;
            TestControlButton.Width = this.Width;
            TestControlButton.Height = this.Height;
            BubbleButton.Width = this.Width;
            BubbleButton.Height = this.Height;
            Arrow.Width = this.Width;
            Arrow.Height = this.Height;
//            InputText.Width = this.Width;
//          InputText.Height = this.Height;
        }

        //loadImage
        private void LoadGifImage(string gifMode)
        {
            var gifPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"../../Resources/gif/{gifMode}.gif");
            var gifImage = new BitmapImage(new Uri(gifPath));
            ImageBehavior.SetAnimatedSource(GifImage, gifImage);
            GifImage.Width = this.Width;
            GifImage.Height = this.Height;
        }
        //控制拖拽
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }









        //这下面是移动的实现
        private DispatcherTimer moveTimer;
        private DispatcherTimer bubbleTimer;
        private DispatcherTimer chatTimer;

        private double deltaX = 0;
        private double deltaY = 0;
        private double speed = 10;
        private Random random = new Random();

        string gifFile = "Walk_left";

        private bool bubbleVisible = false;
        private bool isMoving = false;

        private void Click_move(object sender, RoutedEventArgs e)
        {
            isMoving = true;
            moveTimer = new DispatcherTimer();
            moveTimer.Interval = TimeSpan.FromMilliseconds(10);
            moveTimer.Tick += MoveTimer_Tick;
            moveTimer.Start();
        }
        
        private void Click_stop(object sender, RoutedEventArgs e)
        {
            moveTimer.Stop();
            moveTimer.Start();
        }
        private void MoveTimer_Tick(object sender, EventArgs e)
        {
            // Check if left mouse button is pressed
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                // Stop moving and clear moveTimer
                isMoving = false;
                moveTimer.Stop();
                moveTimer = new DispatcherTimer();
                moveTimer.Start();
                return;
            }
            if (isMoving)
            {
                this.Left += deltaX;
                this.Top += deltaY;

                // Check for change in direction and update GIF
                if (deltaX < 0 && gifFile != "Walk_left")
                {
                    gifFile = "Walk_left";
                    LoadGifImage(gifFile);
                }
                else if (deltaX > 0 && gifFile != "Walk_right")
                {
                    gifFile = "Walk_right";
                    LoadGifImage(gifFile);
                }

                // Check for collision with screen edges
                if (this.Left < 0)
                {
                    this.Left = 0;
                    deltaX = -deltaX;
                }
                else if (this.Left + this.Width > SystemParameters.PrimaryScreenWidth)
                {
                    this.Left = SystemParameters.PrimaryScreenWidth - this.Width;
                    deltaX = -deltaX;
                }
                if (this.Top < 0)
                {
                    this.Top = 0;
                    deltaY = -deltaY;
                }
                else if (this.Top + this.Height > SystemParameters.PrimaryScreenHeight)
                {
                    this.Top = SystemParameters.PrimaryScreenHeight - this.Height;
                    deltaY = -deltaY;
                }

                // Generate new direction after a certain time interval
                if (random.NextDouble() < 0.01)
                {
                    double angle = random.NextDouble() * 2 * Math.PI;
                    deltaX = speed * Math.Cos(angle);
                    deltaY = speed * Math.Sin(angle);
                }

                // Show bubble if not visible
                
            }

        }
        //现在是要气泡
















        private DateTime lastChatBubbleClickTime = DateTime.MinValue; // 记录上一次红色对话框点击的时间

        private void ChatBubble_Click(object sender, RoutedEventArgs e)
        {
            //红色对话框事件
            HideBubble();
            Console.WriteLine("Chat bubble clicked!");
            StartGlobalTimer();
            lastChatBubbleClickTime = DateTime.Now; // 记录当前时间
        }

        private void ChatTimer_Tick(object sender, EventArgs e)
        {
            const int interval = 1; // 时间间隔，单位：秒

            if ((DateTime.Now - lastChatBubbleClickTime).TotalSeconds >= interval)
            {
                ShowBubble();
                chatTimer.Stop(); // 停止定时器
            }
        }


        private void StartGlobalTimer()
        {
            chatTimer = new DispatcherTimer();
            chatTimer.Interval = TimeSpan.FromSeconds(1);
            chatTimer.Tick += ChatTimer_Tick;
            chatTimer.Start();
        }


        
        private void BubbleTimer_Tick(object sender, EventArgs e)
        {

            ShowBubble();
        }

        private void HideBubble()
        {
            ChatBubble.Visibility = Visibility.Collapsed;
            RequestBubble.Visibility = Visibility.Collapsed;
            bubbleVisible = false;
            bubbleTimer.Stop();
        }
        
        private void RequestBubble_Click(object sender, MouseButtonEventArgs e)
        {
            HideBubble();
        }
        private void ChatBubble_Click(object sender, MouseButtonEventArgs e)
        {
            HideBubble();
        }

        private void RequestBubble_Click(object sender, RoutedEventArgs e)
        {
            //蓝色对话框事件
            HideBubble();
            Console.WriteLine("Request bubble clicked!");
            var newWindow = new InputTextWindow();
            newWindow.Owner = this;

            newWindow.Left = this.Left + this.Width ;
            newWindow.Top = this.Top + this.Height-20;

            ArrowImage.Visibility = Visibility.Visible; 
            // 显示新窗口
            newWindow.ShowDialog();

            //获取文本
            string result = newWindow.Result;

            //文本正确
            Console.WriteLine(result);
            //这里应该监听一个指令
            /*
            if (!isWorkCompleted)
            {
                // 工作未完成，暂停操作
                Console.WriteLine("Work is not completed yet, please wait...");
                return;
            }
            */
            ShowBubble();
        }
        /*
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            HideBubble();
        }
        */

        private bool isWorkCompleted = false;

        /*
        // 订阅外来事件
        private void SubscribeToExternalEvent()
        {
            // 假设外来事件是另一个类的事件，该类名为 ExternalClass，事件名为 WorkCompleted
            ExternalClass externalObj = new ExternalClass();
            externalObj.WorkCompleted += ExternalObj_WorkCompleted;
        }

        // 实现事件处理程序
        private void ExternalObj_WorkCompleted(object sender, EventArgs e)
        {
            // 工作已完成，设置标志位
            isWorkCompleted = true;
        }
        */

        private void ShowBubble()
        {
            int randomNum = random.Next(1, 101);
            if (randomNum <= 30)
            {
                HideChatButton();
            }
            else 
            {   
                HideRequestButton();
            }
        }
       
        private void InputBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}