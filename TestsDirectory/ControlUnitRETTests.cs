using System;
using System.Collections.Generic;
using FIC.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;


namespace FIC.Tests
{
    [TestClass]
    public class ControlUnitRETTests
    {
        [TestMethod]
        public void RET_ValidExecution_UpdatesProcessorPCRegister()
        {
            var mockProcessor = new Mock<IProcessor>();
            var controlUnit = new ControlUnit(mockProcessor.Object);
            mockProcessor.Setup(p => p.getRegisterValue("LR")).Returns(10); 
            controlUnit.RET(new string[0]); 

            mockProcessor.Verify(p => p.StoreRegister("PC", 10), Times.Once);
        }

        [TestMethod]
        public void RET_UnexpectedExecution_ThrowsException()
        {
            var mockProcessor = new Mock<IProcessor>();
            var controlUnit = new ControlUnit(mockProcessor.Object);
            Assert.ThrowsException<Exception>(() => controlUnit.RET(new[] { "unexpected", "argument" }));
        }


    }
}
