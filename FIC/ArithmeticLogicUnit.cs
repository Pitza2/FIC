using FIC.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIC
{
    public class ArithmeticLogicUnit :IArithmeticLogicUnit
    {
        public IProcessor processor { get; set; }
        Dictionary<string, Action<string[]>> operations;
        public ArithmeticLogicUnit(IProcessor processor)
        {
            operations = new Dictionary<string, Action<string[]>>
            {
                {"ADD", ADD }
                ,{"SUB",SUB}
                ,{"MUL",MUL}
                ,{"DIV",DIV}
            };
            this.processor = processor;
        }

        public void Calculate(string instr, string[] args)
        {
            operations[instr](args);
        }

        public void ADD(string[] args) 
        {
            if(args.Count()!=2) { throw new Exception($"ERROR AT LINE {processor.getRegisterValue("PC")}"); }

            processor.StoreRegister(args[0], (short)(processor.getRegisterValue(args[0]) + processor.getRegisterValue(args[1])));
        }
        public void SUB(string[] args) 
        {
            if (args.Count() != 2) { throw new Exception($"ERROR AT LINE {processor.getRegisterValue("PC")}"); }

            processor.StoreRegister(args[0], (short)(processor.getRegisterValue(args[0]) - processor.getRegisterValue(args[1])));
        }
        public void MUL(string[] args)
        {
            if (args.Count() != 2) { throw new Exception($"ERROR AT LINE {processor.getRegisterValue("PC")}"); }

            processor.StoreRegister(args[0], (short)(processor.getRegisterValue(args[0]) * processor.getRegisterValue(args[1])));
        }
        public void DIV(string[] args)
        {
            if (args.Count() != 2) { throw new Exception($"ERROR AT LINE {processor.getRegisterValue("PC")}"); }

            processor.StoreRegister(args[0], (short)(processor.getRegisterValue(args[0]) /  processor.getRegisterValue(args[1])));
        }
      


    }
}
