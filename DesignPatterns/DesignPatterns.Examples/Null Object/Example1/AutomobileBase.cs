using System;

namespace DesignPatterns.Examples.Null_Object.Example1
{
    public abstract class AutomobileBase
    {
        public abstract Guid Id { get; }
        public abstract string Name { get; }
        public abstract void Start();
        public abstract void Stop();

        public static NullAutomobile Null { get; } = new NullAutomobile();

        public class NullAutomobile : AutomobileBase
        {
            public override Guid Id => Guid.Empty;

            public override string Name => string.Empty;

            public override void Start() { }
            public override void Stop() { }
        }
    }
}