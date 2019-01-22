using Easycode.DEncrypt;
using System;
using System.Text;

namespace Easycode.Common.Test
{
    class Program
    {
        static void Main(string[] args)
        {

            for (int i = 0; i < 200; i++)
            {
                Console.WriteLine("TickCount:{0}", RandomHelper.RandString(RandomHelper.LowerCase, 6));
            }


            Console.ReadKey();
        }



    }
}
