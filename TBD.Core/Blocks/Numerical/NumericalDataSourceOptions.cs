using System;
using System.Collections.Generic;
using TBD.Model;

namespace TBD.Core
{
    public class NumericalDataSourceOptions : CDataSourceOptions
    {
        public readonly IReadOnlyCollection<Guid> SeriesGuidsOrNull;

        public readonly Boolean IsReport;

        public NumericalDataSourceOptions( Type preferableType, IReadOnlyCollection<Guid> seriesGuidsOrNull, Boolean isReport ) : base( preferableType )
        {
            SeriesGuidsOrNull = seriesGuidsOrNull;
            IsReport = isReport;
        }

        public static CFileLoaderDataSourceOptions CreateReportOptions()
        {
            return new CFileLoaderDataSourceOptions( typeof(CFileLoadFilesReport), null, true );
        }

        public static CFileLoaderDataSourceOptions CreateSeriesOptions(Guid id)
        {
            return new CFileLoaderDataSourceOptions( typeof(CFileLoadFilesReport), new[] {id}, true );
        }
    }
}