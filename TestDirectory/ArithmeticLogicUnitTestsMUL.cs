using Moq;
using FIC.Interfaces;
using FIC;

namespace TestsDirectory
{
    [TestClass]
    public class ArithmeticLogicUnitTestsMUL
    {
        [TestMethod]
        public void MUL_MultiplyByZero()
        {

            Mock<IProcessor> mockProcessor = new Mock<IProcessor>();
            mockProcessor.Setup(x => x.getRegisterValue("R1")).Returns(0);
            mockProcessor.Setup(x => x.getRegisterValue("R2")).Returns(6);

            ArithmeticLogicUnit alu = new ArithmeticLogicUnit(mockProcessor.Object);
            string[] args = new string[] { "R1", "R2" };


            alu.MUL(args);

            mockProcessor.Verify(x => x.StoreRegister("R1",0), Times.Once);
        }

        [TestMethod]
        public void MUL_MultiplyByOne()
        {

            Mock<IProcessor> mockProcessor = new Mock<IProcessor>();
            mockProcessor.Setup(x => x.getRegisterValue("R1")).Returns(1);
            mockProcessor.Setup(x => x.getRegisterValue("R2")).Returns(6);

            ArithmeticLogicUnit alu = new ArithmeticLogicUnit(mockProcessor.Object);
            string[] args = new string[] { "R1", "R2" };


            alu.MUL(args);

            mockProcessor.Verify(x => x.StoreRegister("R1", 6), Times.Once);
        }

        [TestMethod]
        public void MUL_MultiplyByNegative()
        {

            Mock<IProcessor> mockProcessor = new Mock<IProcessor>();
            mockProcessor.Setup(x => x.getRegisterValue("R1")).Returns(-5);
            mockProcessor.Setup(x => x.getRegisterValue("R2")).Returns(6);

            ArithmeticLogicUnit alu = new ArithmeticLogicUnit(mockProcessor.Object);
            string[] args = new string[] { "R1", "R2" };


            alu.MUL(args);

            mockProcessor.Verify(x => x.StoreRegister("R1", -30), Times.Once);
        }

        [TestMethod]
        public void MUL_LowerRange_Success()
        {

            Mock<IProcessor> mockProcessor = new Mock<IProcessor>();
            mockProcessor.Setup(x => x.getRegisterValue("R1")).Returns(short.MinValue/2);
            mockProcessor.Setup(x => x.getRegisterValue("R2")).Returns(2);

            ArithmeticLogicUnit alu = new ArithmeticLogicUnit(mockProcessor.Object);
            string[] args = new string[] { "R1", "R2" };


            alu.MUL(args);

            mockProcessor.Verify(x => x.StoreRegister("R1", short.MinValue), Times.Once);
        }

        [TestMethod]
        public void MUL_UpperRange_Success()
        {

            Mock<IProcessor> mockProcessor = new Mock<IProcessor>();
            mockProcessor.Setup(x => x.getRegisterValue("R1")).Returns((short.MaxValue)/2);
            mockProcessor.Setup(x => x.getRegisterValue("R2")).Returns(2);

            ArithmeticLogicUnit alu = new ArithmeticLogicUnit(mockProcessor.Object);
            string[] args = new string[] { "R1", "R2" };

            alu.MUL(args);


            mockProcessor.Verify(x => x.StoreRegister("R1", short.MaxValue-1), Times.Once);
        }

        [TestMethod]
        public void MUL_MiddleRange_Success()
        {
            Mock<IProcessor> mockProcessor = new Mock<IProcessor>();
            mockProcessor.Setup(x => x.getRegisterValue("R1")).Returns(10);
            mockProcessor.Setup(x => x.getRegisterValue("R2")).Returns(50);

            ArithmeticLogicUnit alu = new ArithmeticLogicUnit(mockProcessor.Object);
            string[] args = new string[] { "R1", "R2" };

            alu.MUL(args);

            mockProcessor.Verify(x => x.StoreRegister("R1", 500), Times.Once);
        }
        [TestMethod]
        [ExpectedException(typeof(Exception), "ERROR AT LINE")]
        public void MUL_InvalidArguments_ExceptionThrown()
        {

            Mock<IProcessor> mockProcessor = new Mock<IProcessor>();
            ArithmeticLogicUnit alu = new ArithmeticLogicUnit(mockProcessor.Object);
            string[] args = new string[] { "R1" }; // fara R2


            alu.MUL(args);


            // "ERROR AT LINE"
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "ERROR AT LINE")]
        public void MUL_NullArguments_ExceptionThrown()
        {
            Mock<IProcessor> mockProcessor = new Mock<IProcessor>();
            ArithmeticLogicUnit alu = new ArithmeticLogicUnit(mockProcessor.Object);
            string[] args = null; // argumente null

            alu.MUL(args);
            // "ERROR AT LINE"
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "ERROR AT LINE")]
        public void MUL_ArgumentCountMismatch_ExceptionThrown()
        {
            Mock<IProcessor> mockProcessor = new Mock<IProcessor>();
            ArithmeticLogicUnit alu = new ArithmeticLogicUnit(mockProcessor.Object);
            string[] args = new string[] { "R1", "R2", "R3" }; //mai multe arg decat trb


            alu.MUL(args);

        }
    }

}
