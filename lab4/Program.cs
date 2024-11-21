using McMaster.Extensions.CommandLineUtils;

// Головна програма з підкомандами
[Command(Name = "lab4", Description = "CLI application for Lab4")]
[Subcommand(typeof(VersionCommand), typeof(RunCommand), typeof(SetPathCommand))]
class Program
{
    static void Main(string[] args) => CommandLineApplication.Execute<Program>(args);

    private void OnExecute()
    {
        Console.WriteLine("Lab4 CLI application. Use '--help' to see available commands.");
    }
}
