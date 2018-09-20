using System;
using System.IO;
using CsvHelper.Configuration;
using gti.core.Interfaces;
using gti.core.Models;
using Newtonsoft.Json;

namespace gti.core.Operations
{
    public class SaveOperation : IOperation
    {
        private readonly IGlobalToolsManager _globalToolsManager;

        private SaveOperationOptions _options;

        public SaveOperation(IGlobalToolsManager globalToolsManager)
        {
            _globalToolsManager = globalToolsManager;
        }
        public void SetOperationOptions(IOperationOptions options)
        {
            var op = options as SaveOperationOptions;
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
            var globalTools = _globalToolsManager.GetGlobalTools();
            if (_options.OutputType == "json")
            {
                var output = JsonConvert.SerializeObject(globalTools);
                File.WriteAllText(_options.Outputfilename,output);

            }
            else if(_options.OutputType == "csv")
            {
                if (File.Exists(_options.Outputfilename))
                {
                    File.Delete(_options.Outputfilename);
                }
                var file = new StreamWriter(_options.Outputfilename);
                
                var csv = new CsvHelper.CsvWriter(file);
                csv.WriteRecords(globalTools.Tools);  
                file.Flush();
                file.Close();
            }
        }
    }
}