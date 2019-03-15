using System;

namespace DesignPatterns.Examples.Null_Object.Example1
{
    public class BMW335XI : AutomobileBase
    {
        public override string Name => "BMW 335 Xi";

        public override Guid Id => new Guid("68BECCDC-0FBD-4FB9-B0BB-D5D8A2AFD9F8");

        public override void Start()
        {
            Console.WriteLine("Beemer started. All 4 wheels under power.");
        }

        public override void Stop()
        {
            Console.WriteLine("Beemer stopped.");
        }
    }
}