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
                processor.Execute();
                Thread.Sleep(200);
            }
        }
    }
}
