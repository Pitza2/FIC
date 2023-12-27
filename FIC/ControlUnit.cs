using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FIC.Interfaces;

namespace FIC
{
    public class ControlUnit : IcontrolUnit
    {
        public IProcessor processor { get; set; }
        public IArithmeticLogicUnit alu { get; set; }
        Dictionary<string, Action<string[]>> operations;
        Flags f;
        public ControlUnit(IProcessor processor)
        {
            operations = new Dictionary<string, Action<string[]>>
            {
                {"INP",INP}
                ,{"OUT",OUT}
                ,{"JMS", JMS}
                ,{"RET", RET}
                ,{"PSH",PSH}
                ,{"POP",POP}
                ,{"BRA",BRA}
                ,{"BEQ",BEQ}
                ,{"BLE",BLE}
                ,{"BLT",BLT}
                ,{"BVS",BVS}

            };
            this.processor = processor;
            f = Flags.GetFlags();
        }
        public void Execute(string instruction)
        {
            if (instruction == null) { throw new Exception("unknown error"); }
            List<string> tokens = instruction.Split(' ').ToList();

            if (tokens.Count > 3) { throw new Exception($"ERROR AT LINE {processor.getRegisterValue("PC")}"); }
            if (tokens.Count == 3)
            {
                alu.Calculate(tokens[0], tokens.Skip(1).ToArray());
                return;
            }
            try
            {
                operations[tokens[0]](tokens.Skip(1).ToArray());
            }
            catch { throw new Exception($"ERROR AT LINE {processor.getRegisterValue("PC")}"); }           
        }
        public void INP(string[] args)
        {
            short val;
            Console.WriteLine("input:");
            val = short.Parse(Console.ReadLine());
            processor.StoreRegister(args[0], val);
        }

        public void OUT(string[] args)
        {
            processor.PrintRegister(args[0]);
        }

        public void JMS(string[] args)
        {
            if (args.Count() > 1) { throw new Exception($"ERROR AT LINE {processor.getRegisterValue("PC")}"); }

            processor.StoreRegister("LR", processor.getRegisterValue("PC"));
            processor.StoreRegister("PC", (short)(processor.getLabelValue(args[0]) - 1));
        }
        public void RET(string[] args)
        {
            if (args.Count() != 0) { throw new Exception($"ERROR AT LINE {processor.getRegisterValue("PC")}"); }

            processor.StoreRegister("PC", (short)(processor.getRegisterValue("LR")));
        }
        public void PSH(string[] args)
        {
            if (args.Count() > 1 && args[0][0]!='{' || args[0][args[0].Length-1]!='}')
            { throw new Exception($"ERROR AT LINE {processor.getRegisterValue("PC")}"); }

            processor.WriteStack(processor.getRegisterValue(args[0].Substring(1, args[0].Length-2)));
            processor.StoreRegister("SP", (short)(processor.getRegisterValue("SP") - 1));
        }
        public void POP(string[] args)
        {
            if (args.Count() > 1 && args[0][0] != '{' || args[0][args[0].Length - 1] != '}')
            { throw new Exception($"ERROR AT LINE {processor.getRegisterValue("PC")}"); }
            processor.ReadStack(args[0].Substring(1, args[0].Length - 2));
            processor.StoreRegister("SP", (short)(processor.getRegisterValue("SP") + 1));
        }
        public void BEQ(string[] args)
        {
            if (args.Count() != 1) { throw new Exception($"ERROR AT LINE {processor.getRegisterValue("PC")}"); }
            if ((f.flags & Flags.SET_ZERO) != 0)
            {
                processor.StoreRegister("PC", (short)(processor.getLabelValue(args[0]) - 1));
            }
        }
        public void BLE(string[] args)
        {
            if (args.Count() != 1) { throw new Exception($"ERROR AT LINE {processor.getRegisterValue("PC")}"); }
            if ((f.flags & Flags.SET_NEGATIVE) != 0 || (f.flags & Flags.SET_ZERO) != 0)
            {
                processor.StoreRegister("PC", (short)(processor.getLabelValue(args[0]) - 1));
            }
        }

        public void BLT(string[] args)
        {
            if (args.Count() != 1) { throw new Exception($"ERROR AT LINE {processor.getRegisterValue("PC")}"); }
            if ((f.flags & Flags.SET_NEGATIVE) != 0)
            {
                processor.StoreRegister("PC", (short)(processor.getLabelValue(args[0]) - 1));
            }
        }

        public void BVS(string[] args)
        {
            if (args.Count() != 1) { throw new Exception($"ERROR AT LINE {processor.getRegisterValue("PC")}"); }
            if ((f.flags & Flags.SET_OVERFLOW) != 0)
            {
                processor.StoreRegister("PC", (short)(processor.getLabelValue(args[0]) - 1));
            }
        }

        public void BRA(string[] args)
        {
            if (args.Count() > 1) { throw new Exception($"ERROR AT LINE {processor.getRegisterValue("PC")}"); }

            processor.StoreRegister("PC", (short)(processor.getLabelValue(args[0]) - 1));
        }


    }
}
