using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp19
{
    public class Memorandum
    {
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
        /* string timeFilePath;*/
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
            if(i<events.Count)
            return  events[i];
            return " ";
        }
        public Memorandum()
        {
            incompleted = new List<string>();
            completed = new List<string>();
            incompletedTime = new List<DateTime?>();
            completedTime = new List<DateTime>();
            inFilePath = "incompleted.txt";
            filePath = "completed.txt";
            /*timeFilePath = "completedTime.txt";*/
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
            if (isExists(timeinFilePath))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    if(line!="-")
                    incompletedTime.Add(DateTime.Parse(line));
                }
            }
            /*if (isExists(timeFilePath))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    incompletedTime.Add(DateTime.Parse(line));
                }
            }*/
            if (reader!=null)reader.Close();
        }

        public void addElem(string elem)
        {
            incompleted.Add(elem);
            var dt = getSetTime(elem);
            File.AppendAllText(inFilePath, elem + Environment.NewLine);
            if(dt!=null)
            File.AppendAllText(timeinFilePath, dt + Environment.NewLine);
            else File.AppendAllText(timeinFilePath, "-" + Environment.NewLine);
            incompletedTime.Add(dt);
            events.Add(myevent);
            save();
        }
        /*public void addElem(string elem, string time)
        {
            incompleted.Add(elem);
            DateTime dt = DateTime.Parse(time);
            incompletedTime.Add(dt);
            File.AppendAllText(inFilePath, elem + Environment.NewLine);
            File.AppendAllText(timeinFilePath, dt + Environment.NewLine);
        }*/
        public void taskCompleted(string elem)
        {
            for (int i = 0; i < incompleted.Count; i++)
            {
                if (incompleted[i] == elem)
                {
                    completed.Add(incompleted[i]);
                    incompleted.RemoveAt(i);
                    incompletedTime.RemoveAt(i);
                    events.RemoveAt(i);
                    break;
                }
            }
            save();
        }
        public void taskCompleted(int i)
        {
            completed.Add(incompleted[i]);
            incompletedTime.RemoveAt(i);
            incompleted.RemoveAt(i);
            save();
        }
        public void delElem(int i)
        {
            if (i < incompleted.Count)
            {
                incompletedTime.RemoveAt(i);
                incompleted.RemoveAt(i);
            }
            else completed.RemoveAt(i - incompleted.Count);
           save();
        }
        public void delElem(string elem)
        {
            for (int i = 0; i < incompleted.Count; i++)
            {
                if (incompleted[i] == elem)
                {
                    incompleted.RemoveAt(i);
                    incompletedTime.RemoveAt(i);
                    events.RemoveAt(i);
                    break;
                }
            }
            for (int i = 0; i < completed.Count; i++)
            {
                if (completed[i] == elem)
                {
                    completed.RemoveAt(i);
                    completedTime.RemoveAt(i);
                    break;
                }
            }
            save();
        }
        public string getCom()
        {
            StringBuilder sb = new StringBuilder();
            int n = incompleted.Count;  
            for (int i = 0; i < completed.Count; i++)
            {
                sb.Append("  ").Append(i + n).Append(".").Append(completed[i]).Append("\n");
            }
            return sb.ToString();
        }
        public string getIncom()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < incompleted.Count; i++)
            {
                sb.Append("  ").Append(i + 1).Append("、").Append(incompleted[i]).Append("\n");
                if (incompletedTime[i] != null) sb.Append("     设置完成时间:").Append(incompletedTime[i]);
                sb.Append("\n");
            }
            return sb.ToString();
        }
        public void clearAll()
        {
            completed.Clear();
            incompleted.Clear();
            completedTime.Clear();
            incompletedTime.Clear();
            events.Clear();
            save();
        }
        public void save()
        {
            File.WriteAllText(filePath, "");
            File.WriteAllText(inFilePath, "");
            File.WriteAllText(timeinFilePath, "");
            for (int i = 0; i < completed.Count; i++)
            {
                File.AppendAllText(filePath, completed[i] + Environment.NewLine);
                
            }
            for (int i = 0; i < incompleted.Count; i++)
            {
                File.AppendAllText(inFilePath, incompleted[i] + Environment.NewLine);
                if (incompletedTime[i] != null)
                    File.AppendAllText(timeinFilePath, incompletedTime[i] + Environment.NewLine);
                else File.AppendAllText(timeinFilePath, "-" + Environment.NewLine);
            }
        }
        public void printAll()
        {
            Console.WriteLine("incompleted:");

            for (int i = 0; i < incompleted.Count; i++)
            {
                Console.WriteLine(incompleted[i]);

            }
            Console.WriteLine("completed:");
            for (int i = 0; i < completed.Count; i++)
            {
                Console.WriteLine(completed[i]);
            }
        }
        ~Memorandum()
        {

        }
        private static DateTime GetNextDateTime(DateTime now, DayOfWeek targetDayOfWeek, int daysToAdd)
        {
            int currentDayOfWeek = (int)now.DayOfWeek;
            int targetDayOfWeekValue = (int)targetDayOfWeek;
            int daysToTargetDay = (targetDayOfWeekValue - currentDayOfWeek + 7) % 7;
            if (daysToTargetDay == 0)
            {
                daysToTargetDay = 0;
            }
            return now.AddDays(daysToAdd + daysToTargetDay);
        }
        Dictionary<string, int> dayModifiers = new Dictionary<string, int>
        {
            { "今天", 0 },
            { "明天", 1 },
            { "后天", 2 },
            { "大后天", 3 }
        };

        Dictionary<string, DayOfWeek> dayOfWeekPhrases = new Dictionary<string, DayOfWeek>
        {
            { "本周一", DayOfWeek.Monday },
            { "本周二", DayOfWeek.Tuesday },
            { "本周三", DayOfWeek.Wednesday },
            { "本周四", DayOfWeek.Thursday },
            { "本周五", DayOfWeek.Friday },
            { "本周六", DayOfWeek.Saturday },
            { "本周日", DayOfWeek.Sunday },
            { "下周一", DayOfWeek.Monday },
            { "下周二", DayOfWeek.Tuesday },
            { "下周三", DayOfWeek.Wednesday },
            { "下周四", DayOfWeek.Thursday },
            { "下周五", DayOfWeek.Friday },
            { "下周六", DayOfWeek.Saturday },
            { "下周日", DayOfWeek.Sunday }
        };
        public DateTime? getSetTime(string input)
        {
            DateTime now = DateTime.Now;
            DayOfWeek currentDayOfWeek = now.DayOfWeek;

            myevent = " ";
            string pattern = @"(?i)(\b明天|\b后天|\b大后天)?(\b本周|\b下周)?([一二三四五六日周])?(早上|上午|下午|晚上)?(\d+)点(\d+)?(分(钟)?)?";
            Match match = Regex.Match(input, pattern);
            if (match.Success)
            {
                string dayModifier = match.Groups[1].Value;
                string weekModifier = match.Groups[2].Value;
                string dayOfWeekPhrase = match.Groups[3].Value;
                string timePhrase = match.Groups[4].Value;
                string hour = match.Groups[5].Value;
                string minute = match.Groups[6].Value;
                for(int i= 0; i < match.Groups.Count; i++)
                {
                    if((match.Groups[i].Value).Length>=2)
                        myevent = match.Groups[i].Value;
                }
                int daysToAdd = 0;
                if (!string.IsNullOrEmpty(dayModifier) && dayModifiers.ContainsKey(dayModifier))
                {
                    daysToAdd = dayModifiers[dayModifier];
                }

                DayOfWeek targetDayOfWeek = currentDayOfWeek;
                if (!string.IsNullOrEmpty(weekModifier) && dayOfWeekPhrases.ContainsKey(weekModifier + dayOfWeekPhrase))
                {
                    targetDayOfWeek = dayOfWeekPhrases[weekModifier + dayOfWeekPhrase];
                }
                else if (!string.IsNullOrEmpty(dayOfWeekPhrase))
                {
                    targetDayOfWeek = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), dayOfWeekPhrase, true);
                }

                int hours = now.Hour;
                if (!string.IsNullOrEmpty(hour))
                {
                    hours = int.Parse(hour);
                }
                if (timePhrase == "下午" || timePhrase == "晚上")
                {
                    hours += 12;
                }
                int minutes = 0;
                if (!string.IsNullOrEmpty(minute))
                {
                    minutes = int.Parse(minute);
                }
                DateTime parsedDateTime = GetNextDateTime(now, targetDayOfWeek, daysToAdd).Date.AddHours(hours).AddMinutes(minutes);
                return parsedDateTime;
            }
            match = Regex.Match(input, @"(\d+)(个)?(半)?((小时)?|(分钟)?|(秒钟)?)(\d+)?((小时)?|(分钟)?|(秒(钟)?)?)后([\u4E00-\u9FFF]+)?");
            if (match.Success)
            {
                int value = int.Parse(match.Groups[1].Value);
                string unit = match.Groups[4].Value;
                int value2 = 0;
                if (!string.IsNullOrEmpty(match.Groups[8].Value))
                    value2 = int.Parse(match.Groups[8].Value);
                string unit2 = match.Groups[9].Value;
                string unit3 = match.Groups[10].Value;
                string unit24 = match.Groups[11].Value;
                string unit25 = match.Groups[12].Value;
                string unit26 = match.Groups[13].Value;
                string unit262 = match.Groups[14].Value;
                string unit263 = match.Groups[15].Value;
                string unit264 = match.Groups[16].Value;
                string unit265 = match.Groups[17].Value;
                string unit266 = match.Groups[18].Value;
                for (int i = 0; i < match.Groups.Count; i++)
                {
                    if ((match.Groups[i].Value).Length >= 2)
                        myevent = match.Groups[i].Value;
                }
                DateTime dt = DateTime.Now;
                if (unit == "小时")
                {
                    if (input.Contains("半"))
                    {
                        dt = dt.AddHours(value).AddMinutes(30);
                    }
                    else
                    {
                        dt = dt.AddHours(value);
                    }
                }

                if (unit == "分钟")
                    dt = dt.AddMinutes(value);

                if (unit == "秒钟")
                    dt = dt.AddSeconds(value);
                if (!string.IsNullOrEmpty(unit2))
                {
                    if (unit2 == "分钟")
                    {
                        if (value2 != 0)
                            dt = dt.AddMinutes(value2);
                        else dt = dt.AddMinutes(value);
                    }
                    if (unit2 == "秒钟")
                    {
                        if (value2 != 0)
                            dt = dt.AddSeconds(value2);
                        else dt = dt.AddSeconds(value);
                    }
                }
                return dt;
            }
            return null;
        }
    }
}
