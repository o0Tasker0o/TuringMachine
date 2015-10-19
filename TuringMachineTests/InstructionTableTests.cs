using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TuringMachine;

namespace TuringMachineTests
{
    [TestClass]
    public class InstructionTableTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void InstructionTableThrowsExceptionOnNullInstructions()
        {
            InstructionTable table = new InstructionTable();
            table.AddInstruction(null);
        }

        [TestMethod]
        public void InstructionTableStoresAddedInstructions()
        {
            InstructionTable table = new InstructionTable();

            Instruction instructionA = new Instruction();
            instructionA.State = "A";

            table.AddInstruction(instructionA);

            Assert.AreEqual(instructionA, table.GetInstruction(instructionA.State));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InstructionTableThrowsExceptionIf2InstructionsHaveTheSameState()
        {
            InstructionTable table = new InstructionTable();

            Instruction instructionA = new Instruction();
            instructionA.State = "A";

            table.AddInstruction(instructionA);
            table.AddInstruction(instructionA);
        }
    }
}
