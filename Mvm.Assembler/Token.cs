using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvm.Assembler
{
    internal class Token
    {
        public int ByteCode { get; set; }
        public string Mnemonic { get; set; }
        public Token(int byteCode, string mnemonic)
        {
            ByteCode = byteCode;
            Mnemonic = mnemonic;
        }
    }
}
