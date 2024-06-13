namespace Mvm.Assembler
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            if (args.Length == 1)
            {
                var fileName = args[0];
                FileOps fileOps = new FileOps(fileName, "out.bin");
                string contents = fileOps.readToEndString();
                Assembler assembler = new Assembler(contents);
                int[] program = assembler.Assemble();
                foreach (var item in program)
                {
                    Console.Write(item + " ");
                }
                fileOps.writeData(program);
            }
            else
            {
                Console.WriteLine("Error: Usage: " + AppDomain.CurrentDomain.FriendlyName + " <program-file>");
            }
            //var fileName = "program.ms";
            //FileOps fileOps = new FileOps(fileName, "out.bin");
            //string contents = fileOps.readToEndString();
            //Assembler assembler = new Assembler(contents);
            //int[] program = assembler.Assemble();
            //fileOps.writeData(program);
        }
    }
}
