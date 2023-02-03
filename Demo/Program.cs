using Demo.Classes;
using Serilog;

namespace Demo;

internal partial class Program
{
    static void Main(string[] args)
    {
        SetupLogging.Production();

        // has a leading space
        SqlServerOperations.Example1(" Mexico");
        // country exist
        SqlServerOperations.Example1("Mexico");


        // order exist
        var (employee, success) = SqlServerOperations.Example2(10262);
        // order does not exist
        SqlServerOperations.Example2(13462);

        //Log.CloseAndFlush();
        //FileOperations.ReadLog();




        ExitPrompt();
    }
}