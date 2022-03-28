using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Emulator
{
    public partial class MainWindow : Form
    {
        //ASM file vars
        private System.Collections.Generic.IEnumerable<String> activeAsmFile = null;
        private String activeAsmFilePath = null;
        private System.Collections.Generic.IEnumerable<String> activeBinFile = null;
        private String activeBinFilePath = null;
        private CpuHandler session;
        private Cpu cpuSession;
        //output console
        private List<String> consoleDisplay;
        public MainWindow()
        {
            InitializeComponent();
            //console init
            consoleDisplay = new List<String>();
           
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            logTime("nBit Emulator GUI loaded\r\n");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
        private void loadBinIntoEditor(String filepath)
        {

            activeBinFile = File.ReadLines(filepath);
            
            updateAsmOutput();
            log("Loaded vbin file located at " + activeBinFilePath + "\r\n");
        }

        private void loadvasmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult fileLoaded =loadAsm.ShowDialog();
            if (fileLoaded == DialogResult.OK)
            {
                activeAsmFilePath=loadAsm.FileName;
                activeAsmFile = File.ReadLines(activeAsmFilePath);
            }
            updateAsmOutput();
            log("Loaded vasm file located at " + activeAsmFilePath+"\r\n");
        }
        private void updateAsmOutput()
        {
            String toDisplay = "";
            if (activeAsmFile != null)
            {
                for (int i = 0; i < activeAsmFile.Count(); i++)
                {
                    toDisplay += activeAsmFile.ElementAt(i) + "\r\n";
                }
                asmOutputBox.Text = toDisplay;
            }
            
            if (activeBinFile != null)
            {
                String toDisplayBin = "";
                for (int i = 0; i < activeBinFile.Count(); i++)
                {
                    toDisplayBin += activeBinFile.ElementAt(i) + "";
                }
                binOutputBox.Text = toDisplayBin;
            }
        }
        public void log(String outp)
        {
            consoleDisplay.Add(outp);
            updateConsoleOutput();
        }
        public void logTime(String outp)
        {
            consoleDisplay.Add("("+System.DateTime.Now.ToString()+") "+outp);
            updateConsoleOutput();
        }
        public void logln(String outp)
        {
            consoleDisplay.Add(outp+"\r\n");
            updateConsoleOutput();
        }
        private void updateConsoleOutput()
        {
            String toDisplay = "";

            toDisplay += "\tConsole\r\n";
            toDisplay += "-----------------------\r\n";
            for (int i = consoleDisplay.Count-1; i >=0 ; i--)
            {
                toDisplay += consoleDisplay.ElementAt(i);
            }
            consoleDisplayOut.Text = toDisplay;
        }
        
        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void splitContainer2_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void assmbleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Assembler assem = new Assembler();
            
            if (activeAsmFile != null)
            {
                String outputDirectory = assem.run(activeAsmFile, activeAsmFilePath, Config.bits,this);
                logTime("Successfully assembled. Output to " + outputDirectory+"\r\n");
                loadBinIntoEditor(outputDirectory);
            }
            else
            {
                logTime("Assmebler failed to start due to no input.\r\n");
                MessagePopup noFileSelected = new MessagePopup("There is no file loaded into the nBit Emulator.");
                noFileSelected.Show();
            }
            

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void binDisplay_TextChanged(object sender, EventArgs e)
        {
         
        }

        private void emulatorToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {

            

            if (activeBinFile != null)
            {

                String programStr = "";
                //parse bits from string
                for(int i = 0; i < activeBinFile.Count(); i++) //put all elements into one string
                {
                    programStr += activeBinFile.ElementAt(i);
                }
                for (int i = programStr.Count() - 1; i >= 0; i--)//remove spaces
                {
                    if(programStr[i]==' ')
                    {
                        programStr.Remove(i, 1);
                    }
                }
                Console.WriteLine(programStr);


                bool[] program = Util.stringTobool(programStr);//convert bool string to binary array
                session = new CpuHandler(this);
                cpuSession = new Cpu(Config.bits,program,session);

                session.cpuTest(cpuSession);
            }
            else
            {
                logTime("Emulator failed to start due to no active binary.\r\n");
                MessagePopup noFileSelected = new MessagePopup("There is no binary loaded into the nBit Emulator.");
                noFileSelected.Show();
            }
        }

        private void asmOutputBox_TextChanged(object sender, EventArgs e)
        {
            string [] toSave;
            List <String> toSaveFile = new List<String>();
            toSave = asmOutputBox.Text.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            for(int i = 0; i < toSave.Count(); i++)
            {
                toSaveFile.Add(toSave[i]);
            }
               activeAsmFile = toSaveFile;
        }

        private void splitContainer3_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void registerViewer_TextChanged(object sender, EventArgs e)
        {

        }
        public void updateCPUViewValues()
        {
            //Console.WriteLine("AHHH!");
           // logTime("AHHHH2!");
            if (session != null ) {
                //List<string> hexDisplay = new List<string>();
             //   Console.WriteLine("AHHH!");
              //  logTime("AHHHH!");
                hexViewer.Text = session.printAllMemory();
            }
        }

        private void hexViewer_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
