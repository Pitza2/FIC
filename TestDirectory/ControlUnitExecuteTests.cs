using System;
using System.Collections.Generic;
using FIC.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FIC.Tests
{
    [TestClass]
    public class ControlUnitExecuteTests
    {
        [TestMethod]
        public void Execute_ThrowsException_NullInstruction()
        {

            var mockProcessor = new Mock<IProcessor>();
            var controlUnit = new ControlUnit(mockProcessor.Object);

 
            Assert.ThrowsException<Exception>(() => controlUnit.Execute(null));
        }

        [TestMethod]
        public void Execute_Calls_ALU_Calculate_When_TokensCountIsThree()
        {
  
            var mockProcessor = new Mock<IProcessor>();
            var mockALU = new Mock<IArithmeticLogicUnit>();
            var controlUnit = new ControlUnit(mockProcessor.Object) { alu = mockALU.Object };

 
            string input = "ADD R1 R2";
            controlUnit.Execute(input);

 
            mockALU.Verify(a => a.Calculate("ADD", new[] { "R1", "R2" }), Times.Once);
        }

        [TestMethod]
        public void Execute_Calls_INP_Method_When_OperationIsINP()
        {

            var mockProcessor = new Mock<IProcessor>();
            var controlUnit = new ControlUnit(mockProcessor.Object);
            short inputValue = 42; 

            Mock<System.IO.TextReader> mockReader = new Mock<System.IO.TextReader>();
            mockReader.Setup(r => r.ReadLine()).Returns(inputValue.ToString());
            Console.SetIn(mockReader.Object);

   
            string input = "INP R1";
            controlUnit.Execute(input);

            mockProcessor.Verify(p => p.StoreRegister("R1", inputValue), Times.Once);
        }


        [TestMethod]
        public void Execute_Calls_BRA_Method_When_OperationIsBRA()
        {

            var mockProcessor = new Mock<IProcessor>();
            var controlUnit = new ControlUnit(mockProcessor.Object);
            mockProcessor.Setup(p => p.getRegisterValue("PC")).Returns(5);
            mockProcessor.Setup(p => p.getLabelValue("Label")).Returns((short)10);


            string input = "BRA Label";
            controlUnit.Execute(input);

            mockProcessor.Verify(p => p.StoreRegister("PC", 9), Times.Once);
        }

        [TestMethod]
        public void Execute_ThrowsException_TooManyTokens()
        {

            var mockProcessor = new Mock<IProcessor>();
            var controlUnit = new ControlUnit(mockProcessor.Object);

            Assert.ThrowsException<Exception>(() => controlUnit.Execute("ADD R1 R2 R3"));
        }

        [TestMethod]
        public void Execute_CatchesException_ForInvalidOperation()
        {

            var mockProcessor = new Mock<IProcessor>();
            var controlUnit = new ControlUnit(mockProcessor.Object);

            Assert.ThrowsException<Exception>(() => controlUnit.Execute("INVALID_OP R1"));
        }

        
    }
}
