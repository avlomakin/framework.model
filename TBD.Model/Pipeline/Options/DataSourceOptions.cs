using System;

namespace TBD.Model
{
    public class CDataSourceOptions
    {
        public readonly Type PreferableType;

        public CDataSourceOptions( Type preferableType )
        {
            PreferableType = preferableType;
        }
    }
}