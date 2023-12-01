
// See https://aka.ms/new-console-template for more information
using FIC;

internal class Program
{
    static void Main(string[] args)
    {
        ControlUnit cu = new ControlUnit(null);
        ArithmeticLogicUnit alu = new ArithmeticLogicUnit(null);
        RiscProcessor processor = new RiscProcessor(cu,alu);
        
        processor.load();
        processor.run();
    }
}
