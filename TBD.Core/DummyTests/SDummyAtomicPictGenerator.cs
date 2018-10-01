using System;
using System.Collections.Generic;
using System.IO;
using TBD.Model;

namespace TBD.Core
{
    public static class SDummyAtomicPictGenerator
    {
        public static CAtomicImage_RENAME GeneratePict()
        {
            List<CMetadataTag> tags = new List<CMetadataTag>
            {
                new CMetadataTag( ETagBalueType.PN, STagId.PatientBirthNameTagId, "John Doe" )
            };

            return  new CAtomicImage_RENAME( new Int16[2,2]{{1,0}, {0,1}}, tags, new FileInfo( "C:\\src\\Test\\dummy.dicom" ) );
        }
    }
}