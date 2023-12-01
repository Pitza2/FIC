using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FIC.Interfaces;

namespace FIC
{
    internal class ControlUnit : IcontrolUnit
    {
        public IProcessor processor { get; set; }
        public IArithmeticLogicUnit alu { get; set; }
        Dictionary<string, Action<string[]>> operations;

        public ControlUnit(IProcessor processor)
        {
            operations = new Dictionary<string, Action<string[]>>
            {
                {"INP",INP}
                ,{"OUT",OUT}
                ,{"BRA",BRA}
                ,{"JMS", JMS}
                ,{"RET", RET}
            };
            this.processor = processor;
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
        void INP(string[] args)
        {
            short val;
            Console.WriteLine("input:");
            val = short.Parse(Console.ReadLine());
            processor.StoreRegister(args[0], val);
        }

        void OUT(string[] args)
        {
            processor.PrintRegister(args[0]);
        }
        void BRA(string[] args)
        {
            if (args.Count() > 1) { throw new Exception($"ERROR AT LINE {processor.getRegisterValue("PC")}"); }

            processor.StoreRegister("PC", (short)(processor.getLabelValue(args[0])-1));
        }
        void JMS(string[] args)
        {
            if (args.Count() > 1) { throw new Exception($"ERROR AT LINE {processor.getRegisterValue("PC")}"); }

            processor.StoreRegister("LR", processor.getRegisterValue("PC"));
            processor.StoreRegister("PC", (short)(processor.getLabelValue(args[0]) - 1));
        }
        void RET(string[] args)
        {
            if (args.Count() != 0) { throw new Exception($"ERROR AT LINE {processor.getRegisterValue("PC")}"); }

            processor.StoreRegister("PC", (short)(processor.getRegisterValue("LR")));
        }
    }
}
