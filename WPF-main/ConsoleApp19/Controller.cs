using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp19
{
    internal class Controller
    {
        SystemTool st;
        Memorandum m;
        public Controller()
        {
            st = new SystemTool();
            m = new Memorandum();
        }
        public string getMemoryInf()
        {
            StringBuilder sb = new StringBuilder("总内存：");
            sb.Append(st.totalMemory).Append("  ").Append("剩余内存：").Append(st.getAvailableMemory());
            return sb.ToString();
        }
    }
}
