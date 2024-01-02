using System;
using System.Linq;
using FIC;
using FIC.Interfaces;
using Moq;

namespace FIC.Tests
{
    [TestClass]
    public class ControlUnitPOPTests
    {

        [TestMethod]
        public void POP_WhenValidArgument_PopsFromStackAndIncrementsSP()
        {
            Mock<IProcessor> mockProcessor = new Mock<IProcessor>();
            ControlUnit controlUnit = new ControlUnit(mockProcessor.Object);
            short initialSPValue = 99;
            short expectedSPValue = 100;
            string[] args = new string[] { "{R1}" };

            mockProcessor.Setup(p => p.getRegisterValue("SP")).Returns(initialSPValue);
            mockProcessor.Setup(p => p.ReadStack("R1")).Verifiable();
            controlUnit.POP(args);
            mockProcessor.Verify(p => p.ReadStack("R1"), Times.Once);
            mockProcessor.Verify(p => p.StoreRegister("SP", expectedSPValue), Times.Once);
        }

        [TestMethod]
        public void POP_WhenInvalidArgument_ThrowsException()
        {
            Mock<IProcessor> mockProcessor = new Mock<IProcessor>();
            ControlUnit controlUnit = new ControlUnit(mockProcessor.Object);
            string[] args = new string[] { "InvalidArgument" };
            Assert.ThrowsException<Exception>(() => controlUnit.POP(args));
        }
    }
}
