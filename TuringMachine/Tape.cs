﻿using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TuringMachine
{
    public class Tape : ITape
    {
        private IList<char?> mSymbols;
        private int mIndex;

        public int GetIndex()
        {
            return mIndex;
        }

        public char? GetSymbol(int index)
        {
            if(index < 0 || index >= mSymbols.Count)
            {
                return ' ';
            }

            if(null == mSymbols[index])
            {
                return ' ';
            }

            return mSymbols[index];
        }

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
