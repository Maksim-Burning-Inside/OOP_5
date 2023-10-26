using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_5
{
    abstract class Shape : IComparable<Shape>
    {
        private double _param_1;
        private double _param_2;

        private double _square;
        private double _perimeter;

        protected Shape(double param_1, double param_2)
        {
            try
            {
                if (param_1 <= 0 || param_2 <= 0) throw new Exception("Параметр фигуры не может быть меньше или равен нуля");
            }
            catch
            {
                throw new Exception("Неккоректный тип передаваемых параметрв");
            }
            _param_1 = param_1;
            _param_2 = param_2;

            СalculateSquare();
            СalculatePerimeter();
        }

        public double P1 
        { 
            get => _param_1;
            set 
            {
                _param_1 = value;
                СalculateSquare();
                СalculatePerimeter();
            }  
        }
        public double P2 
        { 
            get => _param_2;
            set 
            {
                _param_2 = value;
                СalculateSquare();
                СalculatePerimeter();
            }
        }
        public double Square { get => _square; protected set => _square = value; }
        public double Perimeter { get => _perimeter; protected set => _perimeter = value; }
        public abstract int ApexNumber { get; }
        public abstract string Name { get; }

        public abstract double СalculateSquare();
        public abstract double СalculatePerimeter();

        public int CompareTo(Shape other)
        {
            if (Square > other.Square)
                return 1;
            else if (Square < other.Square)
                return -1;
            else
                return 0;
        }
    }

    class Polygon : Shape
    {
        public static double angle;

        public Polygon(double param_1, double param_2) : base(param_1, param_2) { angle = 360 / P2; }

        public override int ApexNumber { get => (int)P2; }
        public override string Name { get => ApexNumber.ToString() + "-угольник"; }

        public override double СalculateSquare()
        {
            Square = (Math.Pow(P1, 2) * P2) / (4 * Math.Tan(180 / P2));
            return Square;
        }

        public override double СalculatePerimeter()
        {
            Perimeter = P1 * P2;
            return Perimeter;
        }
    }

    class Ellipse : Shape
    {
        public Ellipse(double param_1, double param_2) : base(param_1, param_2) { }

        public override int ApexNumber { get => 0; }
        public override string Name { get => "Эллипс"; }

        public override double СalculateSquare()
        {
            Square = Math.PI * P1 * P2;
            return Square;
        }

        public override double СalculatePerimeter()
        {
            Perimeter = (4 * (Math.PI * P1 * P2 - Math.Abs(P1 - P2))) / (P1 + P2);
            return Perimeter;
        }
    }

    class Triangle : Shape
    {
        public Triangle(double param_1, double param_2) : base(param_1, param_2) { }

        public override int ApexNumber { get => 3; }
        public override string Name { get => "Прямоугольный треугольник"; }

        public override double СalculateSquare()
        {
            Square = P1 * P2;
            return Square;
        }

        public override double СalculatePerimeter()
        {
            Perimeter = P1 + P2 + Math.Sqrt(Math.Pow(P1, 2) + Math.Pow(P1, 2));
            return Perimeter;
        }
    }
}
