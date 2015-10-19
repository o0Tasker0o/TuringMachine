using System;
using System.Collections.Generic;

namespace TuringMachine
{
    public class InstructionTable
    {
        private Dictionary<String, Instruction> mInstruction;

        public InstructionTable()
        {
            mInstruction = new Dictionary<String, Instruction>();
        }

        public void AddInstruction(Instruction instruction)
        {
            if(null == instruction)
            {
                throw new ArgumentNullException("Instruction must not be null");
            }

            if(mInstruction.ContainsKey(instruction.State))
            {
                throw new ArgumentException("Instruction table already contains an instruction with this state name");
            }
            else
            {
                mInstruction.Add(instruction.State, instruction);
            }
        }

        public Instruction GetInstruction(String instructionState)
        {
            return mInstruction[instructionState];
        }
    }
}
