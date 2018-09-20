using CommandLine;

namespace gti.Models
{
    public class CommandOptions
    {
        [Option('c', "command", HelpText = "This is the command to run, valid commands are {list, install, save}")]
        public string Command { get; set; }

        [Option('f', "feed", Required = false ,HelpText = "This is the NuGet feed to use in the operation.")]
        public string NuGetFeed { get; set; }
        
        [Option('o', "output", Required = false ,HelpText = "This is output filename")]
        public string OutputFilename { get; set; }
        
        [Option('t', "type", Required = false ,HelpText = "This is file type {json, csv}")]
        public string OutputType { get; set; }
    }
}