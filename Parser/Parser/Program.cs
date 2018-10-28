using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fvi.ProductVersion;
            string shortV = version.Substring(0, version.Length - 33);
            Console.WriteLine("Program version "+ shortV);
            Console.WriteLine("Hello in zakon parcer. You need to enter the path to you *.htm law. Please note, I can parce laws only from http://zakon.rada.gov.ua/laws/file/2341-14. Edition must be from 2015 till now.");
            Console.WriteLine("Your path with *.htm file: ");
            string path = @Console.ReadLine();
            Console.WriteLine("Your path: ");
            string outpath = @Console.ReadLine();
            Parser parser = new Parser(path);
            Collector collector = new Collector(parser.getData(), outpath);
            Console.ReadKey();
        }
    }
}
