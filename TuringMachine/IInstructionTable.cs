using System;

namespace TuringMachine
{
	public interface IInstructionTable
	{
		void AddInstruction(Instruction instruction);
		Instruction GetInstruction(String instructionState);
	}
}
