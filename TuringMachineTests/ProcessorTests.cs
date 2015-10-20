using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using TuringMachine;

namespace TuringMachineTests
{
    [TestClass]
    public class ProcessorTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ProcessorThrowsExceptionOnNullTape()
        {
            IInstructionTable mockTable = Substitute.For<IInstructionTable>();
            Processor processor = new Processor(null, mockTable);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ProcessorThrowsExceptionOnNullInstructionTable()
        {
            ITape mockTape = Substitute.For<ITape>();
            Processor processor = new Processor(mockTape, null);
        }

        [TestMethod]
        public void ProcessorExecutesStartStateFollowedByNextState()
        {
            ITape mockTape = Substitute.For<ITape>();
            mockTape.Read().Returns(' ');
            IInstructionTable mockTable = Substitute.For<IInstructionTable>();
            Processor processor = new Processor(mockTape, mockTable);

            Instruction testInstruction = new Instruction();
            testInstruction.State = "START";
            testInstruction.NextStates[' '] = "A";
            testInstruction.WriteSymbols[' '] = ' ';
            testInstruction.MoveDirections[' '] = MoveDirection.None;

            mockTable.GetInstruction(Arg.Any<String>()).Returns(testInstruction);

            processor.Execute();

            mockTable.Received(1).GetInstruction("START");
            mockTape.Received(1).Read();
            mockTape.Received(1).Write(' ');
            mockTape.Received(0).MoveRight();
            mockTape.Received(0).MoveLeft();

            processor.Execute();

            mockTable.Received(1).GetInstruction(testInstruction.NextStates[' ']);
        }

        [TestMethod]
        public void ProcessorMovesTapeLeftOnLeftInstruction()
        {
            ITape mockTape = Substitute.For<ITape>();
            IInstructionTable mockTable = Substitute.For<IInstructionTable>();
            Processor processor = new Processor(mockTape, mockTable);

            char symbol = 'a';

            Instruction moveLeftInstruction = new Instruction();
            moveLeftInstruction.State = "START";
            moveLeftInstruction.WriteSymbols[symbol] = symbol;
            moveLeftInstruction.MoveDirections[symbol] = MoveDirection.Left;
            moveLeftInstruction.NextStates[symbol] = "HALT";

            mockTape.Read().Returns(symbol);
            mockTable.GetInstruction(Arg.Any<String>()).Returns(moveLeftInstruction);

            processor.Execute();

            mockTable.Received(1).GetInstruction("START");
            mockTape.Received(1).Read();
            mockTape.Received(1).Write(symbol);
            mockTape.Received(1).MoveLeft();
        }

        [TestMethod]
        public void ProcessorMovesTapeRightOnRightInstruction()
        {
            ITape mockTape = Substitute.For<ITape>();
            IInstructionTable mockTable = Substitute.For<IInstructionTable>();
            Processor processor = new Processor(mockTape, mockTable);

            char symbol = 'a';

            Instruction moveRightInstruction = new Instruction();
            moveRightInstruction.State = "START";
            moveRightInstruction.WriteSymbols[symbol] = symbol;
            moveRightInstruction.MoveDirections[symbol] = MoveDirection.Right;
            moveRightInstruction.NextStates[symbol] = "HALT";

            mockTape.Read().Returns(symbol);
            mockTable.GetInstruction(Arg.Any<String>()).Returns(moveRightInstruction);

            processor.Execute();

            mockTable.Received(1).GetInstruction("START");
            mockTape.Received(1).Read();
            mockTape.Received(1).Write(symbol);
            mockTape.Received(1).MoveRight();
        }

        [TestMethod]
        public void ProcessorDoesNotMoveTapeOnNoMoveInstruction()
        {
            ITape mockTape = Substitute.For<ITape>();
            IInstructionTable mockTable = Substitute.For<IInstructionTable>();
            Processor processor = new Processor(mockTape, mockTable);

            char symbol = 'a';

            Instruction noMoveInstruction = new Instruction();
            noMoveInstruction.State = "START";
            noMoveInstruction.WriteSymbols[symbol] = symbol;
            noMoveInstruction.MoveDirections[symbol] = MoveDirection.None;
            noMoveInstruction.NextStates[symbol] = "HALT";

            mockTape.Read().Returns(symbol);
            mockTable.GetInstruction(Arg.Any<String>()).Returns(noMoveInstruction);

            processor.Execute();

            mockTable.Received(1).GetInstruction("START");
            mockTape.Received(1).Read();
            mockTape.Received(1).Write(symbol);
            mockTape.Received(0).MoveRight();
            mockTape.Received(0).MoveLeft();
        }

        [TestMethod]
        public void ProcessorExecutePerformsNoOpOnHaltState()
        {
            ITape mockTape = Substitute.For<ITape>();
            mockTape.Read().Returns(' ');
            IInstructionTable mockTable = Substitute.For<IInstructionTable>();
            Processor processor = new Processor(mockTape, mockTable);

            Instruction testInstruction = new Instruction();
            testInstruction.State = "START";
            testInstruction.NextStates[' '] = "HALT";
            testInstruction.WriteSymbols[' '] = ' ';
            testInstruction.MoveDirections[' '] = MoveDirection.None;

            mockTable.GetInstruction(Arg.Any<String>()).Returns(testInstruction);

            processor.Execute();

            mockTape.ClearReceivedCalls();
            mockTable.ClearReceivedCalls();

            processor.Execute();

            mockTable.Received(0).GetInstruction(Arg.Any<String>());
            mockTape.Received(0).Read();
            mockTape.Received(0).Write(Arg.Any<char>());
            mockTape.Received(0).MoveLeft();
            mockTape.Received(0).MoveRight();
        }
    }
}
