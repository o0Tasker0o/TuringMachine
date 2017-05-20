using System;
using System.Collections.Generic;

namespace TuringMachine
{
	public class Tape : ITape
	{
		private readonly IList<char> _symbols;
		private int _index;

		public int GetIndex()
		{
			return _index;
		}

		public char GetSymbol(int index)
		{
			if (index < 0 || index >= _symbols.Count)
			{
				return ' ';
			}

			return _symbols[index];
		}

		public Tape(string initialiser)
		{
			_symbols = new List<char>();
			var initialiserLines = initialiser.Split('\n');

			foreach (var character in initialiserLines[0].Split(','))
			{
				_symbols.Add(string.IsNullOrEmpty(character) ? ' ' : character[0]);
			}

			_index = 0;
			if (initialiserLines.Length <= 1)
			{
				return;
			}

			var headPosition = initialiser.Split('\n')[1];

			if (!string.IsNullOrEmpty(headPosition))
			{
				_index = int.Parse(headPosition);
			}
		}

		public Tape()
		{
			_symbols = new List<char> {' '};
			_index = 0;
		}

		public char Read()
		{
			FillToIndex();

			return _symbols[_index];
		}

		public void Write(char symbol)
		{
			FillToIndex();

			_symbols[_index] = symbol;
		}

		private void FillToIndex()
		{
			while (_index >= _symbols.Count)
			{
				_symbols.Add(' ');
			}

			while (_index < 0)
			{
				_index++;
				_symbols.Insert(0, ' ');
			}
		}

		public void MoveLeft()
		{
			if (_index == 0)
			{
				_symbols.Insert(0, ' ');
			}
			else
			{
				--_index;
			}
		}

		public void MoveRight()
		{
			_symbols.Add(' ');
			++_index;
		}
	}
}
