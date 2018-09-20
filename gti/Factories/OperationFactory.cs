using System;
using System.Collections.Generic;
using gti.core.Interfaces;
using gti.core.Managers;
using gti.core.Models;
using gti.core.Operations;
using gti.Interfaces;
using gti.Models;

namespace gti.Factories
{
    public class OperationFactory : IOperationFactory
    {
        private Dictionary<string, Type> _operationMappings;
        private Func<Type, IOperation> _func;
        public OperationFactory(Func<Type,IOperation> func)
        {
            _func = func;
            SetupMappings();
        }

        public OperationFactory()
        {
            SetupMappings();

        }
        public IOperation GetOperation(CommandOptions options)
        {
            var operation = GetOperationInstance(options.Command);
            var operationOptions = GetTypeOptions(options.Command, options);
            operation.SetOperationOptions(operationOptions);
            return operation;
        }

        private IOperationOptions GetTypeOptions(string type, CommandOptions options)
        {
            switch (type)
            {
                case "install":
                    var installOperationOptions = new InstallOperationOptions();
                    return installOperationOptions;
                case "save":
                    var saveOperationOptions = new SaveOperationOptions();
                    saveOperationOptions.Outputfilename = "tools.gti";
                    if (!string.IsNullOrWhiteSpace(options.OutputFilename))
                    {
                        saveOperationOptions.Outputfilename = options.OutputFilename;
                    }
                    
                    saveOperationOptions.OutputType = "csv";
                    if (!string.IsNullOrWhiteSpace(options.OutputType))
                    {
                        saveOperationOptions.OutputType = options.OutputType;
                    }
                    return saveOperationOptions;
                case "list":
                    var listOperationOptions = new ListOperationOptions();
                    listOperationOptions.FeedUrl = "https://api.nuget.org/v3/index.json";
                    if (!string.IsNullOrWhiteSpace(options.NuGetFeed))
                    {
                        listOperationOptions.FeedUrl = options.NuGetFeed;
                    }
                    return listOperationOptions;
                default:
                    throw new ArgumentOutOfRangeException($"Invalid operation requested {options.Command}, valid operations are [list,save,install]");                    
            }
        }

        private void SetupMappings()
        {
            _operationMappings = new Dictionary<string, Type>
            {
                {"list", typeof(ListOperation)}, 
                {"save", typeof(SaveOperation)}, 
                {"install", typeof(InstallOperation)}
            };
        }

        private IOperation GetOperationInstance(string operation)
        {
            if (!_operationMappings.ContainsKey(operation))
            {
                throw new ArgumentException("Unknown operation, are you missing a mapping?");
            }

            var type = _operationMappings[operation];
            if (_func == null)
            {
                if (typeof(ListOperation) == type)
                {
                    return new ListOperation();
                }
                if (typeof(SaveOperation) == type)
                {
                    return new SaveOperation(new GlobalToolsManager(new ProcessManager()));
                }
                if (typeof(InstallOperation) == type)
                {
                    return new InstallOperation();
                }
            }
            else
            {
                return _func(type);                
            }
            
            throw new ArgumentException("Unknown operation, are you missing a mapping?");
        }
        
       
    }
}