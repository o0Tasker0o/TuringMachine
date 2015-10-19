using System;

namespace TuringMachine
{
    public class Processor
    {
        private ITape mTape;
        private IInstructionTable mTable;
        private String mNextState;

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
        }

        public void Execute()
        {
            if(mNextState == "HALT")
            {
                return;
            }

            Instruction readInstruction = mTable.GetInstruction(mNextState);
            char? readSymbol = mTape.Read();

            if(readInstruction.ReadSymbol == readSymbol)
            {
                mTape.Write(readInstruction.WriteSymbol);

                switch(readInstruction.MoveDirection)
                {
                    case MoveDirection.Left:
                        mTape.MoveLeft();
                        break;
                    case MoveDirection.Right:
                        mTape.MoveRight();
                        break;
                }
            }

            mNextState = readInstruction.NextState;
        }
    }
}
