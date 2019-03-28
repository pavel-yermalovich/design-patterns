using System;
using System.Windows.Input;

namespace DesignPatterns.Examples.Comand
{
    public class Example1
    {
        public static void Demo()
        {

        }
    }

    public interface ICommandFactory
    {
        string CommandName { get; }
        string Description { get; }

        ICommand MakeCommand(string[] arguments);
    }

    public interface ICommand
    {
        string Name { get; }
        void Execute();
    }

    public class NotFoundCommand : ICommand
    {
        public string Name { get; set; }

        public void Execute()
        {
            Console.WriteLine($"Couldn't find command: {Name}");
        }
    }
}
