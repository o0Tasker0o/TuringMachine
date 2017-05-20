namespace TuringMachine
{
	public interface IInstructionTable
	{
		void AddInstruction(Instruction instruction);
		Instruction GetInstruction(string instructionState);
	}
}
