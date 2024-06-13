using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvm
{
    internal class ALU(Register registerA, Register registerB, Register registerSum)
    {
        
        public void Action(bool isSub, bool isMul)
        {
            if (isSub)
            {
                registerSum.value = (registerA.value - registerB.value);
            }
            else if(isMul)
            {
                int res = 0;
                for (int i = 0; i < registerB.value; i++)
                {
                    res += registerA.value;
                }
                registerSum.value = res;
            }
            else
            {
                registerSum.value = (registerA.value + registerB.value);
            }
        }
        
    }
}
