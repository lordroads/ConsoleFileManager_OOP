namespace FileManagerOOP.Interfaces.Logger;

public class LoggerExceptionInTxt : ILogger
{
    public void Log(Exception ex)
    {
        try
        {
            string pathDirLog = Path.Combine(Directory.GetCurrentDirectory(), "errors");
            if (Directory.Exists(pathDirLog))
            {
                string nameException = DateTime.Now.ToString("dd.MM.yy HH-mm-ss");
                string pathFile = Path.Combine(pathDirLog, nameException + ".txt");

                File.AppendAllText(pathFile, ex.ToString());
            }
            else
            {
                Directory.CreateDirectory(pathDirLog);
                Log(ex);
            }
        }
        catch (Exception criticalEx)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(criticalEx.ToString());
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
