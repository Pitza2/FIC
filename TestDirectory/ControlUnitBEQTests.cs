using System;
using FIC;
using FIC.Interfaces;
using Moq;

namespace FIC.Tests
{
    [TestClass]
    public class ControlUnitBEQTests
    {
        [TestMethod]
        public void BEQ_WhenCountNotEqual_ThrowsException()
        {
            Mock<IProcessor> mockProcessor = new Mock<IProcessor>();
            ControlUnit controlUnit = new ControlUnit(mockProcessor.Object);
            string[] args = new string[] { "arg1", "arg2" };
            Assert.ThrowsException<Exception>(() => controlUnit.BEQ(args));
            mockProcessor.Verify(p => p.StoreRegister("PC", It.IsAny<short>()), Times.Never);
        }

        [TestMethod]
        
        public void BEQ_WhenSetZeroFlag_True_UpdatesPC()
        {
            Mock<IProcessor> mockProcessor = new Mock<IProcessor>();
            ControlUnit controlUnit = new ControlUnit(mockProcessor.Object);
            Flags f = Flags.GetFlags();
            f.flags |= Flags.SET_ZERO; // Set SET_ZERO flag
            string[] args = new string[] { "Label1" };
            short expectedPCValue = 42;

            mockProcessor.Setup(p => p.getLabelValue("Label1")).Returns((short)(expectedPCValue + 1));

            controlUnit.BEQ(args);
            mockProcessor.Verify(p => p.StoreRegister("PC", (short)(expectedPCValue)), Times.Once);
        }


        [TestMethod]
        public void BEQ_WhenSetZeroFlag_False_DoesNotUpdatePC()
        {
            Mock<IProcessor> mockProcessor = new Mock<IProcessor>();
            ControlUnit controlUnit = new ControlUnit(mockProcessor.Object);
            Flags f = Flags.GetFlags();
            f.flags &= ~Flags.SET_ZERO; 
            string[] args = new string[] { "Label1" };
            controlUnit.BEQ(args);
            mockProcessor.Verify(p => p.StoreRegister("PC", It.IsAny<short>()), Times.Never);
        }
    }
}
