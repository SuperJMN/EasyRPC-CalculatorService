using System;
using System.Reflection;
using System.Threading.Tasks;
using EasyRpc.DynamicClient;
using EasyRpc.DynamicClient.Grace;
using Grace.DependencyInjection;
using SampleWebApp.Services;

namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new DependencyInjectionContainer();

            container.Configure(c => c.Export<InterfaceNamingConvention>().As<INamingConventionService>());

            container.ProxyNamespace("https://localhost:44307/", namespaces: typeof(ICalculatorService).Namespace);

            var service = container.Locate<ICalculatorService>();
            var result = service.Add(2, 2);
            Console.WriteLine($"The result is {result}");
            Console.ReadLine();
        }
    }

    internal class InterfaceNamingConvention : INamingConventionService
    {
        public string GetNameForType(Type type)
        {
            return type.Name.Substring(1);
        }

        public string GetMethodName(MethodInfo method)
        {
            return method.Name;
        }
    }
}
