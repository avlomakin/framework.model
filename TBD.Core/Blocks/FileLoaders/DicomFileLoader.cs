using System;
using System.Collections.Generic;
using System.Data;
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
            if (options is CFileLoaderDataSourceOptions fileOptions)
                return ManageFileSLoaderOptions(fileOptions);
            else
                return ManageCommonOptions(options);
        }

        private IDataSource ManageFileSLoaderOptions( CFileLoaderDataSourceOptions fileOptions )
        {
            Log.Message( $"[DicomFileLoader] Received managed options. Is report required : {fileOptions.IsReport}, preferable type {fileOptions.PreferableType.Name} ");

            CAtomicPicts dummyPicts = GenerateDummyLoadedFiles();

            if(fileOptions.IsReport)
                return CFileLoadFilesReport.CreateFromAtomicPicts(dummyPicts);

            return dummyPicts;
        }

        private static IDataSource ManageCommonOptions(CDataSourceOptions options)
        {
            Log.Message( $"[DicomFileLoader] Received common options, preferable type {options.PreferableType.Name}" );

            CAtomicPicts dummyPicts = GenerateDummyLoadedFiles();

            if(options.PreferableType == typeof(DataTable))
                return CFileLoadFilesReport.CreateFromAtomicPicts(dummyPicts);

            return dummyPicts;
        }

        private static CAtomicPicts GenerateDummyLoadedFiles()
        {
            Log.Message( "[DicomFileLoader] creating Atomic pics" );

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
                CSeriesInfo.CreateFromFileInfo(new FileInfo("C:\\src\\Test\\dummy.dicom")), dummyLoad);

            dummySeries.SetCaption( "Test series" );

            return new CAtomicPicts(new[] { dummySeries });
        }

        public void SetInput( IDataSource source )
        {
            throw new NotImplementedException("Dicom File loader doesn't support input setup");
        }

        public String Name => "Dicom File Loader";
    }
}