using System;

namespace Shapes
{
    internal class Circle : Shape
    {
        private double radius;

        public Circle(double radius)
        {
            this.radius = radius;
        }

        public override double CalculateArea() => radius * radius * Math.PI;

        public override double CalculatePerimeter() => 2 * radius * Math.PI;

        public override string Draw() => base.Draw() + this.GetType().Name;
    }
}
