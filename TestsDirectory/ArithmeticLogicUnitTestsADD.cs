using Moq;
using FIC.Interfaces;
using FIC;

namespace TestsDirectory
{
    [TestClass]
    public class ArithmeticLogicUnitTestsADD
    {
        [TestMethod]
        public void ADD_LowerRange_Success()
        {

            Mock<IProcessor> mockProcessor = new Mock<IProcessor>();
            mockProcessor.Setup(x => x.getRegisterValue("R1")).Returns(short.MinValue+1);
            mockProcessor.Setup(x => x.getRegisterValue("R2")).Returns(-1);

            ArithmeticLogicUnit alu = new ArithmeticLogicUnit(mockProcessor.Object);
            string[] args = new string[] { "R1", "R2" };


            alu.ADD(args);

            mockProcessor.Verify(x => x.StoreRegister("R1", short.MinValue), Times.Once);
        }

        [TestMethod]
        public void ADD_UpperRange_Success()
        {

            Mock<IProcessor> mockProcessor = new Mock<IProcessor>();
            mockProcessor.Setup(x => x.getRegisterValue("R1")).Returns(short.MaxValue - 1);
            mockProcessor.Setup(x => x.getRegisterValue("R2")).Returns(1);

            ArithmeticLogicUnit alu = new ArithmeticLogicUnit(mockProcessor.Object);
            string[] args = new string[] { "R1", "R2" };

            alu.ADD(args);


            mockProcessor.Verify(x => x.StoreRegister("R1", short.MaxValue), Times.Once);
        }

        [TestMethod]
        public void ADD_MiddleRange_Success()
        {
            Mock<IProcessor> mockProcessor = new Mock<IProcessor>();
            mockProcessor.Setup(x => x.getRegisterValue("R1")).Returns(100);
            mockProcessor.Setup(x => x.getRegisterValue("R2")).Returns(50);

            ArithmeticLogicUnit alu = new ArithmeticLogicUnit(mockProcessor.Object);
            string[] args = new string[] { "R1", "R2" };

            alu.ADD(args);

            mockProcessor.Verify(x => x.StoreRegister("R1", 150), Times.Once);
        }
        [TestMethod]
        [ExpectedException(typeof(Exception), "ERROR AT LINE")]
        public void ADD_InvalidArguments_ExceptionThrown()
        {
           
            Mock<IProcessor> mockProcessor = new Mock<IProcessor>();
            ArithmeticLogicUnit alu = new ArithmeticLogicUnit(mockProcessor.Object);
            string[] args = new string[] { "R1" }; // fara R2

          
            alu.ADD(args);


            // "ERROR AT LINE"
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "ERROR AT LINE")]
        public void ADD_NullArguments_ExceptionThrown()
        {
            Mock<IProcessor> mockProcessor = new Mock<IProcessor>();
            ArithmeticLogicUnit alu = new ArithmeticLogicUnit(mockProcessor.Object);
            string[] args = null; // argumente null

            alu.ADD(args);
            // "ERROR AT LINE"
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "ERROR AT LINE")]
        public void ADD_ArgumentCountMismatch_ExceptionThrown()
        {
            Mock<IProcessor> mockProcessor = new Mock<IProcessor>();
            ArithmeticLogicUnit alu = new ArithmeticLogicUnit(mockProcessor.Object);
            string[] args = new string[] { "R1", "R2", "R3" }; //mai multe arg decat trb

           
            alu.ADD(args);

        }
    }

}
