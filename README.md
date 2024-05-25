`LoginChain` - pseudocode:
```
interface ILoggerHandler:
    method HandleLog(level, msg)
    method SetNextHandler(nextHandler)

abstract class BaseLoggerHandler implements ILoggerHandler:
    attribute nextHandler

    method SetNextHandler(nextHandler):
        this.nextHandler = nextHandler
        return nextHandler

    method HandleLog(level, msg):
        if nextHandler is not null:
            nextHandler.HandleLog(level, msg)

  class InfoLoggerHandler extends BaseLoggerHandler:
      method HandleLog(level, msg):
          if level == "INFO":
              print("INFO: " + msg)
          else:
              super.HandleLog(level, msg)

class WarningLoggerHandler extends BaseLoggerHandler:
    method HandleLog(level, msg):
        if level == "WARNING":
            print("WARNING: " + msg)
        else:
            super.HandleLog(level, msg)

class ErrorLoggerHandler extends BaseLoggerHandler:
    method HandleLog(level, msg):
        if level == "ERROR":
            print("ERROR: " + msg)
        else:
            super.HandleLog(level, msg)

class Client:
    attribute loggerHandler

    constructor Client():
        infoLoggerHandler = new InfoLoggerHandler()
        warningLoggerHandler = new WarningLoggerHandler()
        errorLoggerHandler = new ErrorLoggerHandler()

        infoLoggerHandler.SetNextHandler(warningLoggerHandler)
                         .SetNextHandler(errorLoggerHandler)

        loggerHandler = infoLoggerHandler

    method Run():
        print("Welcome to the logging system.")
        while true:
            print("Enter log level (INFO, WARNING, ERROR) or 'EXIT' to quit: ")
            level = readLine().toUpperCase()
            if level == "EXIT":
                break

            print("Enter log message: ")
            message = readLine()

            loggerHandler.HandleLog(level, message)

    static method Main(args):
        client = new Client()
        client.Run()
```
