using System.Collections.Generic;
using TBD.Model;

namespace TBD.Core
{
    public class CDocument
    {
        private readonly List<IBlock> _blocks = new List<IBlock>();
        private readonly IFrameHolder _frameHolder;

        public CDocument( IFrameHolder frameHolder )
        {
            _frameHolder = frameHolder;
        }


        public void Add( IBlock block )
        {
            _blocks.Add( block );
        }

        public void GenerateReport()
        {
            foreach (IBlock block in _blocks)
            {
                _frameHolder.GetFrame().DisplayReport( block.GetReport() );
            }
        }
    }
}