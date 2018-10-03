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

            var fileLoader = new CDicomFileLoader();
            var table = fileLoader.GetDataSource( CFileLoaderDataSourceOptions.CreateCommonReportOptions() ).TryGetAs<DataTable>();

            String userFriendlyFileReport = SDataTablePrinter.GetPrettyDataTable( table );

            Console.WriteLine(userFriendlyFileReport);

            pipeline.AppendInPipeline( fileLoader );

            pipeline.AppendInPipeline( new CImageSeriesVolumeCalculator() );

            DataTable resultTable = pipeline.GetPipelineResult().TryGetAs<DataTable>();

            String userFriendlyDataTableRepr = SDataTablePrinter.GetPrettyDataTable( resultTable );

            Console.WriteLine(userFriendlyDataTableRepr);
        }
    }

}
