using System;
using System.IO;

namespace TBD.Logging
{
    /// <summary>
    /// (avlomakin) Dummy log just for me 
    /// </summary>
    public static class Log
    {
        private enum ELogLevel
        {
            Info,
            Warning,
            Error
        }

        private static StreamWriter _logWriter;

        public static void Initalize( String filePath )
        {
            FileStream logFile = new FileStream( "C:\\src\\test.log", FileMode.Append );
            _logWriter = new StreamWriter( logFile );
        }

        public static void Message( String message)
        {
            
            WriteToLogFile( message, ELogLevel.Info );
        }

        public static void Error( String message )
        {
            WriteToLogFile( message, ELogLevel.Error );
        }

        private static void WriteToLogFile( String message, ELogLevel logLevel )
        {
            if (_logWriter == null)
                throw new Exception( "Log was not initalized" );

            String[] lines = message.Split( "\n" );
            String dateTime = '[' + DateTime.Now.ToString( "MM/dd/yyyy hh:mm:ss.fff tt" ) + ']';
            String logLevelString = GetLogLevelAsString( logLevel );
            foreach (String line in lines)
                _logWriter.WriteLine( $"{dateTime}:   {logLevelString}  {line}" );
            _logWriter.Flush();
        }

        private static String GetLogLevelAsString( ELogLevel logLevel )
        {
            switch (logLevel)
            {
                case ELogLevel.Info:
                    return "INFO";
                case ELogLevel.Warning:
                    return "WARNING";
                case ELogLevel.Error:
                    return "ERROR";
                default:
                    throw new ArgumentOutOfRangeException( nameof(logLevel), logLevel, null );
            }
        }
    }
}
