using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TuringMachine;

namespace TuringMachineTests
{
    [TestClass]
    public class TapeTests
    {
        [TestMethod]
        public void TapeCanBeInitialisedWithDifferentValues()
        {
            var tape = new Tape("a,,b,,c");

            Assert.AreEqual('a', tape.Read());
            tape.MoveRight();
            Assert.AreEqual(' ', tape.Read());
            tape.MoveRight();
            Assert.AreEqual('b', tape.Read());
            tape.MoveRight();
            Assert.AreEqual(' ', tape.Read());
            tape.MoveRight();
            Assert.AreEqual('c', tape.Read());
            tape.MoveRight();
        }

        [TestMethod]
        public void TapeCanBeInitialisedToDifferentStartPosition()
        {
            var tape = new Tape("a,,b,,c\n2");

            Assert.AreEqual('b', tape.Read());
            tape.MoveRight();
            Assert.AreEqual(' ', tape.Read());
            tape.MoveRight();
            Assert.AreEqual('c', tape.Read());
            tape.MoveRight();
        }

        [TestMethod]
        public void TapeCanBeInitialisedWithHeadAfterInitialisedCellsAndRead()
        {
            var tape = new Tape("a,b\n2");

            Assert.AreEqual(' ', tape.Read());
            tape.Write('c');
            Assert.AreEqual('c', tape.Read());
        }

        [TestMethod]
        public void TapeCanBeInitialisedWithHeadBeforeInitialisedCellsAndRead()
        {
            var tape = new Tape("a,b\n-1");

            Assert.AreEqual(' ', tape.Read());
            tape.Write('c');
            Assert.AreEqual('c', tape.Read());
        }

        [TestMethod]
        public void TapeCanBeInitialisedWithHeadAfterInitialisedCellsAndWrittenTo()
        {
            var tape = new Tape("a,b\n2");

            tape.Write('c');
            Assert.AreEqual('c', tape.Read());
        }

        [TestMethod]
        public void TapeCanBeInitialisedWithHeadBeforeInitialisedCellsAndWrittenTo()
        {
            var tape = new Tape("a,b\n-1");

            tape.Write('c');
            Assert.AreEqual('c', tape.Read());
        }

        [TestMethod]
        public void ReadingNewTapeReturnsNull()
        {
            var tape = new Tape();

            Assert.AreEqual(' ', tape.Read());
        }

        [TestMethod]
        public void ReadReturnsLastWrittenCharacter()
        {
            var tape = new Tape();

            const char symbol = 'a';
            tape.Write(symbol);

            Assert.AreEqual(symbol, tape.Read());
        }

        [TestMethod]
        public void MovingTapeLeftBringsBlankCellIntoView()
        {
            var tape = new Tape();

            tape.Write('a');
            tape.MoveLeft();

            Assert.AreEqual(' ', tape.Read());
        }

        [TestMethod]
        public void MovingTapeRightBringsBlankCellIntoView()
        {
            var tape = new Tape();

            tape.Write('a');
            tape.MoveRight();

            Assert.AreEqual(' ', tape.Read());
        }

        [TestMethod]
        public void MovingTapeLeftThenRightReturnsTapeToOriginalPosition()
        {
            var tape = new Tape();

            const char symbol = 'a';

            tape.Write(symbol);
            tape.MoveLeft();
            tape.MoveRight();

            Assert.AreEqual(symbol, tape.Read());
        }

        [TestMethod]
        public void MovingTapeRightThenLeftReturnsTapeToOriginalPosition()
        {
            var tape = new Tape();

            const char symbol = 'a';

            tape.Write(symbol);
            tape.MoveRight();
            tape.MoveLeft();

            Assert.AreEqual(symbol, tape.Read());
        }

        [TestMethod]
        public void TapeStoresAllWrittenSymbolsAsItMovesRight()
        {
            var tape = new Tape();

            var symbols = new List<char> { 'a', 'b', 'c', 'd' };

            foreach (var symbol in symbols)
            {
                tape.Write(symbol);
                tape.MoveRight();
            }

            for (var symbolIndex = symbols.Count - 1; symbolIndex >= 0; --symbolIndex)
            {
                tape.MoveLeft();
                Assert.AreEqual(symbols[symbolIndex], tape.Read());
            }
        }

        [TestMethod]
        public void TapeStoresAllWrittenSymbolsAsItMovesLeft()
        {
            var tape = new Tape();

            var symbols = new List<char> { 'a', 'b', 'c', 'd' };

            foreach (var symbol in symbols)
            {
	            tape.Write(symbol);
	            tape.MoveLeft();
            }

            for (var symbolIndex = symbols.Count - 1; symbolIndex >= 0; --symbolIndex)
            {
                tape.MoveRight();
                Assert.AreEqual(symbols[symbolIndex], tape.Read());
            }
        }

        [TestMethod]
        public void GetSymbolReturnsBlankCharForNullElements()
        {
            var tape = new Tape();

            Assert.AreEqual(' ', tape.GetSymbol(0));
        }

        [TestMethod]
        public void GetSymbolReturnsCharStoredInTapeElements()
        {
            var tape = new Tape();

            tape.Write('a');
            Assert.AreEqual('a', tape.GetSymbol(0));
        }

        [TestMethod]
        public void GetSymbolReturnsBlankCharForUninitialisedTapeEntries()
        {
            var tape = new Tape();

            Assert.AreEqual(' ', tape.GetSymbol(-1));
            Assert.AreEqual(' ', tape.GetSymbol(1));
        }

        [TestMethod]
        public void GetIndexReturnsCurrentTapeIndex()
        {
            var tape = new Tape();

            tape.MoveRight();
            Assert.AreEqual(1, tape.GetIndex());
            tape.MoveLeft();
            Assert.AreEqual(0, tape.GetIndex());
        }
    }
}
