using System;
using System.Collections.Generic;
using FIC.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;


namespace FIC.Tests
{
    [TestClass]
    public class ControlUnitJMSTests
    {
        [TestMethod]
        public void JMS_ValidExecution_StoresValuesInProcessor()
        {
            var mockProcessor = new Mock<IProcessor>();
            var controlUnit = new ControlUnit(mockProcessor.Object);
            const string label = "ValidLabel";
            const short labelValue = 10;
            string[] validArguments = new[] { label };

            mockProcessor.Setup(p => p.getRegisterValue("PC")).Returns(5); 
            mockProcessor.Setup(p => p.getLabelValue(label)).Returns(labelValue);

            controlUnit.JMS(validArguments);
            mockProcessor.Verify(p => p.StoreRegister("LR", 5), Times.Once);
            mockProcessor.Verify(p => p.StoreRegister("PC", (short)(labelValue - 1)), Times.Once);
        }


        [TestMethod]
        public void JMS_UnexpectedExecution_ThrowsException()
        {

            var mockProcessor = new Mock<IProcessor>();
            var controlUnit = new ControlUnit(mockProcessor.Object);
            string[] unexpectedArguments = new[] { "unexpected", "argument" };
            Assert.ThrowsException<Exception>(() => controlUnit.JMS(unexpectedArguments));
        }


    }
}
