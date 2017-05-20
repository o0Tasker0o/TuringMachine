using System;
using System.Collections.Generic;

namespace TuringMachine
{
	public enum MoveDirection
	{
		Left,
		Right,
		None
	}

	public class Instruction
	{
		public Instruction()
		{
			MoveDirections = new Dictionary<char, MoveDirection>();
			WriteSymbols = new Dictionary<char, char>();
			NextStates = new Dictionary<char, String>();
		}

		public String State
		{
			get;
			set;
		}

		public Dictionary<char, char> WriteSymbols
		{
			get;
			set;
		}

		public Dictionary<char, MoveDirection> MoveDirections
		{
			get;
			set;
		}

		public Dictionary<char, String> NextStates
		{
			get;
			set;
		}
	}
}
