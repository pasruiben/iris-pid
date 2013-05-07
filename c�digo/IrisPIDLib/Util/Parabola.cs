using System;
namespace IrisPIDLib
{
    public class Parabola
    {

        /*
         * 
         * Ecuación de la parábola -> y = a*x^2 + b*x + c
         * 
         */ 

        public double a { get; set; }
        public double b { get; set; }
        public double c { get; set; }


        public double Valor(double x)
        {
            return (a * Math.Pow(x, 2) + b * x + c);
        }
    }
}
