using System;
using System.Data;
using System.IO;
using DinkToPdf;
using TBD.Core;
using TBD.Logging;
using TBD.Model;

namespace TBD.PdfReporter
{

    public class CPdfReportFromPipelineGenerator
    {
        //TODO: (avlomakin) add supprot for all types of pipelines, Sequential for now
        private readonly CSequentialPipeline _pipeline;

        //TODO: (avlomakin) probably will get from UI , hardcode for now
        public String OutputDirectory = "C:\\src\\reports";

        public CPdfReportFromPipelineGenerator( CSequentialPipeline pipeline )
        {
            _pipeline = pipeline;
        }

        public void GeneratePdfReport()
        {
            Log.Message( "[PdfReportFromPipelineGenerator] generating pdf report" );

            String html = GetPipelineAsHtml();
           
            BasicConverter converter = new BasicConverter(new PdfTools());

            HtmlToPdfDocument doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4Plus,
                    Out = Path.Combine( OutputDirectory, "test.pdf" )
                },

                Objects = {
                    new ObjectSettings() {
                        HtmlContent = html,
                        WebSettings = { DefaultEncoding = "utf-8" },
                        HeaderSettings = { FontSize = 9, Right = "Page [page] of [toPage]", Line = true, Spacing = 2.812 }
                    }
                }
            };

            converter.Convert( doc );
        }

        private String GetPipelineAsHtml()
        {
            //TODO: (avlomakin) dataTable only, replace with TryGetAs<something that can give html>()

            String result = String.Empty;
            foreach (IBlock pipelineBlock in _pipeline.Blocks)
            {
                DataTable table = pipelineBlock.GetDataSource( new CDataSourceOptions( typeof(DataTable) ) ).TryGetAs<DataTable>();
                CDataTableHtmlExporter dataTableExporter = new CDataTableHtmlExporter( table );
                result += dataTableExporter.GetHtml();
            }

            return result;
        }
    }
}