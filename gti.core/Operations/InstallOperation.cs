using System;
using gti.core.Interfaces;
using gti.core.Models;

namespace gti.core.Operations
{
    public class InstallOperation : IOperation
    {
        private InstallOperationOptions _options;

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
            throw new System.NotImplementedException();
        }
    }
}