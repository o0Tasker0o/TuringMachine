using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using System.Threading;

namespace TuringMachine
{
	internal class Program
	{
		private static TapeRenderer _tapeRenderer;

		private static void Main(string[] args)
		{
			Console.OutputEncoding = Encoding.Default;

			ITape tape;

			try
			{
				tape = new Tape(File.ReadAllText("./init.tap"));
			}
			catch (Exception)
			{
				tape = new Tape();
			}

			IInstructionTable instructionTable;

			try
			{
				instructionTable = ParseInstructions(args);
			}
			catch (Exception ex)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("Failed to load instruction table. " + ex.Message);
				Console.ReadLine();
				return;
			}

			var processor = new Processor(tape, instructionTable);
			_tapeRenderer = new TapeRenderer(tape, processor);

			Console.CursorVisible = false;

			var clockDelay = 500;

			try
			{
				clockDelay = int.Parse(ConfigurationManager.AppSettings.Get("ClockDelay"));
			}
			catch (Exception)
			{

			}

			while (true)
			{
				Console.SetCursorPosition(0, 0);

				Console.WriteLine(_tapeRenderer.RenderTape());
				Console.WriteLine(_tapeRenderer.RenderHead());
				Console.WriteLine(_tapeRenderer.RenderStateInfo());

				Console.WriteLine();
				Console.WriteLine("Tick: " + processor.Tick);

				if (!processor.Execute())
				{
					break;
				}
				Thread.Sleep(clockDelay);
			}

			Console.WriteLine("Execution completed!");
			Console.ReadLine();
		}

		private static IInstructionTable ParseInstructions(IReadOnlyList<string> args)
		{
			var filename = "./instructions.tbl";

			if (args.Count >= 1)
			{
				filename = args[0];
			}

			Console.Title = "Turing Machine - " + Path.GetFileNameWithoutExtension(filename);

			var instructionLines = File.ReadAllLines(filename);
			IInstructionTable table = new InstructionTable();

			foreach (var instructionLine in instructionLines)
			{
				table.AddInstruction(InstructionParser.Parse(instructionLine));
			}

			return table;
		}
	}
}
