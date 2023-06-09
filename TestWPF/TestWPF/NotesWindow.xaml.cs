using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
using static System.Net.Mime.MediaTypeNames;
using TestWPF.Resources;
using System.IO;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;

namespace TestWPF
{
    /// <summary>
    /// NotesWindow.xaml 的交互逻辑
    /// </summary>
    /// 

    public static class NotesHistoryFile
    {
        private const string FileName = "note_history.txt";
        public static void Save(string message)
        {
            using (var writer = new StreamWriter(FileName, true))
            {
                writer.WriteLine(message);
            }
        }
        public static void Delete()
        {
            File.WriteAllText("note_history.txt", string.Empty);
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
    public partial class NotesWindow : Window
    {
        public string tmpstr { get; set; }
        List<string> incompleted;
        List<string> completed;
        string filePath;
        string inFilePath;
        List<string> events;
        string myevent;
        public List<DateTime?> getTime { get { return incompletedTime; } }
        //public List<string> getEvents { get { return events; } }
        List<DateTime?> incompletedTime;
        List<DateTime> completedTime;
        string eventsFilePath;
        string timeinFilePath;
        StreamReader reader;
        bool isExists(string path)
        {
            if (!File.Exists(path))
            {
                new FileInfo(path);
                return false;
            }
            reader = new StreamReader(path);
            return true;
        }
        public string getEvent(int i)
        {
            if (i < events.Count)
                return events[i];
            return " ";
        }
        public NotesWindow()
        {
            InitializeComponent();
            //ExtractEventAndTime(tmpstr);
            incompleted = new List<string>();
            completed = new List<string>();
            incompletedTime = new List<DateTime?>();
            completedTime = new List<DateTime>();
            inFilePath = "incompleted.txt";
            filePath = "completed.txt";
            eventsFilePath = "events.txt";
            timeinFilePath = "incompletedTime.txt";
            string line;
            DateTime dt;
            events = new List<string>();
            if (isExists(filePath))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    completed.Add(line);
                }
            }
            if (isExists(inFilePath))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    incompleted.Add(line);
                }
            }
            if (isExists(eventsFilePath))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    events.Add(line);
                }
            }
            if (isExists(timeinFilePath))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    if (line != "-")
                        incompletedTime.Add(DateTime.Parse(line));
                    else incompletedTime.Add(null);
                }
            }
            /*if (isExists(timeFilePath))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    incompletedTime.Add(DateTime.Parse(line));
                }
            }*/
            if (reader != null) reader.Close();
            for (int i = 0; i < incompleted.Count; i++)
            {
                DateTime tmp = (DateTime) incompletedTime[i];
                CreateNewNote(incompleted[i],tmp, false);
            }
            for (int i = 0; i < completed.Count; i++)
            {
              //  DateTime tmp = (DateTime) completedTime[i];
                CreateNewNote(completed[i], DateTime.Now, true);
            }

        }
        public void LoadNotesHistoryFromFile()
        {
            string notesHistory = NotesHistoryFile.Load();
            if (!string.IsNullOrEmpty(notesHistory))
            {
                string tmpmssage = "";
                int num = 0;
                string[] messages = notesHistory.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string message in messages)
                {
                    tmpmssage = message;
                    Match timeMatch = Regex.Match(tmpmssage, @"\[Time\](.+?)\[");
                    Match completedMatch = Regex.Match(tmpmssage, @"\[Completed\](.+?)\[");
                    Match messageMatch = Regex.Match(tmpmssage, @"\[Message\](.+)");

                    // Parse the extracted information into the required variables
                    DateTime time = DateTime.ParseExact(timeMatch.Groups[1].Value, "yyyy/M/d H:mm:ss", CultureInfo.InvariantCulture);
                    bool completed = completedMatch.Groups[1].Value == "Yes";
                    string messageText = messageMatch.Groups[1].Value;
                    Console.WriteLine("+++++++++++");
                    Console.WriteLine(tmpmssage);
                    Console.WriteLine(time);
                    Console.WriteLine(completed);
                    Console.WriteLine(messageText);
                }
            }
        }

        public void ExtractEventAndTime(string input)
        {
            string[] lines = input.Split('\n');
            foreach (string line in lines)
            {
                int index = line.IndexOf("点");
                string eventString = line.Substring(index + 1);
                string timeString = line.Substring(line.IndexOf(":") + 1);
                DateTime completionTime = DateTime.Parse(timeString);
                //Console.WriteLine("Event: " + eventString + ", Completion Time: " + completionTime);
                CreateNewNote(eventString, completionTime,true);
            }
        }

        public string GetNoteAtIndex(int index)
        {
            string notesHistory = NotesHistoryFile.Load();
            if (!string.IsNullOrEmpty(notesHistory))
            {
                string[] messages = notesHistory.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                int currentIndex = 0;
                foreach (string message in messages)
                {
                    Match timeMatch = Regex.Match(message, @"\[Time\](.+?)\[");
                    Match completedMatch = Regex.Match(message, @"\[Completed\](.+?)\[");
                    Match messageMatch = Regex.Match(message, @"\[Message\](.+)");

                    // Parse the extracted information into the required variables
                    DateTime time = DateTime.ParseExact(timeMatch.Groups[1].Value, "yyyy/M/d H:mm:ss", CultureInfo.InvariantCulture);
                    bool completed = completedMatch.Groups[1].Value == "Yes";
                    string messageText = messageMatch.Groups[1].Value;

                    if (currentIndex == index)
                    {


                        return messageText;
                    }

                    currentIndex++;
                }
            }
            return null;
        }
        public void UpdateNoteAtIndex(int index, DateTime time, bool completed) //
        {
            string messageText = GetNoteAtIndex(index);

            string timeString = time.ToString("yyyy/M/d H:mm:ss");
            string completedString = completed ? "Yes" : "No";
            string data = $"[Index]{index}[/Index]{Environment.NewLine}" +
                          $"[Time]{timeString}[/Time]{Environment.NewLine}" +
                          $"[Completed]{completedString}[/Completed]{Environment.NewLine}" +
                          $"[Message]{messageText}[/Message]{Environment.NewLine}";

            // Load the existing notes history and split it into individual messages
            string notesHistory = NotesHistoryFile.Load();
            if (!string.IsNullOrEmpty(notesHistory))
            {
                string[] messages = notesHistory.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

                // Replace the message at the specified index with the new data
                if (index >= 0 && index < messages.Length)
                {
                    messages[index] = data;
                }

                // Join the modified messages back into a single string and save it
                notesHistory = string.Join(Environment.NewLine, messages);
                NotesHistoryFile.Save(notesHistory);
            }
        }

        public void RenewNoteHistoryFile()
        {

        }
        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            stackPanel.UpdateLayout();
            grid.UpdateLayout();


        }
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            Grid grid = radioButton.Parent as Grid;
            //StackPanel stackPanel = grid.Parent as StackPanel;
            if (grid == null)
            {
                // 处理异常情况
                return;
            }
            Style style = (Style)FindResource("strikethroughGreyText");
            foreach (var child in grid.Children)
            {
                if (child is TextBlock textBox)
                {
                    textBox.Style = style;
                }
            }
        }
        private void OpenTimePanelWindow()
        {
            TimePanel timePanel = new TimePanel();
            timePanel.Owner = this;

            timePanel.Left = this.Left;
            timePanel.Top = this.Top;
            timePanel.Show();
            DateTime tmpTime;
            tmpTime = timePanel.RecorededTime;


        }
        private void Time_Clicked(object sender, RoutedEventArgs e)
        {
            OpenTimePanelWindow();

            //
        }

        private void Window_DoubleClicked(object sender, RoutedEventArgs e)
        {
            //CreateNewNote();
        }
        private void StackPanel_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            
            int index = stackPanel.Children.IndexOf((StackPanel)sender);

        }
        private void CreateNewNote(string str,DateTime tmptime,bool Completed)
        {
            Grid newGrid = new Grid();
            newGrid.Margin = new Thickness(10, 10, 10, 10);
            newGrid.Width = 450;

            ColumnDefinition column1 = new ColumnDefinition();
            column1.Width = new GridLength(1, GridUnitType.Auto);
            ColumnDefinition column2 = new ColumnDefinition();
            column2.Width = new GridLength(1, GridUnitType.Star);
            ColumnDefinition column3 = new ColumnDefinition();
            column3.Width = new GridLength(1, GridUnitType.Auto);

            newGrid.ColumnDefinitions.Add(column1);
            newGrid.ColumnDefinitions.Add(column2);
            newGrid.ColumnDefinitions.Add(column3);

            RadioButton newRadioButton = new RadioButton();
            newRadioButton.Content = "";
            newRadioButton.IsChecked = Completed;

            newRadioButton.Margin = new Thickness(10, 0, 0, 0);
            newRadioButton.Checked += RadioButton_Checked;
            newRadioButton.HorizontalAlignment = HorizontalAlignment.Center;

            TextBlock newTextBox = new TextBlock();
            newTextBox.Name = "textBox";
            newTextBox.TextWrapping = TextWrapping.Wrap;
            newTextBox.Margin = new Thickness(10, 0, 0, 0);
            newTextBox.KeyDown += TextBox_KeyDown;
            newTextBox.Text = str;
            if (Completed)
            {
                Style style = (Style)FindResource("strikethroughGreyText");
                newTextBox.Style = style;
            }

            Button newbutton = new Button();
            newbutton.Margin = new Thickness(10, 0, 0, 0);
            newbutton.Content = "Time";
            newbutton.Click += Time_Clicked;

            Grid.SetColumn(newRadioButton, 0);
            Grid.SetColumn(newTextBox, 1);
            Grid.SetColumn(newbutton, 2);


            newGrid.Children.Add(newRadioButton);
            newGrid.Children.Add(newTextBox);
            newGrid.Children.Add(newbutton);

            StackPanel newStackPanel = new StackPanel();
            newStackPanel.Orientation = Orientation.Horizontal;
            newStackPanel.Children.Add(newGrid);
            newStackPanel.Width = 500;
            newStackPanel.PreviewMouseDown += StackPanel_PreviewMouseDown;

            stackPanel.Children.Add(newStackPanel);

            //textBox.Text = str;
            newTextBox.Focus();
            ScrollViewerNotes.ScrollToBottom();

        }
    }
}
