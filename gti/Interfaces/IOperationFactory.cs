using gti.core.Interfaces;

namespace gti.Interfaces
{
    public interface IOperationFactory
    {
        IOperation GetOperation(string operation);
    }
}