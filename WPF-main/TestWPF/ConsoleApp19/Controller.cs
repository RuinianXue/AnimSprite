using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
//using System.Threading;
using System.Threading.Tasks;
using System.Timers;
namespace ConsoleApp19
{
    public struct requestInfo
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
                if (s.Contains("内存")|| s.ToLower().Contains("cpu")||s.Contains("壁纸"))
                {
                    op = 1;
                }
            }
        }
    }
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
        private string setNextWallpaperGallerys()
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
        public string getResponds(requestInfo ri)
        {
            int op = ri.op;
            string request = ri.request;
            int mop = ri.mop;
            switch (op)
            {
                case 1:
                    if (request.Contains("内存"))
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append("总内存：").Append(totalMemory).Append("G\n").Append("可用内存：").Append(_availableMemory).Append("G。");
                        return sb.ToString();
                    }
                    if (request.ToLower().Contains("cpu"))
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append("cpu利用率：").Append(_cpuUsage).Append("%\n").Append("cpu温度：").Append(_temperature).Append("℃。");
                        return sb.ToString();
                    }
                    if (request.Contains("壁纸"))
                    {
                        return setNextWallpaperGallerys();
                    }
                    break;
                case 2:
                    if (mop == 1)
                    {
                        m.addElem(request);
                        return request + "已添加";
                    }else if(mop == 2)
                    {
                        m.delElem(request);
                        return request + "已删除";
                    }
                    else if(mop == 3)
                    {
                        m.taskCompleted(request);
                        return request + "已完成";
                    }
                    else if (mop == 4)
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append("未完成：\n");
                        sb.Append(m.getIncom());
                        sb.Append("已完成：\n");
                        sb.Append(m.getCom());
                        return sb.ToString();
                    }
                    else if(mop == 5)
                    {
                        m.clearAll();
                        return "已完成备忘录清空";
                    }
                    break;
                default:
                    return request;
                    break;
            }
            return request;
        }
    }
}
