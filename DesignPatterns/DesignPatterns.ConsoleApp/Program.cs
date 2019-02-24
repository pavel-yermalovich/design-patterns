using System;

namespace DesignPatterns.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Examples.FactoryMethod.Example1.Demo();
            Examples.Composite.Example1.Demo();
            Examples.Adapter.Example1.Demo();
            Examples.Bridge.Example1.Demo();
            Console.ReadLine();
        }
    }
}
