using System;
using System.Configuration;
using System.IO;
using System.Text;
using System.Threading;

namespace TuringMachine
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.OutputEncoding = Encoding.Default;

			ITape tape = null;

			try
			{
				tape = new Tape(File.ReadAllText("./init.tap"));
			}
			catch (Exception)
			{
				tape = new Tape();
			}

			IInstructionTable instructionTable = null;

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

			Processor processor = new Processor(tape, instructionTable);

			Console.CursorVisible = false;

			int clockDelay = 500;

			try
			{
				clockDelay = Int32.Parse(ConfigurationManager.AppSettings.Get("ClockDelay"));
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

		private static IInstructionTable ParseInstructions(string[] args)
		{
			String filename = "./instructions.tbl";

			if (args.Length >= 1)
			{
				filename = args[0];
			}

			Console.Title = "Turing Machine - " + Path.GetFileNameWithoutExtension(filename);

			String[] instructionLines = File.ReadAllLines(filename);
			IInstructionTable table = new InstructionTable();

			foreach (String instructionLine in instructionLines)
			{
				table.AddInstruction(InstructionParser.Parse(instructionLine));
			}

			return table;
		}

		private static void DrawTape(ITape tape)
		{
			int tapeViewLength = 19;

			for (int index = tape.GetIndex() - tapeViewLength; index < tape.GetIndex() + tapeViewLength; ++index)
			{
				Console.Write((char)194);
				Console.Write((char)196);
			}
			Console.WriteLine();

			for (int index = tape.GetIndex() - tapeViewLength; index < tape.GetIndex() + tapeViewLength; ++index)
			{
				char symbol = tape.GetSymbol(index);
				Console.Write((char)179);
				Console.Write(tape.GetSymbol(index));
			}
			Console.WriteLine();

			for (int index = tape.GetIndex() - tapeViewLength; index < tape.GetIndex() + tapeViewLength; ++index)
			{
				Console.Write((char)193);
				Console.Write((char)196);
			}
			Console.WriteLine();
		}

		private static void DrawHead(Processor processor)
		{
			Console.WriteLine("                                      _^_");
			int stateLength = processor.NextState.Length / 2;
			for (int index = 0; index < 39 - stateLength; index++)
			{
				Console.Write(" ");
			}
			Console.WriteLine(processor.NextState + "                                  ");
		}
	}
}
