using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TuringMachine
{
    public class Tape : ITape
    {
        private IList<char> mSymbols;
        private int mIndex;

        public int GetIndex()
        {
            return mIndex;
        }

        public char GetSymbol(int index)
        {
            if(index < 0 || index >= mSymbols.Count)
            {
                return ' ';
            }

            return mSymbols[index];
        }

        public Tape(String initialiser)
        {
            mSymbols = new List<char>();
            String[] initialiserLines = initialiser.Split('\n');

            foreach (String character in initialiserLines[0].Split(','))
            {
                if(String.IsNullOrEmpty(character))
                {
                    mSymbols.Add(' ');
                }
                else
                {
                    mSymbols.Add(character[0]);
                }
            }

            mIndex = 0;
            if (initialiserLines.Length > 1)
            {
                string headPosition = initialiser.Split('\n')[1];

                if (!String.IsNullOrEmpty(headPosition))
                {
                    mIndex = Int32.Parse(headPosition);
                }
            }

        }

        public Tape()
        {
            mSymbols = new List<char>();
            mSymbols.Add(' ');
            mIndex = 0;
        }

        public char Read()
        {
            FillToIndex();

            return mSymbols[mIndex];
        }

        public void Write(char symbol)
        {
            FillToIndex();

            mSymbols[mIndex] = symbol;
        }

        private void FillToIndex()
        {
            while (mIndex >= mSymbols.Count)
            {
                mSymbols.Add(' ');
            }

            while (mIndex < 0)
            {
                mIndex++;
                mSymbols.Insert(0, ' ');
            }
        }

        public void MoveLeft()
        {
            if(mIndex == 0)
            {
                mSymbols.Insert(0, ' ');
            }
            else
            {
                --mIndex;
            }
        }

        public void MoveRight()
        {
            mSymbols.Add(' ');
            ++mIndex;
        }
    }
}
