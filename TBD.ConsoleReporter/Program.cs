using System;
using System.IO;
using TBD.Logging;

namespace TBD.ConsoleReporter
{
    class Program
    {
        static void Main(string[] args)
        {

            Log.Initalize( "C:\\src\\tbd.log" );
            var b = new B();
            A a = b;
            Console.WriteLine("Hello World!");
        }


        
        public abstract class A
        {
            public static Int32 Id = 1;
        }

        public class B : A
        {
            public new static Int32 Id = 2;
        }

    }

}
