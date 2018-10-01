using System;

namespace TBD.Model
{
    public interface IBlock
    {
        CDataSourceOptions GetDataSourceOptions();

        IDataSource GetDataSource(Object options);

        void SetInput( IDataSource source );
    }
}