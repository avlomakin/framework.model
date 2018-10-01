using System;
using System.Collections.Generic;
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

        public IDataSource GetDataSource( Object options )
        {

            //Empty for now
            List<CAtomicImage_RENAME> dummyLoad =
                new List<CAtomicImage_RENAME>
                {
                    SDummyAtomicPictGenerator.GeneratePict(),
                    SDummyAtomicPictGenerator.GeneratePict(),
                    SDummyAtomicPictGenerator.GeneratePict(),
                    SDummyAtomicPictGenerator.GeneratePict(),
                    SDummyAtomicPictGenerator.GeneratePict()
                };

            return new CAtomicPicts( dummyLoad );
        }

        public void SetInput( IDataSource source )
        {
            throw new NotImplementedException("Dicom File loader doesn't support input setup");
        }
    }
}