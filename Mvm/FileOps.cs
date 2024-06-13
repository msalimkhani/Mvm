using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvm
{
    internal class FileOps
    {
        private string inFile;
        public FileOps(string inFile)
        {
            this.inFile = inFile;
        }
        public string readToEndString()
        {
            using (FileStream stream = File.OpenRead(inFile))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
        public byte[] readToEndBytes()
        {
            var bytes = File.ReadAllBytes(inFile);
            return bytes;
        }
    }
}
