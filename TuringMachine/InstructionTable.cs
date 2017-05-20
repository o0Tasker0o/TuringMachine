using System;
using System.Collections.Generic;

namespace TuringMachine
{
	public class InstructionTable : IInstructionTable
	{
		private Dictionary<String, Instruction> mInstruction;

		public InstructionTable()
		{
			mInstruction = new Dictionary<String, Instruction>();
		}

		public void AddInstruction(Instruction instruction)
		{
			if (null == instruction)
			{
				throw new ArgumentNullException("Instruction must not be null");
			}

			if (mInstruction.ContainsKey(instruction.State))
			{
				foreach (char readSymbol in instruction.WriteSymbols.Keys)
				{
					mInstruction[instruction.State].WriteSymbols[readSymbol] = instruction.WriteSymbols[readSymbol];
				}

				foreach (char readSymbol in instruction.MoveDirections.Keys)
				{
					mInstruction[instruction.State].MoveDirections[readSymbol] = instruction.MoveDirections[readSymbol];
				}

				foreach (char readSymbol in instruction.NextStates.Keys)
				{
					mInstruction[instruction.State].NextStates[readSymbol] = instruction.NextStates[readSymbol];
				}
			}
			else
			{
				mInstruction.Add(instruction.State, instruction);
			}
		}

		public Instruction GetInstruction(String instructionState)
		{
			return mInstruction[instructionState];
		}
	}
}
