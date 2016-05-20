using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace OpenOCDProgramingFrontend
{
    public partial class Form1 : Form
    {
        private char ProgramingDriveLetter = ' ';
        private string programfile;

        private AppData appData;
        Process processGDB;

        private FileSystemWatcher watcher;
        private void initWatchFileChanges(string path)
        {
            watcher = new FileSystemWatcher();
            watcher.Path = path;
            watcher.NotifyFilter = NotifyFilters.LastWrite;
            watcher.Filter = "*.*";
            watcher.Changed += new FileSystemEventHandler(OnFilesInPredefPathChanged);
            watcher.EnableRaisingEvents = true;
        }
  
        private void hideProgrammingInProgress(object sender, EventArgs e)
        {
            picDownloadInProgress.Visible = false;
        }

        private void showProgrammingInProgress(object sender, EventArgs e)
        {
            picDownloadInProgress.Visible = true;
        }


        private void ConvBinToHex(string pathAndFileName)
        {
            //arm-none-eabi-objcopy.exe" -I binary -O ihex Blink_the_led_NUCLEO_F3032RE.bin Blink_the_led_NUCLEO_F3032RE.hex

            string hexFileName = Path.GetFileNameWithoutExtension(pathAndFileName.Trim('\"'));
            string execArg = String.Format("{0} -I binary -O ihex {1} \"z:\\{2}.hex\"",appData.objcopyExtraArgs, pathAndFileName, hexFileName);
            
            // Prepare the process to run
            ProcessStartInfo start = new ProcessStartInfo();
            // Enter in the command line arguments, everything you would enter after the executable name itself
            start.Arguments = execArg;
            // Enter the executable to run, including the complete path
            start.FileName = appData.objcopyEXE;
            // Do you want to show a console window?
            start.WindowStyle = ProcessWindowStyle.Hidden;
            start.CreateNoWindow = true;
            int exitCode;

            // Run the external process & wait for it to finish
            using (Process proc = Process.Start(start))
            {
                proc.WaitForExit();

                // Retrieve the app's exit code
                exitCode = proc.ExitCode;
            }

        }

        private void startProgramming(string pathAndFileName)
        {
            //if format is .bin convert it to .hex
            string ext = Path.GetExtension(pathAndFileName.Trim('\"'));
            if(ext.ToLower().Equals(".bin"))
            {
                ConvBinToHex(pathAndFileName);
                return;
           }
           
            if (!System.IO.File.Exists(appData.gbdEXE))
            {    if (!System.IO.File.Exists(appData.gbdEXE))
                 {  MessageBox.Show(String.Format("Program not found\r\n{0}", appData.gbdEXE));
                    return;
                }
            }

            this.Invoke(new EventHandler(showProgrammingInProgress));
            int timeout = 100;
            using (processGDB = new Process())
            {
                processGDB.StartInfo.FileName = appData.gbdEXE;
                processGDB.StartInfo.Arguments = String.Format(@"""{0}""", pathAndFileName);
                processGDB.StartInfo.UseShellExecute = false;
                processGDB.StartInfo.CreateNoWindow = true;
                processGDB.StartInfo.RedirectStandardOutput = true;
                processGDB.StartInfo.RedirectStandardInput = true;
                processGDB.StartInfo.RedirectStandardError = true;

                StringBuilder error = new StringBuilder();

                using (AutoResetEvent outputWaitHandle = new AutoResetEvent(false))
                using (AutoResetEvent errorWaitHandle = new AutoResetEvent(false))
                {
                    processGDB.OutputDataReceived += (sender, ee) => {
                        if (ee.Data == null)
                        {
                            try
                            { outputWaitHandle.Set(); }
                            catch (Exception)
                            {
                                this.Invoke(new EventHandler(hideProgrammingInProgress));
                                programfile = ""; // Make sure that the same file could be programmed more than once 
                            }
                        }
                        else
                        {
                            RxString = ee.Data.ToString();
                            RxString += "\r\n";
                            this.Invoke(new EventHandler(DisplayText));
                        }
                    };
                   
                    processGDB.ErrorDataReceived += (sender, ee) =>
                    {
                        if (ee.Data == null)
                        {
                            try { errorWaitHandle.Set(); }
                            catch (Exception) { }
                        }
                        else
                        {//   error.AppendLine(ee.Data);
                            RxString = ee.Data.ToString();
                            RxString += "\r\n";
                            this.Invoke(new EventHandler(DisplayText));
                        }
                    };
                   
                    processGDB.Start();
                    
                    processGDB.BeginOutputReadLine();
                    processGDB.BeginErrorReadLine();

                    processGDB.StandardInput.WriteLine(appData.openOCDconnection);
                    int i = 0;
                    foreach (string strCmd in appData.cmdOpenOCD)
                    {
                        i++;
                        if ((i==2)&& chkEnableBoolLoader.Checked)
                        {
                            processGDB.StandardInput.WriteLine(" ");
                            processGDB.StandardInput.WriteLine(appData.cmdNrf51BootErase);
                        }
                        processGDB.StandardInput.WriteLine(" ");
                        processGDB.StandardInput.WriteLine(strCmd);
                    }

                    if (processGDB.WaitForExit(timeout) &&
                        outputWaitHandle.WaitOne(timeout) &&
                        errorWaitHandle.WaitOne(timeout))
                    {
                        // Process completed. Check process.ExitCode here.                       
                    }
                    else
                    {
                        // Timed out.                       
                    }
                }
            }

        }

        private void OnFilesInPredefPathChanged(object source, FileSystemEventArgs e)
        {
            //Update the programfile name only once (we might get more than one file change notification)            
            if (!e.Name.ToString().Equals(programfile))
            {
                programfile = e.Name.ToString();
                startProgramming(String.Format(@"""{0}\{1}""", appData.tempWorkingFolder, programfile));
            }
        }
    
        private string RxString;
        private void DisplayText(object sender, EventArgs e)
        {
            txtGDBOutput.AppendText(RxString);
        }

        private void ProcessGDB_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            RxString = e.Data.ToString();
            this.Invoke(new EventHandler(DisplayText));
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string localAppDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)+@"\OpenOCD mbed frontend";

            //Chk if temp folder exists if not create it
            if (!System.IO.Directory.Exists(localAppDataFolder))
                System.IO.Directory.CreateDirectory(localAppDataFolder);

            string configFile= localAppDataFolder + @"\user.json";            
            if (System.IO.File.Exists(configFile))
                appData = AppData.appDataFrom(System.IO.File.ReadAllText(configFile));
            else
            {   appData = new AppData();  // Get default values
                System.IO.File.WriteAllText(configFile, appData.json);
            }

            labConfigFile.Text = @"Config File: " + configFile;

            txtConfig.Text = appData.json;
            this.Size = appData.clientViewSmalSize;

            txtOpenOCDAddr.Text = appData.remoteAddress;

            cmbDrive.Items.AddRange(  appData.virtualDrives.ToArray());
            cmbDrive.Text = appData.virtualDrives[appData.selectedVirtualDrive].ToString();
            ProgramingDriveLetter = cmbDrive.Text[0];       // get the selected drive char

            txtCmdEraseBoot.Text = appData.cmdNrf51BootErase;

            Folder2Drv.MapDrive(ProgramingDriveLetter , appData.tempWorkingFolder);
            initWatchFileChanges(appData.tempWorkingFolder); // run OnFilesInPredefPathChanged if files is changed in this path

            chkEnableBoolLoader.Checked = appData.enableNrf51BootErase;
            labOutPut.Text = "DBG:" + appData.gbdEXE;
            
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Folder2Drv.UnmapDrive(ProgramingDriveLetter);
            if (processGDB != null)
            {
                try { processGDB.Kill(); }
                catch (Exception ) { };                
            }
        }

        private void cmbDrive_SelectedIndexChanged(object sender, EventArgs e)
        {
            Folder2Drv.UnmapDrive(ProgramingDriveLetter);
            ProgramingDriveLetter = cmbDrive.Text[0];
            Folder2Drv.MapDrive(ProgramingDriveLetter, appData.tempWorkingFolder);

            appData.selectedVirtualDrive = cmbDrive.SelectedIndex;
            txtConfig.Text = appData.json;
        }


        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            foreach (string file in files)
                startProgramming(file);
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            appData.enableNrf51BootErase = chkEnableBoolLoader.Checked;
            txtConfig.Text = appData.json;
        }

        private void IpAddrBox_TextChanged(object sender, EventArgs e)
        {
            appData.remoteAddress = txtOpenOCDAddr.Text;
            txtConfig.Text = appData.json;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (this.Height > appData.clientViewSmalSize.Height)
                this.Size = appData.clientViewSmalSize;
            else
                this.Size = appData.clientViewLargSize;
        }

        private void txtCmdEraseBoot_TextChanged(object sender, EventArgs e)
        {
            appData.cmdNrf51BootErase = txtCmdEraseBoot.Text;
            txtConfig.Text = appData.json;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string localAppDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\OpenOCD mbed frontend";
            string configFile = localAppDataFolder + @"\user.json";

            System.IO.File.Delete(configFile);
            System.IO.File.WriteAllText(configFile, txtCmdEraseBoot.Text);
        }

        private void btnDefConfig_Click(object sender, EventArgs e)
        {
            string localAppDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\OpenOCD mbed frontend\";
            string configFile = localAppDataFolder + @"user.json";
            System.IO.File.Delete(configFile);
            Application.Restart();
        }
    }

    [DataContract]
    public class AppData
    {
        [DataMember]
        public Size clientViewSmalSize { get; set; }

        [DataMember]
        public Size clientViewLargSize { get; set; }

        [DataMember]
        public string gbdEXE { get; set; }  //Location of the GDB (Gnu Debugger) program/app

        [DataMember]
        public string objcopyEXE { get; set; }  //Location of the objcopy (input/output format changer) program/app

        [DataMember]
        public string objcopyExtraArgs { get; set; }  //Addidional arguemnts for objcopy (input/output format changer) program/app


        [DataMember]
        public string tempWorkingFolder { get; set; }  //Folder that is to be mounted as a virtual drive

        [DataMember]
        public List<string> virtualDrives { get; set; } //list of virtual drives

        [DataMember]
        public int selectedVirtualDrive { get; set; } //The selected virtual drive to create

        [DataMember]
        public List<string> cmdOpenOCD { get; set; } // programming CMD's sendt to the OpenOCD server

        [DataMember]
        public bool enableNrf51BootErase { get; set; } // if true run cmdNrf51BootErase

        [DataMember]
        public string cmdNrf51BootErase { get; set; } // programming CMD's sendt to the OpenOCD server

        [DataMember]
        public int remotePort { get; set; }

        [DataMember]
        public string remoteAddress { get; set; }

        private static string jsonFormatter(string json) // function found at: http://stackoverflow.com/questions/4580397/json-formatter-in-c
        {  StringBuilder builder = new StringBuilder();
            bool quotes = false;
            bool ignore = false;
            int offset = 0;
            int position = 0;

            if (string.IsNullOrEmpty(json))
                return string.Empty;

            json = json.Replace(Environment.NewLine, "").Replace("\t", "");

            foreach (char character in json)
            {   switch (character)
                {   case '"':
                        if (!ignore)
                            quotes = !quotes;
                        break;
                    case '\'':
                        if (quotes)
                            ignore = !ignore;
                        break;
                }

                if (quotes)
                    builder.Append(character);
                else
                {
                    switch (character)
                    {   case '{':
                        case '[':
                            builder.Append(character);
                            builder.Append(Environment.NewLine);
                            builder.Append(new string(' ', ++offset * 4));
                            break;
                        case '}':
                        case ']':
                            builder.Append(Environment.NewLine);
                            builder.Append(new string(' ', --offset * 4));
                            builder.Append(character);
                            break;
                        case ',':
                            builder.Append(character);
                            builder.Append(Environment.NewLine);
                            builder.Append(new string(' ', offset * 4));
                            break;
                        case ':':
                            builder.Append(character);
                            builder.Append(' ');
                            break;
                        default:
                            if (character != ' ')
                            {
                                builder.Append(character);
                            }
                            break;
                    }
                    position++;
                }
            }
            return builder.ToString().Trim();
        }

        public string json
        {
            get
            {   //Create a stream to serialize the object to.
                MemoryStream ms = new MemoryStream();
                // Serializer the User object to the stream.
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(AppData));
                ser.WriteObject(ms, this);
                byte[] json = ms.ToArray();
                ms.Close();
                return jsonFormatter(Encoding.UTF8.GetString(json, 0, json.Length));
            }
        }

        public string openOCDconnection
        {
            get  { return @"target remote " + remoteAddress + @":" + remotePort.ToString(); }
        }

        public static AppData appDataFrom(string json)
        {
            AppData deserializedAppData = new AppData();
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(AppData));
            deserializedAppData = ser.ReadObject(ms) as AppData;
            ms.Close();
            return deserializedAppData;
        }

        public AppData()
        {
            remoteAddress = "192.168.0.199";   // OpenOCD remote address could also pa a name
            remotePort = 3333;                 // OpenOCD remote port
            virtualDrives = new List<string> { "Q", "Z", "X", "Y" };  // Add posible drives
            selectedVirtualDrive = 1;         // Select drive Z as default
            
            tempWorkingFolder = System.IO.Path.GetTempPath()+ @"OpenOCDData";
            //Chk if temp folder exists if not create it
            if( !System.IO.Directory.Exists(tempWorkingFolder))
                System.IO.Directory.CreateDirectory(tempWorkingFolder);

            gbdEXE = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\arm-none-eabi-gdb.exe";
            if (!System.IO.File.Exists(gbdEXE))
            {   //Try this if in debug mode   
                string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                gbdEXE = Path.GetFullPath(Path.Combine(path, @"..\..\..\3rdParty\arm-none-eabi-gdb.exe"));
            }


            objcopyEXE = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\arm-none-eabi-objcopy.exe";
            if (!System.IO.File.Exists(objcopyEXE))
            {   //Try this if in debug mode   
                string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                objcopyEXE = Path.GetFullPath(Path.Combine(path, @"..\..\..\3rdParty\arm-none-eabi-objcopy.exe"));
            }

            objcopyExtraArgs = "--change-addresses 0x8000000";

            //Commands used by OpenOCD to program a chip :)
            cmdOpenOCD = new List<string> { "monitor reset halt", "load", "monitor reset", "quit", "y" };
            enableNrf51BootErase = false;
            cmdNrf51BootErase = "monitor nrf51 mass_erase";

            clientViewSmalSize= new Size(254,145);
            clientViewLargSize = new Size(781, 640);
        }
    }
}
