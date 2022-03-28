using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Emulator
{
    class Assembler
    {
		public int bits = 64;
		public List<string> tags = new List<string>();
		public List<int> offset = new List<int>();
		public int lastOffsetIndex = 0;
		public List<bool[]> db = new List<bool[]>();
		public int dbcount = 0;
		public int dbpcincrement = 0;
		public int programStart = 10000;//50000-3500;
		public int previouspc = 0;
		public int pc = 0;
		public bool runAgain = true;
		private Emulator.Util utils=new Emulator.Util();
		private MainWindow mainWin;
		public String run(System.Collections.Generic.IEnumerable<string> fileContents,string directory, int bit, MainWindow mw)
		{
			mainWin = mw;
					bits = bit;
				string[] toBuild;
			List<bool> outpput = new List<bool>();

			toBuild = fileContents.ToArray();
			
			for (int i = 0; i < toBuild.Count(); i++)
			{

				if (toBuild[i].Count() > 1)
				{
					bool[] toAdd = Emulator.Util.stringTobool(parseLine(toBuild[i]));
					for (int j = 0; j < toAdd.Count(); j++)
						outpput.Add(toAdd[j]);
				}
			}
			runAgain = false;
			outpput.Clear();
			pc = 0;
			for (int i = 0; i < toBuild.Count(); i++)
			{

				if (toBuild[i].Count() > 1)
				{
					bool[] toAdd = Emulator.Util.stringTobool(parseLine(toBuild[i]));
					for (int j = 0; j < toAdd.Count(); j++)
						outpput.Add(toAdd[j]);
				}
			}
			bool[] toPrint = new bool[outpput.Count()];
			for (int i = 0; i < toPrint.Count(); i++)
			{
				toPrint[i] = outpput.ElementAt(i);
			}
			Console.WriteLine(addSpace(Emulator.Util.boolTostring(toPrint)));
			string fileName = JavaStyleSubstring(directory,0, directory.IndexOf(".")) + ".vbin";
			StreamWriter writer = null;
			try
			{
				writer = new StreamWriter(fileName);
				writer.Write(addSpace(Emulator.Util.boolTostring(toPrint)));

			}
			catch (Exception e)
			{
			}
			finally
			{
				try
				{
					if (writer != null)
						writer.Close();
					
				}
				catch (Exception e)
				{
					//return null;
				}
			}
			return fileName;
		}
		public string stripComments(string inp)
		{
			if (inp.IndexOf(";") != -1){
				return JavaStyleSubstring(inp,0, inp.IndexOf(";"));
			}
		else
				return inp;
		}
		public string addSpace(string inp)
		{
			string outp = "";
			for (int i = 0; i < inp.Count(); i++){
			outp+=inp[i];
				if ((i + 1) % 4 == 0)
				outp+= " ";
				if ((i + 1) % 16 == 0)
				outp+= "\n";
			}
			return outp;
		}
		public bool ContainsTag(string inp)
		{
			for (int i = 0; i < tags.Count(); i++)
			{
				if (inp.Contains(tags.ElementAt(i)))
				return true;
			}
		return false;
		}
		public string replaceTag(string inp)
		{
			string outp = "";
			for (int i = 0; i < tags.Count(); i++)
			{
				if (inp.Contains(tags.ElementAt(i)) && !inp.Contains(":"))

					outp= JavaStyleSubstring(inp,0, inp.IndexOf(tags.ElementAt(i))) + (programStart + (offset.ElementAt(i) + 2)) +inp.Substring(inp.IndexOf(tags.ElementAt(i)) + tags.ElementAt(i).Count());
			}

			return outp;
		}
		public string parseLine(string line)
		{

			if (!line.Contains("DB") && !line.Contains("\""))
				line = line.ToUpper();
			line = stripComments(line);
			bool[] outpput = null;
			string[] outp = line.Split(' ');
			if (outp[0] == "DB".ToLower()){
			string toAdd = "";
			for (int i = 1; i < outp.Count(); i++)
						if (i ==outp.Count() - 1)
							toAdd +=outp[i];
						else
				toAdd +=outp[i]+" ";
			if (runAgain)

				db.Add(handleDB(toAdd));

			if (!runAgain)
			{
				outpput = db.ElementAt(dbcount);
				pc += dbpcincrement / 2;
				dbcount++;

			}
		}
				else
		{
			if (outp.Count() > 1){
				if (!outp[0].StartsWith("J"))
							for (int i = 0; i < 2; i++)
				{
					if (ContainsTag(outp[1]))
									outp[1]= replaceTag(outp[1]);
				}
			}
			if (outp.Count() >= 1){
				if (outp[0].IndexOf(":") != -1){
					if (runAgain)
						jump(outp[0]);
				}
						else if (outp[0].IndexOf("STOP") != -1){
					outpput = Emulator.Util.stringTobool("0000010101010101");
				}
						else if (outp[0].StartsWith("J")){
					if (!runAgain)
						outpput = assembleJumps(outp[0], outp[1]);
					else
						countJumps();
				}
						else if (outp.Count() < 2){
					Console.WriteLine(outp[0]);
					outpput = emulatedInstruction(outp[0], "none");
				}
						else if (outp[1].Split(',').Count() < 2){
					Console.WriteLine(outp[0] + " " +outp[1]);
					if (!isEmulatedInstruction(outp[0]))
						outpput = assembleSO(outp[0], outp[1]);
					else
						outpput = emulatedInstruction(outp[0], outp[1]);
				}

						else
				{

					Console.WriteLine(outp[0] + " " +outp[1]);
					outpput = assemble(outp[0], outp[1]);

				}
			}
		}

		if (outpput != null)
			return Emulator.Util.boolTostring(outpput);
		else
			return "";
	
			}
			public bool[] handleDB(string inp)
		{
			List<bool> outpput = new List<bool>();
			if (inp.Contains("\"")){
			string workWith = JavaStyleSubstring(inp,1, inp.Count() - 1);
			bool[] toAdd = null;
			int newlineoffset = 0;
			for (int i = 0; i < workWith.Count(); i++)
			{
				if (i != workWith.Count() - 1 && workWith[i] == '\\' && workWith[i + 1] == 'n')
				{
					toAdd = assemble("MOV.B", "#" + 10 + ",&" + (programStart + 2 + offset.ElementAt(lastOffsetIndex) + (i - newlineoffset)));
					i++;
					newlineoffset++;
				}
				else
					toAdd = assemble("MOV.B", "#" + (int)workWith[i] + ",&" + (programStart + 2 + offset.ElementAt(lastOffsetIndex) + (i - newlineoffset)));
				for (int j = 0; j < toAdd.Count(); j++)
					outpput.Add(toAdd[j]);
			}
			toAdd = assemble("MOV.B", "#" + 0 + ",&" + (programStart + 2 + offset.ElementAt(lastOffsetIndex) + workWith.Count()));
			for (int j = 0; j < toAdd.Count(); j++)
				outpput.Add(toAdd[j]);
		}
				else
		{
			string workWith = inp;

			bool[] toAdd = null;
			toAdd = assemble("MOV.M", "#" + workWith + ",&" + (programStart + 2 + offset.ElementAt(lastOffsetIndex)));
			Console.WriteLine("REEEE"+Emulator.Util.boolTostring(toAdd));
			for (int j = 0; j < toAdd.Count(); j++)
				outpput.Add(toAdd[j]);
		}


		bool [] toReturn = new bool[outpput.Count()];
		for (int j = 0; j < toReturn.Count(); j++)
			toReturn[j] = outpput.ElementAt(j);

		dbpcincrement += toReturn.Count() / 8;

		return toReturn;
			}
			public bool[] emulatedInstruction(string inps, string dst)
		{
			string inp = inps.Split(new string[] { "\\." }, StringSplitOptions.None)[0];
			string byteCount = "M";
			if (inps.Split(new string[] { "\\." }, StringSplitOptions.None).Count() > 1)
			{
				byteCount = inps.Split(new string[] { "\\." }, StringSplitOptions.None)[1];
			}
			switch (inp){
				case "ADC":
					return assemble("ADDC." + byteCount, "#0," + dst);
				case "BR":
					return assemble("MOV." + byteCount, dst + ",R2");
				case "CLR":
					return assemble("MOV." + byteCount, "#0," + dst);
				case "CLRC":
					return assemble("BIC." + byteCount, "#1,R1");
				case "CLRN":
					return assemble("BIC." + byteCount, "#4,R1");
				case "CLRZ":
					return assemble("BIC." + byteCount, "#2,R1");
				case "DADC":
					return assemble("DADD." + byteCount, "#0," + dst);
				case "DEC":
					return assemble("SUB." + byteCount, "#1," + dst);
				case "DECD":
					return assemble("SUB." + byteCount, "#2," + dst);
				case "DINT":
					return assemble("BIC." + byteCount, "#8,R1");
				case "EINT":
					return assemble("BIS." + byteCount, "#8,R1");
				case "INC":
					return assemble("ADD." + byteCount, "#1," + dst);
				case "INCD":
					return assemble("ADD." + byteCount, "#2," + dst);
				case "INV":
					return assemble("XOR." + byteCount, "#-1," + dst);
				case "NOP":
					return assemble("MOV." + byteCount, "#0,R3");
				case "POP":
					return assemble("MOV." + byteCount, "@R0+," + dst);
				case "RET":
					return assemble("MOV." + byteCount, "@R0+,R2");
				case "RLA":
					return assemble("ADD." + byteCount, dst + "," + dst);
				case "RLC":
					return assemble("ADDC." + byteCount, dst + "," + dst);
				case "SBC":
					return assemble("SUBC." + byteCount, "#0," + dst);
				case "SETC":
					return assemble("BIS." + byteCount, "#1,R1");
				case "SETN":
					return assemble("BIS." + byteCount, "#4,R1");
				case "SETZ":
					return assemble("BIS." + byteCount, "#2,R1");
				case "TST":
					return assemble("CMP." + byteCount, "#0," + dst);
		}
		return null;
			}
			public void jump(string tag)
		{
			string marker = JavaStyleSubstring(tag, 0, tag.Count() - 1);
			Console.WriteLine(marker + " at line " + pc);
			tags.Add(marker);
			lastOffsetIndex = offset.Count();
			offset.Add(pc - 2);
		}
		public void countJumps()
		{
			pc += 2;
		}
		public int getTagOffset(string tag)
		{
			int index = -1;
			for (int i = 0; i < tags.Count(); i++)
			{
				if (tags.ElementAt(i).ToUpper() == tag.ToUpper())
				{
					index = i;
				}
			}
			return offset.ElementAt(index);
		}
		public bool isEmulatedInstruction(string inp)
		{

				inp=inp.Split(new string[] { "\\." }, StringSplitOptions.None)[0];
			if (inp.ToUpper()=="ADC"){
			return true;
		}
				else if (inp.ToUpper()=="BR"){
			return true;
		}
				else if (inp.ToUpper()=="CLR"){
			return true;
		}
				else if (inp.ToUpper()=="CLRC"){
			return true;
		}
				else if (inp.ToUpper()=="CLRN"){
			return true;
		}
				else if (inp.ToUpper()=="CLRZ"){
			return true;
		}
				else if (inp.ToUpper()=="DADC"){
			return true;
		}
				else if (inp.ToUpper()=="DEC"){
			return true;
		}
				else if (inp.ToUpper()=="DECD"){
			return true;
		}
				else if (inp.ToUpper()=="DINT"){
			return true;
		}
				else if (inp.ToUpper()=="EINT"){
			return true;
		}
				else if (inp.ToUpper()=="INC"){
			return true;
		}
				else if (inp.ToUpper()=="INCD"){
			return true;
		}
				else if (inp.ToUpper()=="INV"){
			return true;
		}
				else if (inp.ToUpper()=="NOP"){
			return true;
		}
				else if (inp.ToUpper()=="POP"){
			return true;
		}
				else if (inp.ToUpper()=="RET"){
			return true;
		}
				else if (inp.ToUpper()=="RLA"){
			return true;
		}
				else if (inp.ToUpper()=="RLC"){
			return true;
		}
				else if (inp.ToUpper()=="SBC"){
			return true;
		}
				else if (inp.ToUpper()=="SETC"){
			return true;
		}
				else if (inp.ToUpper()=="SETN"){
			return true;
		}
				else if (inp.ToUpper()=="SETZ"){
			return true;
		}
				else if (inp.ToUpper()=="TST"){
			return true;
		}
				else
			return false;

			}
			public bool[] assembleJumps(string instruction, string dest)
		{
			dest = JavaStyleSubstring(dest, 1, dest.Count() - 1);
			Console.WriteLine(pc + " " + instruction + " " + dest);
			List<bool> outpput = new List<bool>();
			bool[] opcode = { false, false, true };
			bool[] condition = new bool[3];
			bool[] offset = new bool[10];

			switch (instruction)
			{
				case "JNZ":
				case "JNE":
					condition = Emulator.Util.stringTobool("000");
					break;
				case "JEQ":
				case "JZ":
					condition = Emulator.Util.stringTobool("001");
					break;
				case "JNC":
				case "JLO":
					condition = Emulator.Util.stringTobool("010");
					break;
				case "JC":
				case "JHS":
					condition = Emulator.Util.stringTobool("011");
					break;
				case "JN":
					condition = Emulator.Util.stringTobool("100");
					break;
				case "JGE":
					condition = Emulator.Util.stringTobool("101");
					break;
				case "JL":
					condition = Emulator.Util.stringTobool("110");
					break;
				case "JMP":
					condition = Emulator.Util.stringTobool("111");
					break;
			}
		offset = Emulator.Util.addPadding(Emulator.Util.decimalToSignedBinary(getTagOffset(dest) - pc, 10), 10);


			for (int i = 0; i < opcode.Count(); i++)
			{
				outpput.Add(opcode[i]);
			}
			for (int i = 0; i < condition.Count(); i++)
			{
				outpput.Add(condition[i]);
			}
			for (int i = 0; i < offset.Count(); i++)
			{
				outpput.Add(offset[i]);
			}
			bool[] toPrint = new bool[outpput.Count()];
			for (int i = 0; i < toPrint.Count(); i++)
			{
				toPrint[i] = outpput.ElementAt(i);
			}

			pc += toPrint.Count() / 8;
			Console.WriteLine(Emulator.Util.boolTostring(toPrint));
			return toPrint;
		}
		public bool[] assemble(string instruction, string operand)
		{
			string pInstruction = null;
			string pByte = null;
			string src = null;
			string dst = null;

			bool[] instruct = new bool[4];
			bool[] source = new bool[4];
			bool[] asBAd = new bool[4];
			bool[] dest = new bool[4];
			List<bool> bytes = new List<bool>();


			List<bool> outpput = new List<bool>();
			Console.WriteLine(instruction.Count());
			if (instruction.IndexOf(".") != -1)
			{

				pInstruction = JavaStyleSubstring(instruction, 0, instruction.IndexOf("."));

				pByte = JavaStyleSubstring(instruction,instruction.IndexOf(".") + 1, instruction.Count());
				Console.WriteLine(pInstruction + " " + pByte);
			}
			else
			{
				pInstruction = instruction;
			}
			if (operand.IndexOf(",") != -1)
			{
				src = JavaStyleSubstring(operand, 0, operand.IndexOf(","));
				dst = JavaStyleSubstring(operand, operand.IndexOf(",") + 1, operand.Count());
				Console.WriteLine(pc + " " + src + " " + dst);
			}
			else
			{
				src = operand;
			}

			switch (pInstruction)
			{ //parse instruction
				case "MOV":
					instruct = Emulator.Util.stringTobool("0100");
					break;
				case "ADD":
					instruct = Emulator.Util.stringTobool("0101");
					break;
				case "ADDC":
					instruct = Emulator.Util.stringTobool("0110");
					break;
				case "BIS":
					instruct = Emulator.Util.stringTobool("1101");
					break;
				case "AND":
					instruct = Emulator.Util.stringTobool("1111");
					break;
				case "SUB":
					instruct = Emulator.Util.stringTobool("1000");
					break;
				case "SUBC":
					instruct = Emulator.Util.stringTobool("0111");
					break;
				case "CMP":
					instruct = Emulator.Util.stringTobool("1001");
					break;
				case "DADD":
					instruct = Emulator.Util.stringTobool("1010");
					break;
				case "BIT":
					instruct = Emulator.Util.stringTobool("1011");
					break;
				case "BIC":
					instruct = Emulator.Util.stringTobool("1100");
					break;
				case "XOR":
					instruct = Emulator.Util.stringTobool("1110");
					break;

			}

			int byteSize = 1;
			bool[] toAdd = null;
			if (pByte != null)
			{
				switch (pByte)
				{ //parse byteCount
					case "B": //handles case for byte
						asBAd[1] = true;
						byteSize = 1;
						break;
					case "W": //handles case for word
						asBAd[1] = false;
						toAdd = Emulator.Util.addPadding(Emulator.Util.stringTobool("10"), 16);
						byteSize = 2;

						if (!(src.IndexOf("R") != -1 && dst.IndexOf("R") != -1 && (src.IndexOf("(") == -1 && src.IndexOf("@") == -1 && dst.IndexOf("(") == -1 && dst.IndexOf("@") == -1)))
							for (int i = 0; i < toAdd.Count(); i++)
							{
								bytes.Add(toAdd[i]);
							}
						break;
					case "D": //handles case for dword
						asBAd[1] = false;
						toAdd = Emulator.Util.addPadding(Emulator.Util.stringTobool("100"), 16);
						byteSize = 4;
						if (!(src.IndexOf("R") != -1 && dst.IndexOf("R") != -1 && (src.IndexOf("(") == -1 && src.IndexOf("@") == -1 && dst.IndexOf("(") == -1 && dst.IndexOf("@") == -1)))
							for (int i = 0; i < toAdd.Count(); i++)
							{
								bytes.Add(toAdd[i]);
							}
						break;
					case "Q": //handles case for qword
						asBAd[1] = false;
						toAdd = Emulator.Util.addPadding(Emulator.Util.stringTobool("1000"), 16);
						byteSize = 8;
						if (!(src.IndexOf("R") != -1 && dst.IndexOf("R") != -1 && (src.IndexOf("(") == -1 && src.IndexOf("@") == -1 && dst.IndexOf("(") == -1 && dst.IndexOf("@") == -1)))
							for (int i = 0; i < toAdd.Count(); i++)
							{
								bytes.Add(toAdd[i]);
							}
						break;
					case "M": //handles case for max (max bits)
						asBAd[1] = false;
						toAdd = Emulator.Util.addPadding(Emulator.Util.stringTobool(Emulator.Util.decimalToBinary(bits / 8)), 16);
						byteSize = bits / 8;
						if (!(src.IndexOf("R") != -1 && dst.IndexOf("R") != -1 && (src.IndexOf("(") == -1 && src.IndexOf("@") == -1 && dst.IndexOf("(") == -1 && dst.IndexOf("@") == -1)))
							for (int i = 0; i < toAdd.Count(); i++)
							{
								bytes.Add(toAdd[i]);
							}
						break;
				}
			}
			else
			{
				asBAd[1] = false;
				toAdd = Emulator.Util.addPadding(Emulator.Util.stringTobool(Emulator.Util.decimalToBinary(bits / 8)), 16);
				byteSize = bits / 8;
				if (!(src.IndexOf("R") != -1 && dst.IndexOf("R") != -1 && (src.IndexOf("(") == -1 && src.IndexOf("@") == -1 && dst.IndexOf("(") == -1 && dst.IndexOf("@") == -1)))
					for (int i = 0; i < toAdd.Count(); i++)
					{
						bytes.Add(toAdd[i]);
					}
			}
			//parse source
			if (src.IndexOf("(") == -1 && src.IndexOf("@") == -1 && src.IndexOf("R") != -1)
			{ //register direct
			  source = Emulator.Util.addPadding(Emulator.Util.stringTobool(Emulator.Util.decimalToBinary(JavaStyleSubstring(src, 1, src.Count()))), 4);
				Console.WriteLine("@@@@@"+Emulator.Util.decimalToBinary(JavaStyleSubstring(src, 1, src.Count())));
			}
			else if (src.IndexOf("(") != -1)
			{
				asBAd[3] = true;
				string register = JavaStyleSubstring(src,src.IndexOf("(") + 1, src.IndexOf(")"));
				bool[] value = null;
				string strValue = null;
				if (src.IndexOf("X") != -1)
					value = Emulator.Util.addPadding(Emulator.Util.stringTobool(Emulator.Util.hexToBinary(JavaStyleSubstring(src, src.IndexOf("X") + 1, src.IndexOf("(")))), 16);
				else
				{
					value = Emulator.Util.addPadding(Emulator.Util.stringTobool(Emulator.Util.decimalToBinary(JavaStyleSubstring(src,0, src.IndexOf("(")))), 16);
				}
				for (int i = 0; i < value.Count(); i++)
				{
					bytes.Add(value[i]);
				}
				source = Emulator.Util.addPadding(Emulator.Util.stringTobool(Emulator.Util.decimalToBinary(JavaStyleSubstring(register,1, register.Count()))), 4);
			}
			else if (src.IndexOf("@") != -1 && src.IndexOf("+") == -1)
			{
				asBAd[2] = true;
				source = Emulator.Util.addPadding(Emulator.Util.stringTobool(Emulator.Util.decimalToBinary(JavaStyleSubstring(src,2, src.Count()))), 4);
			}
			else if (src.IndexOf("@") != -1 && src.IndexOf("+") != -1)
			{
				asBAd[2] = true;
				asBAd[3] = true;
				source = Emulator.Util.addPadding(Emulator.Util.stringTobool(Emulator.Util.decimalToBinary(JavaStyleSubstring(src,2, src.Count() - 1))), 4);
			}
			else if (src.IndexOf("(") == -1 && src.IndexOf("@") == -1 && src.IndexOf("R") == -1 && src.IndexOf("&") == -1 && src.IndexOf("#") == -1)
			{
				asBAd[3] = true;
				bool[] value = null;
				string strValue = null;
				if (src.IndexOf("X") != -1)
					value = Emulator.Util.trimbool(Emulator.Util.addPadding(Emulator.Util.stringTobool(Emulator.Util.hexToBinary(JavaStyleSubstring(src,src.IndexOf("X") + 1, src.Count()))), 16), 16);
				else
				{
					value = Emulator.Util.addPadding(Emulator.Util.stringTobool(Emulator.Util.decimalToBinary(JavaStyleSubstring(src,0, src.Count()))), 16);
				}
				for (int i = 0; i < value.Count(); i++)
				{
					bytes.Add(value[i]);
				}
				source = Emulator.Util.addPadding(Emulator.Util.stringTobool("10"), 4);
			}
			else if (src.IndexOf("(") == -1 && src.IndexOf("@") == -1 && src.IndexOf("R") == -1 && src.IndexOf("&") != -1)
			{
				asBAd[3] = true;
				bool[] value = null;
				string strValue = null;
				if (src.IndexOf("X") != -1)
				{

					value = Emulator.Util.trimbool(Emulator.Util.addPadding(Emulator.Util.stringTobool(Emulator.Util.hexToBinary(JavaStyleSubstring(src,src.IndexOf("X") + 1, src.Count()))), 16), 16);
				}
				else
				{
					value = Emulator.Util.trimbool(Emulator.Util.addPadding(Emulator.Util.stringTobool(Emulator.Util.decimalToBinary(JavaStyleSubstring(src,1, src.Count()))), 16), 16);
				}
				for (int i = 0; i < value.Count(); i++)
				{
					bytes.Add(value[i]);
				}
				source = Emulator.Util.addPadding(Emulator.Util.stringTobool("11"), 4);
			}
			else if (src.IndexOf("#") != -1)
			{
				asBAd[2] = true;
				asBAd[3] = true;
				bool[] value = null;
				source = Emulator.Util.addPadding(Emulator.Util.stringTobool("10"), 4);
				if (src.IndexOf("X") != -1)
					value = Emulator.Util.trimbool(Emulator.Util.addPadding(Emulator.Util.stringTobool(Emulator.Util.hexToBinary(JavaStyleSubstring(src,src.IndexOf("X") + 1, src.Count()))), byteSize * 8), byteSize * 8);
				else if (src.IndexOf("H") != -1)
					value = Emulator.Util.trimbool(Emulator.Util.addPadding(Emulator.Util.stringTobool(Emulator.Util.hexToBinary(JavaStyleSubstring(src,1, src.IndexOf("H")))), byteSize * 8), byteSize * 8);
				else if (src.IndexOf("B") != -1)
					value = Emulator.Util.trimbool(Emulator.Util.addPadding(Emulator.Util.stringTobool(src.Substring(1, src.IndexOf("B"))), byteSize * 8), byteSize * 8);
				else
				{
					value = Emulator.Util.trimbool(Emulator.Util.addPadding(Emulator.Util.stringTobool(Emulator.Util.decimalToBinary(JavaStyleSubstring(src,1, src.Count()))), byteSize * 8), byteSize * 8);
				}
				for (int i = 0; i < value.Count(); i++)
				{
					bytes.Add(value[i]);
				}
			}

			//parse dest
			if (dst.IndexOf("(") == -1 && dst.IndexOf("@") == -1 && dst.IndexOf("R") != -1)
			{ //register direct
				dest = Emulator.Util.addPadding(Emulator.Util.stringTobool(Emulator.Util.decimalToBinary(JavaStyleSubstring(dst, 1, dst.Count()))), 4);
			}
			else if (dst.IndexOf("(") != -1)
			{
				asBAd[0] = true;
				string register = JavaStyleSubstring(dst, dst.IndexOf("(") + 1, dst.IndexOf(")"));
				bool[] value = null;
				string strValue = null;
				if (dst.IndexOf("X") != -1)
					value = Emulator.Util.addPadding(Emulator.Util.stringTobool(Emulator.Util.hexToBinary(JavaStyleSubstring(dst, dst.IndexOf("X") + 1, dst.IndexOf("(")))), 16);
				else
				{
					value = Emulator.Util.addPadding(Emulator.Util.stringTobool(Emulator.Util.decimalToBinary(JavaStyleSubstring(dst, 0, dst.IndexOf("(")))), 16);
				}
				for (int i = 0; i < value.Count(); i++)
				{
					bytes.Add(value[i]);
				}
				dest = Emulator.Util.addPadding(Emulator.Util.stringTobool(Emulator.Util.decimalToBinary(JavaStyleSubstring(register, 1, register.Count()))), 4);
			}
			else if (dst.IndexOf("(") == -1 && dst.IndexOf("@") == -1 && dst.IndexOf("R") == -1 && dst.IndexOf("&") == -1 && dst.IndexOf("#") != -1)
			{
				asBAd[0] = true;
				bool[] value = null;
				string strValue = null;
				if (dst.IndexOf("X") != -1)
					value = Emulator.Util.addPadding(Emulator.Util.stringTobool(Emulator.Util.hexToBinary(JavaStyleSubstring(dst, dst.IndexOf("X") + 1, dst.Count()))), 16);
				else
				{
					value = Emulator.Util.addPadding(Emulator.Util.stringTobool(Emulator.Util.decimalToBinary(JavaStyleSubstring(dst, 1, dst.Count()))), 16);
				}
				for (int i = 0; i < value.Count(); i++)
				{
					bytes.Add(value[i]);
				}
				dest = Emulator.Util.addPadding(Emulator.Util.stringTobool("10"), 4);
			}
			else if (dst.IndexOf("(") == -1 && dst.IndexOf("@") == -1 && dst.IndexOf("R") == -1 && dst.IndexOf("&") != -1)
			{
				asBAd[0] = true;
				bool[] value = null;
				string strValue = null;
				if (dst.IndexOf("X") != -1)
				{
					value = Emulator.Util.trimbool(Emulator.Util.addPadding(Emulator.Util.stringTobool(Emulator.Util.hexToBinary(JavaStyleSubstring(dst, dst.IndexOf("X") + 1, dst.Count()))), 16), 16);
				}
				else
				{
					value = Emulator.Util.trimbool(Emulator.Util.addPadding(Emulator.Util.stringTobool(Emulator.Util.decimalToBinary(JavaStyleSubstring(dst, 1, dst.Count()))), 16), 16);
				}
				for (int i = 0; i < value.Count(); i++)
				{
					bytes.Add(value[i]);
				}
				dest = Emulator.Util.addPadding(Emulator.Util.stringTobool("11"), 4);
			}
			for (int i = 0; i < instruct.Count(); i++)
			{
				outpput.Add(instruct[i]);
			}
			for (int i = 0; i < source.Count(); i++)
			{
				outpput.Add(source[i]);
			}
			for (int i = 0; i < asBAd.Count(); i++)
			{
				outpput.Add(asBAd[i]);
			}
			for (int i = 0; i < dest.Count(); i++)
			{
				outpput.Add(dest[i]);
			}
			for (int i = 0; i < bytes.Count(); i++)
			{
				outpput.Add(bytes.ElementAt(i));
			}

			bool[] toPrint = new bool[outpput.Count()];
			for (int i = 0; i < toPrint.Count(); i++)
			{
				toPrint[i] = outpput.ElementAt(i);
			}
			previouspc = pc;
			pc += toPrint.Count() / 8;
			return toPrint;

		}
		public bool[] assembleSO(string instruction, string operand)
		{
			string pInstruction = null;
			string pByte = null;
			string src = null;

			bool[] start = Emulator.Util.stringTobool("000100");
			bool[] instruct = new bool[3];
			bool[] source = new bool[4];
			bool[] asBAd = new bool[3];
			List<bool> bytes = new List<bool>();


			List<bool> outpput = new List<bool>();
			Console.WriteLine(instruction.Count());
			if (instruction.IndexOf(".") != -1)
			{
				pInstruction = JavaStyleSubstring(instruction,0, instruction.IndexOf("."));
				pByte = JavaStyleSubstring(instruction,instruction.IndexOf(".") + 1, instruction.Count());
				Console.WriteLine(pInstruction + " " + pByte);
			}
			else
			{
				pInstruction = instruction;
			}
			if (operand.IndexOf(",") != -1)
			{
				src = JavaStyleSubstring(operand,0, operand.IndexOf(","));
				Console.WriteLine(pc + " " + src);
			}
			else
			{
				src = operand;
			}

			switch (pInstruction)
			{ //parse instruction
				case "RRC":
					instruct = Emulator.Util.stringTobool("000");
					break;
				case "SWPB":
					instruct = Emulator.Util.stringTobool("001");
					break;
				case "RRA":
					instruct = Emulator.Util.stringTobool("010");
					break;
				case "SXT":
					instruct = Emulator.Util.stringTobool("011");
					break;
				case "PUSH":
					instruct = Emulator.Util.stringTobool("100");
					break;
				case "CALL":
					instruct = Emulator.Util.stringTobool("101");
					break;
				case "RETI":
					instruct = Emulator.Util.stringTobool("110");
					break;


			}

			int byteSize = 1;
			bool[] toAdd = null;
			if (pByte != null)
			{
				switch (pByte)
				{ //parse byteCount
					case "B": //handles case for byte
						asBAd[0] = true;
						byteSize = 1;
						break;
					case "W": //handles case for word
						asBAd[0] = false;
						toAdd = Emulator.Util.addPadding(Emulator.Util.stringTobool("10"), 16);
						byteSize = 2;

						if (!(src.IndexOf("R") != -1 && (src.IndexOf("(") == -1 && src.IndexOf("@") == -1)))
							for (int i = 0; i < toAdd.Count(); i++)
							{
								bytes.Add(toAdd[i]);
							}
						break;
					case "D": //handles case for dword
						asBAd[0] = false;
						toAdd = Emulator.Util.addPadding(Emulator.Util.stringTobool("100"), 16);
						byteSize = 4;
						if (!(src.IndexOf("R") != -1 && (src.IndexOf("(") == -1 && src.IndexOf("@") == -1)))
							for (int i = 0; i < toAdd.Count(); i++)
							{
								bytes.Add(toAdd[i]);
							}
						break;
					case "Q": //handles case for qword
						asBAd[0] = false;
						toAdd = Emulator.Util.addPadding(Emulator.Util.stringTobool("1000"), 16);
						byteSize = 8;
						if (!(src.IndexOf("R") != -1 && (src.IndexOf("(") == -1 && src.IndexOf("@") == -1)))
							for (int i = 0; i < toAdd.Count(); i++)
							{
								bytes.Add(toAdd[i]);
							}
						break;
					case "M": //handles case for max (max bits)
						asBAd[0] = false;
						toAdd = Emulator.Util.addPadding(Emulator.Util.stringTobool(Emulator.Util.decimalToBinary(bits / 8)), 16);
						byteSize = bits / 8;
						if (!(src.IndexOf("R") != -1 && (src.IndexOf("(") == -1 && src.IndexOf("@") == -1)))
							for (int i = 0; i < toAdd.Count(); i++)
							{
								bytes.Add(toAdd[i]);
							}
						break;
				}
			}
			else
			{
				asBAd[0] = false;
				toAdd = Emulator.Util.addPadding(Emulator.Util.stringTobool(Emulator.Util.decimalToBinary(bits / 8)), 16);
				byteSize = bits / 8;
				if (!(src.IndexOf("R") != -1 && (src.IndexOf("(") == -1 && src.IndexOf("@") == -1)))
					for (int i = 0; i < toAdd.Count(); i++)
					{
						bytes.Add(toAdd[i]);
					}
			}
			//parse source
			if (src.IndexOf("(") == -1 && src.IndexOf("@") == -1 && src.IndexOf("R") != -1)
			{ //register direct
				source = Emulator.Util.addPadding(Emulator.Util.stringTobool(Emulator.Util.decimalToBinary(JavaStyleSubstring(src,1, src.Count()))), 4);
			}
			else if (src.IndexOf("(") != -1)
			{
				asBAd[2] = true;
				string register = JavaStyleSubstring(src,src.IndexOf("(") + 1, src.IndexOf(")"));
				bool[] value = null;
				string strValue = null;
				if (src.IndexOf("X") != -1)
					value = Emulator.Util.addPadding(Emulator.Util.stringTobool(Emulator.Util.hexToBinary(JavaStyleSubstring(src,src.IndexOf("X") + 1, src.IndexOf("(")))), 16);
				else
				{
					value = Emulator.Util.addPadding(Emulator.Util.stringTobool(Emulator.Util.decimalToBinary(JavaStyleSubstring(src,0, src.IndexOf("(")))), 16);
				}
				for (int i = 0; i < value.Count(); i++)
				{
					bytes.Add(value[i]);
				}
				source = Emulator.Util.addPadding(Emulator.Util.stringTobool(Emulator.Util.decimalToBinary(JavaStyleSubstring(register,1, register.Count()))), 4);
			}
			else if (src.IndexOf("@") != -1 && src.IndexOf("+") == -1)
			{
				asBAd[1] = true;
				source = Emulator.Util.addPadding(Emulator.Util.stringTobool(Emulator.Util.decimalToBinary(JavaStyleSubstring(src,2, src.Count()))), 4);
			}
			else if (src.IndexOf("@") != -1 && src.IndexOf("+") != -1)
			{
				asBAd[1] = true;
				asBAd[2] = true;
				source = Emulator.Util.addPadding(Emulator.Util.stringTobool(Emulator.Util.decimalToBinary(JavaStyleSubstring(src,2, src.Count() - 1))), 4);
			}
			else if (src.IndexOf("(") == -1 && src.IndexOf("@") == -1 && src.IndexOf("R") == -1 && src.IndexOf("&") == -1 && src.IndexOf("#") == -1)
			{
				asBAd[2] = true;
				bool[] value = null;
				string strValue = null;
				if (src.IndexOf("X") != -1)
					value = Emulator.Util.trimbool(Emulator.Util.addPadding(Emulator.Util.stringTobool(Emulator.Util.hexToBinary(JavaStyleSubstring(src,src.IndexOf("X") + 1, src.Count()))), 16), 16);
				else
				{
					value = Emulator.Util.addPadding(Emulator.Util.stringTobool(Emulator.Util.decimalToBinary(JavaStyleSubstring(src,0, src.Count()))), 16);
				}
				for (int i = 0; i < value.Count(); i++)
				{
					bytes.Add(value[i]);
				}
				source = Emulator.Util.addPadding(Emulator.Util.stringTobool("10"), 4);
			}
			else if (src.IndexOf("(") == -1 && src.IndexOf("@") == -1 && src.IndexOf("R") == -1 && src.IndexOf("&") != -1)
			{
				asBAd[2] = true;
				bool[] value = null;
				string strValue = null;
				if (src.IndexOf("X") != -1)
				{

					value = Emulator.Util.trimbool(Emulator.Util.addPadding(Emulator.Util.stringTobool(Emulator.Util.hexToBinary(JavaStyleSubstring(src,src.IndexOf("X") + 1, src.Count()))), 16), 16);
				}
				else
				{
					value = Emulator.Util.trimbool(Emulator.Util.addPadding(Emulator.Util.stringTobool(Emulator.Util.decimalToBinary(JavaStyleSubstring(src,1, src.Count()))), 16), 16);
				}
				for (int i = 0; i < value.Count(); i++)
				{
					bytes.Add(value[i]);
				}
				source = Emulator.Util.addPadding(Emulator.Util.stringTobool("11"), 4);
			}
			else if (src.IndexOf("#") != -1)
			{
				asBAd[1] = true;
				asBAd[2] = true;
				bool[] value = null;
				source = Emulator.Util.addPadding(Emulator.Util.stringTobool("10"), 4);
				if (src.IndexOf("X") != -1)
					value = Emulator.Util.trimbool(Emulator.Util.addPadding(Emulator.Util.stringTobool(Emulator.Util.hexToBinary(JavaStyleSubstring(src,src.IndexOf("X") + 1, src.Count()))), byteSize * 8), byteSize * 8);
				else if (src.IndexOf("H") != -1)
					value = Emulator.Util.trimbool(Emulator.Util.addPadding(Emulator.Util.stringTobool(Emulator.Util.hexToBinary(JavaStyleSubstring(src,1, src.IndexOf("H")))), byteSize * 8), byteSize * 8);
				else if (src.IndexOf("B") != -1)
					value = Emulator.Util.trimbool(Emulator.Util.addPadding(Emulator.Util.stringTobool(JavaStyleSubstring(src,1, src.IndexOf("B"))), byteSize * 8), byteSize * 8);
				else
				{
					value = Emulator.Util.trimbool(Emulator.Util.addPadding(Emulator.Util.stringTobool(Emulator.Util.decimalToBinary(JavaStyleSubstring(src,1, src.Count()))), byteSize * 8), byteSize * 8);
				}
				for (int i = 0; i < value.Count(); i++)
				{
					bytes.Add(value[i]);
				}
			}
			for (int i = 0; i < start.Count(); i++)
			{
				outpput.Add(start[i]);
			}
			for (int i = 0; i < instruct.Count(); i++)
			{
				outpput.Add(instruct[i]);
			}
			for (int i = 0; i < asBAd.Count(); i++)
			{
				outpput.Add(asBAd[i]);
			}
			for (int i = 0; i < source.Count(); i++)
			{
				outpput.Add(source[i]);
			}
			for (int i = 0; i < bytes.Count(); i++)
			{
				outpput.Add(bytes.ElementAt(i));
			}

			bool[] toPrint = new bool[outpput.Count()];
			for (int i = 0; i < toPrint.Count(); i++)
			{
				toPrint[i] = outpput.ElementAt(i);
			}
			previouspc = pc;
			pc += toPrint.Count() / 8;
			return toPrint;

		}

			public string hexToBinary(string hex)
		{

			BigInteger outp = 0;

			try
			{
				//hexToBinary
				outp = BigInteger.Parse(hex, NumberStyles.AllowHexSpecifier);
				return BigIntegerExtensions.ToBinaryString(outp);

			}
			catch (FormatException)
			{
				Console.WriteLine("Unable to convert the string '{0}' to a BigInteger value.",
								  hex);
				return null;
			}
		}
		static string JavaStyleSubstring(string s, int beginIndex, int endIndex)
		{
			// simulates Java substring function
			int len = endIndex - beginIndex;
			return s.Substring(beginIndex, len);
		}
		public string intToBinary(string decimalNum)
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
				Console.WriteLine("Unable to convert the string '{0}' to a BigInteger value.",
								  decimalNum);
				return null;
			}
		}
	}
}
