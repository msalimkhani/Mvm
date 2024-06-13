using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvm.Assembler
{
    internal class FileOps
    {
        private string inFile;
        private string outFile;
        public FileOps(string inFile, string outFile)
        {
            this.inFile = inFile;
            this.outFile = outFile;
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
        public void writeData(int[] data)
        {
            using (FileStream stream = File.OpenWrite(outFile))
            {
                foreach (var item in data)
                {
                    stream.WriteByte((byte)item);
                }
            }
        }
    }
}
