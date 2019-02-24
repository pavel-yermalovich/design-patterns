using System;

namespace DesignPatterns.Examples.Bridge
{
    public class Example1
    {
        public static void Demo()
        {
            ElectronicGoods item = new Television();
            item.State = new OnState();
            item.MoveToCurrentState();
            item.State = new OffState();
            item.MoveToCurrentState();

            item = new WashingMachine();
            item.State = new OffState();
            item.MoveToCurrentState();
        }
    }

    public interface IState // Implementor
    {
        void MoveState();
    }

    public abstract class ElectronicGoods
    {
        public IState State { get; set; }
        public abstract void MoveToCurrentState();
    }

    public class OnState : IState // Concrete Implementor 1
    {
        public void MoveState()
        {
            Console.WriteLine("On State");
        }
    }

    public class OffState : IState // Concrete Implementor 2
    {
        public void MoveState()
        {
            Console.WriteLine("Off State");
        }
    }

    public class Television : ElectronicGoods // Refined Abstraction
    {
        public override void MoveToCurrentState()
        {
            Console.Write("\n Television is functionaing at ");
            State.MoveState();
        }
    }

    public class WashingMachine : ElectronicGoods
    {
        public override void MoveToCurrentState()
        {
            Console.Write("\n Washing machine is functionaing at ");
            State.MoveState();
        }
    }
}
