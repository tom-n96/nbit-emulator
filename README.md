# nbit-emulator
Proof of concept emulation of a RISC cpu with adjustable n-bit arithmetic.<br>

This is the C# port of my previous project. Performance is not great on this platform, but optimization will come in the future.

ALU logic is based off the 74181.
CPU instruction set is based off MSP430 logic, with byte, word, doubleword, quadword, and max addressing.
<h2>CPU Operations</h2>
<h3>Operations</h3>
The CPU Instruction set is mostly identical to the TI MSP430 instruction set. Code intended for the MSP430 may assemble, but binary thats been assembled for the MSP430 most likely won't work. This is not intended to be an emulation of the MSP430 itself, so there is no guarantee that MSP430 programs will run. 
<br>
<br>

![msp430instructions](https://user-images.githubusercontent.com/9161414/160333626-b8487627-f5fc-4055-a0cf-2f5cb5775d26.png)
<h3>Addressing Modes</h3>

![msp430addressing](https://user-images.githubusercontent.com/9161414/160333770-1f94e6ed-76a8-4812-9cb1-3940a7e4c5cf.png)

<br>
<h2>GUI</h2>
GUI is a WIP and doesn't yet have much utility besides editing code and a secondary console. 
<br>
<h2>Known Issues/To-do</h2>
<ul>
  <li>Performance - GUI is single threaded and freezes when running. CPU also becomes unresponsive when high bit modes are selected. Something is optimized very poorly for C# and needs to be identified. </li>
  <li>Hex Rollover - the way C#'s big integer parsing works causes integers to roll over differently than the original project. May or may not be an issue, but consistency between the projects is desirable.</li>
  <li>Memory Addressing - Memory is not addressable in the n-bit space at the moment and is confined to about 4 gb. This means only the CPU instructions benefit from the n-bit configuration.</li>
  <li>GUI isn't great - need to finish up the GUI and make it useful.</li>
</ul> 
