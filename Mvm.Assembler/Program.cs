namespace Mvm.Assembler
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            if (args.Length == 1)
            {
                var fileName = args[0];
                var ext = Path.GetExtension(fileName);
                if(ext == ".ms")
                {
                    try
                    {
                        FileOps fileOps = new FileOps(fileName, Path.Join(Environment.CurrentDirectory, Path.GetFileNameWithoutExtension(fileName) + ".bin"));
                        string contents = fileOps.readToEndString();
                        Assembler assembler = new Assembler(contents);
                        int[] program = assembler.Assemble();
                        foreach (var item in program)
                        {
                            Console.Write(item + " ");
                        }
                        fileOps.writeInstructions(program);
                    }
                    catch (Exception e)
                    {
                        Console.Error.WriteLine("Cannot Read File " + fileName + ": " + e.Message);
                    }
                }
                else
                {
                    PrintHelp();
                }
            }
            else
            {
                PrintHelp();
            }

            //var fileName = "program.ms";
            //FileOps fileOps = new FileOps(fileName, "out.bin");
            //string contents = fileOps.readToEndString();
            //Assembler assembler = new Assembler(contents);
            //int[] program = assembler.Assemble();
            //fileOps.writeData(program);
        }
        private static void PrintHelp()
        {
            Console.WriteLine("Mvm Assembler");
            Console.WriteLine("Error: Usage: " + AppDomain.CurrentDomain.FriendlyName + " <ms-program-file>");
            Console.WriteLine("Example: " + AppDomain.CurrentDomain.FriendlyName + " program.ms");
        }
    }
}
