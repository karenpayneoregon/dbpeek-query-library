using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace Demo;
internal partial class Program
{
    [ModuleInitializer]
    public static void Init()
    {
        Console.Title = "Code sample";
        WindowUtility.SetConsoleWindowPosition(WindowUtility.AnchorWindow.Fill);
    }
    private static void Render(Rule rule)
    {
        AnsiConsole.Write(rule);
        AnsiConsole.WriteLine();
    }

    private static void ExitPrompt()
    {
        Console.WriteLine();
        Render(new Rule($"[white on blue]Press a ENTER to exit[/]")
            .RuleStyle(Style.Parse("cyan"))
            .Centered());
        Console.ReadLine();
    }
}
