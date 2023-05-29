using Microsoft.Win32;
using System;

using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace ConsoleApp19
{
    public class SystemTool
    {
        public double totalMemory { get; }//总内存
        
        string wallpaperFolderPath;
        private const int SPI_SETDESKWALLPAPER = 20;
        private const int SPIF_UPDATEINIFILE = 0x01;
        private const int SPIF_SENDWININICHANGE = 0x02;
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);
        private PerformanceCounter cpuCounter;
        private PerformanceCounter pc;
        private ManagementObjectSearcher searcher;
        public SystemTool()
        {
            cpuCounter = new PerformanceCounter("Processor Information", "% Processor Utility", "_Total");
            pc = new PerformanceCounter("Memory", "Available Bytes");
            totalMemory = Math.Round((double)(new Microsoft.VisualBasic.Devices.ComputerInfo().TotalPhysicalMemory) / 1073741824, 2);
            searcher = new ManagementObjectSearcher(@"root\WMI", "SELECT * FROM MSAcpi_ThermalZoneTemperature");
            wallpaperFolderPath = System.IO.Directory.GetCurrentDirectory()+@"\newfolder";
            Directory.CreateDirectory(wallpaperFolderPath);
        }
        public double getCPUusage()  //cpu利用率
        {
            cpuCounter.NextValue();
            System.Threading.Thread.Sleep(1000);
            return  Math.Round(cpuCounter.NextValue(), 2);
        }

        public double getAvailableMemory()  //可用内存，更新较快建议循环更新获取
        {
            return Math.Round((double)(Convert.ToInt64(pc.NextValue())) / 1073741824,2);
        }
        public double getTemperature()//获取cpu温度
        {
            try
            {
                foreach (ManagementObject obj in searcher.Get())
                {
                    double temp = Convert.ToDouble(obj["CurrentTemperature"].ToString());
                    return Math.Round(((temp - 2732) / 10.0), 2);
                    return -1;
                }
            }catch { return -1; }
            return -1;
        }

        public void setWallpaper(string wallpaperFilePath)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);
            key.SetValue("WallpaperStyle", "2");
            key.SetValue("TileWallpaper", "0");
            SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, wallpaperFilePath, SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
            Console.WriteLine("换了一个");
        }
        public string[] getWallpaperGallerys()
        {
            string[] files = Directory.GetFiles(wallpaperFolderPath, "*.*", SearchOption.TopDirectoryOnly)
                             .Where(file => file.ToLower().EndsWith(".jpg") || file.ToLower().EndsWith(".png") || file.ToLower().EndsWith(".bmp") || file.ToLower().EndsWith(".gif"))
                             .ToArray();
            //建议这里加一个线程，用来播放壁纸，爬虫程序？
            return files;
        }
    }
}
