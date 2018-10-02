using System;
using System.IO;

namespace TBD.Model
{
    public class CSeriesInfo
    {
        public readonly String Folder;
        public readonly String Extension;
        public readonly Guid Id;

        private String _humanReadableName;

        public  String HumanReadableName
        {
            get
            {
                if (String.IsNullOrEmpty( _humanReadableName ))
                    return ToString();

                return _humanReadableName;
            }
            set => _humanReadableName = value;
        }

        public CSeriesInfo( String folder, String extension, Guid id )
        {
            Folder = folder;
            Id = id;
            Extension = extension.ToUpper();
        }

        public static CSeriesInfo CreateFromFileInfo(FileInfo info)
        {
            return new CSeriesInfo( info.DirectoryName, info.Extension, Guid.NewGuid() );
        }


        public override String ToString()
        {
            return $"(ID: {Id},  folder: {Folder}, type: {Extension})";
        }
    }
}