using System;

namespace LoginChain
{
    public interface ILoggerHandler
    {
        void HandleLog(string level, string msg);
        ILoggerHandler SetNextHandler(ILoggerHandler nextHandler);
    }

    public abstract class BaseLoggerHandler : ILoggerHandler
    {
        private ILoggerHandler nextHandler;

        public virtual void HandleLog(string level, string msg)
        {
            if (nextHandler != null)
            {
                nextHandler.HandleLog(level, msg);
            }
        }

        public ILoggerHandler SetNextHandler(ILoggerHandler nextHandler)
        {
            this.nextHandler = nextHandler;
            return nextHandler;
        }
    }

    public class InfoLoggerHandler : BaseLoggerHandler
    {
        public override void HandleLog(string level, string msg)
        {
            if (level == "INFO")
            {
                Console.WriteLine("INFO: " + msg);
            }
            else
            {
                base.HandleLog(level, msg);
            }
        }
    }

    public class WarningLoggerHandler : BaseLoggerHandler
    {
        public override void HandleLog(string level, string msg)
        {
            if (level == "WARNING")
            {
                Console.WriteLine("WARNING: " + msg);
            }
            else
            {
                base.HandleLog(level, msg);
            }
        }
    }

    public class ErrorLoggerHandler : BaseLoggerHandler
    {
        public override void HandleLog(string level, string msg)
        {
            if (level == "ERROR")
            {
                Console.WriteLine("ERROR: " + msg);
            }
            else
            {
                base.HandleLog(level, msg);
            }
        }
    }

    public class Client
    {
        private ILoggerHandler loggerHandler;

        public Client()
        {
            ILoggerHandler infoLoggerHandler = new InfoLoggerHandler();
            ILoggerHandler warningLoggerHandler = new WarningLoggerHandler();
            ILoggerHandler errorLoggerHandler = new ErrorLoggerHandler();

            infoLoggerHandler.SetNextHandler(warningLoggerHandler)
                             .SetNextHandler(errorLoggerHandler);

            loggerHandler = infoLoggerHandler;
        }

        public void Run()
        {
            Console.WriteLine("Welcome to the logging system.");
            while (true)
            {
                Console.Write("Enter log level (INFO, WARNING, ERROR) or 'EXIT' to quit: ");
                string level = Console.ReadLine().ToUpper();
                if (level == "EXIT") break;

                Console.Write("Enter log message: ");
                string message = Console.ReadLine();

                loggerHandler.HandleLog(level, message);
            }
        }

        public static void Main(string[] args)
        {
            Client client = new Client();
            client.Run();
        }
    }
}
