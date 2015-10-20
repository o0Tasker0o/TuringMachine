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
            Assert.AreEqual(0, instruction.NextStates.Keys.Count);
            Assert.AreEqual(0, instruction.MoveDirections.Keys.Count);
            Assert.AreEqual(0, instruction.WriteSymbols.Keys.Count);
        }
    }
}
