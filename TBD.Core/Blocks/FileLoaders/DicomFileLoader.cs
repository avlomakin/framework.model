using System;
using System.Collections.Generic;
using System.IO;
using TBD.Logging;
using TBD.Model;

namespace TBD.Core
{
    public class CDicomFileLoader : IBlock
    {
        public CDataSourceOptions GetDataSourceOptions()
        {
            throw new NotImplementedException();
        }

        public IDataSource GetDataSource( CDataSourceOptions options )
        {
            //(avlomakin) dummy loader for now
            
            List<CAtomicImage_RENAME> dummyLoad =
                new List<CAtomicImage_RENAME>
                {
                    SDummyAtomicPictGenerator.GeneratePict(),
                    SDummyAtomicPictGenerator.GeneratePict(),
                    SDummyAtomicPictGenerator.GeneratePict(),
                    SDummyAtomicPictGenerator.GeneratePict(),
                    SDummyAtomicPictGenerator.GeneratePict()
                };

            CAtomicImageSeries dummySeries = new CAtomicImageSeries(
                CSeriesInfo.CreateFromFileInfo( new FileInfo( "C:\\src\\Test\\dummy.dicom" ) ), dummyLoad );

            return new CAtomicPicts( new[] { dummySeries } );
        }

        public void SetInput( IDataSource source )
        {
            throw new NotImplementedException("Dicom File loader doesn't support input setup");
        }

        public String Name => "Dicom File Loader";
    }
}