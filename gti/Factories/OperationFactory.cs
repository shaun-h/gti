using System;
using gti.core.Interfaces;
using gti.core.Operations;
using gti.Interfaces;

namespace gti.Factories
{
    public class OperationFactory : IOperationFactory
    {
        public OperationFactory()
        {
            
        }

        public IOperation GetOperation(string operation)
        {
            switch (operation)
            {
                case "list":
                    return new ListOperation();
                    break;
                case "save":
                    return new SaveOperation();
                    break;
                case "install":
                    return new InstallOperation();
                    break;
                default:
                    throw new ArgumentOutOfRangeException($"Invalid operation requested {operation}, valid operations are [list,save,install]");
                    break;
                    
            }
        }
    }
}