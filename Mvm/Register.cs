using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvm
{
    internal class Register
    {
        public int id { get;private set; }
        public string name { get; private set; }
        public int value { get; set; }
        public Register(int id, string name, int value)
        {
            this.id = id;
            this.name = name;
            this.value = value;
        }
        public override string ToString()
        {
            return name + " :" + value;
        }
    }
}
