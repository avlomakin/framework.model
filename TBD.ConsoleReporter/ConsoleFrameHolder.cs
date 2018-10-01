using TBD.Model;

namespace TBD.ConsoleReporter
{
    public class CConsoleFrameHolder : IFrameHolder
    {
        public IFrame GetFrame()
        {
            return new CConsoleFrame();
        }
        
    }
}