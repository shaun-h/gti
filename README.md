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

## Operations

### Save

The save operation is used to save all currently installed global tools on the machine out in to a file so they can be installed again later.

You use this command run the following command.

`gti save `

It will save a file out in the current folder named tools.gti unless a different filename has been given to the command.

---

### List


---

## Contribute

Let people know how they can contribute into your project. A [contributing guideline](https://github.com/zulip/zulip-electron/blob/master/CONTRIBUTING.md) will be a big plus.


## License

See [LICENSE]()

## Release notes

## Road map



## Known issues

- Currently optional parameters based on types are not checked they are valid.