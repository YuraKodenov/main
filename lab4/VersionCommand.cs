using McMaster.Extensions.CommandLineUtils;

[Command(Name = "version", Description = "Display program version and author information")]
class VersionCommand
{
    private void OnExecute()
    {
        Console.WriteLine("Author: Your Name");
        Console.WriteLine("Version: 1.0.0");
    }
}
