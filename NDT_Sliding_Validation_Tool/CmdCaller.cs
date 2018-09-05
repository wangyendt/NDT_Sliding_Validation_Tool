using System.Diagnostics;

namespace NDT_Sliding_Validation_Tool
{
    public class CmdCaller
    {
        private string _filename;

        public CmdCaller(string filename)
        {
            _filename = filename;
        }

        public string  StartProcess(string arguments)
        {
            Process p = new Process();
            p.StartInfo = new ProcessStartInfo(
                _filename,
                arguments
                )
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            p.Start();
            string output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();

            return output;
        }
    }
}