using System;
using System.Collections.Generic;
using TBD.Model;

namespace TBD.Core
{
    public class CImageSeriresGuidComparer : IEqualityComparer<CAtomicImageSeries>
    {
        public Boolean Equals( CAtomicImageSeries x, CAtomicImageSeries y )
        {
            return x.Info.Id == y.Info.Id;
        }

        public Int32 GetHashCode( CAtomicImageSeries obj )
        {
            return obj.Info.Id.GetHashCode();
        }
    }
}