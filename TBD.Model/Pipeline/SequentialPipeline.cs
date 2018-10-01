using System.Collections.Generic;
using System.Linq;
using TBD.Logging;

namespace TBD.Model
{
    public class CSequentialPipeline
    {
        private List<IBlock> _blocks = new List<IBlock>();


        public void AppendInPipeline( IBlock block )
        {
            _blocks.Append( block );
        }

        public IDataSource GetPipelineResult()
        {
            if (!_blocks.Any())
            {
                Log.Message( "[SequentialPipeline] no elements in pipeline" );
                return null;
            }

            return _blocks.Last().GetDataSource( null );
        }
    }
}