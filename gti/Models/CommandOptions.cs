using CommandLine;

namespace gti.Models
{
    public class CommandOptions
    {
        [Option('o', "operation", HelpText = "This is the operation to run, valid operations are {list, install, save}")]
        public string Operation { get; set; }
    }
}