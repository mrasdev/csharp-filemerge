using FileMerge;

namespace FileMerge
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string startDirectory;

            if (args.Length > 0 && Directory.Exists(args[0]))
            {
                startDirectory = args[0];
            }
            else
            {
                startDirectory = Directory.GetCurrentDirectory();
            }

            Console.WriteLine($"Using directory: {startDirectory}");

            var merger = new FileMerger
            {
                SourceDirectory = startDirectory
            };

            merger.Run();

            Console.WriteLine("\nPress any key...");
            Console.ReadKey(intercept: true);
        }
    }
}
