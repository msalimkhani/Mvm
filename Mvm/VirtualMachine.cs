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
        public void If(Register r1, Register r2)
        {
            if(r1.value == r2.value)
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
                }
                else if (program == Programs.JMP)
                {
                    programCounter++;
                    var data = ram.ProgramMemory[programCounter];
                    programCounter = data - 1;
                    continue;
                }
                else if(program == Programs.IF)
                {
                    If(registerA1, registerA2);
                    continue;
                }
                else if (program == Programs.IFN)
                {
                    Ifn(registerA1, registerA2);
                    continue;
                }
                else if(program == Programs.JICT)
                {
                    if (registerC.value == 1)
                    {
                        programCounter++;
                        var data = ram.ProgramMemory[programCounter];
                        
                        programCounter = data - 1;
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
                    }
                    continue;
                }
                else if(program == Programs.MOV)
                {
                    programCounter++;
                    var register = ram.ProgramMemory[programCounter];
                    if(register == Programs.A1)
                    {
                        programCounter++;
                        var data = ram.ProgramMemory[programCounter];
                        if(data == Programs.A)
                        {
                            registerA1.value = registerA.value;
                        }
                        else if (data == Programs.A1)
                        {
                            registerA1.value = registerA1.value;
                        }
                        else if(data == Programs.A2)
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
                    }
                    continue;
                }
                else if(program == Programs.PROMPT)
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
                    else if(register == Programs.A)
                    {
                        int read;
                        read = Convert.ToInt32(Console.ReadLine());
                        registerA.value = read;
                    }
                    else if(register == Programs.B)
                    {
                        int read;
                        read = Convert.ToInt32(Console.ReadLine());
                        registerB.value = read;
                    }
                    continue;
                }
                else if(program ==Programs.NOP)
                {
                    continue;
                }
                else if(program == Programs.HALT)
                {
                    break;
                }
            }
        }
    }
}
