using gti.core.Interfaces;
using gti.Models;

namespace gti.Interfaces
{
    public interface IOperationFactory
    {
        IOperation GetOperation(CommandOptions options);
    }
}