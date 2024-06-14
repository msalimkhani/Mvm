using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvm.Assembler
{
    internal class Assembler
    {
        private static List<Token> tokens = new List<Token>()
        {
            new Token(9, "LOADA"),
            new Token(10, "LOADB"),
            new Token(11, "ADD"),
            new Token(12, "SUB"),
            new Token(13, "MUL"),
            new Token(13, "DIV"),
            new Token(15, "SHIFTR"),
            new Token(16, "AND"),
            new Token(17, "OR"),
            new Token(18, "XOR"),
            new Token(19, "SHIFTL"),
            new Token(20, "PRINTRES"),
            new Token(21, "GETSTAT"),
            new Token(0, "HALT"),
            new Token(8, "JMP"),
            new Token(7, "JICT"),
            new Token(6, "JICF"),
            new Token(5, "IF"),
            new Token(4, "IFN"),
            new Token(3, "MOV"),
            new Token(101, "A1"),
            new Token(102, "A2"),
            new Token(123, "PROMPT"),
            new Token(100, "A"),
            new Token(99, "B"),
            new Token(98, "RES"),
            new Token(50, "NOP"),
            new Token(77, "C"),
            new Token(0, "RET"),
            new Token(78, "SUBR"),
            new Token(79, "INC")
    };
        private static List<Token> labels = new List<Token>();
        private static List<int> argCount = new List<int>();
        private string contents;
        public Assembler(string contents)
        {
            this.contents = contents;
        }
        private int SumTo(List<int> ints,int offset)
        {
            int sum = 0;
            for (int i = 0; i < offset; i++)
            {
                sum += ints[i];
            }
            return sum;
        }
        public int[] Assemble()
        {
            string[] lines = contents.Split("\r\n");
            List<int> result = new List<int>();

            if (lines.Length == 0)
                return Array.Empty<int>();
            int pc = -1;
            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                var splitted = line.Split(',');
                argCount.Add(splitted.Length-1);
            }
            foreach (var item in argCount)
            {
                Console.Write(item + " ");
            }
            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                var splitted = line.Split(',');
                var command = splitted[0];
                if (command.Contains(':'))
                {
                    pc = i;
                    Console.WriteLine(argCount.Sum());
                    pc += SumTo(argCount, i);
                    var labelwithcommand = command.Split(':');
                    labels.Add(new Token(pc, labelwithcommand[0]));
                    //Console.WriteLine("lbl found");
                    //Console.WriteLine(labels.Count);
                }
                else
                {
                    //Console.WriteLine("lbl not found");
                }
            }
            foreach (string line in lines)
            {
                var splitted = line.Split(',');
                var command = splitted[0];
                if (command.Contains(':'))
                {
                    var labelwithcommand = command.Split(':');
                    command = labelwithcommand[1];
                    action(result, splitted, command);
                    continue;
                }
                else
                {
                    action(result, splitted, command);
                    continue;
                }
            }
            result.Add(0);
            return result.ToArray();
        }

        private static void action(List<int> result, string[] splitted, string command)
        {
            int[] args = Array.Empty<int>();
            if (splitted.Length > 1)
            {

                args = new int[splitted.Length - 1];

                for (int i = 0; i < args.Length; i++)
                {
                    int numArg = 0;
                    var arg = splitted[i + 1];
                    var tokenArg = tokens.Where(t => t.Mnemonic == arg).FirstOrDefault();
                    var lbl = labels.Where(l => l.Mnemonic.Contains(arg)).FirstOrDefault();
                    if (lbl != null) Console.WriteLine(lbl.Mnemonic + ":" + lbl.ByteCode);
                    if (tokenArg != null)
                    {
                        args[i] = tokenArg.ByteCode;
                    }
                    else if (lbl != null)
                    {
                        Console.WriteLine("lbl actioned");
                        args[i] = lbl.ByteCode;
                        continue;
                    }
                    else if (int.TryParse(arg, out numArg))
                    {
                        args[i] = numArg;
                    }
                }
            }
            var token = tokens.Where(t => t.Mnemonic == command).FirstOrDefault();
            if (token != null)
            {
                result.Add(token.ByteCode);
                if (args.Length > 0)
                {
                    result.AddRange(args);
                }
            }
            else
            {
                Console.WriteLine("Instruction " + command + "Not Found");
            }
        }
    }
}
