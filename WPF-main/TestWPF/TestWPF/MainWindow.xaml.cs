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
using TestWPF.Resources;
using WpfAnimatedGif;
using System.Threading;
using ConsoleApp19;
using System.IO;

namespace TestWPF
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    /// 



    public partial class MainWindow : Window
    {
        Controller c;
        Timer updataStatusTimer;
        DispatcherTimer monitorStatusTimer;
       // Timer BubbleTimer;
        public MainWindow()
        {
            InitializeComponent(); 
            c = new Controller();
            updataStatusTimer =  new Timer(c.setInfoEvent, null, 0, 1000);
            c.throwRequest += getRequest;
            monitorStatusTimer = new DispatcherTimer();
            monitorStatusTimer.Interval= TimeSpan.FromSeconds(10);
            monitorStatusTimer.Tick += c.monitorStatus;
            monitorStatusTimer.Start();
            // BubbleTimer = new Timer(c.throwRequest, null, 0, 3000);
            LoadGifImage("Stay");
            /*
            if (!bubbleVisible)
            {
                setBubbleTimer();
            }
            setChatTimer();*/
            haveRequest = true;
            ShowBubble();
            //setBubbleTimer();
        }
        public void ShowChatButton()
        {
            lock (lockObject)
            {
                ChatBubble.Visibility = Visibility.Collapsed;
                RequestBubble.Visibility = Visibility.Visible;
                ChatButton.Visibility = Visibility.Collapsed;
                RequestButton.Visibility = Visibility.Visible;
            }
        }
        bool haveRequest;
        public void getRequest(string requestInfomation)
        {
            haveRequest = true;
            infomation = requestInfomation;
            ShowRequestButton();
        }
        public void ShowRequestButton()
        {
            lock (lockObject)
            {
                ChatBubble.Visibility = Visibility.Visible;
                RequestBubble.Visibility = Visibility.Collapsed;
                ChatButton.Visibility= Visibility.Visible;
                RequestButton.Visibility = Visibility.Collapsed;
            }
        }


        public void setBubbleTimer()
        {
            bubbleTimer = new DispatcherTimer();
            bubbleTimer.Interval = TimeSpan.FromSeconds(1);
            bubbleTimer.Tick += BubbleTimer_Tick;
            //bubbleTimer.Start();
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
//          InputText.Width = this.Width;
//          InputText.Height = this.Height;
        }

        //loadImage
        private void LoadGifImage(string gifMode)
        {
            var gifPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"../../Resources/{gifMode}.gif");
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
        private DispatcherTimer stayTimer;

        private double deltaX = 0;
        private double deltaY = 0;
        private double speed = 10;
        private Random random = new Random();

        string moveGif = "Walk_left";

        private bool bubbleVisible = false;
        private bool isMoving = false;

        private void Move()
        {
            isMoving = true;
            moveTimer = new DispatcherTimer();
            moveTimer.Interval = TimeSpan.FromMilliseconds(10);
            moveTimer.Tick += MoveTimer_Tick;
            moveTimer.Start();
            LoadGifImage(moveGif);
        }

        private void Click_move(object sender, RoutedEventArgs e)
        {
            Move();
        }
        
        private void RandomStaying()
        {
            int state = random.Next(1, 5);
            switch (state)
            {
                case 1:
                    LoadGifImage("Stay");
                    break;
                case 2:
                    LoadGifImage("Kick");
                    break;
                case 3:
                    LoadGifImage("Rain");
                    break;
                case 4:
                    Move();
                    break;
            }
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
                //moveTimer.Start();
                RandomStaying();
                return;
            }
            if (isMoving)
            {
                this.Left += deltaX;
                this.Top += deltaY;

                // Check for change in direction and update GIF
                if (deltaX < 0 && moveGif != "Walk_left")
                {
                    moveGif = "Walk_left";
                    LoadGifImage(moveGif);
                }
                else if (deltaX > 0 && moveGif != "Walk_right")
                {
                    moveGif = "Walk_right";
                    LoadGifImage(moveGif);
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

        private string infomation;
        private void ChatBubble_Click(object sender, RoutedEventArgs e)
        {
            //红色对话框事件
            //bubbleTimer.Stop();
            HideBubble();
            Console.WriteLine("Chat bubble clicked!");
            // StartChatTimer();//注意这里
            // lastChatBubbleClickTime = DateTime.Now; // 记录当前时间
            lock(lockObject)
            CallReplyWindow(infomation);
            

        }



        private void StartChatTimer()
        {
            chatTimer = new DispatcherTimer();
            chatTimer.Interval = TimeSpan.FromSeconds(1);
            chatTimer.Tick += ChatTimer_Tick;
            chatTimer.Start();
        }
        
        private void StartStayTimer()
        {
            stayTimer = new DispatcherTimer();
            stayTimer.Interval = TimeSpan.FromSeconds(3);
            stayTimer.Tick += StayTimer_Tick;
            stayTimer.Start();
        }

        private void StayTimer_Tick(object sender, EventArgs e)
        {
            if(!isMoving)
            {
                RandomStaying();
            }
        }
        private void ChatTimer_Tick(object sender, EventArgs e) //
        {
            const int interval = 3; // 时间间隔，单位：秒

            if ((DateTime.Now - lastChatBubbleClickTime).TotalSeconds >= interval)
            {
                //ShowBubble();
                chatTimer.Stop(); // 停止定时器
            }
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
            //bubbleTimer.Stop();
        }
        
        //气泡被点击
        private void RequestBubble_Click(object sender, MouseButtonEventArgs e)
        {
            HideBubble();
        }
        private void ChatBubble_Click(object sender, MouseButtonEventArgs e)
        {
            HideBubble();
        }

        //处理请求信息
        private string CallRequestWindow()
        {

            lock (lockObject)
            {
                /*
                double augmentRate = this.Width / 100;
                var requestWindow = new InputTextWindow();
                requestWindow.Owner = this;

                requestWindow.Height *= augmentRate;
                requestWindow.Width *= augmentRate;

                requestWindow.Left = this.Left + this.Width;
                requestWindow.Top = this.Top + this.Width / 3;

                TextBox inputBox = (TextBox)requestWindow.FindName("InputBox");
                inputBox.Width *= augmentRate;
                inputBox.Height *= augmentRate;

                Viewbox inputView = (Viewbox)requestWindow.FindName("InputBoxView");
                inputView.Width *= augmentRate;
                inputView.Height *= augmentRate;
                */
                //ArrowImage.Visibility = Visibility.Visible;//
                // 显示新窗口
                var requestWindow = new ChatWindow();
                requestWindow.Owner = this;
                requestWindow.ShowDialog();
                //获取文本
                return requestWindow.SendText;
                //文本正确
                //Console.WriteLine(result);
                //这里应该监听一个指令
                /*
                if (!isWorkCompleted)
                {
                    // 工作未完成，暂停操作
                    Console.WriteLine("Work is not completed yet, please wait...");
                    return;
                }
                */
            }
        }

        //处理回复信息
        private void CallReplyWindow(string text)
        {
            lock (lockObject)
            {
                double augmentRate = this.Width / 100;
                // Update ReplyWindow size and position
                ReplyBoxWindow replyWindow = new ReplyBoxWindow();
                replyWindow.Owner = this;

                replyWindow.Width = replyWindow.Width * augmentRate;
                replyWindow.Height = replyWindow.Height * augmentRate;

                // Update font size
                TextBlock replyBox = (TextBlock)replyWindow.FindName("ReplyBox");
                replyBox.Width *= augmentRate;
                replyBox.Height *= augmentRate;

                replyBox.FontSize = replyBox.FontSize * augmentRate;
                replyBox.Text = text;

                Viewbox replyView = (Viewbox)replyWindow.FindName("ReplyBoxView");
                replyView.Width *= augmentRate;
                replyView.Height *= augmentRate;

                replyWindow.Left = this.Left + this.Width;
                replyWindow.Top = this.Top;
                replyWindow.ShowDialog();

                //如果要显示文本这里需要传入文本。！！！！！！
                //bubbleTimer.Start();
                haveRequest = false;
                ShowBubble();
            }
        }

        private void MainWindow_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            RandomStaying();
        }

        private void ProcessRequests(string input)
        {
            CallReplyWindow(c.getResponds(new requestInfo(input)));
        }

        private void RequestBubble_Click(object sender, RoutedEventArgs e)

        {
            //bubbleTimer.Stop();
            //蓝色对话框事件
            HideBubble();
            Console.WriteLine("Request bubble clicked!");
            //HideBubble();
            lock (lockObject)
            {
                string input = CallRequestWindow();
                ProcessRequests(input);
            }
          //  bubbleTimer.Start();
            //ShowBubble();
            //如果事件来了
            /*if(true)
            {
                ArrowImage.Visibility = Visibility.Hidden;//
                ShowBubble();
            }*/
            //Move();

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
        static object lockObject = new object();
        private void ShowBubble()
        {
            lock(lockObject){
                int randomNum = random.Next(1, 101);
                bubbleVisible = true;                            
                if (haveRequest)
                {
                    ShowRequestButton();
                }
                else
                {
                    ShowChatButton();
                }
            }
        }
        

        private void InputBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                //string filePath = "path/to/text/file.txt";
                //string fileContents = File.ReadAllText(filePath);
                // Perform processing on fileContents here
            }
        }


        private void InputBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        /*
private void ChatInput_TextChanged(object sender, TextChangedEventArgs e)
{
   if (string.IsNullOrEmpty(ChatInput.Text))
   {
       ShowBubble();
   }
}*/
    }
}