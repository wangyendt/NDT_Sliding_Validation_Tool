using System.IO;
using System.Windows;
using Microsoft.Win32;

namespace NDT_Sliding_Validation_Tool
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void UpdateParams()
        {
            INIIO ini = new INIIO(Path.GetFullPath("config.ini"));
            ini.IniWriteValue("Signal", "use fake data", (!rdbSignal.IsChecked).ToString());
            ini.IniWriteValue("Signal", "signal path", tbSignalPath.Text);
            ini.IniWriteValue("Signal", "noise sigma", tbSignalSigma.Text);
            ini.IniWriteValue("Signal", "drift max", tbSignalDriftMax.Text);
            ini.IniWriteValue("Signal", "drift momentum", tbSignalDriftMomentum.Text);
            ini.IniWriteValue("Model", "use fake model", (!rdbModel.IsChecked).ToString());
            ini.IniWriteValue("Model", "model path", tbModelPath.Text);
            ini.IniWriteValue("Model", "little finger peak", tbModelLFpeak.Text);
            ini.IniWriteValue("Model", "ring finger peak", tbModelRFpeak.Text);
            ini.IniWriteValue("Model", "gap", tbModelGap.Text);
            ini.IniWriteValue("Model", "decay", tbModelDecay.Text);
            ini.IniWriteValue("Model", "horizontal noise", tbModelHNoise.Text);
            ini.IniWriteValue("Model", "vertical noise", tbModelVNoise.Text);
        }

        private void BtnAnalyze_Click(object sender, RoutedEventArgs e)
        {
            //            ShowLog(new INIIO("config.ini").IniReadValue("Model","gap"));
            UpdateParams();
            CmdCaller cc = new CmdCaller(@"anapython.exe");
            ShowLog(cc.StartProcess("ndt_al_str_tool.py hello world"));
        }

        public void ShowLog(string log)
        {
            rtbLog.AppendText(">>> " + log + "\r\n");
            rtbLog.ScrollToEnd();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // import signal
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = Directory.GetCurrentDirectory();
            ofd.Filter = "Text Files|*.txt|All Files|*.*";
            ofd.Multiselect = false;
            ofd.Title = "Import signal";
            if (ofd.ShowDialog() == true)
            {
                if (ofd.CheckFileExists)
                {
                    tbSignalPath.Text = ofd.FileName;
                    rdbSignal.IsEnabled = true;
                }
                else
                {
                    tbSignalPath.Text = "File not exists!";
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // import model
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = Directory.GetCurrentDirectory();
            ofd.Filter = "Text Files|*.txt|All Files|*.*";
            ofd.Multiselect = false;
            ofd.Title = "Import model";
            if (ofd.ShowDialog() == true)
            {
                if (ofd.CheckFileExists)
                {
                    tbModelPath.Text = ofd.FileName;
                    rdbModel.IsEnabled = true;
                }
                else
                {
                    tbModelPath.Text = "File not exists!";
                }
            }
        }
    }
}