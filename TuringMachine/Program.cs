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
				DrawTape(tape);
				DrawHead(processor);
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

		private static void DrawTape(ITape tape)
		{
			const int tapeViewLength = 19;

			for (var index = tape.GetIndex() - tapeViewLength; index < tape.GetIndex() + tapeViewLength; ++index)
			{
				Console.Write('+');
				Console.Write('-');
			}
			Console.WriteLine();

			for (var index = tape.GetIndex() - tapeViewLength; index < tape.GetIndex() + tapeViewLength; ++index)
			{
				Console.Write('|');
				Console.Write(tape.GetSymbol(index));
			}
			Console.WriteLine();

			for (var index = tape.GetIndex() - tapeViewLength; index < tape.GetIndex() + tapeViewLength; ++index)
			{
				Console.Write('+');
				Console.Write('-');
			}
			Console.WriteLine();
		}

		private static void DrawHead(Processor processor)
		{
			Console.WriteLine("                                      _^_");
			var stateLength = processor.NextState.Length / 2;
			for (var index = 0; index < 39 - stateLength; index++)
			{
				Console.Write(" ");
			}
			Console.WriteLine(processor.NextState + "                                  ");
		}
	}
}
