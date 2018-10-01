using System;
using System.Collections.Generic;
using System.Linq;
using TBD.Logging;
using TBD.Model;

namespace TBD.Core
{
    public class VolumeCalculator : IBlock
    {

        private readonly DataTableHeader VolumeOneHeader = new DataTableHeader(typeof(String), "Volume 1");
        private readonly DataTableHeader VolumeTwoHeader = new DataTableHeader(typeof(String), "Volume 2");

        private IDataSource _dataSource;

        public CDataSourceOptions GetDataSourceOptions()
        {
            throw new NotImplementedException();
        }

        public IDataSource GetDataSource( Object options )
        {
            Log.Message( "[VolumeCalculator] Initalizing volume calculation" );
            
            CAtomicPicts pics = _dataSource.TryGetAs<CAtomicPicts>();

            if (pics != null)
                return GetFromAtomicPics( pics );

            Log.Message( "[VolumeCalculator] cannot parse DataSource as Atomic pics" );

            throw new Exception( "[VolumeCalculator] failed to get data from input" );
        }

        private IDataSource GetFromAtomicPics( CAtomicPicts pics )
        {
            
            CNumSeriesPropertySummary summary = new CNumSeriesPropertySummary(VolumeOneHeader, VolumeTwoHeader);

            foreach (CSeriesInfo id in ids)
            {
                Double volume = GetSeriesTotalVolume( pics.GetSeriesById( id ) );
                summary.AddPropertyForSeries( id, volume );
            }

            return summary;
        }

        private Double GetSeriesTotalVolume( List<CAtomicImage_RENAME> images )
        {
            Double result = 0;
            //Dummy for now
            foreach (CAtomicImage_RENAME image in images)
                result += image.GetPixelData().Cast<Int16>().Sum( x => x );

            return result;
        }

        public void SetInput( IDataSource source )
        {
            _dataSource = source;
        }


    }
}