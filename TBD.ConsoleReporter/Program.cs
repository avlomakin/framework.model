using System;
using System.Data;
using System.IO;
using TBD.Core;
using TBD.Logging;
using TBD.Model;

namespace TBD.ConsoleReporter
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.Initalize( "C:\\src\\tbd.log" );

            Log.Message( "------- Console reporter started -------" );
            
            CSequentialPipeline pipeline = new CSequentialPipeline();

            pipeline.AppendInPipeline( new CDicomFileLoader() );

            pipeline.AppendInPipeline( new CImageSeriesVolumeCalculator() );

            DataTable resultTable = pipeline.GetPipelineResult().TryGetAs<DataTable>();

            String userFriendlyDataTableRepr = SDataTablePrinter.GetPrettyDataTable( resultTable );

            Console.WriteLine(userFriendlyDataTableRepr);
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
