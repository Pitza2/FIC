using System;
using System.Linq;
using FIC;
using FIC.Interfaces;
using Moq;


namespace FIC.Tests
{
    [TestClass]
    public class ControlUnitPSHTests
    {
        [TestMethod]
        public void PSH_WhenValidArgument_PushesToStackAndDecrementsSP()
        {
            Mock<IProcessor> mockProcessor = new Mock<IProcessor>();
            ControlUnit controlUnit = new ControlUnit(mockProcessor.Object);
            short initialSPValue = 100;
            short expectedSPValue = 99;
            string[] args = new string[] { "{R1}" };
            mockProcessor.Setup(p => p.getRegisterValue("SP")).Returns(initialSPValue);
            mockProcessor.Setup(p => p.getRegisterValue("R1")).Returns(42);
            controlUnit.PSH(args);
            mockProcessor.Verify(p => p.WriteStack(42), Times.Once);
            mockProcessor.Verify(p => p.StoreRegister("SP", expectedSPValue), Times.Once);
        }

        [TestMethod]
       // [ExpectedException(typeof(Exception), "ERROR AT LINE 81")]
        public void PSH_WhenInvalidArgument_ThrowsException()
        {
            Mock<IProcessor> mockProcessor = new Mock<IProcessor>();
            ControlUnit controlUnit = new ControlUnit(mockProcessor.Object);
            string[] args = new string[] { "InvalidArgument" };
            Assert.ThrowsException<Exception>(() => controlUnit.PSH(args));
        }
    }
}
