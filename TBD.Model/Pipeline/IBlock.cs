using System;

namespace TBD.Model
{
    public interface IBlock
    {
        CDataSourceOptions GetDataSourceOptions();

        IDataSource GetDataSource(CDataSourceOptions options);

        void SetInput( IDataSource source );
    }
}