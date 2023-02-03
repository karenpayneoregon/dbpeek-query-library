
namespace Demo.Classes;

/// <summary>
/// Code for code sample only
/// </summary>
internal class FileOperations
{
    /// <summary>
    /// Highlight WHERE
    /// </summary>
    public static void ReadLog()
    {
        var fileName = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory, 
            "LogFiles", $"Log{DateTime.Now.Year}{DateTime.Now.Month:d2}{DateTime.Now.Day:d2}.txt");

        if (!File.Exists(fileName)) return;

        var lines = File.ReadAllLines(fileName);

        for (int index = 0; index < lines.Length; index++)
        {
            if (lines[index].Contains("WHERE"))
            {
                lines[index] = lines[index].Replace("WHERE", "[white on blue]WHERE[/]");
            }
            if (lines[index].Contains("[INF]"))
            {
                lines[index] = lines[index].Replace("[INF]", "[[INF]]");
            }

            if (lines[index].StartsWith(DateTime.Now.Year.ToString()))
            {
                lines[index] = $"\n[white]{lines[index]}[/]\n";
            }
        }

        var result = string.Join("\n", lines);
        AnsiConsole.MarkupLine(result);
        
    }
}
