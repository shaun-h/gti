using System;
using System.Runtime.CompilerServices;
using CommandLine;
using gti.core.Interfaces;
using gti.core.Managers;
using gti.core.Operations;
using gti.Factories;
using gti.Interfaces;
using gti.Models;
using Ninject;
using Ninject.Activation;

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
                    var operation = factory.GetOperation(x);
                    operation.PerformOperation();
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
            _kernel.Bind<IOperationFactory>().ToMethod(a => new OperationFactory(GetTypeInstanceFromKernel));
            _kernel.Bind<InstallOperation>().ToSelf();
            _kernel.Bind<SaveOperation>().ToSelf();
            _kernel.Bind<IProcessManager>().To<ProcessManager>();
            _kernel.Bind<IGlobalToolsManager>().To<GlobalToolsManager>();
        }

        private static IOperation GetTypeInstanceFromKernel(Type type)
        {
            return (IOperation)_kernel.Get(type);
        }
    }
}