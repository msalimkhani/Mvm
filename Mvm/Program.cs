namespace Mvm
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 1)
            {
                var fileName = args[0];
                FileOps fileOps = new FileOps(fileName);
                byte[] bytes = fileOps.readToEndBytes();
                List<int> program = [.. bytes];
                RAM ram = new RAM()
                { ProgramMemory = program.ToArray() };
                VirtualMachine virtualMachine = new VirtualMachine(ram);
                virtualMachine.run();
            }
            else
            {
                Console.WriteLine("Usage: " + AppDomain.CurrentDomain.FriendlyName + " <binary_file>");
            }
        }
    }
}
