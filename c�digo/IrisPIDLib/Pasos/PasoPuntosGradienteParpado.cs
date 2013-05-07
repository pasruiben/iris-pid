using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using IrisPIDLib.Util;

namespace IrisPIDLib
{
    public abstract class PasoPuntosGradienteParpado : Paso
    {
        protected int limiteInferior;

        protected List<Vector2> puntosGradiente;

        protected void EncuentraPuntosGradiente(int puntoX, int puntoY)
        {
            Bitmap imagenFiltroGaussiano = Estado.Instance.ImagenFiltroGaussiano;

            //además de los puntos anterior y posterior en la dirección
            Vector2 puntoAnterior = new Vector2(puntoX, puntoY - 1);
            Vector2 puntoPosterior = new Vector2(puntoX, puntoY + 1);

            //calculamos la variación de intensidad del punto anterior al posterior
            int intensidadPuntoAnterior = imagenFiltroGaussiano.GetPixel(puntoAnterior.X, puntoAnterior.Y).R;
            int intensidadPuntoPosterior = imagenFiltroGaussiano.GetPixel(puntoPosterior.X, puntoPosterior.Y).R;

            int variacion = Math.Abs(intensidadPuntoAnterior - intensidadPuntoPosterior);

            //nos quedamos sólo con aquellos con una variación considerable
            if (variacion > limiteInferior)
            {
                Vector2 punto = new Vector2(puntoX, puntoY);

                puntosGradiente.Add(punto);
            }
        }

        protected abstract Parabola MiParabolaVirtual();
    }
}
