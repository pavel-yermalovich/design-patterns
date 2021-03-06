﻿using System;

namespace DesignPatterns.Examples.FactoryMethod
{
    public interface IProduct { }

    public class Camera : IProduct { }
    public class MobilePhone : IProduct { }

    public abstract class Creator
    {
        public abstract IProduct FactoryMethod(string type);
    }

    public class ConcreteCreator : Creator
    {
        public override IProduct FactoryMethod(string type)
        {
            switch (type)
            {
                case "Camera":
                    return new Camera();
                case "MobilePhone":
                case "Mobile":
                    return new MobilePhone();
                default:
                    throw new ArgumentException("Invalid type", "type");
            }
        }
    }

    public class Example1
    {
        static ConcreteCreator Creator = new ConcreteCreator();

        public static void Demo()
        {
            var product1 = Creator.FactoryMethod("Mobile");
            var product2 = Creator.FactoryMethod("Camera");
        }
    }
}
