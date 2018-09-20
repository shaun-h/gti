using gti.core.Interfaces;

namespace gti.core.Models
{
    public class SaveOperationOptions : IOperationOptions
    {
        public string Outputfilename { get; set; }
        public string OutputType { get; set; }
    }
}