using System;

namespace TBD.Model
{
    public struct STagId
    {
        public static readonly  STagId PatientBirthNameTagId = new STagId(0010,1005);

        public Int16 First { get; }
        public Int16 Second { get; }

        public STagId( Int16 first, Int16 second )
        {
            First = first;
            Second = second;
        }

        public override String ToString()
        {
            return $"({First},{Second})";
        }
    }
}