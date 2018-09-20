using System.Collections.Generic;
using System.Security.Principal;

namespace gti.core.Models
{
    public class GlobalTools
    {
        public GlobalTools()
        {
            Tools = new List<GlobalTool>();
        }
        public List<GlobalTool> Tools;
    }
}