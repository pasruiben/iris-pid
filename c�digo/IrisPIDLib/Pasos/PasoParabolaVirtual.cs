using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IrisPIDLib.Util;
using System.Drawing;

namespace IrisPIDLib
{
    public abstract class PasoParabolaVirtual : Paso
    {
        protected readonly int incrementoVerticeParabola = 10;

        protected Vector2 puntoParabola1, puntoParabola2, verticeParabola;
        protected Parabola parabola;

        protected override void Logica()
        {

            /*
             Punto -> (p, q)
             Vertice -> (u, v)
             Ecuación de la parábola conocido vértice y un punto:  (y - v) = t*(x - u)^2
             Calculamos t usando el punto -> t = (q - v) / (p - u)^2
             
             
             y = t*x^2 - 2*t*u*x + t*u^2 + v
             
             Ecuación de la parábola -> y = a*x^2 + b*x + c
             
             a = t
             b = -2*t*u
             c = t*u^2 + v
             

            */

            InicializaPuntos();

            

            double denominador = Math.Pow(puntoParabola1.X - verticeParabola.X, 2);
            if (denominador == 0) throw new Exception("Algo va mal!!!!");

            double t = (puntoParabola1.Y - verticeParabola.Y) / denominador;

            parabola = new Parabola();

            parabola.a = t;
            parabola.b = -2 * t * verticeParabola.X;
            parabola.c = (t * Math.Pow(verticeParabola.X, 2)) + verticeParabola.Y;

            AsignaParabolaAEstado();

        }

        protected abstract void AsignaParabolaAEstado();


        protected override void Presentacion()
        {
            Estado.Instance.ImagenAMostrar = Estado.Instance.ImagenInicial.Clone() as Bitmap;

            Pintor.MarcaCirculoPupila();
            Pintor.MarcaCirculoIris();
        }

        protected abstract void InicializaPuntos();
    }
}
