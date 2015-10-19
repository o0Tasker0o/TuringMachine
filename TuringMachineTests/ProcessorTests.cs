﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            IInstructionTable mockTable = Substitute.For<IInstructionTable>();
            Processor processor = new Processor(mockTape, mockTable);

            Instruction testInstruction = new Instruction();
            testInstruction.State = "START";
            testInstruction.NextState = "A";

            mockTable.GetInstruction(Arg.Any<String>()).Returns(testInstruction);

            processor.Execute();

            mockTable.Received(1).GetInstruction("START");
            mockTape.Received(1).Read();
            mockTape.Received(1).Write(null);
            mockTape.Received(0).MoveRight();
            mockTape.Received(0).MoveLeft();

            processor.Execute();

            mockTable.Received(1).GetInstruction(testInstruction.NextState);
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
            moveLeftInstruction.ReadSymbol = symbol;
            moveLeftInstruction.WriteSymbol = symbol;
            moveLeftInstruction.MoveDirection = MoveDirection.Left;

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
            moveRightInstruction.ReadSymbol = symbol;
            moveRightInstruction.WriteSymbol = symbol;
            moveRightInstruction.MoveDirection = MoveDirection.Right;

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
            noMoveInstruction.ReadSymbol = symbol;
            noMoveInstruction.WriteSymbol = symbol;
            noMoveInstruction.MoveDirection = MoveDirection.None;

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
            IInstructionTable mockTable = Substitute.For<IInstructionTable>();
            Processor processor = new Processor(mockTape, mockTable);

            Instruction testInstruction = new Instruction();
            testInstruction.State = "START";
            testInstruction.NextState = "HALT";

            mockTable.GetInstruction(Arg.Any<String>()).Returns(testInstruction);

            processor.Execute();

            mockTape.ClearReceivedCalls();
            mockTable.ClearReceivedCalls();

            processor.Execute();

            mockTable.Received(0).GetInstruction(Arg.Any<String>());
            mockTape.Received(0).Read();
            mockTape.Received(0).Write(Arg.Any<char?>());
            mockTape.Received(0).MoveLeft();
            mockTape.Received(0).MoveRight();
        }
    }
}
