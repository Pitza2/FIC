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
        Flags f;

        public ArithmeticLogicUnit(IProcessor processor)
        {
            operations = new Dictionary<string, Action<string[]>>
            {
                {"ADD", ADD }
                ,{"SUB",SUB}
                ,{"MUL",MUL}
                ,{"DIV",DIV}
                ,{"CMP",CMP}
                
            };
            this.processor = processor;
            f = Flags.GetFlags();
            
        }
        private void setNegZeroFlags(string arg1)
        {
            if (processor.getRegisterValue(arg1) == 0)
            {
                f.flags = (short)(f.flags | Flags.SET_ZERO);
            }
            if (processor.getRegisterValue(arg1) < 0)
            {
                f.flags = (short)(f.flags | Flags.SET_NEGATIVE);
            }
        }
        public void setCMPFlags(short num1, short num2)
        {
            f.flags = 0;
            if (num2 > 0 && num1 > short.MaxValue - num2 || num2 < 0 && num1 < int.MinValue - num2)
            {
                f.flags = (short)(f.flags | Flags.SET_OVERFLOW);
            }
            if (num1-num2 == 0)
            {
                f.flags = (short)(f.flags | Flags.SET_ZERO);
            }
            if ((num1-num2) < 0)
            {
                f.flags = (short)(f.flags | Flags.SET_NEGATIVE);
            }
        }
        private void setADDFlags(short num1,short num2,string arg1)
        {
            f.flags = 0;
            if(num2 > 0 && num1 > short.MaxValue - num2 || num2 < 0 && num1 < int.MinValue - num2)
            {
                f.flags = (short)(f.flags | Flags.SET_OVERFLOW);
            }
           setNegZeroFlags(arg1);
        }
        private void setSUBFlags(short num1, short num2, string arg1)
        {
            f.flags = 0;
            if (num2 > 0 && num1 < short.MinValue + num2 || num2 < 0 && num1 > short.MaxValue + num2)
            {
                f.flags = (short)(f.flags | Flags.SET_OVERFLOW);
            }
            setNegZeroFlags(arg1);
        }
        private void setMULFlags(short num1,short num2,string arg1)
        {
            f.flags = 0;
            if (num1 > int.MaxValue / num2 || num1 < int.MinValue / num2)
            {
                f.flags = (short)(f.flags | Flags.SET_OVERFLOW);
            }
            setNegZeroFlags(arg1);
        }
        public void Calculate(string instr, string[] args)
        {
            operations[instr](args);
        }

        public void ADD(string[] args) 
        {
            if(args.Count()!=2) { throw new Exception($"ERROR AT LINE {processor.getRegisterValue("PC")}"); }
            int num;
            if (int.TryParse(args[1], out num)){

                processor.StoreRegister(args[0], (short)(processor.getRegisterValue(args[0]) + num));
                setADDFlags(processor.getRegisterValue(args[0]), (short)num, args[0]);
                return;
            }
           
            processor.StoreRegister(args[0], (short)(processor.getRegisterValue(args[0]) + processor.getRegisterValue(args[1])));
            setADDFlags(processor.getRegisterValue(args[0]), processor.getRegisterValue(args[1]), args[0]);
        }
        public void SUB(string[] args) 
        {
            if (args.Count() != 2) { throw new Exception($"ERROR AT LINE {processor.getRegisterValue("PC")}"); }
            int num;
            if (int.TryParse(args[1], out num))
            {
                processor.StoreRegister(args[0], (short)(processor.getRegisterValue(args[0]) - num));
                setSUBFlags(processor.getRegisterValue(args[0]), (short)num, args[0]);
                return;
            }
            processor.StoreRegister(args[0], (short)(processor.getRegisterValue(args[0]) - processor.getRegisterValue(args[1])));
            setSUBFlags(processor.getRegisterValue(args[0]), processor.getRegisterValue(args[1]), args[0]);
        }
        public void MUL(string[] args)
        {
            if (args.Count() != 2) { throw new Exception($"ERROR AT LINE {processor.getRegisterValue("PC")}"); }
            int num;
            if (int.TryParse(args[1], out num))
            {
                processor.StoreRegister(args[0], (short)(processor.getRegisterValue(args[0]) * num));
                setMULFlags(processor.getRegisterValue(args[0]), (short)num, args[0]);
                return;
            }
            processor.StoreRegister(args[0], (short)(processor.getRegisterValue(args[0]) * processor.getRegisterValue(args[1])));
            setMULFlags(processor.getRegisterValue(args[0]), processor.getRegisterValue(args[1]), args[0]);
        }
        public void DIV(string[] args)
        {
            if (args.Count() != 2) { throw new Exception($"ERROR AT LINE {processor.getRegisterValue("PC")}"); }
            int num;
            if (int.TryParse(args[1], out num))
            {
                processor.StoreRegister(args[0], (short)(processor.getRegisterValue(args[0]) / num));
                return;
            }
            processor.StoreRegister(args[0], (short)(processor.getRegisterValue(args[0]) /  processor.getRegisterValue(args[1])));
        }

        public void CMP(string[] args)
        {
            if (args.Count() != 2) { throw new Exception($"ERROR AT LINE {processor.getRegisterValue("PC")}"); }
            int num;
            if (int.TryParse(args[1], out num))
            {
                setCMPFlags(processor.getRegisterValue(args[0]), (short)num);
                return;
            }
            setCMPFlags(processor.getRegisterValue(args[0]), processor.getRegisterValue(args[1]));
        }


    }
}
