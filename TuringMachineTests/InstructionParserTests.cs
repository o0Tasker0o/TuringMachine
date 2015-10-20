using System;
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
            Instruction parsedInstruction = InstructionParser.Parse("START,a,b,Right,HALT");
            Assert.AreEqual("START", parsedInstruction.State);
            Assert.AreEqual('a', parsedInstruction.ReadSymbol);
            Assert.AreEqual('b', parsedInstruction.WriteSymbol);
            Assert.AreEqual(MoveDirection.Right, parsedInstruction.MoveDirection);
            Assert.AreEqual("HALT", parsedInstruction.NextState);
        }

        [TestMethod]
        public void InstructionParserTreatsBlankSymbolsAsNull()
        {
            Instruction parsedInstruction = InstructionParser.Parse("START,,,Right,HALT");
            Assert.AreEqual(' ', parsedInstruction.ReadSymbol);
            Assert.AreEqual(' ', parsedInstruction.WriteSymbol);
        }
    }
}
