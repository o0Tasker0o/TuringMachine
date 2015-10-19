using System.Collections.Generic;

namespace TuringMachine
{
    public class Tape : ITape
    {
        private List<char?> mSymbols;
        private int mIndex;

        public Tape()
        {
            mSymbols = new List<char?>();
            mSymbols.Add(null);
            mIndex = 0;
        }

        public char? Read()
        {
            return mSymbols[mIndex];
        }

        public void Write(char? symbol)
        {
            mSymbols[mIndex] = symbol;
        }

        public void MoveLeft()
        {
            if(mIndex == 0)
            {
                mSymbols.Insert(0, null);
            }
            else
            {
                --mIndex;
            }
        }

        public void MoveRight()
        {
            mSymbols.Add(null);
            ++mIndex;
        }
    }
}
