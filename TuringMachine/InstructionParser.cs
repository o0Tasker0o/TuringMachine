using System;

namespace TuringMachine
{
	public class InstructionParser
	{
		public static Instruction Parse(string instruction)
		{
			if (string.IsNullOrEmpty(instruction))
			{
				throw new ArgumentNullException("Instruction string must not be null");
			}

			var elements = instruction.Split(',');

			if (elements.Length != 5)
			{
				throw new ArgumentException("Incorrect number of parameters specified for instruction");
			}

			var parsedInstruction = new Instruction {State = elements[0]};

			var readSymbol = ParseSymbol(elements[1]);

			parsedInstruction.WriteSymbols[readSymbol] = ParseSymbol(elements[2]);
			parsedInstruction.MoveDirections[readSymbol] = (MoveDirection)Enum.Parse(typeof(MoveDirection), elements[3]);
			parsedInstruction.NextStates[readSymbol] = elements[4];

			return parsedInstruction;
		}

		private static char ParseSymbol(string symbolString)
		{
			return string.IsNullOrEmpty(symbolString) ? ' ' : symbolString[0];
		}
	}
}
