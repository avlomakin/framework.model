using System;
using System.Collections.Generic;
using System.IO;
using TBD.Logging;

namespace TBD.Model
{
    public class CAtomicImage_RENAME
    {
        private readonly Object _tagsToken = new Object();
        private readonly Dictionary<STagId, CMetadataTag> _tags = new Dictionary<STagId, CMetadataTag>();

        private readonly FileInfo _fileInfo;

        public FileInfo FileInfo
        {
            get
            {
                if (_fileInfo != null)
                    return _fileInfo;

                throw new Exception( "File info not set" );
            }
        }

        private readonly Int16[,] _pixelData;

        public CAtomicImage_RENAME( Int16[,] pixelData, IEnumerable<CMetadataTag> tags )
        {
            _pixelData = pixelData;

            foreach (CMetadataTag tag in tags)
                AddOrUpdateTag( tag );
        }

        public CAtomicImage_RENAME( Int16[,] pixelData, IEnumerable<CMetadataTag> tags, FileInfo fileInfo) : this(pixelData, tags)
        {
            _fileInfo = fileInfo;
        }

        public void AddOrUpdateTag( CMetadataTag tag )
        {
            lock (_tagsToken)
                _tags[ tag.TagId ] = tag;

        }

        public CMetadataTag FindMetadataTag( STagId tagId )
        {
            lock (_tagsToken)
                return _tags[ tagId ];
        }

        public Int16[,] GetPixelData()
        {
            return _pixelData.Clone() as Int16[,];
        }
    }


}