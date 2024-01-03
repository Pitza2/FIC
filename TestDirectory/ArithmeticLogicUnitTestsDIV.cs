using Moq;
using FIC.Interfaces;
using FIC;

namespace TestsDirectory
{
    [TestClass]
    public class ArithmeticLogicUnitTestsDIV
    {
        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException), "ERROR AT LINE")]
        public void DIV_DividedByZero()
        {

            Mock<IProcessor> mockProcessor = new Mock<IProcessor>();
            mockProcessor.Setup(x => x.getRegisterValue("R1")).Returns(6);
            mockProcessor.Setup(x => x.getRegisterValue("R2")).Returns(0);

            ArithmeticLogicUnit alu = new ArithmeticLogicUnit(mockProcessor.Object);
            string[] args = new string[] { "R1", "R2" };


            alu.DIV(args);

            mockProcessor.Verify(x => x.StoreRegister("R1", 0), Times.Once);
        }

        [TestMethod]
        public void DIV_DividedByOne()
        {

            Mock<IProcessor> mockProcessor = new Mock<IProcessor>();
            mockProcessor.Setup(x => x.getRegisterValue("R1")).Returns(6);
            mockProcessor.Setup(x => x.getRegisterValue("R2")).Returns(1);

            ArithmeticLogicUnit alu = new ArithmeticLogicUnit(mockProcessor.Object);
            string[] args = new string[] { "R1", "R2" };


            alu.DIV(args);

            mockProcessor.Verify(x => x.StoreRegister("R1", 6), Times.Once);
        }

        [TestMethod]
        public void DIV_DividedByNegative()
        {

            Mock<IProcessor> mockProcessor = new Mock<IProcessor>();
            mockProcessor.Setup(x => x.getRegisterValue("R1")).Returns(-10);
            mockProcessor.Setup(x => x.getRegisterValue("R2")).Returns(2);

            ArithmeticLogicUnit alu = new ArithmeticLogicUnit(mockProcessor.Object);
            string[] args = new string[] { "R1", "R2" };


            alu.DIV(args);

            mockProcessor.Verify(x => x.StoreRegister("R1", -5), Times.Once);
        }

        [TestMethod]
        public void DIV_ZeroDivided()
        {

            Mock<IProcessor> mockProcessor = new Mock<IProcessor>();
            mockProcessor.Setup(x => x.getRegisterValue("R1")).Returns(0);
            mockProcessor.Setup(x => x.getRegisterValue("R2")).Returns(2);

            ArithmeticLogicUnit alu = new ArithmeticLogicUnit(mockProcessor.Object);
            string[] args = new string[] { "R1", "R2" };


            alu.DIV(args);

            mockProcessor.Verify(x => x.StoreRegister("R1", 0), Times.Once);
        }

        

        [TestMethod]
        public void DIV_MiddleRange_Success()
        {
            Mock<IProcessor> mockProcessor = new Mock<IProcessor>();
            mockProcessor.Setup(x => x.getRegisterValue("R1")).Returns(60);
            mockProcessor.Setup(x => x.getRegisterValue("R2")).Returns(10);

            ArithmeticLogicUnit alu = new ArithmeticLogicUnit(mockProcessor.Object);
            string[] args = new string[] { "R1", "R2" };

            alu.DIV(args);

            mockProcessor.Verify(x => x.StoreRegister("R1", 6), Times.Once);
        }
        [TestMethod]
        [ExpectedException(typeof(Exception), "ERROR AT LINE")]
        public void DIV_InvalidArguments_ExceptionThrown()
        {

            Mock<IProcessor> mockProcessor = new Mock<IProcessor>();
            ArithmeticLogicUnit alu = new ArithmeticLogicUnit(mockProcessor.Object);
            string[] args = new string[] { "R1" }; // fara R2


            alu.DIV(args);


            // "ERROR AT LINE"
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "ERROR AT LINE")]
        public void DIV_NullArguments_ExceptionThrown()
        {
            Mock<IProcessor> mockProcessor = new Mock<IProcessor>();
            ArithmeticLogicUnit alu = new ArithmeticLogicUnit(mockProcessor.Object);
            string[] args = null; // argumente null

            alu.DIV(args);
            // "ERROR AT LINE"
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "ERROR AT LINE")]
        public void DIV_ArgumentCountMismatch_ExceptionThrown()
        {
            Mock<IProcessor> mockProcessor = new Mock<IProcessor>();
            ArithmeticLogicUnit alu = new ArithmeticLogicUnit(mockProcessor.Object);
            string[] args = new string[] { "R1", "R2", "R3" }; //mai multe arg decat trb


            alu.DIV(args);

        }
    }

}
