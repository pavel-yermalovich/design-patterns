using System;

namespace DesignPatterns.ConsoleApp
{
    class Program
    {
        static void Main()
        {
            Examples.FactoryMethod.Example1.Demo();
            Examples.Composite.Example1.Demo();
            Examples.Adapter.Example1.Demo();
            Examples.Bridge.Example1.Demo();

            Examples.Singleton.Example2.Demo();
            Examples.Specification.Example1.Demo();
            Examples.Chain_Of_Responsibility.Example1.Demo();
            Examples.Comand.Example1.Demo();
            Console.ReadLine();
        }
    }
}
