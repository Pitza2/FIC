using System;
using System.Collections.Generic;
using FIC.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;


namespace FIC.Tests
{
    [TestClass]
    public class ControlUnitINPTests
    {
        [TestMethod]
        public void INP_StoresValueInProcessor_RegisterUpdated()
        {

            var mockProcessor = new Mock<IProcessor>();
            var controlUnit = new ControlUnit(mockProcessor.Object);
            const short inputValue = 42;
            string userInput = inputValue.ToString();
            TextReader originalIn = Console.In;
            using (StringReader sr = new StringReader(userInput))
            {
                Console.SetIn(sr);
                controlUnit.INP(new[] { "R1" });
            }

            Console.SetIn(originalIn);
            mockProcessor.Verify(p => p.StoreRegister("R1", inputValue), Times.Once);
        }

        [TestMethod]
        public void INP_InvalidInput_ThrowsFormatException()
        {

            var mockProcessor = new Mock<IProcessor>();
            var controlUnit = new ControlUnit(mockProcessor.Object);

            string invalidInput = "not_a_number";

            TextReader originalIn = Console.In;

            using (StringReader sr = new StringReader(invalidInput))
            {
                Console.SetIn(sr);
                Assert.ThrowsException<FormatException>(() => controlUnit.INP(new[] { "R1" }));
            }

            Console.SetIn(originalIn);
        }



    }
}
