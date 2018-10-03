using System;
using System.Collections.Generic;
using System.Linq;

namespace TBD.Model
{
    public class CAtomicImageSeries
    {
        private readonly List<CAtomicImage_RENAME> _images;

        public readonly CSeriesInfo Info;

        public IReadOnlyCollection<CAtomicImage_RENAME> Images => _images;

        public CAtomicImageSeries( CSeriesInfo info, IEnumerable<CAtomicImage_RENAME> images )
        {
            Info = info;
            _images = images.ToList();
        }

        public static CAtomicImageSeries CreateFromAtomicImages( IEnumerable<CAtomicImage_RENAME> images )
        {
            IEnumerable<CAtomicImage_RENAME> cAtomicImageRenames = images as CAtomicImage_RENAME[] ?? images.ToArray();

            if (IsImagesValid( cAtomicImageRenames, out var info ))
                return new CAtomicImageSeries( info, cAtomicImageRenames);

            throw new Exception( "Failed to validate images" );
        }

        private static Boolean IsImagesValid( IEnumerable<CAtomicImage_RENAME> images, out CSeriesInfo info )
        {
            //TODO: (avlomakin) check if file infos are consistent
            info = CSeriesInfo.CreateFromFileInfo( images.First().FileInfo );
            return true;
        }

        public void SetCaption( String caption )
        {
            Info.HumanReadableName = caption;
        }
    }
}