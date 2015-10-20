using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TuringMachine;

namespace TuringMachineTests
{
    [TestClass]
    public class InstructionTests
    {
        [TestMethod]
        public void InstructionIsConstructedWithDefaultValues()
        {
            Instruction instruction = new Instruction();
            Assert.AreEqual(null, instruction.State);
            Assert.AreEqual(null, instruction.NextState);
            Assert.AreEqual(MoveDirection.None, instruction.MoveDirection);
            Assert.AreEqual(' ', instruction.ReadSymbol);
            Assert.AreEqual(' ', instruction.WriteSymbol);
        }
    }
}
