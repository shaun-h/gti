namespace gti.core.Interfaces
{
    public interface IOperation
    {
        void SetOperationOptions(IOperationOptions options);
        void PerformOperation();
    }
}