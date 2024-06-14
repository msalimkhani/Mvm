using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace Mvm
{
    internal class VirtualMachine
    {
        private int programCounter = -1;
        Register registerA;
        Register registerB;
        Register registerSum;
        Register registerC;
        Register registerRes;
        Register registerA1;
        Register registerA2;
        RAM ram;
        ALU alu;
        public VirtualMachine(RAM ram)
        {
            this.ram = ram;
            registerA = new Register(1, "A", 0);
            registerB = new Register(2, "B", 0);
            registerSum = new Register(3, "Sum", 0);
            registerC = new Register(4, "C", 0);
            registerRes = new Register(5, "RES", 0);
            registerA1 = new Register(6, "A1", 0);
            registerA2 = new Register(7, "A2", 0);
            alu = new ALU(registerA, registerB, registerSum);
        }
        public void loadA(int data)
        {
            registerA.value = data;
        }
        public void loadB(int data)
        {
            registerB.value = data;
        }
        public void And(Register r1, Register r2)
        {
            r1.value = r1.value & r2.value;
        }
        public void Or(Register r1, Register r2)
        {
            r1.value = r1.value | r2.value;
        }
        public void actionAnd(Register r)
        {
            programCounter++;
            var r2 = ram.ProgramMemory[programCounter];
            switch (r2)
            {
                case Programs.A:
                    And(r, registerA);
                    break;
                case Programs.B:
                    And(r, registerB);
                    break;
                case Programs.A1:
                    And(r, registerA1);
                    break;
                case Programs.A2:
                    And(r, registerA2);
                    break;
                case Programs.RES:
                    And(r, registerRes);
                    break;
            }
        }
        public void actionOr(Register r)
        {
            programCounter++;
            var r2 = ram.ProgramMemory[programCounter];
            switch (r2)
            {
                case Programs.A:
                    Or(r, registerA);
                    break;
                case Programs.B:
                    Or(r, registerB);
                    break;
                case Programs.A1:
                    Or(r, registerA1);
                    break;
                case Programs.A2:
                    Or(r, registerA2);
                    break;
                case Programs.RES:
                    Or(r, registerRes);
                    break;
            }
        }
        public void actionSubR(Register r)
        {
            programCounter++;
            var r2 = ram.ProgramMemory[programCounter];
            switch (r2)
            {
                case Programs.A:
                    SubR(r, registerA);
                    break;
                case Programs.B:
                    SubR(r, registerB);
                    break;
                case Programs.A1:
                    SubR(r, registerA1);
                    break;
                case Programs.A2:
                    SubR(r, registerA2);
                    break;
                case Programs.RES:
                    SubR(r, registerRes);
                    break;
                default:
                    r.value -= r2;
                    break;

            }
        }
        public void actionAddR(Register r)
        {
            programCounter++;
            var r2 = ram.ProgramMemory[programCounter];
            switch (r2)
            {
                case Programs.A:
                    AddR(r, registerA);
                    break;
                case Programs.B:
                    AddR(r, registerB);
                    break;
                case Programs.A1:
                    AddR(r, registerA1);
                    break;
                case Programs.A2:
                    AddR(r, registerA2);
                    break;
                case Programs.RES:
                    AddR(r, registerRes);
                    break;
                default:
                    r.value += r2;
                    break;

            }
        }
        public void SubR(Register r1, Register r2)
        {
            r1.value -= r2.value;
        }
        public void AddR(Register r1, Register r2)
        {
            r1.value += r2.value;
        }
        public void If(Register r1, Register r2)
        {
            if (r1.value == r2.value)
            {
                registerC.value = 1;
            }
            else
            {
                registerC.value = 2;
            }
        }
        public void Ifn(Register r1, Register r2)
        {
            if (r1.value == r2.value)
            {
                registerC.value = 2;
            }
            else
            {
                registerC.value = 1;
            }
        }
        public void run()
        {
            for (programCounter = 0; programCounter < ram.ProgramMemory.Length; programCounter++)
            {
                var program = ram.ProgramMemory[programCounter];
                if (program == Programs.LOADA)
                {
                    programCounter++;
                    var data = ram.ProgramMemory[programCounter];
                    loadA(data);
                    continue;
                }
                else if (program == Programs.LOADB)
                {
                    programCounter++;
                    var data = ram.ProgramMemory[programCounter];
                    loadB(data);
                    continue;
                }
                else if (program == Programs.ADD)
                {
                    alu.Action(false, false);
                    registerRes.value = registerSum.value;
                    continue;
                }
                else if (program == Programs.SUB)
                {
                    alu.Action(true, false);
                    registerRes.value = registerSum.value;
                    continue;
                }
                else if (program == Programs.PRINTRES)
                {
                    Console.WriteLine(registerRes.value);
                    continue;
                }
                else if (program == Programs.GETSTAT)
                {
                    Console.WriteLine(registerA);
                    Console.WriteLine(registerB);
                    Console.WriteLine(registerSum);
                    Console.WriteLine(registerRes);
                    Console.WriteLine(registerC);
                    continue;
                }
                else if (program == Programs.MUL)
                {
                    alu.Action(false, true);
                    registerRes.value = registerSum.value;
                    continue;
                }
                else if (program == Programs.JMP)
                {
                    programCounter++;
                    var data = ram.ProgramMemory[programCounter];
                    programCounter = data - 1;
                    continue;
                }
                else if (program == Programs.IF)
                {
                    If(registerA1, registerA2);
                    continue;
                }
                else if (program == Programs.IFN)
                {
                    Ifn(registerA1, registerA2);
                    continue;
                }
                else if (program == Programs.JICT)
                {
                    if (registerC.value == 1)
                    {
                        programCounter++;
                        var data = ram.ProgramMemory[programCounter];
                        programCounter = data - 1;
                        continue;
                    }
                    continue;
                }
                else if (program == Programs.JICF)
                {
                    if (registerC.value == 2)
                    {
                        programCounter++;
                        var data = ram.ProgramMemory[programCounter];
                        programCounter = data - 1;
                        continue;
                    }
                    continue;
                }
                else if (program == Programs.MOV)
                {
                    programCounter++;
                    var register = ram.ProgramMemory[programCounter];
                    if (register == Programs.A1)
                    {
                        programCounter++;
                        var data = ram.ProgramMemory[programCounter];
                        if (data == Programs.A)
                        {
                            registerA1.value = registerA.value;
                        }
                        else if (data == Programs.A1)
                        {
                            registerA1.value = registerA1.value;
                        }
                        else if (data == Programs.A2)
                        {
                            registerA1.value = registerA2.value;
                        }
                        else if (data == Programs.B)
                        {
                            registerA1.value = registerB.value;
                        }
                        else if (data == Programs.RES)
                        {
                            registerA1.value = registerRes.value;
                        }
                        else
                        {
                            registerA1.value = data;
                        }

                        continue;
                    }
                    else if (register == Programs.A2)
                    {
                        programCounter++;
                        var data = ram.ProgramMemory[programCounter];
                        if (data == Programs.A)
                        {
                            registerA2.value = registerA.value;
                        }
                        else if (data == Programs.A1)
                        {
                            registerA2.value = registerA1.value;
                        }
                        else if (data == Programs.B)
                        {
                            registerA2.value = registerB.value;
                        }
                        else if (data == Programs.RES)
                        {
                            registerA2.value = registerRes.value;
                        }
                        else
                        {
                            registerA2.value = data;
                        }

                        continue;
                    }
                    else if (register == Programs.RES)
                    {
                        programCounter++;
                        var data = ram.ProgramMemory[programCounter];
                        if (data == Programs.A)
                        {
                            registerRes.value = registerA.value;
                        }
                        else if (data == Programs.A1)
                        {
                            registerRes.value = registerA1.value;
                        }
                        else if (data == Programs.B)
                        {
                            registerRes.value = registerB.value;
                        }
                        else if (data == Programs.A2)
                        {
                            registerRes.value = registerA2.value;
                        }
                        else if (data == Programs.C)
                        {
                            registerRes.value = registerC.value;
                        }
                        else
                        {
                            registerRes.value = data;
                        }

                        continue;
                    }
                    else if (register == Programs.A)
                    {
                        programCounter++;
                        var data = ram.ProgramMemory[programCounter];
                        if (data == Programs.A2)
                        {
                            registerA.value = registerA2.value;
                        }
                        else if (data == Programs.A1)
                        {
                            registerA.value = registerA1.value;
                        }
                        else if (data == Programs.B)
                        {
                            registerA.value = registerB.value;
                        }
                        else if (data == Programs.RES)
                        {
                            registerA.value = registerRes.value;
                        }
                        else
                        {
                            registerA.value = data;
                        }

                        continue;
                    }
                    else if (register == Programs.B)
                    {
                        programCounter++;
                        var data = ram.ProgramMemory[programCounter];
                        if (data == Programs.A2)
                        {
                            registerB.value = registerA2.value;
                        }
                        else if (data == Programs.A1)
                        {
                            registerB.value = registerA1.value;
                        }
                        else if (data == Programs.A)
                        {
                            registerB.value = registerA.value;
                        }
                        else if (data == Programs.RES)
                        {
                            registerB.value = registerRes.value;
                        }
                        else
                        {
                            registerB.value = data;
                        }

                        continue;
                    }
                }
                else if (program == Programs.PROMPT)
                {
                    programCounter++;
                    var register = ram.ProgramMemory[programCounter];
                    if (register == Programs.A1)
                    {
                        int read;
                        read = Convert.ToInt32(Console.ReadLine());
                        registerA1.value = read;
                    }
                    else if (register == Programs.A2)
                    {
                        int read;
                        read = Convert.ToInt32(Console.ReadLine());
                        registerA2.value = read;
                    }
                    else if (register == Programs.A)
                    {
                        int read;
                        read = Convert.ToInt32(Console.ReadLine());
                        registerA.value = read;
                    }
                    else if (register == Programs.B)
                    {
                        int read;
                        read = Convert.ToInt32(Console.ReadLine());
                        registerB.value = read;
                    }
                    continue;
                }
                else if (program == Programs.NOP)
                {
                    continue;
                }
                else if (program == Programs.AND)
                {
                    programCounter++;
                    var r1 = ram.ProgramMemory[programCounter];
                    switch (r1)
                    {
                        case Programs.A:
                            actionAnd(registerA);
                            break;
                        case Programs.A2:
                            actionAnd(registerA2);
                            break;
                        case Programs.B:
                            actionAnd(registerB);
                            break;
                        case Programs.A1:
                            actionAnd(registerA1);
                            break;
                        case Programs.RES:
                            actionAnd(registerRes);
                            break;
                    }
                    continue;
                }
                else if (program == Programs.OR)
                {
                    programCounter++;
                    var r1 = ram.ProgramMemory[programCounter];
                    switch (r1)
                    {
                        case Programs.A:
                            actionOr(registerA);
                            break;
                        case Programs.A2:
                            actionOr(registerA2);
                            break;
                        case Programs.B:
                            actionOr(registerB);
                            break;
                        case Programs.A1:
                            actionOr(registerA1);
                            break;
                        case Programs.RES:
                            actionOr(registerRes);
                            break;
                    }
                    continue;
                }
                else if (program == Programs.SUBR)
                {
                    programCounter++;
                    var r1 = ram.ProgramMemory[programCounter];
                    switch (r1)
                    {
                        case Programs.A:
                            actionSubR(registerA);
                            break;
                        case Programs.A2:
                            actionSubR(registerA2);
                            break;
                        case Programs.B:
                            actionSubR(registerB);
                            break;
                        case Programs.A1:
                            actionSubR(registerA1);
                            break;
                        case Programs.RES:
                            actionSubR(registerRes);
                            break;
                    }
                    continue;
                }
                else if (program == Programs.HALT)
                {
                    break;
                }
                else if (program == Programs.INC)
                {
                    programCounter++;
                    var register = ram.ProgramMemory[programCounter];
                    switch (register)
                    {
                        case Programs.A:
                            actionINC(registerA);
                            break;
                        case Programs.A2:
                            actionINC(registerA2);
                            break;
                        case Programs.A1:
                            actionINC(registerA1);
                            break;
                        case Programs.RES:
                            actionINC(registerRes);
                            break;
                        case Programs.B:
                            actionINC(registerB);
                            break;
                        default:
                            throw new Exception("Unhandled Code.");
                    }
                }
                else if (program == Programs.ADDR)
                {
                    programCounter++;
                    var r1 = ram.ProgramMemory[programCounter];
                    switch (r1)
                    {
                        case Programs.A:
                            actionAddR(registerA);
                            break;
                        case Programs.A2:
                            actionAddR(registerA2);
                            break;
                        case Programs.B:
                            actionAddR(registerB);
                            break;
                        case Programs.A1:
                            actionAddR(registerA1);
                            break;
                        case Programs.RES:
                            actionAddR(registerRes);
                            break;
                    }
                    continue;
                }
            }
        }

        private void actionINC(Register r)
        {
            r.value += 1;
        }
    }
}
