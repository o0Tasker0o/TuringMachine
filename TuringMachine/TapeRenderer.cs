using System.Text;

namespace TuringMachine
{
	public class TapeRenderer
	{
		private readonly ITape _tape;
		private readonly Processor _processor;

		public TapeRenderer(ITape tape, Processor processor)
		{
			_tape = tape;
			_processor = processor;
		}

		public string RenderHead()
		{
			return "                                      _^_";
		}

		public string RenderTape()
		{
			var tapeView = new StringBuilder();
			const int tapeViewLength = 19;

			for (var index = _tape.GetIndex() - tapeViewLength; index < _tape.GetIndex() + tapeViewLength; ++index)
			{
				tapeView.Append('+');
				tapeView.Append('-');
			}

			tapeView.AppendLine();

			for (var index = _tape.GetIndex() - tapeViewLength; index < _tape.GetIndex() + tapeViewLength; ++index)
			{
				tapeView.Append('|');
				tapeView.Append(_tape.GetSymbol(index));
			}

			tapeView.AppendLine();

			for (var index = _tape.GetIndex() - tapeViewLength; index < _tape.GetIndex() + tapeViewLength; ++index)
			{
				tapeView.Append('+');
				tapeView.Append('-');
			}

			tapeView.AppendLine();

			return tapeView.ToString();
		}

		public string RenderStateInfo()
		{
			var stateInfo = new StringBuilder();
			stateInfo.AppendLine();

			var stateLength = _processor.NextState.Length / 2;
			for (var index = 0; index < 39 - stateLength; index++)
			{
				stateInfo.Append(" ");
			}
			stateInfo.AppendLine($"{_processor.NextState}                                  ");

			return stateInfo.ToString();
		}
	}
}
