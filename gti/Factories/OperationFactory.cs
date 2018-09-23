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
                    installOperationOptions.Inputfilename = "tools.gti";
                    if (!string.IsNullOrWhiteSpace(options.InputFilename))
                    {
                        installOperationOptions.Inputfilename = options.InputFilename;
                    }
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
                default:
                    throw new ArgumentOutOfRangeException($"Invalid operation requested {options.Command}, valid operations are [save,install]");                    
            }
        }

        private void SetupMappings()
        {
            _operationMappings = new Dictionary<string, Type>
            {
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
                if (typeof(SaveOperation) == type)
                {
                    return new SaveOperation(new GlobalToolsManager(new ProcessManager()));
                }
                if (typeof(InstallOperation) == type)
                {
                    return new InstallOperation(new GlobalToolsManager(new ProcessManager()));
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