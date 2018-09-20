using gti.core.Models;

namespace gti.core.Interfaces
{
    public interface IProcessManager
    {
        ProcessOutput RunProcess(string process, string arguments);
    }
}