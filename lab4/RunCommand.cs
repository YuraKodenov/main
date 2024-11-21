using McMaster.Extensions.CommandLineUtils;
using Library;

[Command(Name = "run", Description = "Run Lab3 task")]
class RunCommand
{
    [Option("-I|--input", Description = "Input file path")]
    public string? InputFile { get; set; }

    [Option("-o|--output", Description = "Output file path")]
    public string? OutputFile { get; set; }

    private void OnExecute()
    {
        Console.WriteLine("Running Lab3...");

        // Передаємо порожній рядок, якщо параметр не задано
        Lab3Runner.Run(InputFile ?? string.Empty, OutputFile ?? string.Empty);
    }
}
