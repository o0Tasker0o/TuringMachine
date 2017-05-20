using System;
using System.Collections.Generic;

namespace TuringMachine
{
	public class Processor
	{
		private readonly ITape _tape;
		private readonly IInstructionTable _table;

		public long Tick
		{
			get;
			private set;
		}

		public string NextState { get; private set; }

		public Processor(ITape tape, IInstructionTable table)
		{
			if (null == tape)
			{
				throw new ArgumentNullException("Tape must not be null");
			}

			if (null == table)
			{
				throw new ArgumentNullException("Instruction table must not be null");
			}

			_table = table;
			_tape = tape;
			NextState = "START";
			Tick = 0;
		}

		public bool Execute()
		{
			if (NextState == "HALT")
			{
				return false;
			}

			Instruction readInstruction;

			try
			{
				readInstruction = _table.GetInstruction(NextState);
			}
			catch (KeyNotFoundException)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine($"Unable to find next state: {NextState}");
				return false;
			}

			var readSymbol = _tape.Read();

			try
			{
				_tape.Write(readInstruction.WriteSymbols[readSymbol]);
			}
			catch (KeyNotFoundException)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine($"No action from state: {readInstruction.State} associated with read symbol: '{readSymbol}'");
				return false;
			}

			switch (readInstruction.MoveDirections[readSymbol])
			{
				case MoveDirection.Left:
					_tape.MoveLeft();
					break;
				case MoveDirection.Right:
					_tape.MoveRight();
					break;
				case MoveDirection.None:
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			NextState = readInstruction.NextStates[readSymbol];
			++Tick;

			return true;
		}
	}
}
