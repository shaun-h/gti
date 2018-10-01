using CommandLine;

namespace gti.Models
{
    public class CommandOptions
    {
        [Option('c', "command", Required = true, HelpText = "This is the command to run, valid commands are {install, save}")]
        public string Command { get; set; }
        
        [Option('o', "output", Required = false ,HelpText = "This is output filename")]
        public string OutputFilename { get; set; }
        
        [Option('t', "type", Required = false ,HelpText = "This is file type {json, csv}")]
        public string OutputType { get; set; }
        
        [Option('i', "input", Required = false ,HelpText = "This is input filename")]
        public string InputFilename { get; set; }
    }
}