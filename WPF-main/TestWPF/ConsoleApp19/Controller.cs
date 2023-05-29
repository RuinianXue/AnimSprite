using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
//using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Configuration;
using System.Reflection;

namespace ConsoleApp19
{
    /*public struct requestInfo
    {
        public int op;
        public string request;
        public int mop ;
        public requestInfo(int o,string r,int m)
        {
            op = o;
            request = r;
            mop = m;
        }
       
        public requestInfo(string s)
        {
            List<string> key1;//1级key
            List<string> key2;//2级key;
            if(s == null)
            {
                op = -1;
                request = " ";
                mop = -1;
            }
            foreach(string key in ConfigurationManager.AppSettings.Keys)
            {
                if (s.Contains(key))
                {
                    op = int.Parse(ConfigurationManager.AppSettings.GetValues(key)[0]);
                    if (key != "备忘录")
                    {
                        break;
                    }
                }
            }
            *//*
            if (s.Contains("备忘录"))
            {
                op = 2;
                try {
                    string[] parts = s.Split('：');
                    request = parts[1].Trim();
                }
                catch {
                    op = 0;
                    request =  "错误备忘录指令"; }

                if (s.Contains("添加"))
                {
                    mop = 1;
                }
                else if (s.Contains("删除"))
                {
                    mop = 2;
                }
                else if (s.Contains("完成"))
                {
                    mop = 3;
                }
                else if (s.Contains("查询"))
                {
                    op = 2;
                    mop = 4;
                }
                else if (s.Contains("清空"))
                {
                    op = 2;
                    mop = 5;
                }
                else mop = 0;
            }else
            {
                request = s;
                mop = 0;
                op = 3;
                op =int.Parse(ConfigurationManager.AppSettings["Name"]);
                if (s.Contains("内存")|| s.ToLower().Contains("cpu")||s.Contains("壁纸"))
                {
                    op = 1;
                }
            }*//*
        }
    }*/
    public class Controller
    {
        //事件，提醒功能，一旦过高，调用桌面精灵的气泡显示
        static Timer timer;
        SystemTool st;
        Memorandum m;
        public event Action<string> throwRequest;
        public double totalMemory { get; }//总内存
        private double _cpuUsage;
        public double cpuUsage { get { return _cpuUsage; } }
        private double _availableMemory;
        public double availableMemory { get { return _availableMemory; } }
        private double _temperature;
        public double temperature { get { return _temperature; } }
        private string[] wallpaperGallerys;
        private int wallpaperGallerysID;//壁纸标号

        int op;
        string request;
        int mop ;
        public Controller()
        {
            st = new SystemTool();
            m = new Memorandum();
            totalMemory = st.totalMemory;
            wallpaperGallerysID = 0;
        }
        /* public void updataInfo()
         {
             timer = new Timer(1000);
             timer.Elapsed += setInfoEvent;
         }*/
        public void monitorStatus(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            if (_availableMemory < 5)
            {
                sb.Append("当前总内存：").Append(totalMemory).Append("G\n").Append("可用内存：").Append(_availableMemory).Append("G，内存使用有些过高\n");               
            }
            if (_temperature > 70)
            {
                sb.Append("当前cpu温度：").Append(_temperature).Append("℃，温度有些过高");
            }
            if(sb.Length > 0)
            throwRequest(sb.ToString());
        }
        public string setNextWallpaperGallerys()
        {
            try
            {
                wallpaperGallerys = st.getWallpaperGallerys();
                st.setWallpaper(wallpaperGallerys[wallpaperGallerysID]);
                wallpaperGallerysID = (wallpaperGallerysID + 1) < wallpaperGallerys.Count() ? wallpaperGallerysID + 1 : 0;
                return "更换壁纸成功";
            }
            catch
            {
                wallpaperGallerysID = 0;
                return "更换壁纸失败，请重试";
            }
        }
        public void setInfoEvent(object state)
        {
                _cpuUsage = st.getCPUusage();
                _availableMemory = st.getAvailableMemory();
                _temperature = st.getTemperature();             
        }
        public string getMemory()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("总内存：").Append(totalMemory).Append("G\n").Append("可用内存：").Append(_availableMemory).Append("G。");
            return sb.ToString();
        }
        public string getcpu()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("cpu利用率：").Append(_cpuUsage).Append("%\n").Append("cpu温度：").Append(_temperature).Append("℃。");
            return sb.ToString();
        }
        public string m_add()
        {
            m.addElem(request);
            return request + "已添加";
        }
        public string m_del()
        {
            m.delElem(request);
            return request + "已删除";
        }
        public string m_change()
        {
            m.taskCompleted(request);
            return request + "已完成";
        }
        public string m_find()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("未完成：\n");
            sb.Append(m.getIncom());
            sb.Append("已完成：\n");
            sb.Append(m.getCom());
            return sb.ToString();
        }
        public string m_clear()
        {
            m.clearAll();
            return "已完成备忘录清空";
        }


        public string getResponds(string s)
        {
            if (s == null) return " ";
            string[] keys = ConfigurationManager.AppSettings.AllKeys;
            foreach (string key in keys)
            {
                if (s.Contains(key))
                {
                   // return ConfigurationManager.AppSettings[key];
                    MethodInfo methodInfo = this.GetType().GetMethod(ConfigurationManager.AppSettings[key]);
                    if (key.Contains("添加备忘录")|| key.Contains("完成备忘录")|| key.Contains("删除备忘录"))
                    {
                        try
                        {
                            string[] parts = s.Split('：');
                            request = parts[1].Trim();
                        }
                        catch
                        {
                            return " ";
                        }
                    }
                    if (methodInfo != null) return methodInfo.Invoke(this, null).ToString();
                    else return s;
                }
            }
            return s;
        }
    }
}
