namespace TestFileGenerator
{
    internal class TestFileGenerator
    {
        static void Main(string[] args)
        {
            long numberOfLines = 0;

            bool shouldExit = false;

            if (args == null || args.Length < 1 || !long.TryParse(args[0], out numberOfLines))
            { 
                Console.WriteLine("Please provide number of lines as first parameter");
                shouldExit = true;
            }

            if(args == null || args.Length < 2)
            {
                Console.WriteLine("Please provide output as second parameter");
                shouldExit = true;
            }
            string outputPath = args![1];

            if (shouldExit)
                return;

            Generator generator = new Generator(numberOfLines, outputPath);
            generator.Generate();
        }
    }
}