using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_With_Parameters
{
    class Program
    {
        static  string func(object s)
        {
            try
            {
                Console.WriteLine(s.ToString());    
                return s.ToString();

            }
            catch (Exception ex)
            {

                return ex.Message;
            }
            
        }
            static void Main(string[] args)
        {
            int i = 0;i++;
            List<Task<string>> taskList = new List<Task<string>>();
            for (int j = 0; j < 10; j++)
            {
                taskList.Add(Task.Factory.StartNew(func, j, TaskCreationOptions.LongRunning));
                
               //taskList.Add( Task.Run<string>(() =>  func(j) ));         
            }
            Task.WaitAll(taskList.ToArray());
            foreach (var item in taskList)
            {
                Console.WriteLine(item.Result);
            }
            i++;
            Console.ReadKey();
        }
    }
}
