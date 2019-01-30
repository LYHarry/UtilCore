using Easycode.TimerService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Easycode.Common.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("========== Main ======= 开始 =========");
            //TaskTest();
            TaskTest().Wait();

            Console.WriteLine("========== Main ======= 结束 =========");

            Console.ReadKey();
        }


        public static async Task TaskTest()
        {
            Console.WriteLine("========== TaskTest ======= 开始 =========");
            int count = 0;
            await Task.Run(() =>
              {
                  Console.WriteLine("========== Task.Run1 =========");
                  System.Threading.Thread.Sleep(5000);
                  count = 10;
                  Console.WriteLine("========== Task.Run1 === 结束 ======");
              });
            await Task.Run(() =>
            {
                Console.WriteLine("========== Task.Run2 =========");
                if (count == 10)
                {
                    Console.WriteLine("========== Task.Run2 ==== count:10 =====");
                }
                Console.WriteLine("========== Task.Run2 === 结束 ======");
            });
            Console.WriteLine("========== TaskTest ======= 结束 =========");

            //return Task.CompletedTask;
        }



    }
}
