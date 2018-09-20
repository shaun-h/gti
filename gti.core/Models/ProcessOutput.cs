using System.Collections.Generic;

namespace gti.core.Models
{
    public class ProcessOutput
    {
        public int ExitCode { get; set; }
        public string StandardOut { get; set; }
        public List<string> StandardOutLines { get; set; }
        public string StandardError { get; set; }
        public List<string> StandardErrorLines { get; set; }
    }
}