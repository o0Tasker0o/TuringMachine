using System;
using System.IO;
using System.Threading;

namespace TuringMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            ITape tape = new Tape();
            IInstructionTable instructionTable = ParseInstructions(args);

            Processor processor = new Processor(tape, instructionTable);

            Console.CursorVisible = false;

            while(true)
            {
                Console.SetCursorPosition(0, 0);
                DrawTape(tape);
                DrawHead(processor);
                Console.WriteLine();
                Console.WriteLine("Tick: " + processor.Tick);

                if(!processor.Execute())
                {
                    break;
                }

                Thread.Sleep(100);
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
            for (int index = tape.GetIndex() - 15; index < tape.GetIndex() + 15; ++index)
            {
                char symbol = tape.GetSymbol(index);
                Console.Write("|" + tape.GetSymbol(index));
            }
            Console.WriteLine();
        }

        private static void DrawHead(Processor processor)
        {
            Console.WriteLine("                              _^_");
            int stateLength = processor.NextState.Length / 2;
            for (int index = 0; index < 31 - stateLength; index++)
            {
                Console.Write(" ");
            }
            Console.WriteLine(processor.NextState + "                                  ");
        }
    }
}
