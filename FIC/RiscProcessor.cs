using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FIC.Interfaces;

namespace FIC
{
    public class RiscProcessor : IProcessor
    {
        
        static readonly string[] reservedKeywords = { "INP" ,"OUT", "ADD", "SUB", "MUL", "DIV", "BRA", "CMP", "BEQ", "HLT", "RET", "JMS" };
        Dictionary<string, short> labelLine=new Dictionary<string, short>();
        Dictionary<string, short> registers;
        List<short> mainMemory;
        List<string> instructions;
        IcontrolUnit IcontrolUnit;
        public RiscProcessor(ControlUnit cu,ArithmeticLogicUnit alu)
        {
            registers = new Dictionary<string, short>
            {
                {"R0",0 }
                ,{"R1",0}
                ,{"R2",0}
                ,{"R3",0}
                ,{"R4",0}
                ,{"R5",0}
                ,{"R6",0}
                ,{"R7",0}
                ,{"SP",400}
                ,{"LR",0}
                ,{"PC",0}
            };
            instructions = new List<string>();
            mainMemory = new List<short>(400);
            cu.processor = this;
            alu.processor = this;
            cu.alu= alu;
            IcontrolUnit = cu;
            
        }
        public void load()
        {
            string path = "C:\\Users\\IO\\source\\repos\\FIC\\Program.txt";
            instructions.Clear();
            string[] preInstructions= File.ReadAllLines(path);
            preInstructions = preInstructions.Where(x => x != string.Empty).ToArray();
            //pre process part
            for(int i=0;i<preInstructions.Length;i++)
            {
                string[] tokens = preInstructions[i].Split(' ');
                if (!reservedKeywords.Contains(tokens[0]))
                {
                    labelLine.Add(tokens[0], (short)(i));
                    preInstructions[i] = preInstructions[i].Trim().Substring(preInstructions[i].IndexOf(" ")+1);
                    Console.WriteLine(preInstructions[i]);

                }
            }
            instructions = preInstructions.ToList();
        }
        public void run()
        {
            while (instructions[registers["PC"]] != "HLT")
            {
                //dostuff
                IcontrolUnit.Execute(instructions[registers["PC"]]);
                registers["PC"]++;
            }
        }
        public void StoreRegister(string reg,short value) {
            registers[reg] = value;
        }
        public void PrintRegister(string reg) {
            Console.WriteLine(registers[reg]);
        }
        public short getRegisterValue(string reg) { return registers[reg]; }
        public short getLabelValue(string reg) { return labelLine[reg]; }
        public List<string> getInstructions()
        {
            return instructions;
        }
    }
}
