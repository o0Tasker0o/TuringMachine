using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Collections.Generic;
using NSubstitute.ExceptionExtensions;
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
			var mockTable = Substitute.For<IInstructionTable>();
			var processor = new Processor(null, mockTable);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ProcessorThrowsExceptionOnNullInstructionTable()
		{
			var mockTape = Substitute.For<ITape>();
			var processor = new Processor(mockTape, null);
		}

		[TestMethod]
		public void ProcessorExecutesStartStateFollowedByNextState()
		{
			var mockTape = Substitute.For<ITape>();
			mockTape.Read().Returns(' ');
			var mockTable = Substitute.For<IInstructionTable>();
			var processor = new Processor(mockTape, mockTable);

			var testInstruction = new Instruction
			{
				State = "START",
				NextStates = { [' '] = "A" },
				WriteSymbols = { [' '] = ' ' },
				MoveDirections = { [' '] = MoveDirection.None }
			};

			mockTable.GetInstruction(Arg.Any<string>()).Returns(testInstruction);

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
			var mockTape = Substitute.For<ITape>();
			var mockTable = Substitute.For<IInstructionTable>();
			var processor = new Processor(mockTape, mockTable);

			const char symbol = 'a';

			var moveLeftInstruction = new Instruction
			{
				State = "START",
				WriteSymbols = {[symbol] = symbol},
				MoveDirections = {[symbol] = MoveDirection.Left},
				NextStates = {[symbol] = "HALT"}
			};

			mockTape.Read().Returns(symbol);
			mockTable.GetInstruction(Arg.Any<string>()).Returns(moveLeftInstruction);

			processor.Execute();

			mockTable.Received(1).GetInstruction("START");
			mockTape.Received(1).Read();
			mockTape.Received(1).Write(symbol);
			mockTape.Received(1).MoveLeft();
		}

		[TestMethod]
		public void ProcessorMovesTapeRightOnRightInstruction()
		{
			var mockTape = Substitute.For<ITape>();
			var mockTable = Substitute.For<IInstructionTable>();
			var processor = new Processor(mockTape, mockTable);

			const char symbol = 'a';

			var moveRightInstruction = new Instruction
			{
				State = "START",
				WriteSymbols = {[symbol] = symbol},
				MoveDirections = {[symbol] = MoveDirection.Right},
				NextStates = {[symbol] = "HALT"}
			};

			mockTape.Read().Returns(symbol);
			mockTable.GetInstruction(Arg.Any<string>()).Returns(moveRightInstruction);

			processor.Execute();

			mockTable.Received(1).GetInstruction("START");
			mockTape.Received(1).Read();
			mockTape.Received(1).Write(symbol);
			mockTape.Received(1).MoveRight();
		}

		[TestMethod]
		public void ProcessorDoesNotMoveTapeOnNoMoveInstruction()
		{
			var mockTape = Substitute.For<ITape>();
			var mockTable = Substitute.For<IInstructionTable>();
			var processor = new Processor(mockTape, mockTable);

			const char symbol = 'a';

			var noMoveInstruction = new Instruction
			{
				State = "START",
				WriteSymbols = {[symbol] = symbol},
				MoveDirections = {[symbol] = MoveDirection.None},
				NextStates = {[symbol] = "HALT"}
			};

			mockTape.Read().Returns(symbol);
			mockTable.GetInstruction(Arg.Any<string>()).Returns(noMoveInstruction);

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
			var mockTape = Substitute.For<ITape>();
			mockTape.Read().Returns(' ');
			var mockTable = Substitute.For<IInstructionTable>();
			var processor = new Processor(mockTape, mockTable);

			var testInstruction = new Instruction
			{
				State = "START",
				NextStates = {[' '] = "HALT"},
				WriteSymbols = {[' '] = ' '},
				MoveDirections = {[' '] = MoveDirection.None}
			};

			mockTable.GetInstruction(Arg.Any<string>()).Returns(testInstruction);

			processor.Execute();

			mockTape.ClearReceivedCalls();
			mockTable.ClearReceivedCalls();

			processor.Execute();

			mockTable.Received(0).GetInstruction(Arg.Any<string>());
			mockTape.Received(0).Read();
			mockTape.Received(0).Write(Arg.Any<char>());
			mockTape.Received(0).MoveLeft();
			mockTape.Received(0).MoveRight();
		}

		[TestMethod]
		public void ProcessorExecuteReturnsFalseOnHaltState()
		{
			var mockTape = Substitute.For<ITape>();
			mockTape.Read().Returns(' ');
			var mockTable = Substitute.For<IInstructionTable>();
			var processor = new Processor(mockTape, mockTable);

			var testInstruction = new Instruction
			{
				State = "START",
				NextStates = {[' '] = "HALT"},
				WriteSymbols = {[' '] = ' '},
				MoveDirections = {[' '] = MoveDirection.None}
			};

			mockTable.GetInstruction(Arg.Any<string>()).Returns(testInstruction);

			Assert.IsTrue(processor.Execute());
			Assert.IsFalse(processor.Execute());
		}

		[TestMethod]
		public void ProcessorExecuteReturnsFalseWhenNextStateNotFound()
		{
			var mockTape = Substitute.For<ITape>();
			mockTape.Read().Returns(' ');
			var mockTable = Substitute.For<IInstructionTable>();
			var processor = new Processor(mockTape, mockTable);

			mockTable.GetInstruction(Arg.Any<string>()).Throws(new KeyNotFoundException());

			Assert.IsFalse(processor.Execute());
		}
	}
}
