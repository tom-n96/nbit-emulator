using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Emulator
{
    class CpuHandler
    {
		private bool isRunning=false;
		private Cpu cpu;
		private MainWindow window;
		public CpuHandler( MainWindow mw)
        {
			
			this.window = mw;
		}
		public void stop()
        {
			isRunning = false;
        }
		public string printAllMemory()
		{
			if (cpu != null)
				return cpu.printMemoryString(0, 50000);//be careful. may need to change to variable later when improving memory handling	
			return "No Memory Loaded";
        }
		public void cpuTest(Cpu c) //initializes this version of the CPU environment
		{
			this.cpu = c;
			isRunning = true;
			updateDisplay();
			cpu.printMemory(46500, 64);
			try
			{
				Thread.Sleep(1000);
			}
			catch (Exception e)
			{
				// TODO Auto-generated catch block

			}
			long start = DateTimeOffset.Now.ToUnixTimeMilliseconds();

			while (isRunning)
			{
				cpu.cycle();
				if (Config.printRegisters)
					cpu.printRegisterContents();
				if (Config.recordClockSpeed)
					Console.WriteLine(Util.getAvgClockSpeed() + " hz");
			}
			cpu.printRegisterContents();

			cpu.printMemory(0, 128);
			cpu.printMemory(46500, 64);
			long stop = DateTimeOffset.Now.ToUnixTimeMilliseconds();
			Console.WriteLine("Took " + (stop - start) + " ms (" + ((stop - start) / 1000.0) + ") seconds");
		}
		public void updateDisplay()
        {		
			if (window != null)
			{
				window.updateCPUViewValues();
            }
			
        }
	}
}
