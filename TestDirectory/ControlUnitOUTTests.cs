using System;
using System.Collections.Generic;
using FIC.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;


namespace FIC.Tests
{
    [TestClass]
    public class ControlUnitOUTTests
    {

        [TestMethod]
        public void OUT_CallsPrintRegister()
        {

            var mockProcessor = new Mock<IProcessor>();
            var controlUnit = new ControlUnit(mockProcessor.Object);
            const string registerToPrint = "R1";
            controlUnit.OUT(new[] { registerToPrint });
            mockProcessor.Verify(p => p.PrintRegister(registerToPrint), Times.Once);
        }



    }
}
