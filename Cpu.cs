using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator
{
    class Cpu
    {
		private int bits;                   //bits of the cpu
		private int numRegisters = Config.numRegisters;      //number of registers
		private int memorySize = Config.memorySize;     //memory size in bytes
		private int romSize = Config.romSize;         //Rom Size in bytes
		private int stackLocation = Config.stackLocation;


		private bool[] ROM;
		//private bool [] register[2];
		private bool[][] register;   // Register 0: stack pointer | Register 1: Status reg [c,z,n] | Register 2: pccounter | register 3: const 0
		private bool[] memory;
		private bool lastN = false;
		private CpuHandler session;


		public Cpu(int bit, bool[] ROMArray,CpuHandler session)
		{
			this.session = session;
			bits = bit;                                 //sets instruction size
			//removed bits from second part of register
			register = new bool[numRegisters][]; // init registers
			for (int i = 0; i < numRegisters; i++) 
				register[i] = new bool[bits];
			memory = new bool[memorySize * 8];                   // sets memory size
			ROM = new bool[romSize * 8];
			register[0] = Util.stringTobool(Util.decimalToBinary(stackLocation)); //init Stack
			register[2] = Util.addPadding(Util.stringTobool(Util.decimalToBinary((memorySize) - (romSize))), bits); //inits the 16 bit program counter

			//ROM = Util.stringTobool("0100010000000010 0101001000000100 0100001000000101 01010010000001000000000000000000");			

			//INIT ROM INTO MEMORY
			writeMemory(Util.stringTobool(Util.decimalToBinary(memorySize - romSize)), ROMArray);
			//DONT REMOVE

			Util.printHex(Util.boolTostring(readMemory(Util.stringTobool(Util.decimalToBinary(69)), 12)));
		}

		public void cycle()
		{
			bool[] toExecute = new bool[16];
			if (Config.printInstruction)
				Util.printHex(Util.boolTostring(readMemory(register[2], 2)));
			toExecute = readMemory(register[2], 2);
			instruction(toExecute);
			register[2] = Util.addPadding(Util.stringTobool(Util.decimalToBinary(((Util.binaryToDecimal(Util.boolTostring(register[2])) + 2)))), bits);

		}
		public void printRegisterContents()
		{

			Util.printHex(Util.boolTostring(register[2]));
			for (int i = 0; i < register.Count(); i++)
			{

				Console.Write("R" + i + ": ");
				Util.printHex(Util.boolTostring(register[i]));
			}
		}
		public bool[] getNextByteInStream()
		{
			bool[] nextByte = Util.stringTobool(Util.decimalToBinary(((Util.binaryToDecimal(Util.boolTostring(register[2])) + 2))));

			return readMemory(nextByte, 1);
		}
		public bool[] getNextWordInStream()
		{
			bool[] nextByte = Util.stringTobool(Util.decimalToBinary(((Util.binaryToDecimal(Util.boolTostring(register[2])) + 2))));

			return readMemory(nextByte, 2);
		}
		public bool[] getNextXInStream(int bytes)
		{
			bool[] nextByte = Util.stringTobool(Util.decimalToBinary(((Util.binaryToDecimal(Util.boolTostring(register[2])) + 2))));

			return readMemory(nextByte, bytes);
		}

		public void instruction(bool[] inp)
		{ //handles instructions to the cpu
			if (Config.printInstruction)
				Console.WriteLine(Util.boolTostring(inp));
			bool[] data = flipBytes(inp);
			if (Util.boolTostring(inp)==("0000010101010101"))
			{
				session.stop();
			}
			switch (msbIndex(data))
			{
				case 12:
					bool[] opcodee = { data[9], data[8], data[7] };
					bool[] bw = { data[6] };
					bool[] asd = { data[5], data[4] };
					bool[] sourcee = { data[3], data[2], data[1], data[0] };
					bool[] bytess = Util.stringTobool("1");
					bool[] memoryAddress = null;
					int byteCountt = 1;
					if (!bw[0])
					{
						bytess = Util.addPadding(getNextWordInStream(), bits);
						byteCountt = Util.binaryToDecimal(Util.boolTostring(bytess));
					}

					bool[] srcc = null;
					if (!asd[0] && !asd[1])
					{
						srcc = register[Util.binaryToDecimal(Util.boolTostring(sourcee))];
						
					}
					if (!asd[0] && asd[1])
					{
						if (bw[0])
						{
							
							memoryAddress = trimAluOutputNoZero(LogicUnits.aluCpu(register[Util.binaryToDecimal(Util.boolTostring(sourcee))], Util.addPadding(getNextWordInStream(), bits), true, false, false, true, false, false, bits));
							srcc = readMemory(memoryAddress, 1);
				
							register[2] = Util.addPadding(Util.stringTobool(Util.decimalToBinary(((Util.binaryToDecimal(Util.boolTostring(register[2])) + 2)))), bits);
						}
						if (!bw[0])
						{
							if (Util.binaryToDecimal(Util.boolTostring(sourcee)) >= 0)
							{
								register[2] = Util.addPadding(Util.stringTobool(Util.decimalToBinary(((Util.binaryToDecimal(Util.boolTostring(register[2])) + 2)))), bits);
								memoryAddress = trimAluOutputNoZero(LogicUnits.aluCpu(register[Util.binaryToDecimal(Util.boolTostring(sourcee))], Util.addPadding(getNextWordInStream(), bits), true, false, false, true, false, false, bits));
								srcc = readMemory(memoryAddress, byteCountt);

							}
							else
							{
								register[2] = Util.addPadding(Util.stringTobool(Util.decimalToBinary(((Util.binaryToDecimal(Util.boolTostring(register[2])) + 2)))), bits);
								memoryAddress = Util.addPadding(getNextWordInStream(), bits);
								srcc = readMemory(memoryAddress, byteCountt);
							}
							register[2] = Util.addPadding(Util.stringTobool(Util.decimalToBinary(((Util.binaryToDecimal(Util.boolTostring(register[2])) + 2)))), bits);

						}
					}
					if (asd[0] && !asd[1])
					{
						if (bw[0])
						{

							memoryAddress = register[Util.binaryToDecimal(Util.boolTostring(sourcee))];
							srcc = readMemory(memoryAddress, 1);

						}
						if (!bw[0])
						{
							memoryAddress = register[Util.binaryToDecimal(Util.boolTostring(sourcee))];
							srcc = readMemory(memoryAddress, byteCountt);

							register[2] = Util.addPadding(Util.stringTobool(Util.decimalToBinary(((Util.binaryToDecimal(Util.boolTostring(register[2])) + 2)))), bits);
							register[2] = Util.addPadding(Util.stringTobool(Util.decimalToBinary(((Util.binaryToDecimal(Util.boolTostring(register[2])) + byteCountt)))), bits);

						}
					}
					if (asd[0] && asd[1])
					{
						if (bw[0])
						{
							if (Util.binaryToDecimal(Util.boolTostring(sourcee)) == 2)
								register[2] = Util.addPadding(Util.stringTobool(Util.decimalToBinary(((Util.binaryToDecimal(Util.boolTostring(register[2])) + 2)))), bits);
							memoryAddress = register[Util.binaryToDecimal(Util.boolTostring(sourcee))];
							srcc = readMemory(memoryAddress, 1);
							register[Util.binaryToDecimal(Util.boolTostring(sourcee))] = trimAluOutputNoZero(LogicUnits.aluCpu(register[Util.binaryToDecimal(Util.boolTostring(sourcee))], Util.addPadding(Util.stringTobool("1"), bits), true, false, false, true, false, false, bits));
							if (Util.binaryToDecimal(Util.boolTostring(sourcee)) == 2)
								register[2] = Util.addPadding(Util.stringTobool(Util.decimalToBinary(((Util.binaryToDecimal(Util.boolTostring(register[2])) - 2)))), bits);

						}
						if (!bw[0])
						{
							register[2] = Util.addPadding(Util.stringTobool(Util.decimalToBinary(((Util.binaryToDecimal(Util.boolTostring(register[2])) + 4)))), bits);
							memoryAddress = register[Util.binaryToDecimal(Util.boolTostring(sourcee))];
							srcc = readMemory(memoryAddress, byteCountt);

							register[Util.binaryToDecimal(Util.boolTostring(sourcee))] = trimAluOutputNoZero(LogicUnits.aluCpu(register[Util.binaryToDecimal(Util.boolTostring(sourcee))], Util.addPadding(bytess, bits), true, false, false, true, false, false, bits));
							register[2] = Util.addPadding(Util.stringTobool(Util.decimalToBinary(((Util.binaryToDecimal(Util.boolTostring(register[2])) - 2)))), bits);
						}
					}
					if (!asd[0] && !asd[1])
					{
						register[Util.binaryToDecimal(Util.boolTostring(sourcee))] = SOArithmetic(opcodee, Util.addPadding(srcc, bits));
					}
					if (!asd[0] && asd[1])
					{
						if (bw[0])
						{
							writeMemory(
										memoryAddress,
										Util.trimbool(SOArithmetic(opcodee, Util.addPadding(srcc, bits)), 8));

						}
						if (!bw[0])
						{
							writeMemory(
										  memoryAddress,
										  Util.trimbool(SOArithmetic(opcodee, Util.addPadding(srcc, bits)), byteCountt * 8));
						}
					}
					if (asd[0] && !asd[1])
					{
						if (bw[0])
						{
							writeMemory(
										memoryAddress,
										Util.trimbool(SOArithmetic(opcodee, Util.addPadding(srcc, bits)), 8));

						}
						if (!bw[0])
						{
							writeMemory(
										  memoryAddress,
										  Util.trimbool(SOArithmetic(opcodee, Util.addPadding(srcc, bits)), byteCountt * 8));
						}
					}
					if (asd[0] && asd[1])
					{
						if (bw[0])
						{
							writeMemory(
										memoryAddress,
										Util.trimbool(SOArithmetic(opcodee, Util.addPadding(srcc, bits)), 8));

						}
						if (!bw[0])
						{
							writeMemory(
										  memoryAddress,
										  Util.trimbool(SOArithmetic(opcodee, Util.addPadding(srcc, bits)), byteCountt * 8));
						}
					}

					break;
				case 13://conditional jump
					bool[] condition = { data[12], data[11], data[10] };
					bool[] sign = { data[9] };
					bool[] offset = { data[8], data[7], data[6], data[5], data[4], data[3], data[2], data[1], data[0] };
					bool shouldJump = false;
					if (condition[0] && condition[1] && condition[2])
					{ //unconditional jump
						shouldJump = true;
					}
					if (!condition[0] && !condition[1] && condition[2])
					{ //jump if zero
						if (register[1][register[1].Count() - 2])
							shouldJump = true;
					}
					if (!condition[0] && !condition[1] && !condition[2])
					{ //jump if not zero
						if (!register[1][register[1].Count() - 2])
							shouldJump = true;
					}
					if (!condition[0] && condition[1] && !condition[2])
					{ //jump if no carry
						if (!register[1][register[1].Count() - 1])
							shouldJump = true;
					}
					if (!condition[0] && condition[1] && condition[2])
					{ //jump if carry
						if (register[1][register[1].Count() - 1])
							shouldJump = true;
					}
					if (condition[0] && !condition[1] && !condition[2])
					{ //jump if negative
						if (register[1][register[1].Count() - 3])
							shouldJump = true;
					}

					if (shouldJump)
					{
						if (!sign[0])
						{
							register[2] = trimAluOutputNoZero(LogicUnits.aluCpu(register[2], Util.addPadding(offset, bits), true, false, false, true, false, false, bits));
						
						}
						else if (sign[0])
						{
							register[2] = trimAluOutputNoZero(LogicUnits.aluCpu(register[2], Util.addPadding(offset, bits), false, true, true, false, true, false, bits));
						
						}
					}

					break;
				case 15:
				case 14://two operand arithmetic
					bool[] opcode = { data[15], data[14], data[13], data[12] };
					bool[] source = { data[11], data[10], data[9], data[8] };
					bool[] ad = { data[7] };
					bool[] b = { data[6] };
					bool[] asp = { data[5],data[4]};
					bool[] destination = { data[3], data[2], data[1], data[0] };

					//Two-operand arithmetic
					bool[] bytes = Util.stringTobool("1");
					int byteCount = 1;
					if (!b[0])
					{
						bytes = Util.addPadding(getNextWordInStream(), bits);
						byteCount = Util.binaryToDecimal(Util.boolTostring(bytes));
					}

					bool[] src = null;
					if (!asp[0] && !asp[1])
					{
						src = register[Util.binaryToDecimal(Util.boolTostring(source))];
					
					}
					if (!asp[0] &&asp[1])
					{
						if (b[0])
						{
							if (Util.binaryToDecimal(Util.boolTostring(source)) >= 0)
								src = readMemory(trimAluOutputNoZero(LogicUnits.aluCpu(register[Util.binaryToDecimal(Util.boolTostring(source))], Util.addPadding(getNextWordInStream(), bits), true, false, false, true, false, false, bits)), 1);
							else
							{
							
								src = readMemory(Util.addPadding(getNextWordInStream(), bits), 1);
							}
							register[2] = Util.addPadding(Util.stringTobool(Util.decimalToBinary(((Util.binaryToDecimal(Util.boolTostring(register[2])) + 2)))), bits);
						}
						if (!b[0])
						{
							if (Util.binaryToDecimal(Util.boolTostring(source)) >= 0)
							{
								register[2] = Util.addPadding(Util.stringTobool(Util.decimalToBinary(((Util.binaryToDecimal(Util.boolTostring(register[2])) + 2)))), bits);

								src = readMemory(trimAluOutputNoZero(LogicUnits.aluCpu(register[Util.binaryToDecimal(Util.boolTostring(source))], Util.addPadding(getNextWordInStream(), bits), true, false, false, true, false, false, bits)), byteCount);

							}
							else
							{
								register[2] = Util.addPadding(Util.stringTobool(Util.decimalToBinary(((Util.binaryToDecimal(Util.boolTostring(register[2])) + 2)))), bits);

								src = readMemory(Util.addPadding(getNextWordInStream(), bits), byteCount);
							}
							register[2] = Util.addPadding(Util.stringTobool(Util.decimalToBinary(((Util.binaryToDecimal(Util.boolTostring(register[2])) + 2)))), bits);

						}
					}
					if (asp[0] && !asp[1])
					{
						if (b[0])
						{
							src = readMemory(register[Util.binaryToDecimal(Util.boolTostring(source))], 1);
							//make sure register[2] doesnt skip bytes when implementing word addressing

						}
						if (!b[0])
						{
							
							src = readMemory(register[Util.binaryToDecimal(Util.boolTostring(source))], byteCount);
							register[2] = Util.addPadding(Util.stringTobool(Util.decimalToBinary(((Util.binaryToDecimal(Util.boolTostring(register[2])) + 2)))), bits);

							//changed this
							register[2] = Util.addPadding(Util.stringTobool(Util.decimalToBinary(((Util.binaryToDecimal(Util.boolTostring(register[2])) - 4)))), bits);
							register[2] = Util.addPadding(Util.stringTobool(Util.decimalToBinary(((Util.binaryToDecimal(Util.boolTostring(register[2])) + byteCount)))), bits);

						}
					}
					if (asp[0] &&asp[1])
					{
						if (b[0])
						{
							if (Util.binaryToDecimal(Util.boolTostring(source)) == 2)
								register[2] = Util.addPadding(Util.stringTobool(Util.decimalToBinary(((Util.binaryToDecimal(Util.boolTostring(register[2])) + 2)))), bits);
						src = readMemory(register[Util.binaryToDecimal(Util.boolTostring(source))], 1);
							register[Util.binaryToDecimal(Util.boolTostring(source))] = trimAluOutputNoZero(LogicUnits.aluCpu(register[Util.binaryToDecimal(Util.boolTostring(source))], Util.addPadding(Util.stringTobool("1"), bits), true, false, false, true, false, false, bits));
							if (Util.binaryToDecimal(Util.boolTostring(source)) == 2)
								register[2] = Util.addPadding(Util.stringTobool(Util.decimalToBinary(((Util.binaryToDecimal(Util.boolTostring(register[2])) - 2)))), bits);

							//make sure register[2] doesnt skip bytes when implementing word addressing

						}
						if (!b[0])
						{
							register[2] = Util.addPadding(Util.stringTobool(Util.decimalToBinary(((Util.binaryToDecimal(Util.boolTostring(register[2])) + 4)))), bits);
							src = readMemory(register[Util.binaryToDecimal(Util.boolTostring(source))], byteCount);
						
							register[Util.binaryToDecimal(Util.boolTostring(source))] = trimAluOutputNoZero(LogicUnits.aluCpu(register[Util.binaryToDecimal(Util.boolTostring(source))], Util.addPadding(bytes, bits), true, false, false, true, false, false, bits));
							register[2] = Util.addPadding(Util.stringTobool(Util.decimalToBinary(((Util.binaryToDecimal(Util.boolTostring(register[2])) - 2)))), bits);
					}
					}
					if (!ad[0])
					{
						Console.WriteLine("attemping to access register " + (Util.binaryToDecimal(Util.boolTostring(destination) )));
						register[Util.binaryToDecimal(Util.boolTostring(destination))] = TOArithmetic(opcode, Util.addPadding(src, bits), Util.addPadding(register[Util.binaryToDecimal(Util.boolTostring(destination))], bits));
					}
					if (ad[0])
					{
						if (b[0])
						{
							if (Util.binaryToDecimal(Util.boolTostring(destination)) >= 0)
							{
								writeMemory(
										trimAluOutputNoZero(LogicUnits.aluCpu(register[Util.binaryToDecimal(Util.boolTostring(destination))], Util.addPadding(getNextWordInStream(), bits), true, false, false, true, false, false, bits)),
										Util.trimbool(TOArithmetic(opcode, Util.addPadding(src, bits), Util.addPadding(readMemory(trimAluOutputNoZero(LogicUnits.aluCpu(register[Util.binaryToDecimal(Util.boolTostring(destination))], Util.addPadding(getNextWordInStream(), bits), true, false, false, true, false, false, bits)), 1), bits)), 8));
								
							}
							else
							{
							writeMemory(
										Util.addPadding(getNextWordInStream(), bits),
										Util.trimbool(TOArithmetic(opcode, Util.addPadding(src, bits), Util.addPadding(readMemory(Util.addPadding(getNextWordInStream(), bits), 1), bits)), 8));
							}
							register[2] = Util.addPadding(Util.stringTobool(Util.decimalToBinary(((Util.binaryToDecimal(Util.boolTostring(register[2])) + 2)))), bits);
						}
						if (!b[0])
						{

							if (!asp[0] && !asp[1])
								register[2] = Util.addPadding(Util.stringTobool(Util.decimalToBinary(((Util.binaryToDecimal(Util.boolTostring(register[2])) + 2)))), bits);

							if (Util.binaryToDecimal(Util.boolTostring(destination)) > 2)
							{

								writeMemory(
										  trimAluOutputNoZero(LogicUnits.aluCpu(register[Util.binaryToDecimal(Util.boolTostring(destination))], Util.addPadding(getNextWordInStream(), bits), true, false, false, true, false, false, bits)),
										  Util.trimbool(TOArithmetic(opcode, Util.addPadding(src, bits), Util.addPadding(readMemory(trimAluOutputNoZero(LogicUnits.aluCpu(register[Util.binaryToDecimal(Util.boolTostring(destination))], Util.addPadding(getNextWordInStream(), bits), true, false, false, true, false, false, bits)), byteCount), bits)), byteCount * 8));
								register[2] = Util.addPadding(Util.stringTobool(Util.decimalToBinary(((Util.binaryToDecimal(Util.boolTostring(register[2])) - 2)))), bits);

							}
							else
							{
							writeMemory(
										Util.addPadding(getNextWordInStream(), bits),
										Util.trimbool(TOArithmetic(opcode, Util.addPadding(src, bits), Util.addPadding(readMemory(Util.addPadding(getNextWordInStream(), bits), byteCount), bits)), byteCount * 8));
							register[2] = Util.addPadding(Util.stringTobool(Util.decimalToBinary(((Util.binaryToDecimal(Util.boolTostring(register[2])) - 2)))), bits);

							}
							register[2] = Util.addPadding(Util.stringTobool(Util.decimalToBinary(((Util.binaryToDecimal(Util.boolTostring(register[2])) + 4)))), bits);

						}
					}

					break;

			}

		}
		private bool[] SOArithmetic(bool[] opcode, bool[] src)
		{
			if (!opcode[0] && !opcode[1] && !opcode[2])
			{ //RRC
				bool[] outp = new bool[src.Count()];
			outp[0]= register[1][register[1].Count() - 1];
				for (int i = 1; i < src.Count(); i++)
				{
				outp[i]= src[i - 1];
				}
				register[1][register[1].Count() - 1] = src[src.Count() - 1];
				return outp;
			}
			if (!opcode[0] && !opcode[1] && opcode[2])
			{ //SWPB
				bool[] outp = new bool[src.Count()];
			outp= swapEndian(src);
				return outp;
			}
			if (!opcode[0] && opcode[1] && !opcode[2])
			{ //RRA
				bool[] outp = new bool[src.Count()];
				register[1][register[1].Count() - 1] = src[src.Count() - 1];
			outp[0]= src[0];
				for (int i = 1; i < src.Count(); i++)
				{
				outp[i]= src[i - 1];
				}
				return outp;
			}
			if (!opcode[0] && opcode[1] && opcode[2])
			{ //SXT
				bool[] trimmedSrc = flipBytes(Util.trimbool(flipBytes(src), 8));
				return Util.addPadding(trimmedSrc, src.Count());
			}
			if (opcode[0] && !opcode[1] && !opcode[2])
			{ //PUSH
				register[0] = Util.addPadding(Util.stringTobool(Util.decimalToBinary(((Util.binaryToDecimal(Util.boolTostring(register[0])) - (src.Count() / 8))))), bits);
				writeMemory(register[0], src);
				return src;
			}
			if (opcode[0] && !opcode[1] && !opcode[2])
			{ //CALL
				writeMemory(register[0], register[2]);
				register[0] = Util.addPadding(Util.stringTobool(Util.decimalToBinary(((Util.binaryToDecimal(Util.boolTostring(register[0])) - (register[2].Count() / 8))))), bits);
				register[2] = Util.addPadding(src, bits);
				return src;
			}
			//pop = MOV @SP+,dst
			if (opcode[0] && !opcode[1] && !opcode[2])
			{ //RETI
				writeMemory(register[0], register[2]);
				register[0] = Util.addPadding(Util.stringTobool(Util.decimalToBinary(((Util.binaryToDecimal(Util.boolTostring(register[0])) - (register[2].Count() / 8))))), bits);
				register[2] = Util.addPadding(src, bits);
				return src;
			}
			return null;
		}
		public bool[] swapEndian(bool[] src)
		{
			bool[] outp = new bool[src.Count()];
			List<bool[]> bytesIn = new List<bool[]>();
			List<bool[]> bytes = new List<bool[]>();

			int count = 0;
			for (int i = 0; i < outp.Count(); i += 8){
				bytesIn.Add(new bool[8]);
				for (int j = 0; j < 8; j++)
				{
					bytesIn.ElementAt(count)[j] = src[i + j];
				}
				count++;
			}
			for (int i = 0; i < bytesIn.Count(); i++)
			{
				bytes.Add(bytesIn.ElementAt(bytesIn.Count() - i - 1));
			}
			int outCount = 0;
			for (int i = 0; i < bytes.Count(); i++)
			{
				for (int j = 0; j < bytes.ElementAt(i).Count(); j++)
				{
				outp[outCount]= bytes.ElementAt(i)[j];
					outCount++;
				}
			}
			return outp;
		}
		public bool[] TOArithmetic(bool[] opcode, bool[] src, bool[] dest)
		{
		if (!opcode[0] && opcode[1] && !opcode[2] && opcode[3])
			{ //ADD = 1001 A PLUS B
				return trimAluOutput(LogicUnits.aluCpu(src, dest, true, false, false, true, false, false, bits));
			}
			if (!opcode[0] && opcode[1] && opcode[2] && !opcode[3])
			{ //ADDC = 1001 A PLUS B + C
				return trimAluOutput(LogicUnits.aluCpu(src, dest, true, false, false, true, register[1][register[1].Count() - 1], false, bits));
			}
			if (opcode[0] && !opcode[1] && !opcode[2] && !opcode[3])
			{ //SUB = 0110 A MINUS B
				return trimAluOutput(LogicUnits.aluCpu(dest, src, false, true, true, false, true, false, bits));
			}
			if (opcode[0] && !opcode[1] && !opcode[2] && opcode[3])
			{ //CMP = 0110 A MINUS B
				trimAluOutput(LogicUnits.aluCpu(dest, src, false, true, true, false, true, false, bits));
				return dest;
			}
			if (!opcode[0] && opcode[1] && opcode[2] && opcode[3])
			{ //SUBC = 0110 A MINUS B - C
				return trimAluOutput(LogicUnits.aluCpu(dest, src, false, true, true, false, register[1][register[1].Count() - 1], false, bits));
			}
			if (opcode[0] && opcode[1] && !opcode[2] && opcode[3])
			{  //BIS = 1011 A+B
				return trimAluOutput(LogicUnits.aluCpu(src, dest, true, false, true, true, false, true, bits));
			}
			if (opcode[0] && opcode[1] && opcode[2] && !opcode[3])
			{  //XOR = 1011 A~+B
				return trimAluOutput(LogicUnits.aluCpu(src, dest, true, false, false, true, false, true, bits));
			}
			if (opcode[0] && opcode[1] && opcode[2] && opcode[3])
			{  //AND = 1110 A AND B
				return trimAluOutput(LogicUnits.aluCpu(src, dest, true, true, true, false, false, true, bits));
			}
			if (opcode[0] && opcode[1] && !opcode[2] && !opcode[3])
			{  //BIC = 1110 A AND ~B
				return trimAluOutput(LogicUnits.aluCpu(invertbool(src), dest, true, true, true, false, false, true, bits));
			}
			if (opcode[0] && !opcode[1] && opcode[2] && opcode[3])
			{  //BIT = 1110 A AND B
				trimAluOutput(LogicUnits.aluCpu(src, dest, true, true, true, false, false, true, bits));
				return dest;
			}

			return src;
		}
		public bool[] invertbool(bool[] inp)
		{
			bool [] outp = new bool[inp.Count()];
			for (int i = 0; i < outp.Count(); i++){
			outp[i]= !inp[i];
			}
			return outp;

		}
		public void printMemory(int address, int bytes)
		{ //for debug purposes. Obviously this method wont work if trying to print higher than 32-bit address space
			bool[][] outp = new bool[bytes][];
			for (int i = 0; i < bytes; i++)
				outp[i] = new bool[8];
			for (int j = 0; j < bytes; j++)
			{
				int ad = address + j;
				bool[] toAdd = readMemory(Util.stringTobool(Util.decimalToBinary(ad)), 1);
			outp[j]= toAdd;			
			}
			for (int i = 0; i < outp.Count(); i += Config.bits / 8) {
				Console.Write("0x" + (address + (i)).ToString("X") + ": ");
				string toPrint = "";
				for (int q = 0; q < Config.bits / 8; q++)
				{
					if (outp.Count() > i + q)
					toPrint += Util.boolTostring(outp[i + q]);
			}
			Util.printHex(toPrint);
		}
	}
		public String printMemoryString(int address, int bytes)
		{ //for debug purposes. Obviously this method wont work if trying to print higher than 32-bit address space
			List<string> output = new List<string>();
			bool[][] outp = new bool[bytes][];
			for (int i = 0; i < bytes; i++)
				outp[i] = new bool[8];
			for (int j = 0; j < bytes; j++)
			{
				int ad = address + j;
				bool[] toAdd = readMemory(Util.stringTobool(Util.decimalToBinary(ad)), 1);
			outp[j]= toAdd;
			}
			for (int i = 0; i < outp.Count(); i += Config.bits / 8) {
				output.Add("0x" + (address + (i)).ToString("X") + ":\t ");
				string toPrint = "";
				for (int q = 0; q < Config.bits / 8; q++)
				{
					if (outp.Count() > i + q)
					toPrint += Util.boolTostring(outp[i + q]);
			}
				output.Add(Util.printHexString(toPrint)+"\r\n");
				
		}

			return Util.listToString(output);
		}
		
	public bool[] readMemory(bool[] address, int bytes)
	{
		int ad = Util.binaryToDecimal(Util.boolTostring(address));
		bool[] outp = new bool[bytes * 8];

		if (ad < memorySize - (bytes * 8) && ad >= memorySize - romSize)
		{  //space reservered for rom is memorysize - romsize; 
			for (int i = 0; i < bytes * 8; i++)
			{
					outp [i] = ROM[(ad * 8) - ((memorySize * 8) - (romSize * 8)) + i];
			}
		}
		else if (ad >= 0 && ad <= memorySize - romSize - (bytes * 8))
		{      //ram
			for (int i = 0; i < bytes * 8; i++)
			{
				outp [i] = memory[(ad * 8) + i];
			}
		}

		return outp;
	}
	public bool writeMemory(bool[] address, bool[] data)
	{ //returns true if memory was successfully written
		int ad = Util.binaryToDecimal(Util.boolTostring(address));
		int bytes = data.Count() / 8;

		if (ad < memorySize - (bytes * 8) && ad >= memorySize - romSize)
		{  //space reservered for rom is memorysize - romsize; 
			for (int i = 0; i < bytes * 8; i++)
			{
				ROM[(ad * 8) - ((memorySize * 8) - (romSize * 8)) + i] = data[i];
			}
				session.updateDisplay();
			return true;
		}
		else if (ad >= 0 && ad <= memorySize - romSize - (bytes * 8))
		{     //ram
			for (int i = 0; i < bytes * 8; i++)
			{
				memory[(ad * 8) + i] = data[i];
			}
				session.updateDisplay();
				return true;
		}
			session.updateDisplay();
			return false;
	}
	public bool[] trimAluOutput(bool[] inp)
	{
		bool[] outp = new bool[bits];
		for (int i = 0; i < outp.Count(); i++){
			outp[i] = inp[i];
		}
		register[1][register[1].Count() - 1] =inp[bits+1];
		if (Util.binaryToLargeDecimal(Util.boolTostring(outp))==(Util.binaryToLargeDecimal("0")))
			register[1][register[1].Count() - 2] = true;
		else
			register[1][register[1].Count() - 2] = false;
		if (outp[0]){ //&&!lastN
			register[1][register[1].Count() - 3] = true;
			
		}
		if (!outp[0]){
			register[1][register[1].Count() - 3] = false;
			
		}


		return outp;
	}
	public bool[] trimAluOutputNoZero(bool[] inp)
	{
		bool[] outp = new bool[bits];
		for (int i = 0; i < outp.Count(); i++){
			outp[i] = inp[i];
		}
		register[1][register[1].Count() - 1] =inp[bits+1];
		return outp;
	}
	private bool[] flipBytes(bool[] inp)
	{ //flips order of bytes for ease of data manipulation
		bool[] outp = new bool[inp.Count()];
		for (int i = 0; i < inp.Count(); i++){
			outp[i]=inp[(inp.Count() - 1)-i];
		}
		return outp;
	}
	private int msbIndex(bool[] inp)
	{   //returns the index of the msb
		for (int i = inp.Count() - 1; i >= 0; i--){
			if (inp[i])
				return i;
		}
		return -1;
	}
}
}
