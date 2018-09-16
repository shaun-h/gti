using System;
using CommandLine;
using gti.Factories;
using gti.Interfaces;
using gti.Models;
using Ninject;

namespace gti
{
    class Program
    {
        private static IKernel _kernel;
        static void Main(string[] args)
        {
            RegisterTypes();
            var parser = new Parser(with =>
            {
                with.EnableDashDash = true;
                with.HelpWriter = Console.Out;
            });
            var result = parser.ParseArguments<CommandOptions>(args);
            result.WithParsed(x =>
            {
                try
                {
                    var factory = _kernel.Get<IOperationFactory>();
                    var operation = factory.GetOperation(x.Operation);
                }
                catch (ArgumentOutOfRangeException e)
                {
                    Console.WriteLine(e.Message);
                    Environment.ExitCode = 1;
                }
            });
            result.WithNotParsed(x => { });
        }

        private static void RegisterTypes()
        {
            _kernel = new StandardKernel();
            _kernel.Bind<IOperationFactory>().To<OperationFactory>();
        }
    }
}