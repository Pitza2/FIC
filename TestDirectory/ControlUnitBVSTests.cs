using System;
using FIC;
using FIC.Interfaces;
using Moq;

namespace FIC.Tests
{
    [TestClass]
    public class ControlUnitBVSTests
    {
        [TestMethod]
        public void BVS_WhenCountNotEqual_ThrowsException()
        {
            Mock<IProcessor> mockProcessor = new Mock<IProcessor>();
            ControlUnit controlUnit = new ControlUnit(mockProcessor.Object);
            string[] args = new string[] { "arg1", "arg2" };
            Assert.ThrowsException<Exception>(() => controlUnit.BVS(args));
            mockProcessor.Verify(p => p.StoreRegister("PC", It.IsAny<short>()), Times.Never);
        }

        [TestMethod]
        public void BVS_WhenOverflowFlagSet_UpdatesPC()
        {
            Mock<IProcessor> mockProcessor = new Mock<IProcessor>();
            ControlUnit controlUnit = new ControlUnit(mockProcessor.Object);
            Flags f = Flags.GetFlags();
            f.flags |= Flags.SET_OVERFLOW;
            string[] args = new string[] { "Label1" };
            short expectedPCValue = 42;

            mockProcessor.Setup(p => p.getLabelValue("Label1")).Returns((short)(expectedPCValue + 1));
            controlUnit.BVS(args);
            mockProcessor.Verify(p => p.StoreRegister("PC", (short)(expectedPCValue)), Times.Once);
        }

        [TestMethod]
        public void BVS_WhenOverflowFlagNotSet_DoesNotUpdatePC()
        {
            Mock<IProcessor> mockProcessor = new Mock<IProcessor>();
            ControlUnit controlUnit = new ControlUnit(mockProcessor.Object);
            Flags f = Flags.GetFlags();
            f.flags &= ~Flags.SET_OVERFLOW;
            string[] args = new string[] { "Label1" };
            controlUnit.BVS(args);
            mockProcessor.Verify(p => p.StoreRegister("PC", It.IsAny<short>()), Times.Never);
        }
    }
}
