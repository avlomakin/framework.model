using System;
using System.Collections.Generic;

namespace TBD.Core
{
    public static class SDictionaryExtensions
    {
        public static T GetOrCreate<T1, T>( this Dictionary<T1, T> dict, T1 key,  Func<T> elementGeterator )
        {
            if (dict.TryGetValue( key, out T result ))
                return result;

            result = elementGeterator.Invoke();
            dict[ key ] = result;
            return result;
        }
    }
}