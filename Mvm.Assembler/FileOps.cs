using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            try
            {
                using (FileStream stream = File.OpenRead(inFile))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public byte[] readToEndBytes()
        {
            var bytes = File.ReadAllBytes(inFile);
            return bytes;
        }
        public void writeInstructions(int[] ints)
        {
            try
            {
                using (FileStream stream = File.OpenWrite(outFile))
                {
                    foreach (var item in ints)
                    {
                        stream.WriteByte((byte)item);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
