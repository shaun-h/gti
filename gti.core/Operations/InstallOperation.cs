using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using gti.core.Interfaces;
using gti.core.Models;
using Newtonsoft.Json;

namespace gti.core.Operations
{
    public class InstallOperation : IOperation
    {
        private readonly IGlobalToolsManager _globalToolsManager;
        private InstallOperationOptions _options;

        public InstallOperation(IGlobalToolsManager globalToolsManager)
        {
            _globalToolsManager = globalToolsManager;
        }
        
        public void SetOperationOptions(IOperationOptions options)
        {
            var op = options as InstallOperationOptions;
            if (op == null)
            {
                throw new ArgumentException(nameof(options));
            }
            else
            {
                _options = op;
            }
        }

        public void PerformOperation()
        {
            var parsed = false;
            var globalTools = new List<GlobalTool>();
            try
            {
                if (!parsed)
                {
                    globalTools = ParseJsonInput().ToList();
                    parsed = true;
                }
            }
            catch (Exception e)
            {
                //Failed json parsing, try next input type
            }

            try
            {
                if (!parsed)
                {
                    globalTools = ParseCsvInput().ToList();
                    parsed = true;
                }
            }
            catch (Exception e)
            {
                
            }

            if (parsed)
            {
                foreach (var tool in globalTools)
                {
                    _globalToolsManager.InstallGlobalTool(tool);
                }    
            }
            else
            {
                Console.WriteLine("Unable to parse input.");
            }
            
            
        }

        private IEnumerable<GlobalTool> ParseJsonInput()
        {
            var contents = File.ReadAllText(_options.Inputfilename);
            var globalTools = JsonConvert.DeserializeObject<GlobalTools>(contents);
            return globalTools.Tools;
        }
        
        private IEnumerable<GlobalTool> ParseCsvInput()
        {
            var file = new StreamReader(_options.Inputfilename);
            var csv = new CsvHelper.CsvReader(file);
            return csv.GetRecords<GlobalTool>();  
        }
    }
}