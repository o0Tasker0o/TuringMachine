﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TuringMachine;

namespace TuringMachineTests
{
	[TestClass]
	public class InstructionParserTests
	{
		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void InstructionParserThrowsExceptionOnNullString()
		{
			Assert.IsNull(InstructionParser.Parse(null));
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void InstructionParserThrowsExceptionOnEmptyString()
		{
			Assert.IsNull(InstructionParser.Parse(""));
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void InstructionParserThrowsExceptionOnTooFewArguments()
		{
			Assert.IsNull(InstructionParser.Parse("START,a,b,Right"));
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void InstructionParserThrowsExceptionOnTooManyArguments()
		{
			Assert.IsNull(InstructionParser.Parse("START,a,b,Right,HALT,Extra Info"));
		}

		[TestMethod]
		public void InstructionParserParsesValidInstruction()
		{
			var parsedInstruction = InstructionParser.Parse("START,a,b,Right,HALT");
			Assert.AreEqual("START", parsedInstruction.State);
			Assert.AreEqual('b', parsedInstruction.WriteSymbols['a']);
			Assert.AreEqual(MoveDirection.Right, parsedInstruction.MoveDirections['a']);
			Assert.AreEqual("HALT", parsedInstruction.NextStates['a']);
		}

		[TestMethod]
		public void InstructionParserTreatsBlankSymbolsAsNull()
		{
			var parsedInstruction = InstructionParser.Parse("START,,,Right,HALT");
			Assert.AreEqual(' ', parsedInstruction.WriteSymbols[' ']);
		}
	}
}
