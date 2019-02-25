using System;

namespace DesignPatterns.Examples.Adapter
{
    public class Example1
    {
        public static void Demo()
        {
            Console.WriteLine("Adapter Pattern. Example 1");
            var triangle = new Triangle(123, 567);
            var calculatorAdapter = new CalculatorAdapter();
            var area = calculatorAdapter.GetArea(triangle);
            Console.WriteLine($"Triangle area is {area}");
        }
    }

    public class Rectangle
    {
        public Rectangle(double length, double width)
        {
            Length = length;
            Width = width;
        }

        public double Length { get; set; }
        public double Width { get; set; }
    }

    public class Triangle
    {
        public Triangle(double @base, double height)
        {
            Base = @base;
            Height = height;
        }

        public double Base { get; set; }
        public double Height { get; set; }
    }

    public class Calculator
    {
        public double GetArea(Rectangle rectangle)
        {
            return rectangle.Width * rectangle.Length;
        }
    }

    public class CalculatorAdapter
    {
        public double GetArea(Triangle triangle)
        {
            var rectangle = new Rectangle(0.5 * triangle.Height, triangle.Base);
            var calculator = new Calculator();
            return calculator.GetArea(rectangle);
        }
    }
}
