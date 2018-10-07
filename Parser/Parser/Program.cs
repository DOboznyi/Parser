using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"D:\3.htm";
            Parser parser = new Parser(path, @"D:\out");            
            Console.ReadKey();
        }
    }
}
