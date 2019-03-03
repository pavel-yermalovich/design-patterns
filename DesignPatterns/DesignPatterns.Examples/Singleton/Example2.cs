using System;

namespace DesignPatterns.Examples.Singleton
{
    public class Example2
    {
        public static void Demo()
        {
            var instance1 = LazySingleton.Instance;
            Console.WriteLine($"We've created {nameof(instance1)}");
            var instance2 = LazySingleton.Instance;
            Console.WriteLine($"We've created {nameof(instance2)}");
        }
    }

    public class LazySingleton
    {
        private LazySingleton() { }

        public static LazySingleton Instance => Nested._instance;

        private class Nested
        {
            static Nested() { }

            internal static readonly LazySingleton _instance = CreateInstance();

            private static LazySingleton CreateInstance()
            {
                Console.WriteLine("An instance of LazySingleton is being created...");
                return new LazySingleton();
            }
        }
    }
}
