using gti.core.Models;

namespace gti.core.Interfaces
{
    public interface IGlobalToolsManager
    {
        GlobalTools GetGlobalTools();
        void InstallGlobalTool(GlobalTool globalTool);
    }
}