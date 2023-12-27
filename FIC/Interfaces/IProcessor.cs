using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIC.Interfaces
{
    public interface IProcessor
    {
        public void StoreRegister(string reg, short val);
        public void WriteToMainMem(short val, int location);
        public void WriteStack(short val);
        public void ReadStack(string arg);
        public void PrintRegister(string reg);
        public short getRegisterValue(string reg);
        public short getLabelValue(string reg);
    }
}
