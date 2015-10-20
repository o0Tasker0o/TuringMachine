﻿using System;

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
            if(null == tape)
            {
                throw new ArgumentNullException("Tape must not be null");
            }

            if(null == table)
            {
                throw new ArgumentNullException("Instruction table must not be null");
            }

            mTable = table;
            mTape = tape;
            mNextState = "START";
            Tick = 0;
        }

        public void Execute()
        {
            if(mNextState == "HALT")
            {
                return;
            }

            Instruction readInstruction = mTable.GetInstruction(mNextState);
            char readSymbol = mTape.Read();

            mTape.Write(readInstruction.WriteSymbols[readSymbol]);

            switch(readInstruction.MoveDirections[readSymbol])
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
        }
    }
}
