using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp19
{
    internal class Memorandum
    {
        List<string> incompleted;
        List<string> completed;
        string filePath;
        string inFilePath;
        public Memorandum()
        {
            incompleted = new List<string>();
            completed = new List<string>();
            inFilePath = "incompleted.txt";
            filePath = "completed.txt";
            if (!File.Exists(filePath)) new FileInfo(filePath);
            else
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        completed.Add(line);
                    }
                }
            }
            if (!File.Exists(inFilePath)) new FileInfo(inFilePath);
            else
            {
                using (StreamReader reader = new StreamReader(inFilePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        incompleted.Add(line);
                    }
                }
            }
        }
        public void addElem(string elem)
        {
            incompleted.Add(elem);
            File.AppendAllText(inFilePath,elem + Environment.NewLine);
        }
        public void taskCompleted(string elem)
        {
            for(int i = 0; i < incompleted.Count; i++)
            {
                completed.Add(incompleted[i]);
                incompleted.RemoveAt(i);
            }
            save();
        }
        public void clearAll()
        {
            completed.Clear();
            incompleted.Clear();
        }
        public void save()
        {
            File.WriteAllText(filePath, "");
            File.WriteAllText(inFilePath, "");
            for (int i = 0; i < completed.Count; i++)
            {
                File.AppendAllText(filePath, completed[i] + Environment.NewLine);
            }
            for (int i = 0; i < incompleted.Count; i++)
            {
                File.AppendAllText(inFilePath, completed[i] + Environment.NewLine);
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
        ~Memorandum(){
           
        }
    }
}
