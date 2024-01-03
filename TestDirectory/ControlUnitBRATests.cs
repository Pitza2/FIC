using System;
using System.Collections.Generic;
using FIC.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;


namespace FIC.Tests
{
    [TestClass]
    public class ControlUnitBRATests
    {
        [TestMethod]
        public void BRA_ValidExecution_StoreRegisterCalledWithExpectedValue()
        {
            var mockProcessor = new Mock<IProcessor>();
            var controlUnit = new ControlUnit(mockProcessor.Object);
            const string label = "ValidLabel";
            const short labelValue = 10;
            string[] validArguments = new[] { label };

            mockProcessor.Setup(p => p.getLabelValue(label)).Returns(labelValue);
            controlUnit.BRA(validArguments);
            mockProcessor.Verify(p => p.StoreRegister("PC", (short)(labelValue - 1)), Times.Once);
        }

        [TestMethod]
        public void BRA_UnexpectedExecution_ThrowsException()
        {

            var mockProcessor = new Mock<IProcessor>();
            var controlUnit = new ControlUnit(mockProcessor.Object);
            string[] unexpectedArguments = new[] { "unexpected", "argument" };
            Assert.ThrowsException<Exception>(() => controlUnit.BRA(unexpectedArguments));
        }



    }
}
