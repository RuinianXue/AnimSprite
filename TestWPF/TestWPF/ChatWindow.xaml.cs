using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
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
    /// ChatWindow.xaml 的交互逻辑
    /// </summary>
    /// 
    public static class ChatHistoryFile
    {
        private const string FileName = "chat_history.txt";

        public static void Save(string message)
        {
            using (var writer = new StreamWriter(FileName, true))
            {
                writer.WriteLine(message);
            }
        }
        public static void Delete()
        {
            File.WriteAllText("chat_history.txt", string.Empty);
        }
        public static string Load()
        {
            if (!File.Exists(FileName))
            {
                return "";
            }
            using (var reader = new StreamReader(FileName))
            {
                return reader.ReadToEnd();
            }
        }
    }
    public partial class ChatWindow : Window
    {
        public string SendText { get; set; }
        public event Action<string,int> OnMessageReceived;
        public event Action closed;
        public event Func<string,string> getM;
        public void LoadChatHistoryFromFile()
        {
            string chatHistory = ChatHistoryFile.Load();
            if (!string.IsNullOrEmpty(chatHistory))
            {
                string tmpmssage = "";
                string[] messages = chatHistory.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string message in messages)
                {
                    tmpmssage = message;
                    // Create a new Grid element for the message bubble
                    Grid messageBubble = new Grid();
                    messageBubble.Margin = new Thickness(0, 0, 0, 10);

                    // Create a new Ellipse element for the avatar circle
                    Ellipse avatarCircle = new Ellipse();
                    avatarCircle.Width = 40;
                    avatarCircle.Height = 40;
                    avatarCircle.Fill = Brushes.LightBlue;

                    // Create a new Border element for the message text
                    Border messageTextBorder = new Border();
                    if (message.StartsWith("[Send]"))
                    {
                        messageTextBorder.Background = Brushes.LightBlue;
                        tmpmssage = tmpmssage.Substring(6);
                    }
                    else if (message.StartsWith("[Reply]"))
                    {
                        messageTextBorder.Background = Brushes.LightGray;
                        tmpmssage = tmpmssage.Substring(7);
                    }
                    else
                    {
                        Console.WriteLine(tmpmssage + "Chat History File Error");
                    }
                    messageTextBorder.CornerRadius = new CornerRadius(10);
                    messageTextBorder.Padding = new Thickness(10);

                    // Create a new TextBlock element for the message text
                    TextBlock messageTextBlock = new TextBlock();
                    messageTextBlock.Text = tmpmssage;

                    // Add the TextBlock to the Border element
                    messageTextBorder.Child = messageTextBlock;

                    // Add the Ellipse and Border elements to the Grid element
                    messageBubble.Children.Add(avatarCircle);
                    messageBubble.Children.Add(messageTextBorder);

                    // Add the Grid element to the messageStackPanel
                    messageStackPanel.Children.Add(messageBubble);
                }
            }
        }
        public ChatWindow()
        {
            InitializeComponent();
            LoadChatHistoryFromFile();
            // Load the chat history from the file

        }
        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                // Call the SendMessage event handler to send the message
                SendMessage(sender, e);

                // Set the Handled property to true to prevent further processing of the key event
                e.Handled = true;
            }
        }

        private void NewMessageBubble(bool isSendMessage,string messageText)
        {

            // Save the message to the file
            if (isSendMessage)
            {
                if(messageText.Contains("bye")||messageText.Contains("再见"))
                {
                    closed();
                    Close();
                    return;
                }
                SendText = messageText;
                ChatHistoryFile.Save("[Send]" + messageText);
            }
            else
            {
                ChatHistoryFile.Save("[Reply]" + messageText);
            }

            // Create a new Grid element for the message bubble
            Grid messageBubble = new Grid();
            messageBubble.Margin = new Thickness(0, 0, 0, 10);

            // Create a new Ellipse element for the avatar circle
            Ellipse avatarCircle = new Ellipse();
            avatarCircle.Width = 40;
            avatarCircle.Height = 40;

            // Create a new Border element for the message text
            Border messageTextBorder = new Border();
            messageTextBorder.CornerRadius = new CornerRadius(10);
            messageTextBorder.Padding = new Thickness(10);

            if (isSendMessage)
            {
                avatarCircle.Fill = Brushes.LightBlue;
                messageTextBorder.Background = Brushes.LightBlue;
            }
            else
            {
                avatarCircle.Fill = Brushes.LightGray;
                messageTextBorder.Background = Brushes.LightGray;
            }

            // Create a new TextBlock element for the message text
            TextBlock messageTextBlock = new TextBlock();
            messageTextBlock.Text = messageText;
            messageTextBlock.TextWrapping = TextWrapping.Wrap; // Set the TextWrapping property to Wrap


            // Add the TextBlock to the Border element
            messageTextBorder.Child = messageTextBlock;


            // Add the Ellipse and Border elements to the Grid element
            messageBubble.Children.Add(avatarCircle);
            messageBubble.Children.Add(messageTextBorder);

            // Add the Grid element to the messageStackPanel
            messageStackPanel.Children.Add(messageBubble);

            ScrollViewer.ScrollToBottom();
        }
        //——————————————————————————————————————
        //————————这里需要修改——————————————
        public void ReplyMessage(string s)
        {
            string messageText = s;//这里需要修改！！！！！
            NewMessageBubble(false, messageText);
        }
        string added="";
        private void SendMessage(object sender, RoutedEventArgs e)
        {
            string messageText = "";
            messageText = added+messageInput.Text;//这个是message，可以被读取

            NewMessageBubble(true, messageText);
            OnMessageReceived(messageText,2);
            // Clear the input TextBox
            messageInput.Text = "";
            added = "";

        }
        private void SendMessage()
        {
            string messageText = "";
            messageText = added + messageInput.Text;//这个是message，可以被读取

            NewMessageBubble(true, messageText);
            OnMessageReceived(messageText, 2);
            // Clear the input TextBox
            messageInput.Text = "";
            added = "";

        }
        private void ClearMessages_Click(object sender, RoutedEventArgs e)
        {
            messageStackPanel.Children.Clear();
            ChatHistoryFile.Delete();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            added = "清空备忘录";
            SendMessage();
            added = "";
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //直接打开可视化界面
            NotesWindow notesWindow = new NotesWindow();
            //notesWindow.tmpstr = getM("查询备忘录");
            //Console.WriteLine(notesWindow.tmpstr);
            notesWindow.Show();
            added = "查询备忘录";
            SendMessage();
            added = "";
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            added = "添加备忘录：";
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            added = "删除备忘录：";
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            added = "完成备忘录："; 
        }
    }
}
