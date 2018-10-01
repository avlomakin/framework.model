using System;

namespace TBD.Model
{
    public enum ETagBalueType
    {
        PN, //Person Name
        IS, //Integer string 
    }

    public class CMetadataTag 
    {

        public STagId TagId { get; }

        public ETagBalueType ValueType { get; }

        public Object Value { get; }

        public CMetadataTag( ETagBalueType valueType, STagId tagId, Object value )
        {
            ValueType = valueType;
            TagId = tagId;
            Value = value;
        }
    }

}