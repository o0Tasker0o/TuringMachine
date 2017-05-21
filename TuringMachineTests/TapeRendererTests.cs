using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TuringMachine;

namespace TuringMachineTests
{
	[TestClass]
	public class TapeRendererTests
	{
		[TestMethod]
		public void RenderTapeRendersEmptyTape()
		{
			var tape = new Tape();

			var tapeRenderer = new TapeRenderer(tape, null);

			var actual = $"+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-{Environment.NewLine}" +
			             $"| | | | | | | | | | | | | | | | | | | | | | | | | | | | | | | | | | | | | | {Environment.NewLine}" +
			             $"+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-{Environment.NewLine}";

			var expected = tapeRenderer.RenderTape();

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void RenderTapeRendersPopulatedTape()
		{
			var tape = new Tape("a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z");

			var tapeRenderer = new TapeRenderer(tape, null);

			var actual = $"+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-{Environment.NewLine}" +
			             $"| | | | | | | | | | | | | | | | | | | |a|b|c|d|e|f|g|h|i|j|k|l|m|n|o|p|q|r|s{Environment.NewLine}" +
			             $"+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-{Environment.NewLine}";

			var expected = tapeRenderer.RenderTape();

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void RenderTapeRendersShiftedTape()
		{
			var tape = new Tape("a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z");
			tape.MoveRight();
			tape.MoveRight();
			tape.MoveRight();

			var tapeRenderer = new TapeRenderer(tape, null);

			var actual = $"+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-{Environment.NewLine}" +
			             $"| | | | | | | | | | | | | | | | |a|b|c|d|e|f|g|h|i|j|k|l|m|n|o|p|q|r|s|t|u|v{Environment.NewLine}" +
			             $"+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-{Environment.NewLine}";

			var expected = tapeRenderer.RenderTape();

			Assert.AreEqual(expected, actual);
		}
	}
}
