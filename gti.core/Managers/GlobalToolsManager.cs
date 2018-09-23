using System;
using System.Linq;
using System.Net;
using System.Text;
using gti.core.Interfaces;
using gti.core.Models;

namespace gti.core.Managers
{
    public class GlobalToolsManager : IGlobalToolsManager
    {
        private readonly IProcessManager _processManager;

        public GlobalToolsManager(IProcessManager processManager)
        {
            _processManager = processManager;
        }

        public GlobalTools GetGlobalTools()
        {
            var output = _processManager.RunProcess("dotnet", "tool list -g");
            return ParseListOutput(output);
        }

        public void InstallGlobalTool(GlobalTool globalTool)
        {
            var arguments = BuildArgumentString(globalTool);
            Console.WriteLine($"Installing tool {globalTool.Id}");
            var output = _processManager.RunProcess("dotnet", arguments);
            WriteOuput(output);
        }

        private void WriteOuput(ProcessOutput output)
        {
            var originalColour = Console.ForegroundColor;
            if (output.ExitCode == 0)
            {

                Console.ForegroundColor = ConsoleColor.Green;
                foreach (var line in output.StandardOutLines)
                {
                    Console.WriteLine(line);
                }

                Console.ForegroundColor = originalColour;
            }
            else
            {
                if (output.StandardError.ToLowerInvariant().Contains("is already installed"))
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    foreach (var line in output.StandardErrorLines)
                    {
                        Console.WriteLine(line);
                    }

                    Console.ForegroundColor = originalColour;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    foreach (var line in output.StandardErrorLines)
                    {
                        Console.WriteLine(line);
                    }

                    Console.ForegroundColor = originalColour;
                }
            }
        }

        private string BuildArgumentString(GlobalTool globalTool)
        {
            var sb = new StringBuilder();
            sb.Append("tool install -g ");
            if (string.IsNullOrWhiteSpace(globalTool.Id))
            {
                throw new ArgumentException($"Global tools require a id");
            }

            sb.Append($"{globalTool.Id} ");
            if (!string.IsNullOrWhiteSpace(globalTool.FeedUri))
            {
                sb.Append($"--add-source {globalTool.FeedUri} ");
            }
            if (!string.IsNullOrWhiteSpace(globalTool.Version))
            {
                sb.Append($"--version {globalTool.Version} ");
            }

            return sb.ToString();
        }

        private GlobalTools ParseListOutput(ProcessOutput output)
        {
            var gts = new GlobalTools();

            if (output.StandardOutLines.Count > 2)
            {
                for (var i = 2; i < output.StandardOutLines.Count; i++)
                {
                    var line = output.StandardOutLines[i];
                    if (line != null)
                    {
                        var id = "";
                        var version = "";
                        var command = "";
                        var current = 0;
                        var valid = false;
                        foreach (var ch in line)
                        {
                            if (ch == ' ' && valid)
                            {
                                current++;
                                valid = false;
                            }
                            else if(ch != ' ')
                            {
                                valid = true;
                                if (current == 0)
                                {
                                    id = id + ch;
                                }
                                else if (current == 1)
                                {
                                    version = version + ch;
                                }
                                else if (current == 2)
                                {
                                    command = command + ch;
                                }
                            }
                        }

                        var gt = new GlobalTool();
                        gt.Id = id;
                        gt.Command = command;
                        gt.Version = version;
                        gt.FeedUri = "https://api.nuget.org/v3/index.json";

                        gts.Tools.Add(gt);
                    }
                }
            }

            return gts;
        }
    }
}