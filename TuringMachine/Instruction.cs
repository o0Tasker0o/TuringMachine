using System;

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
            MoveDirection = MoveDirection.None;
        }

        public String State
        {
            get;
            set;
        }

        public char? ReadSymbol
        {
            get;
            set;
        }

        public char? WriteSymbol
        {
            get;
            set;
        }

        public MoveDirection MoveDirection
        {
            get;
            set;
        }

        public String NextState
        {
            get;
            set;
        }
    }
}
