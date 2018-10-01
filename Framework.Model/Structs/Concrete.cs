using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Framework.Model.Structs
{

    public class FakeFileLoaderBlock : IBlock
    {
        public List<String> FakeFiles;

        public IReport GetReport()
        {
            return FileLoadFilesReport.CreateFromFileNameList( FakeFiles );
        }

        public IDataSource GetDataSource()
        {
            throw new System.NotImplementedException();
        }
    }

    public class FileLoadFilesReport : IReport
    {
        public static readonly FileLoadFilesReport EmptyReport = new FileLoadFilesReport( "NO FILES LOADED", 0, "undefined");

        public Int32 TotalFilesLoaded { get; }
        public String WorkingDir { get; }

        public String Extension { get; }

        public FileLoadFilesReport( String workingDir, Int32 totalFilesLoaded, String extension )
        {
            WorkingDir = workingDir;
            TotalFilesLoaded = totalFilesLoaded;
            Extension = extension;
        }

        public T GetAs<T>()
        {
            if (typeof(T) == typeof(String))
                return (T)((Object)GenerateStringReport());

            throw new Exception("Unsupported report type");
        }

        private String GenerateStringReport()
        {
            String result;
            result = result + "WORKING DIR ".PadRight( WorkingDir.Length + 2 ) + " |   FORMAT   |" +   
        }

        public static IReport CreateFromFileNameList( List<String> fakeFiles )
        {
            if (fakeFiles.Count == 0)
                return EmptyReport;

            String workingDir = Path.GetDirectoryName(fakeFiles.First());
            String extension = Path.GetExtension( fakeFiles.First() );

            foreach (String fakeFile in fakeFiles)
            {
                if (Path.GetDirectoryName(fakeFile) != workingDir)
                    throw new Exception("cannot define working directory");
                
                if (Path.GetExtension(fakeFile) != extension)
                    throw new Exception("cannot define extension");
            }

            return new FileLoadFilesReport( workingDir, fakeFiles.Count, extension );
        }
    }
}