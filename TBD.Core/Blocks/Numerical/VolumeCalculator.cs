using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TBD.Logging;
using TBD.Model;

namespace TBD.Core
{
    /// <summary>
    /// Dummy volume calculator 
    /// </summary>
    public class CImageSeriesVolumeCalculator : IBlock
    {
        private readonly DataTableHeader _volumeOneHeader = new DataTableHeader(typeof(String), "Volume 1");
        private readonly DataTableHeader _volumeTwoHeader = new DataTableHeader(typeof(String), "Volume 2");

        private IDataSource _dataSource;

        public CDataSourceOptions GetDataSourceOptions()
        {
            return new CDataSourceOptions( typeof(CAtomicPicts) );
        }

        public IDataSource GetDataSource( CDataSourceOptions options )
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
            Log.Message( "[VolumeCalculator] Generating summary" );

            CNumSeriesPropertySummary summary = InitalizeSummmary();

            foreach (CAtomicImageSeries series in pics.GetAs<List<CAtomicImageSeries>>())
            {
                summary.AddPropertyForSeries( series.Info, GetSeriesVol1( series ) );
                summary.AddPropertyForSeries( series.Info, GetSeriesVol2( series ) );
            }

            return summary;
        }

        private CNumSeriesPropertySummary InitalizeSummmary()
        {
            return new CNumSeriesPropertySummary( _volumeOneHeader, _volumeTwoHeader );
        }

        private NumProperty GetSeriesVol1( CAtomicImageSeries series )
        {
            Double result = series.Images.Aggregate<CAtomicImage_RENAME, Double>( 
                0, 
                ( current, image ) => current + image.GetPixelData().Cast<Int16>().Sum( x => x ) );
            
            return new NumProperty(_volumeOneHeader.Name, result.ToString( CultureInfo.InvariantCulture ));
        }

        private NumProperty GetSeriesVol2( CAtomicImageSeries series )
        {
            Double result = series.Images.Aggregate<CAtomicImage_RENAME, Double>( 
                0, 
                ( current, image ) => current + image.GetPixelData().Cast<Int16>().Sum( x => x + 1) );

            return new NumProperty(_volumeTwoHeader.Name, result.ToString( CultureInfo.InvariantCulture ));
        }

        public void SetInput( IDataSource source )
        {
            _dataSource = source;
        }

        public String Name => "Volume calculator";
    }
}