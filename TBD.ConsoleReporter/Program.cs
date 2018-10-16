using System;
using System.Data;
using System.IO;
using TBD.Core;
using TBD.Logging;
using TBD.Model;
using TBD.PdfReporter;

namespace TBD.ConsoleReporter
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.Initalize( "C:\\src\\tbd.log" );

            Log.Message( "------- Console reporter started -------" );

            CSequentialPipeline pipeline = InitalizePipeline();
            PrintToConsoleReportFromPipeline( pipeline );

            CPdfReportFromPipelineGenerator generator = new CPdfReportFromPipelineGenerator( pipeline );
            generator.GeneratePdfReport();

        }
        
        static CSequentialPipeline InitalizePipeline()
        {
            CSequentialPipeline pipeline = new CSequentialPipeline();

            pipeline.AppendInPipeline(  new CDicomFileLoader() );
            pipeline.AppendInPipeline( new CImageSeriesVolumeCalculator() );

            return pipeline;
        }

        static void PrintToConsoleReportFromDicomLoader()
        {
            CDicomFileLoader fileLoader = new CDicomFileLoader();
            DataTable table = fileLoader.GetDataSource( CFileLoaderDataSourceOptions.CreateCommonReportOptions() )
                .TryGetAs<DataTable>();
            String userFriendlyFileReport = SDataTablePrinter.GetPrettyDataTable( table );

            Console.WriteLine( userFriendlyFileReport );
        }

        static void PrintToConsoleReportFromPipeline( CSequentialPipeline pipeline )
        {
            DataTable resultTable = pipeline.GetPipelineResult().TryGetAs<DataTable>();
            String userFriendlyDataTableRepr = SDataTablePrinter.GetPrettyDataTable( resultTable );

            Console.WriteLine(userFriendlyDataTableRepr);
        }
    }

}
