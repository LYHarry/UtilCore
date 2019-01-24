using System;
using System.IO;
using System.Text;

namespace Easycode.Common.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var ext = Path.GetExtension("abc.txt").ToLower();
            Console.WriteLine(ext);


            Console.ReadKey();
        }



    }
}
