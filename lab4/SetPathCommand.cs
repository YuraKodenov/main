using McMaster.Extensions.CommandLineUtils;

[Command(Name = "set-path", Description = "Set LAB_PATH environment variable")]
class SetPathCommand
{
    [Option("-p|--path", Description = "Specify the folder path for input/output files")]
    public string? Path { get; set; }

    private void OnExecute()
    {
        if (!string.IsNullOrEmpty(Path))
        {
            Environment.SetEnvironmentVariable("LAB_PATH", Path, EnvironmentVariableTarget.User);
            Console.WriteLine($"LAB_PATH set to: {Path}");
        }
        else
        {
            Console.WriteLine("Error: Path is required. Use -p or --path to specify.");
        }
    }
}
