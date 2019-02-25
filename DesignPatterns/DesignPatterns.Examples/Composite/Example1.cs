using System;
using System.Collections.Generic;
using static System.Console;

namespace DesignPatterns.Examples.Composite
{
    public class Example1
    {
        public static void Demo()
        {
            var leaf = new Leaf("Leaf Z");

            var root = new Composite("Root")
                .Add(new Leaf("Leaf A"))
                .Add(new Leaf("Leaf B"))
                .Add(new Leaf("Leaf C"))
                .Add(new Composite("Composite X")
                    .Add(new Leaf("Leaf XA"))
                    .Add(new Leaf("Leaf XB"))
                 )
                .Add(new Leaf("Leaf C"))
                .Add(leaf)
                .Remove(leaf)
                .Add(new Composite("Composite Y")
                    .Add(new Leaf("Leaf YA"))
                    .Add(new Composite("Composite YB")
                        .Add(new Leaf("Leaf YBM"))
                        .Add(new Leaf("Leaf YBN"))
                     )
                    .Add(new Leaf("Leaf YC"))
                 );

            root.Display(0);
        }
    }

    public abstract class Component
    {
        protected string name;

        public Component(string name)
        {
            this.name = name;
        }

        public abstract Component Add(Component component);
        public abstract Component Remove(Component component);
        public abstract void Display(int depth);
    }

    public class Composite : Component
    {
        private List<Component> _children = new List<Component>();

        public Composite(string name):base(name)
        {

        }

        public override Component Add(Component component)
        {
            _children.Add(component);
            return this;
        }

        public override void Display(int depth)
        {
            WriteLine(new string('-', depth) + name);

            foreach(var component in _children)
            {
                component.Display(depth + 3);
            }
        }

        public override Component Remove(Component component)
        {
            if (_children.Contains(component))
                _children.Remove(component);
            return this;
        }
    }

    public class Leaf : Composite
    {
        public Leaf(string name) : base(name)
        {
        }

        public override Component Add(Component component)
        {
            WriteLine("Cannon add to a leaf.");
            return this;
        }

        public override void Display(int depth)
        {
            WriteLine(new string('-', depth) + name);
        }

        public override Component Remove(Component component)
        {
            WriteLine("Cannot remove from a leaf");
            return this;
        }
    }
}
