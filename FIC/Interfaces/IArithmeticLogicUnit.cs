using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIC.Interfaces
{
    public interface IArithmeticLogicUnit
    {
        public void Calculate(string instr, string[] args);
    }
}
