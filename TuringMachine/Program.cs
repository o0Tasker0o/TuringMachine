using System;
using System.IO;
using System.Threading;

namespace TuringMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            String[] instructionLines = File.ReadAllLines("./instructions.tbl");
            IInstructionTable instructionTable = new InstructionTable();

            foreach(String instructionLine in instructionLines)
            {
                instructionTable.AddInstruction(InstructionParser.Parse(instructionLine));
            }

            ITape tape = new Tape();
            Processor processor = new Processor(tape, instructionTable);

            while(true)
            {
                DrawTape(tape);
                DrawHead(processor);
                Console.WriteLine();
                Console.WriteLine("Tick: " + processor.Tick);

                Console.SetCursorPosition(0, 0);

                processor.Execute();
                Thread.Sleep(100);
            }
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
