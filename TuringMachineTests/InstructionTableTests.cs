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
            var table = new InstructionTable();
            table.AddInstruction(null);
        }

        [TestMethod]
        public void InstructionTableStoresAddedInstructions()
        {
            var table = new InstructionTable();

            var instructionA = new Instruction();
            instructionA.State = "A";

            table.AddInstruction(instructionA);

            Assert.AreEqual(instructionA, table.GetInstruction(instructionA.State));
        }

        [TestMethod]
        public void InstructionTableAddsToWriteSymbolsIf2InstructionsHaveTheSameState()
        {
            var table = new InstructionTable();

            var instructionA1 = new Instruction();
            instructionA1.State = "A";
            instructionA1.WriteSymbols['0'] = '1';

            var instructionA2 = new Instruction();
            instructionA2.State = "A";
            instructionA2.WriteSymbols['1'] = '0';

            table.AddInstruction(instructionA1);
            table.AddInstruction(instructionA2);

            var storedInstruction = table.GetInstruction("A");
            Assert.AreEqual(2, storedInstruction.WriteSymbols.Keys.Count);
            Assert.AreEqual('1', storedInstruction.WriteSymbols['0']);
            Assert.AreEqual('0', storedInstruction.WriteSymbols['1']);
        }

        [TestMethod]
        public void InstructionTableAddsToMoveDirectionsIf2InstructionsHaveTheSameState()
        {
            var table = new InstructionTable();

            var instructionA1 = new Instruction();
            instructionA1.State = "A";
            instructionA1.MoveDirections['0'] = MoveDirection.Left;

            var instructionA2 = new Instruction();
            instructionA2.State = "A";
            instructionA2.MoveDirections['1'] = MoveDirection.Right;

            table.AddInstruction(instructionA1);
            table.AddInstruction(instructionA2);

            var storedInstruction = table.GetInstruction("A");
            Assert.AreEqual(2, storedInstruction.MoveDirections.Keys.Count);
            Assert.AreEqual(MoveDirection.Left, storedInstruction.MoveDirections['0']);
            Assert.AreEqual(MoveDirection.Right, storedInstruction.MoveDirections['1']);
        }

        [TestMethod]
        public void InstructionTableAddsToNextStatesIf2InstructionsHaveTheSameState()
        {
            var table = new InstructionTable();

            var instructionA1 = new Instruction();
            instructionA1.State = "A";
            instructionA1.NextStates['0'] = "B";

            var instructionA2 = new Instruction();
            instructionA2.State = "A";
            instructionA2.NextStates['1'] = "C";

            table.AddInstruction(instructionA1);
            table.AddInstruction(instructionA2);

            var storedInstruction = table.GetInstruction("A");
            Assert.AreEqual(2, storedInstruction.NextStates.Keys.Count);
            Assert.AreEqual("B", storedInstruction.NextStates['0']);
            Assert.AreEqual("C", storedInstruction.NextStates['1']);
        }
    }
}
