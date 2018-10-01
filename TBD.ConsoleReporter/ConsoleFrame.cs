using System;
using TBD.Model;

namespace TBD.ConsoleReporter
{
    public class CConsoleFrame : IFrame
    {
        public void DisplayReport( ITableReport report )
        {
            Console.WriteLine(report.GetAs<String>());
        }
    }
}