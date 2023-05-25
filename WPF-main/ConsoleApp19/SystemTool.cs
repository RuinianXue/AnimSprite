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
    internal class SystemTool
    {
        public string totalMemory { get; }//总内存
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
            totalMemory = ((double)(new Microsoft.VisualBasic.Devices.ComputerInfo().TotalPhysicalMemory)/1073741824).ToString("0.00")+"G";
            searcher = new ManagementObjectSearcher(@"root\WMI", "SELECT * FROM MSAcpi_ThermalZoneTemperature");
            wallpaperFolderPath = System.IO.Directory.GetCurrentDirectory()+@"\newfolder";
            Directory.CreateDirectory(wallpaperFolderPath);
        }
        public string cpuUsage()  //cpu利用率
        {
            cpuCounter.NextValue();
            System.Threading.Thread.Sleep(1000);
            return cpuCounter.NextValue().ToString("0.0")+"%";
        }

        public string getAvailableMemory()  //可用内存，更新较快建议循环更新获取
        {
            return ((double)(Convert.ToInt64(pc.NextValue())) / 1073741824).ToString("0.00") + "G";
        }
        public string GetTemperature()//获取cpu温度
        {
            foreach (ManagementObject obj in searcher.Get())
            {
                double temp = Convert.ToDouble(obj["CurrentTemperature"].ToString());
                return ((temp - 2732) / 10.0).ToString()+ "°C";
            }
            return "null";
        }

        public static void SetWallpaper(string wallpaperFilePath)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);
            key.SetValue("WallpaperStyle", "2");
            key.SetValue("TileWallpaper", "0");
            SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, wallpaperFilePath, SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
        Console.WriteLine("换了一个");
        }
        public void getWallpaperGallery()
        {
            string[] files = Directory.GetFiles(wallpaperFolderPath, "*.*", SearchOption.TopDirectoryOnly)
                             .Where(file => file.ToLower().EndsWith(".jpg") || file.ToLower().EndsWith(".png") || file.ToLower().EndsWith(".bmp") || file.ToLower().EndsWith(".gif"))
                             .ToArray();
            //建议这里加一个线程，用来播放壁纸，爬虫程序？
            int i = 0;
            while (true)
            {
                 SetWallpaper(files[i]);
                i = (i + 1 < files.Count()) ? i + 1 : 0;
                Console.WriteLine(i);
                System.Threading.Thread.Sleep(5000);
            }
        }
    }
}
