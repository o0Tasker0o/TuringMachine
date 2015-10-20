using System;

namespace TuringMachine
{
    public class InstructionParser
    {
        public static Instruction Parse(String instruction)
        {
            if(string.IsNullOrEmpty(instruction))
            {
                throw new ArgumentNullException("Instruction string must not be null");
            }

            string [] elements = instruction.Split(',');

            if(elements.Length != 5)
            {
                throw new ArgumentException("Incorrect number of parameters specified for instruction");
            }

            Instruction parsedInstruction = new Instruction();
            parsedInstruction.State = elements[0];
            parsedInstruction.ReadSymbol = ParseSymbol(elements[1]);
            parsedInstruction.WriteSymbol = ParseSymbol(elements[2]);
            parsedInstruction.MoveDirection = (MoveDirection) Enum.Parse(typeof(MoveDirection), elements[3]);
            parsedInstruction.NextState = elements[4];

            return parsedInstruction;
        }

        private static char ParseSymbol(string symbolString)
        {
            if (string.IsNullOrEmpty(symbolString))
            {
                return ' ';
            }
            else
            {
                return symbolString[0];
            }
        }
    }
}
