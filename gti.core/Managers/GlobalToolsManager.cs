using System;
using System.Linq;
using System.Net;
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