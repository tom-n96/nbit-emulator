using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator
{
    class LogicUnits
    {
		public static bool multiplexer4x1(bool s1, bool s0, bool d3, bool d2, bool d1, bool d0)
		{
			return
			(
				Logic.or4(
						Logic.and3(d0, !s1, !s0),
						Logic.and3(d1, !s1, s0),
						Logic.and3(d2, s1, !s0),
						Logic.and3(d3, s1, s0)
						)
			);
		}
		public static bool[] logicUnit(bool a, bool b, bool gs3, bool gs2, bool gs1, bool gs0, bool ps3, bool ps2, bool ps1, bool ps0)
		{
			//output is [g,p]
			bool[] output = { !multiplexer4x1(a, b, gs3, gs2, gs1, gs0), !multiplexer4x1(a, b, ps3, ps2, ps1, ps0) };
			return output;
		}
		public static bool[] commandMapping(bool s2, bool s1, bool s0)
		{
			//output is [gs3,gs2,gs1,gs0,ps3,ps2,ps1,ps0,cen]
			bool[] output = {
				Logic.nand3(!s2,s1,s0), //gs3
				Logic.nand3(!s2, !s1, s0), //gs2
				Logic.nand3(!s2, s1, !s0), //gs1
				true, //gs0 - always outputs 1
				Logic.nor3(Logic.and2(s2,s0), Logic.and2(s1,!s0), Logic.and2(!s1, s0)), //ps3
				Logic.nor2(Logic.and2(s2, !s1), Logic.and2(s1, s0)), //ps2 && ps1
				Logic.nor2(Logic.and2(s2, !s1), Logic.and2(s1, s0)),
				Logic.nor3(Logic.and3(s2, s1, s0), Logic.and3(!s2, s1, !s0), Logic.and3(!s2, !s1, s0)), //ps0
				Logic.or2(Logic.and2(!s2, s1), Logic.and2(!s2, s0))//cen
		};
			return output;
		}
		public static bool[] carryChain(bool g3, bool p3, bool g2, bool p2, bool g1, bool p1, bool g0, bool p0, bool cen, bool cn)
		{
			//output is [f3,f2,f1,f0,p,g]
			bool[] output = {
				Logic.xor2(p3, Logic.or4(Logic.and2(g2, cen), Logic.and3(p2, g1, cen), Logic.and4(p2,p1,g0,cen), Logic.and5(p2, p1, p0, cn, cen))),
				Logic.xor2(p2, Logic.or3(Logic.and2(g1, cen), Logic.and3(p1, g0, cen),Logic.and4(p1, p0, cn, cen))),
				Logic.xor2(p1, Logic.or2(Logic.and2(g0, cen), Logic.and3(p0, cn, cen))),
				Logic.xor2(p0, Logic.and2(cn, cen)),
				Logic.nand4(p3,p2,p1,p0),
				Logic.nor4(g3,Logic.and2(p3, g2),Logic.and3(p3, p2, g1),Logic.and4(p3, p2, p1, g0))
		};
			return output;
		}
		public static bool[] carryOut(bool g3, bool p3, bool g2, bool p2, bool g1, bool p1, bool g0, bool p0, bool cen, bool cn)
		{
			//output is [f3,f2,f1,f0,p,g]
			bool[] output = {
				Logic.or4(Logic.and2(g2, cen), Logic.and3(p2, g1, cen), Logic.and4(p2,p1,g0,cen), Logic.and5(p2, p1, p0, cn, cen)),
				Logic.or3(Logic.and2(g1, cen), Logic.and3(p1, g0, cen),Logic.and4(p1, p0, cn, cen)),
				Logic.or2(Logic.and2(g0, cen), Logic.and3(p0, cn, cen)),
				Logic.and2(cn, cen),
				Logic.nand4(p3,p2,p1,p0),
				Logic.nor4(g3,Logic.and2(p3, g2),Logic.and3(p3, p2, g1),Logic.and4(p3, p2, p1, g0))
		};
			return output;
		}
		public static bool[] alu(bool a3, bool a2, bool a1, bool a0, bool b3, bool b2, bool b1, bool b0, bool s2, bool s1, bool s0)
		{
			//output is [f3,f2,f1,f0,p,g]
			bool[] cmdMap = commandMapping(s2, s1, s0);
			//printBinaryOutput(cmdMap);
			bool[] LU3 = logicUnit(a3, b3, cmdMap[0], cmdMap[1], cmdMap[2], cmdMap[3], cmdMap[4], cmdMap[5], cmdMap[6], cmdMap[7]);
			//printBinaryOutput(LU3);
			bool[] LU2 = logicUnit(a2, b2, cmdMap[0], cmdMap[1], cmdMap[2], cmdMap[3], cmdMap[4], cmdMap[5], cmdMap[6], cmdMap[7]);
			//printBinaryOutput(LU2);
			bool[] LU1 = logicUnit(a1, b1, cmdMap[0], cmdMap[1], cmdMap[2], cmdMap[3], cmdMap[4], cmdMap[5], cmdMap[6], cmdMap[7]);
			//printBinaryOutput(LU1);
			bool[] LU0 = logicUnit(a0, b0, cmdMap[0], cmdMap[1], cmdMap[2], cmdMap[3], cmdMap[4], cmdMap[5], cmdMap[6], cmdMap[7]);
			//printBinaryOutput(LU0);
			bool[] output = carryChain(LU3[0], LU3[1], LU2[0], LU2[1], LU1[0], LU1[1], LU0[0], LU0[1], cmdMap[8], false);
			return output;
		}
		public static bool[] alu74181(bool a3, bool a2, bool a1, bool a0, bool b3, bool b2, bool b1, bool b0, bool s3, bool s2, bool s1, bool s0, bool NotCn, bool M)
		{
			// output f0, f1, f2, f3, g, !Cn+4, P 
			bool[] LU3 = logicUnit74181(!a3, !b3, s3, s2, s1, s0);
			//printBinaryOutput(LU3);
			bool[] LU2 = logicUnit74181(!a2, !b2, s3, s2, s1, s0);
			//printBinaryOutput(LU2);
			bool[] LU1 = logicUnit74181(!a1, !b1, s3, s2, s1, s0);
			//printBinaryOutput(LU1);
			bool[] LU0 = logicUnit74181(!a0, !b0, s3, s2, s1, s0);
			bool[] output = carryChain74181(LU3[1], LU3[0], LU2[1], LU2[0], LU1[1], LU1[0], LU0[1], LU0[0], NotCn, M);
			return invert(output);
		}
		public static bool[] alu16(bool[] a, bool[] b, bool s3, bool s2, bool s1, bool s0, bool NotCn, bool M)
		{
			// output f0, f1, f2, f3, g, !Cn+4, P 
			int bits = 16;
			bool[] output = new bool[bits];
			List<bool> outpp = new List<bool>();
			bool cn = NotCn;
			for (int i = 0; i < output.Count(); i += 4)
			{
				bool[] outp = alu74181(a[(output.Count() - i) - 4], a[(output.Count() - i) - 3], a[(output.Count() - i) - 2], a[(output.Count() - i) - 1], b[(output.Count() - i) - 4], b[(output.Count() - i) - 3], b[(output.Count() - i) - 2], b[(output.Count() - i) - 1], s3, s2, s1, s0, cn, M);
				//Console.WriteLine();
				for (int j = 3; j >= 0; j--)
				{
					outpp.Add(outp[j]);
				}

				cn = !outp[7];
			}
			for (int i = 0; i < outpp.Count(); i++)
			{
				output[i] = outpp.ElementAt((outpp.Count() - 1) - i);
			}
			return output;
		}
		public static bool[] aluCpu(bool[] a, bool[] b, bool s3, bool s2, bool s1, bool s0, bool NotCn, bool M, int bits)
		{
			// output output[bits], g, !Cn+4, P 
			long start = 0;
			if (Config.recordClockSpeed)
				start = Util.nanoTime();
			bool[] output = new bool[bits + 3];
			List<bool> outpp = new List<bool>();
			bool cn = NotCn;
			bool finalG = false;
			bool finalP = false;

			for (int i = 0; i < bits; i += 4)
			{
				bool[] outp = alu74181(a[(bits - i) - 4], a[(bits - i) - 3], a[(bits - i) - 2], a[(bits - i) - 1], b[(bits - i) - 4], b[(bits - i) - 3], b[(bits - i) - 2], b[(bits - i) - 1], s3, s2, s1, s0, cn, M);
				//Console.WriteLine();
				for (int j = 3; j >= 0; j--)
				{
					outpp.Add(outp[j]);
				}
				finalG =outp[5];
				cn = !outp[7];
				finalP =outp[4];
			}
			for (int i = 0; i < outpp.Count(); i++)
			{
				output[i] = outpp.ElementAt((outpp.Count() - 1) - i);
			}
			output[bits] = finalG;
			output[bits + 1] = cn;
			output[bits + 2] = finalP;
			if (Config.recordClockSpeed)
			{
				long stop = Util.nanoTime();
				Console.WriteLine((stop - start) / 1000000.0);
				Config.avgClockSpeed += (stop - start) / 1000000.0;
				Config.recordedCycles += 1;
			}
			return output;
		}
		public static bool[] invert(bool[] a)
		{
			for (int i = 0; i < a.Count(); i++)
			{
				a[i] = !a[i];
			}
			return a;
		}
		public static bool[] logicUnit74181(bool a, bool b, bool s3, bool s2, bool s1, bool s0)
		{
			//output [!P, !G]
			bool[] output = {
				Logic.nor3(a, Logic.and2(b, s0), Logic.and2(s1, !b)),
				Logic.nor2(Logic.and3(!b, s2, a), Logic.and3(a, s3, b))
		};
			return output;
		}
		public static bool[] carryChain74181(bool g3, bool p3, bool g2, bool p2, bool g1, bool p1, bool g0, bool p0, bool notCn, bool M)
		{
			//output is [f3,f2,f1,f0,p,g,comparator,NotCn+4]
			bool[] output = {
				Logic.xor2(Logic.nor4(Logic.and2(!M, p2), Logic.and3(!M, p1, g2), Logic.and4(!M, p0, g1, g2), Logic.and5(!M, notCn, g0, g1, g2)), Logic.xor2(p3, g3)),
				Logic.xor2(Logic.nor3(Logic.and2(!M, p1), Logic.and3(!M, p0, g1), Logic.and4(!M, notCn, g0, g1)), Logic.xor2(p2, g2)),
				Logic.xor2(Logic.nor2(Logic.and2(!M, p0), Logic.and3(!M, g0, notCn)), Logic.xor2(p1, g1)),
				Logic.xor2(Logic.nand2(notCn, !M), Logic.xor2(p0, g0)),
				Logic.nor4(Logic.and4(p0, g1, g2, g3), Logic.and3(p1, g2, g3), Logic.and2(p2, g3), p3),
				Logic.nand4(g0, g1, g2, g3),
				false,
				false
		};
			output[6] = Logic.and4(output[3], output[2], output[1], output[0]);
			output[7] = Logic.or2(!Logic.nand5(notCn, g0, g1, g2, g3), !output[4]);
			return output;
		}
		public static void printBinaryOutput(bool[] output)
		{
			for (int i = 0; i < output.Count(); i++)
			{
				if (output[i] == true)
					Console.Write(1 + " ");
			else
					Console.Write(0 + " ");
			}
			Console.Write("\n");
		}
	}
}
