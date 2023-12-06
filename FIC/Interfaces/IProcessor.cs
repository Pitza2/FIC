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
        public void PrintRegister(string reg);
        public short getRegisterValue(string reg);
        public short getLabelValue(string reg);
    }
}
