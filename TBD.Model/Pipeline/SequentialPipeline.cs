using System;
using System.Collections.Generic;
using System.Linq;
using TBD.Logging;

namespace TBD.Model
{
    public enum EPipelineBlockManageType
    {
        //(avlomakin) Just append block int pipeline, assuming that initalization was made outside 
        ByBlock,
        //(avlomakin) Let pipeline do all the data bindings
        ByPipeline
    }

    public class CSequentialPipeline
    {
        private List<IBlock> _blocks = new List<IBlock>();

        private readonly CDataSourceOptions _pipelineOptions = new CDataSourceOptions( typeof(Object) );

        public void AppendInPipeline( IBlock block,
            EPipelineBlockManageType manageType = EPipelineBlockManageType.ByPipeline )
        {
            switch (manageType)
            {
                case EPipelineBlockManageType.ByBlock:
                    Log.Message( "[CSequentialPipeline] Append new block, managed mode: by Block" );
                    _blocks.Add( block );
                    break;
                case EPipelineBlockManageType.ByPipeline:
                    Log.Message( "[CSequentialPipeline] Append new block, managed mode: by Pipeline" );
                    if (_blocks.Any())
                        block.SetInput( _blocks.Last().GetDataSource( block.GetDataSourceOptions() ) );
                    _blocks.Add( block );
                    break;
                default:
                    throw new ArgumentOutOfRangeException( nameof(manageType), manageType, null );
            }
        }
        
        public IDataSource GetPipelineResult()
        {
            if (!_blocks.Any())
            {
                Log.Message( "[SequentialPipeline] no elements in pipeline" );
                return null;
            }

            return _blocks.Last().GetDataSource( _pipelineOptions );
        }

        public IDataSource GetPipelineResult(CDataSourceOptions options)
        {
            if (!_blocks.Any())
            {
                Log.Message( "[SequentialPipeline] no elements in pipeline" );
                return null;
            }

            return _blocks.Last().GetDataSource( options );
        }
    }
}