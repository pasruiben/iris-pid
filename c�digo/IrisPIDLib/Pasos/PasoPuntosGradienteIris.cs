using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AForge.Imaging.Filters;
using System.Drawing;
using IrisPIDLib.Util;

namespace IrisPIDLib
{
    public class PasoPuntosGradienteIris : Paso
    {
        readonly int LimiteInferior = 5;

        //almacenaremos la distancia a la que se encuentran los puntos de gradiente
        List<Vector2> puntosGradienteIris;

        #region Singleton
        static readonly PasoPuntosGradienteIris instance = new PasoPuntosGradienteIris();

        static PasoPuntosGradienteIris()
        {
        }

        public static PasoPuntosGradienteIris Instance
        {
            get
            {
                return instance;
            }
        }
        #endregion

        PasoPuntosGradienteIris()
        {
            base.Fase = FaseLimiteIrisEsclerotica.Instance;
            base.Nombre = "Detección de los puntos de mayor gradiente entre los círculos interno y externo";
            base.Descripcion = "Se localizan los puntos de mayor gradiente de intensidad en la región delimitada por los círculos interno y externo calculados anteriormente. El cálculo del gradiente de intensidad se hace en dirección radial desde el centro de la pupila. Se buscan estos puntos solamente en los cuadrantes inferiores, alrededor de la línea horizontal que atraviesa el centro de la pupila, ya que es la región donde es menos probable encontrar interferencias de los párpados.";
            base.PasoSiguiente = PasoRadioIris.Instance;
        }

        protected override void Logica()
        {
            puntosGradienteIris = new List<Vector2>();

            int distanciaInicial = (int)Estado.Instance.RadioCirculoInterno;
            int distanciaFinal = (int)Estado.Instance.RadioCirculoExterno;

            //zona izquierda
            for (int d = 0; d < 5; d++)
            {
                double direccion = Math.PI + d * 0.15;

                for (int i = distanciaInicial; i < distanciaFinal; i++)
                {
                    EncuentraPuntosGradiente(i, direccion);
                }
            }

            //zona derecha
            for (int d = 0; d < 5; d++)
            {
                double direccion = -d * 0.15;

                for (int i = distanciaInicial; i < distanciaFinal; i++)
                {
                    EncuentraPuntosGradiente(i, direccion);
                }
            }

            Estado.Instance.PuntosGradienteIrisEsclerotica = puntosGradienteIris;
        }

        protected override void Presentacion()
        {
            Estado.Instance.ImagenAMostrar = Estado.Instance.ImagenFiltroGaussiano.Clone() as Bitmap;

            foreach(Vector2 punto in puntosGradienteIris)
                Pintor.MarcaPuntoCirculo(Estado.Instance.ImagenAMostrar, punto, Color.Blue);

            Pintor.MarcaCirculoExterno();
            Pintor.MarcaCirculoInterno();
        }

        private void EncuentraPuntosGradiente(int distancia, double direccion)
        {
            Bitmap imagenFiltroGaussiano = Estado.Instance.ImagenFiltroGaussiano;
            Vector2 centro = Estado.Instance.CentroPupila;

            //encontramos el punto
            Vector2 punto = new Vector2(centro.X + (int)(Math.Cos(direccion) * distancia), centro.Y - (int)(Math.Sin(direccion) * distancia));

            //además de los puntos anterior y posterior en la dirección
            Vector2 puntoAnterior = new Vector2(centro.X + (int)(Math.Cos(direccion) * (distancia - 1)), centro.Y - (int)(Math.Sin(direccion) * (distancia - 1)));
            Vector2 puntoPosterior = new Vector2(centro.X + (int)(Math.Cos(direccion) * (distancia + 1)), centro.Y - (int)(Math.Sin(direccion) * (distancia + 1)));

            //calculamos la variación de intensidad del punto anterior al posterior
            int intensidadPuntoAnterior = imagenFiltroGaussiano.GetPixel(puntoAnterior.X, puntoAnterior.Y).R;
            int intensidadPuntoPosterior = imagenFiltroGaussiano.GetPixel(puntoPosterior.X, puntoPosterior.Y).R;

            int variacion = Math.Abs(intensidadPuntoAnterior - intensidadPuntoPosterior);

            //nos quedamos sólo con aquellos que pasan de oscuro a claro y con una variación considerable
            if (intensidadPuntoPosterior > intensidadPuntoAnterior && variacion > LimiteInferior)
            {
                puntosGradienteIris.Add(punto);
            }
        }
    }
}
