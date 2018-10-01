using System;
using System.Collections.Generic;

namespace Framework.Model.Structs
{
    public class Document
    {
        private readonly List<IBlock> _blocks = new List<IBlock>();
        private readonly IFrameHolder _frameHolder;

        public Document( IFrameHolder frameHolder )
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

    public interface IFrameHolder
    {
        IFrame GetFrame();
    }

    public class ConsoleFrameHolder : IFrameHolder
    {
        public IFrame GetFrame()
        {
            return new ConsoleFrame();
        }
        
    }

    public interface IBlock
    {
        IReport GetReport();

        IDataSource GetDataSource();
    }

    public interface IDataSource
    {
        T GetAs<T>();
    }

    /// <summary>
    /// Document provides Frames for Block to display their inform 
    /// </summary>
    public interface IFrame
    {
        void DisplayReport(IReport report);
    }

    public interface IReport
    {
        T GetAs<T>();
    }

    public class ConsoleFrame : IFrame
    {
        public void DisplayReport( IReport report )
        {
            Console.WriteLine(report.GetAs<String>());
        }
    }


}