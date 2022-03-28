using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Emulator
{
    class Util
    {
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
		public static void logicControlSignalTest()
		{
			bool[] o = LogicUnits.commandMapping(false, false, true);
			printBinaryOutput(LogicUnits.logicUnit(false, false, o[0], o[1], o[2], o[3], o[4], o[5], o[6], o[7]));
		}
		public static string boolTostring(bool a)
		{
			if (a == false)
				return "0";
			else
				return "1";
		}
		public static void xorTest()
		{
			bool[] Output = {
				Logic.xor2(false, false),
				Logic.xor2(false, true),
				Logic.xor2(true, false),
				Logic.xor2(true, true)
			};
			printBinaryOutput(Output);
		}
		public static bool[] stringTobool(string inpput)
		{
			inpput = stripSpace(inpput);
			bool[] Output = new bool[inpput.Count()];
			for (int i = 0; i < inpput.Count(); i++)
			{
				if (inpput[i] == '0')
					Output[i] = false;
				else if (inpput[i] == '1')
					Output[i] = true;
			}
			return Output;
		}
		public static string stripSpace(string inp)
		{
			string Output = "";
			for (int i = 0; i < inp.Count(); i++)
				if (inp[i] == '0' ||inp[i] == '1')
					Output +=inp[i];
			return Output;
		}
		public static long nanoTime()
		{
			long nano = 10000L * Stopwatch.GetTimestamp();
			nano /= TimeSpan.TicksPerMillisecond;
			nano *= 100L;
			return nano;
		}
		public static string boolTostring(bool[] inpput)
		{
			string Output = "";
			for (int i = 0; i < inpput.Count(); i++)
			{
				if (inpput[i] == false)
					Output += 0;
				else
					Output += 1;
			}
			return Output;
		}
		public static bool[] addPadding(bool[] inp, int bits)
		{
			if (inp.Count() < bits){
				bool[] outpp = new bool[bits];
				for (int i = 0; i < bits -inp.Count(); i++) {
					outpp[i] = false;
				}
				for (int i = bits -inp.Count(); i < bits; i++)
						outpp[i] =inp[i-(bits -inp.Count())];
				return outpp;
			}
			else
				return inp;
		}
		public static int binaryToDecimal(string a)
		{

			BigInteger res = 0;


			foreach (char c in a)
			{
				res <<= 1;
				res += c == '1' ? 1 : 0;
			}

			return (int)res;
		}
		public static BigInteger binaryToLargeDecimal(string a)
		{

			BigInteger res = 0;

		
			foreach (char c in a)
			{
				res <<= 1;
				res += c == '1' ? 1 : 0;
			}

			return res;
		}

		public static String binaryToHex(String binary)
        {
			String hex = string.Join(" ",
			Enumerable.Range(0, binary.Length / 8)
			.Select(i => Convert.ToByte(binary.Substring(i * 8, 8), 2).ToString("X2")));
			return hex;
		}
		public static string decimalToBinary(string decimalNum)
		{
			BigInteger toUse;
			try
			{
				//hexToBinary
				toUse = BigInteger.Parse(decimalNum);
				

			}
			catch (FormatException)
			{
				Console.WriteLine("Err 3434:Unable to convert the string '{0}' to a BigInteger value.",
								  decimalNum);
				return null;
			}
			string toReturn = BigIntegerExtensions.ToBinaryString(toUse);
			if(toReturn.IndexOf('1')!=-1)
			toReturn = toReturn.Substring(toReturn.IndexOf('1'));
			return toReturn;
		}
		static string JavaStyleSubstring(string s, int beginIndex, int endIndex)
		{
			// simulates Java substring function
			int len = endIndex - beginIndex;
			return s.Substring(beginIndex, len);
		}
		public static string decimalToBinary(int decimalNum)
		{
			return BigIntegerExtensions.ToBinaryString(new BigInteger(decimalNum));
		}

		
		public static bool[] decimalToSignedBinary(int decimalNum,int bits)
		{
			Console.WriteLine("Turning input " + decimalNum + " into binary");
			if (decimalNum < 0)
				decimalNum--;
			bool [] toReturn = stringTobool(BigIntegerExtensions.ToBinaryString(new BigInteger(decimalNum)));
			bool[] leadingZeroes = new bool[bits];
            if (bits > toReturn.Count())//if bits is longer than input, add leading zeros
            {
				for(int i = 0; i < bits; i++)
                {
					if (i < bits - toReturn.Count())
					{
						if(decimalNum<0)
							leadingZeroes[i] = true;
						else
							leadingZeroes[i] = false;
					}
					else
						leadingZeroes[i] = toReturn[i - (bits - toReturn.Count())]; 
                }
				toReturn = leadingZeroes;
            }
            if (toReturn[0] == true)
            {
				for(int i = 1; i < toReturn.Count(); i++)
                {
					toReturn[i] = !toReturn[i];
                }
            }
			return toReturn;
		}
		public static String listToString(List<String> input)
        {
			string toAssemble = "";
			for(int i = 0; i < input.Count(); i++)
            {
				toAssemble += input.ElementAt(i);
            }
			return toAssemble;
        }
		public static float getAvgClockSpeed()
		{
			Console.WriteLine(Config.avgClockSpeed + "   " + Config.recordedCycles);
			return (float)(1000.0 / (Config.avgClockSpeed / Config.recordedCycles));

		}
		public static string stripstring(string inp)
		{
			string outp = "";
			for (int i = 0; i < inp.Count(); i++){
				if (inp[i] != ' ')
					outp+=inp[i];
			}
			return outp;
		}
		public static string hexToBinary(String hex)
		{

			BigInteger outp = 0;

			try
			{

				outp = BigInteger.Parse(hex, NumberStyles.AllowHexSpecifier); //need to fix words that take up the max bit space becoming negative
				return BigIntegerExtensions.ToBinaryString(outp);

			}
			catch (FormatException)
			{
				Console.WriteLine("Err 6969: Unable to convert the string '{0}' to a BigInteger value.",
								  hex);
				return null;
			}
		}
		public static string intToBinary(string decimalNum)
		{

			BigInteger outp = 0;
			try
			{
				//intToBinary
				outp = BigInteger.Parse(decimalNum);
				return BigIntegerExtensions.ToBinaryString(outp);

			}
			catch (FormatException)
			{
				Console.WriteLine("Err 2222: Unable to convert the string '{0}' to a BigInteger value.",
								  decimalNum);
				return null;
			}
		}
		public static void printHex(string binary)
		{
			string Output = bitsToHexConversion(stripSpace(binary));
			string outpp = "";
			for (int i = 0; i < Output.Count(); i++)
			{
				if (i % 2 == 0 && i != 0)
					outpp += " " + Output[i];
				else
					outpp += "" + Output[i];
			}
			Console.WriteLine(outpp);
		}
		public static string printHexString(string binary)
		{
			string Output = bitsToHexConversion(stripSpace(binary));
			string outpp = "";
			for (int i = 0; i < Output.Count(); i++)
			{
				if (i % 2 == 0 && i != 0)
					outpp += " " + Output[i];
				else
					outpp += "" + Output[i];
			}
			return outpp;
		}
		public static bool[] trimbool(bool[] inp, int bits)
		{
			bool[] flippedIn = new bool[inp.Count()];
			for (int i = 0; i < inp.Count(); i++)
				flippedIn[i] =inp[inp.Count()-i - 1];
			bool[] Output = new bool[bits];
			if (inp.Count() > bits){
				for (int i = 0; i < bits; i++)
				{
					Output[i] = flippedIn[i];
				}
			}
			else
				return inp;
			bool[] flippedoutp = new bool[bits];
			for (int i = 0; i < flippedoutp.Count(); i++)
			{
				flippedoutp[i] = Output[Output.Count() - i - 1];
			}
			return flippedoutp;
		}
		private static string bitsToHexConversion(string bitStream)
		{

			int byteLength = 4;
			int bitStartPos = 0, bitPos = 0;
			string hexstring = "";
			int sum = 0;

			// pad '0' to make inpput bit stream multiple of 4 

			if (bitStream.Count() % 4 != 0)
			{
				int tempCnt = 0;
				int tempBit = bitStream.Count() % 4;
				while (tempCnt < (byteLength - tempBit))
				{
					bitStream = "0" + bitStream;
					tempCnt++;
				}
			}

			// Group 4 bits, and find Hex equivalent 

			while (bitStartPos < bitStream.Count())
			{
				while (bitPos < byteLength)
				{
					sum = (int)(sum + int.Parse("" + bitStream[bitStream.Count() - bitStartPos - 1]) * Math.Pow(2, bitPos));
					bitPos++;
					bitStartPos++;
				}
				if (sum < 10)
					hexstring = ""+sum + hexstring;
				else
					hexstring = (char)(sum + 55) + hexstring;

				bitPos = 0;
				sum = 0;
			}
			return hexstring;
		}
		
		public static string combineArray(string[] inp)
		{
			string outp = "";
			for (int i = 0; i < inp.Count(); i++)
				outp+=inp[i];
			return outp;
		}
	}
}
