using gti.core.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using gti.core.Models;

namespace gti.core.Managers
{
    public class ProcessManager : IProcessManager
    {
        public ProcessOutput RunProcess(string process, string arguments)
        {
            Process p = new Process();
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.FileName = process;
            p.StartInfo.Arguments = arguments;

            var standardOut = new StringBuilder();
            var standardOutLines = new List<string>();
            var odreh = new DataReceivedEventHandler(
                (s, e) => 
                { 
                    standardOut.AppendLine(e.Data);
                    standardOutLines.Add(e.Data);
                }
            );
            var standardError = new StringBuilder();
            var standardErrorLines = new List<string>();
            var edreh = new DataReceivedEventHandler(
                (s, e) => 
                { 
                    standardError.AppendLine(e.Data);
                    standardErrorLines.Add(e.Data);
                }
            );
            p.OutputDataReceived += odreh;
            p.ErrorDataReceived += edreh;

            p.Start();
            p.BeginOutputReadLine();
            p.BeginErrorReadLine();
            
            p.WaitForExit();

            p.OutputDataReceived -= odreh;
            p.ErrorDataReceived -= edreh;
            
            var po = new ProcessOutput();
            po.StandardOut = standardOut.ToString();
            po.StandardOutLines = standardOutLines;
            po.StandardError = standardError.ToString();
            po.StandardErrorLines = standardErrorLines;
            po.ExitCode = p.ExitCode;
            return po;
        }
    }
}