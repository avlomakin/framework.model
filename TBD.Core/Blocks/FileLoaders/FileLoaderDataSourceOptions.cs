using System;
using System.Collections.Generic;
using TBD.Model;

namespace TBD.Core
{
    public class CFileLoaderDataSourceOptions : CDataSourceOptions
    {
        public readonly IReadOnlyCollection<Guid> SeriesGuidsOrNull;

        public readonly Boolean IsReport;

        public CFileLoaderDataSourceOptions( Type preferableType, IReadOnlyCollection<Guid> seriesGuidsOrNull, Boolean isReport ) : base( preferableType )
        {
            SeriesGuidsOrNull = seriesGuidsOrNull;
            IsReport = isReport;
        }

        public static CFileLoaderDataSourceOptions CreateCommonReportOptions()
        {
            return new CFileLoaderDataSourceOptions( typeof(CFileLoadFilesReport), null, true );
        }

        public static CFileLoaderDataSourceOptions CreateSeriesOptions(Guid id)
        {
            return new CFileLoaderDataSourceOptions( typeof(CFileLoadFilesReport), new[] {id}, true );
        }
    }
}