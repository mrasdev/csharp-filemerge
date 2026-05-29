namespace FileMerge;

internal class FileMerger
{
    public string SourceDirectory { get; set; } = Directory.GetCurrentDirectory();

    private readonly HashSet<string> _extensions = new(StringComparer.OrdinalIgnoreCase)
    {
        ".cs", ".js", ".ts", ".java", ".cpp", ".h",
        ".py"
    };

    public void Run()
    {
        try
        {
            var allFiles = Directory
                .EnumerateFiles(SourceDirectory, "*.*", SearchOption.AllDirectories)
                .Where(file =>
                {
                    string fileName = Path.GetFileName(file);

                    // skip merged.*
                    if (fileName.StartsWith("merged.", StringComparison.OrdinalIgnoreCase))
                        return false;

                    // skip Debug/Release
                    if (file.Contains("debug", StringComparison.OrdinalIgnoreCase) ||
                        file.Contains("release", StringComparison.OrdinalIgnoreCase))
                        return false;

                    string ext = Path.GetExtension(file);
                    return _extensions.Contains(ext);
                });

            // group by file extension
            var grouped = allFiles
                .GroupBy(f => Path.GetExtension(f).ToLowerInvariant());

            foreach (var group in grouped)
            {
                string extension = group.Key;
                string outputFile = Path.Combine(SourceDirectory, $"merged{extension}");

                Console.WriteLine($"Creating {Path.GetRelativePath(SourceDirectory, outputFile)}");

                using StreamWriter writer = new(outputFile, false); // overwrite

                foreach (var file in group)
                {

                    string relativePath = Path.GetRelativePath(SourceDirectory, file);
                    Console.WriteLine($"Processing {relativePath}");

                    writer.WriteLine($"// ===== File: {relativePath} =====");

                    string content = File.ReadAllText(file);
                    writer.WriteLine(content);
                    writer.WriteLine();
                }
            }

            Console.WriteLine("All files merged successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
