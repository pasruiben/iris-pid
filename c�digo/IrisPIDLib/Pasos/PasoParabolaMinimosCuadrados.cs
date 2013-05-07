using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;

namespace IrisPIDLib
{
    public abstract class PasoParabolaMinimosCuadrados : Paso
    {
        protected override void Logica()
        {
            if (ParpadoEncontrado())
            {
                Parabola parabola = new Parabola();

                List<Vector2> listaPuntos = MisPuntosGradiente();

                BigInteger s00 = listaPuntos.Count;

                BigInteger s10 = 0, s20 = 0, s30 = 0, s40 = 0, s01 = 0, s11 = 0, s21 = 0;
                Decimal a, b, c;

                foreach (Vector2 punto in listaPuntos)
                {
                    BigInteger puntoX = punto.X;
                    BigInteger puntoY = punto.Y;

                    s10 += puntoX; // Suma de x
                    s20 += puntoX * puntoX; // Suma de x cuadrada
                    s30 += puntoX * puntoX * puntoX; // Suma de x cúbica
                    s40 += puntoX * puntoX * puntoX * puntoX; // Suma x cuarta

                    s01 += puntoY; // Suma de y
                    s11 += puntoX * puntoY; // Suma de xy
                    s21 += puntoX * puntoX * puntoY;
                }

                BigInteger numA = (s21 * (s20 * s00 - s10 * s10) - s11 * (s30 * s00 - s10 * s20) + s01 * (s30 * s10 - s20 * s20));
                BigInteger numB = (s40 * (s11 * s00 - s01 * s10) - s30 * (s21 * s00 - s01 * s20) + s20 * (s21 * s10 - s11 * s20));
                BigInteger numC = (s40 * (s20 * s01 - s10 * s11) - s30 * (s30 * s01 - s10 * s21) + s20 * (s30 * s11 - s20 * s21));
                BigInteger deno = (s40 * (s20 * s00 - s10 * s10) - s30 * (s30 * s00 - s10 * s20) + s20 * (s30 * s10 - s20 * s20));

                if (deno == 0) throw new Exception("Algo va mal");

                a = (decimal)numA / (decimal)deno;
                b = (decimal)numB / (decimal)deno;
                c = (decimal)numC / (decimal)deno;

                parabola.a = (double)a;
                parabola.b = (double)b;
                parabola.c = (double)c;

                ActualizaParabola(parabola);
            }            
        }

        protected abstract bool ParpadoEncontrado();
        protected abstract List<Vector2> MisPuntosGradiente();
        protected abstract void ActualizaParabola(Parabola parabola);
    }
}
