using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TBD.Logging;
using TBD.Model;

namespace TBD.Core
{ 
    public class CAtomicPicts : IDataSource
    {
        private readonly HashSet<CAtomicImageSeries> _seriesSet = new HashSet<CAtomicImageSeries>(new CImageSeriresGuidComparer());

        public T GetAs<T>() where T: class
        {
            T result = TryGetAs<T>();

            if(result == null)
                throw new Exception( $"Failed to get Atomic picts as {typeof(T)}" );

            return result;
        }

        public T TryGetAs<T>() where T : class
        {
            if (typeof(T) == typeof(CAtomicPicts))
                return (T) (Object) ( this );

            return null;
        }

        public CAtomicPicts(IEnumerable<CAtomicImageSeries> seriesEnum)
        {
            foreach (CAtomicImageSeries imageSeries in seriesEnum)
                _seriesSet.Add( imageSeries );
        }

        public CAtomicImageSeries GetSeriesById( Guid id )
        {
            return _seriesSet.First( x => x.Info.Id == id );
        }
    }
}