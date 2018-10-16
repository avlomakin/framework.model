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
        public List<IBlock> Blocks = new List<IBlock>();

        private readonly CDataSourceOptions _pipelineOptions = new CDataSourceOptions( typeof(Object) );

        public void AppendInPipeline( IBlock block,
            EPipelineBlockManageType manageType = EPipelineBlockManageType.ByPipeline )
        {
            switch (manageType)
            {
                case EPipelineBlockManageType.ByBlock:
                    Log.Message( $"[SequentialPipeline] Append block {block.Name}, managed mode: by Block" );
                    Blocks.Add( block );
                    break;
                case EPipelineBlockManageType.ByPipeline:
                    AddManagedByPipelineBlock( block );
                    Blocks.Add( block );
                    break;
                default:
                    throw new ArgumentOutOfRangeException( nameof(manageType), manageType, null );
            }
        }

        private void AddManagedByPipelineBlock( IBlock block )
        {
            if (Blocks.Any())
            {
                IDataSource dataSource = Blocks.Last().GetDataSource( block.GetDataSourceOptions() );
                Log.Message(
                    $"[SequentialPipeline] Append block {block.Name}, managed mode: by Pipeline, data source type: {dataSource.GetType().Name}" );
                block.SetInput( Blocks.Last().GetDataSource( block.GetDataSourceOptions() ) );
            }
            else
                Log.Message(
                    $"[SequentialPipeline] Append block {block.Name}, managed mode: by Pipeline, data source type: undefined (first element in pipeline)" );
        }

        public IDataSource GetPipelineResult()
        {
            if (!Blocks.Any())
            {
                Log.Message( "[SequentialPipeline] no elements in pipeline" );
                return null;
            }

            String flow = GetPipelineFlowModelLog();
            Log.Message( $"[SequentialPipeline] Initiating pipeline result calculation, flow model: {flow}" );

            return Blocks.Last().GetDataSource( _pipelineOptions );
        }

        public IDataSource GetPipelineResult( CDataSourceOptions options )
        {
            if (!Blocks.Any())
            {
                Log.Message( "[SequentialPipeline] no elements in pipeline" );
                return null;
            }

            return Blocks.Last().GetDataSource( options );
        }

        private String GetPipelineFlowModelLog()
        {
            return String.Join( " -> ", Blocks.Select( x => x.Name ) );
        }
    }
}