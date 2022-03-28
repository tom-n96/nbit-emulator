using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator
{
    class Config
    {
		public static double avgClockSpeed = -2;
		public static double recordedCycles = 0;
		public static bool recordClockSpeed = false;
		public static int bits = 64;
		public static bool printRegisters = true;
		public static bool printInstruction = true;

		public static int numRegisters = 16;      //number of registers
		public static int memorySize = 50000;     //memory size in bytes
		public static int romSize = 3500;         //Rom Size in bytes
		public static int stackLocation = 40000;
	}
}
