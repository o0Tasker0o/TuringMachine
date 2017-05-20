using System;
using System.Collections.Generic;

namespace TuringMachine
{
	public class InstructionTable : IInstructionTable
	{
		private readonly Dictionary<string, Instruction> _instruction;

		public InstructionTable()
		{
			_instruction = new Dictionary<string, Instruction>();
		}

		public void AddInstruction(Instruction instruction)
		{
			if (null == instruction)
			{
				throw new ArgumentNullException("Instruction must not be null");
			}

			if (_instruction.ContainsKey(instruction.State))
			{
				foreach (var readSymbol in instruction.WriteSymbols.Keys)
				{
					_instruction[instruction.State].WriteSymbols[readSymbol] = instruction.WriteSymbols[readSymbol];
				}

				foreach (var readSymbol in instruction.MoveDirections.Keys)
				{
					_instruction[instruction.State].MoveDirections[readSymbol] = instruction.MoveDirections[readSymbol];
				}

				foreach (var readSymbol in instruction.NextStates.Keys)
				{
					_instruction[instruction.State].NextStates[readSymbol] = instruction.NextStates[readSymbol];
				}
			}
			else
			{
				_instruction.Add(instruction.State, instruction);
			}
		}

		public Instruction GetInstruction(string instructionState)
		{
			return _instruction[instructionState];
		}
	}
}
