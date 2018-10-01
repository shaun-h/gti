# Global Tools Installer


| | |
| --- | --- |
| Build Status | [![Build status](https://dev.azure.com/shaunhevey/GitHub/_apis/build/status/Gti)](https://dev.azure.com/shaunhevey/GitHub/_build/latest?definitionId=9) |
| NuGet Version | [![NuGet](https://img.shields.io/nuget/v/gti.svg)](https://www.nuget.org/packages/gti/) |

Global Tools installer is a .Net Global tool that allows you to define a list of global tools that you would like to install on your machine and makes it easy to do so.

This tool is very much in beta so please report any issues or suggestions.

## Installation

This tool requires .Net Core 2.1 or newer you can install it from [here](https://aka.ms/DotNetCore21)

To install this tool run the following command.

` dotnet tool install gti -g `

Once installed Global tools installer has a number of operations it can perform.

## Commands

The following list of commands are the basic commands that are available, other options for these commands are available in the in app help.

### Save

The save command is used to save all currently installed global tools on the machine out in to a file so they can be installed again later.

You use this command run the following command.

`gti -c save `

It will save a file out in the current folder named tools.gti unless a different filename has been given to the command.

---

### Install

The install command is used to to install global tools on a machine from a global tools (tools.gti) file.

`gti -c install `

This will search for a file named tools.gti in the current directory to install, if this isn't overwritten.

---

## License

See [LICENSE]()

## Release notes

- 0.5 - this the first public release, treat this as a beta.

## Road map

- Improved documentation
- Show a list of tools about to be installed and ask to accept

## Known issues

- Currently optional parameters based on types are not checked they are valid.