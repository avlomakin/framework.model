using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using TBD.Logging;
using TBD.Model;

namespace TBD.Core
{
    public class CFileLoadFilesReport : IDataSource
    {
        List<(CSeriesInfo Info, Int32 TotalLoaded)> _data = new List<(CSeriesInfo info, Int32 totalLoaded)>();

        public void AddSeriesInfo( CAtomicImageSeries series )
        {
            _data.Add( (series.Info, series.Images.Count) );
        }

        public static CFileLoadFilesReport CreateFromAtomicPicts( CAtomicPicts picts )
        {
            CFileLoadFilesReport report = new CFileLoadFilesReport();

            foreach (CAtomicImageSeries series in picts.TryGetAs<IEnumerable<CAtomicImageSeries>>())
                report.AddSeriesInfo( series );

            return report;
        }

        public DataTable GenerateSummary()
        {
            Log.Message( "Generating table" );

            DataTable table = new DataTable();

            DataColumn folder = new DataColumn
            {
                DataType = typeof(String),
                ColumnName = "Folder",
                ReadOnly = true
            };


            DataColumn format = new DataColumn
            {
                DataType = typeof(String),
                ColumnName = "Format",
                ReadOnly = true
            };

            DataColumn count = new DataColumn();
            format.DataType = typeof(Int32);
            format.ColumnName = "Total loaded";
            format.ReadOnly = true;


            table.Columns.Add( folder );
            table.Columns.Add( format );
            table.Columns.Add( count );

            foreach (var series in _data)
            {
                DataRow dataRow = table.NewRow();
                dataRow[ "Folder" ] = series.Info.Folder;
                dataRow[ "Format" ] = series.Info.Extension;
                dataRow[ "Total loaded" ] = series.TotalLoaded;
            }

            return table;
        }

        public T TryGetAs<T>() where T : class
        {
            if (typeof(T) == typeof(DataTable))
                return (T)((Object)GenerateSummary());

            throw new Exception("Unsupported report type");
        }
    }
}