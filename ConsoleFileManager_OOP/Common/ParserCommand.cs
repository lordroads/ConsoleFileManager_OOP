using FileManagerOOP.Models;

public class ParserCommand
{
    public Command Parse(string commandLine, char separator = ' ', char ignoreSpace = '"')
    {
        Command userCommand;
        string command = string.Empty;
        string[] resultParseLine = new string[0];
        bool flag = true;

        if (string.IsNullOrWhiteSpace(commandLine))
        {
            return new Command() { CommandName = " " };
        }

        for (int i = 0; i < commandLine.Length; i++)
        {
            if (commandLine[i] == separator & flag)
            {
                string[] copy = resultParseLine;
                string[] result = new string[copy.Length + 1];

                for (int y = 0; y < copy.Length; y++)
                {
                    result[y] = copy[y];
                }
                result[copy.Length] = command;
                resultParseLine = result;
                command = string.Empty;
            }
            else
            {
                if (commandLine[i] == ignoreSpace)
                {
                    flag = !flag;
                }
                else
                {
                    command += commandLine[i];
                }
            }
            if (i == commandLine.Length - 1)
            {
                string[] copy = resultParseLine;
                string[] result = new string[copy.Length + 1];

                for (int y = 0; y < copy.Length; y++)
                {
                    result[y] = copy[y];
                }
                result[copy.Length] = command;
                resultParseLine = result;
                command = string.Empty;
            }
        }

        string name = resultParseLine[0];
        
        if (resultParseLine.Length > 1)
        {
            string[] copy = resultParseLine;
            resultParseLine = new string[copy.Length - 1];

            for (int i = 0; i < resultParseLine.Length; i++)
            {
                resultParseLine[i] = copy[i + 1];
            }
            userCommand = new Command()
            {
                CommandName = name,
                Args = resultParseLine
            }; 
        }
        else
        {
            userCommand = new Command()
            {
                CommandName = name,
                Args = null
            };
        }
        return userCommand;
    }
}