using Easycode.DEncrypt;
using System;
using System.Text;

namespace Easycode.Common.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var str = "Program";


            var ab = new { a = "Program", b = 1 };

            var pass = MD5Cipher.Encrypt(ab.a);

            Console.ReadKey();
        }



    }
}
