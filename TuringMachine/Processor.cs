using System;
using System.Collections.Generic;

namespace TuringMachine
{
	public class Processor
	{
		private ITape mTape;
		private IInstructionTable mTable;
		private String mNextState;

		public long Tick
		{
			get;
			private set;
		}

		public String NextState
		{
			get
			{
				return mNextState;
			}
		}

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

			mTable = table;
			mTape = tape;
			mNextState = "START";
			Tick = 0;
		}

		public bool Execute()
		{
			if (mNextState == "HALT")
			{
				return false;
			}

			Instruction readInstruction = null;

			try
			{
				readInstruction = mTable.GetInstruction(mNextState);
			}
			catch (KeyNotFoundException)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("Unable to find next state: " + mNextState);
				return false;
			}

			char readSymbol = mTape.Read();

			try
			{
				mTape.Write(readInstruction.WriteSymbols[readSymbol]);
			}
			catch (KeyNotFoundException)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("No action from state: " + readInstruction.State + " associated with read symbol: '" + readSymbol + "'");
				return false;
			}

			switch (readInstruction.MoveDirections[readSymbol])
			{
				case MoveDirection.Left:
					mTape.MoveLeft();
					break;
				case MoveDirection.Right:
					mTape.MoveRight();
					break;
			}

			mNextState = readInstruction.NextStates[readSymbol];
			++Tick;

			return true;
		}
	}
}
