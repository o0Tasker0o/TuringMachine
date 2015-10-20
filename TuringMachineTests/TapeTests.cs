using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TuringMachine;

namespace TuringMachineTests
{
    [TestClass]
    public class TapeTests
    {
        [TestMethod]
        public void ReadingNewTapeReturnsNull()
        {
            Tape tape = new Tape();

            Assert.AreEqual(' ', tape.Read());
        }

        [TestMethod]
        public void ReadReturnsLastWrittenCharacter()
        {
            Tape tape = new Tape();

            char symbol = 'a';
            tape.Write(symbol);

            Assert.AreEqual(symbol, tape.Read());
        }

        [TestMethod]
        public void MovingTapeLeftBringsBlankCellIntoView()
        {
            Tape tape = new Tape();

            tape.Write('a');
            tape.MoveLeft();

            Assert.AreEqual(' ', tape.Read());
        }

        [TestMethod]
        public void MovingTapeRightBringsBlankCellIntoView()
        {
            Tape tape = new Tape();

            tape.Write('a');
            tape.MoveRight();

            Assert.AreEqual(' ', tape.Read());
        }

        [TestMethod]
        public void MovingTapeLeftThenRightReturnsTapeToOriginalPosition()
        {
            Tape tape = new Tape();

            char symbol = 'a';

            tape.Write(symbol);
            tape.MoveLeft();
            tape.MoveRight();

            Assert.AreEqual(symbol, tape.Read());
        }

        [TestMethod]
        public void MovingTapeRightThenLeftReturnsTapeToOriginalPosition()
        {
            Tape tape = new Tape();

            char symbol = 'a';

            tape.Write(symbol);
            tape.MoveRight();
            tape.MoveLeft();

            Assert.AreEqual(symbol, tape.Read());
        }

        [TestMethod]
        public void TapeStoresAllWrittenSymbolsAsItMovesRight()
        {
            Tape tape = new Tape();

            List<char> symbols = new List<char>() { 'a', 'b', 'c', 'd' };

            for(int symbolIndex = 0; symbolIndex < symbols.Count; ++symbolIndex)
            {
                tape.Write(symbols[symbolIndex]);
                tape.MoveRight();
            }

            for (int symbolIndex = symbols.Count - 1; symbolIndex >= 0; --symbolIndex)
            {
                tape.MoveLeft();
                Assert.AreEqual(symbols[symbolIndex], tape.Read());
            }
        }

        [TestMethod]
        public void TapeStoresAllWrittenSymbolsAsItMovesLeft()
        {
            Tape tape = new Tape();

            List<char> symbols = new List<char>() { 'a', 'b', 'c', 'd' };

            for (int symbolIndex = 0; symbolIndex < symbols.Count; ++symbolIndex)
            {
                tape.Write(symbols[symbolIndex]);
                tape.MoveLeft();
            }

            for (int symbolIndex = symbols.Count - 1; symbolIndex >= 0; --symbolIndex)
            {
                tape.MoveRight();
                Assert.AreEqual(symbols[symbolIndex], tape.Read());
            }
        }
    }
}
