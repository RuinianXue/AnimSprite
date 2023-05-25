using System;
namespace ConsoleApp19
{
    class Test
    {
        public static void runSystemTool()
        {
            SystemTool st = new SystemTool();
            Console.WriteLine(st.totalMemory);
            Console.WriteLine(st.getAvailableMemory());
            Console.WriteLine(st.cpuUsage());
            try { Console.WriteLine(st.GetTemperature()); }
            catch { Console.WriteLine("查询温度需要管理员权限"); }
        }
        public static void runMemorandum()
        {
            Memorandum m = new Memorandum();
            bool bo = true;
            while (bo)
            {

                int op;
                try
                {
                    op = int.Parse(Console.ReadLine());
                    switch (op)
                    {
                        case 1:
                            m.addElem(Console.ReadLine());
                            break;
                        case 2:
                            m.taskCompleted(Console.ReadLine());
                            break;
                        case 3:
                            m.printAll();
                            break;
                        case 4:
                            m.clearAll();
                            break;
                        default:
                            bo = false;
                            break;
                    }
                }
                catch { };
            }
        }
        public static void runbizhi()
        {
            SystemTool st = new SystemTool();
            st.getWallpaperGallery();
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            try { Test.runSystemTool(); }
            catch { }
            Test.runMemorandum();
            Test.runbizhi();
        } 
    }
}
