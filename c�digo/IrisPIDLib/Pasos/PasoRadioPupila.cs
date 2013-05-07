using AForge.Imaging.Filters;
using System.Drawing;
using System;
using IrisPIDLib.Util;

namespace IrisPIDLib
{
    public class PasoRadioPupila : Paso
    {

        #region Singleton
        static readonly PasoRadioPupila instance = new PasoRadioPupila();

        static PasoRadioPupila()
        {
        }

        public static PasoRadioPupila Instance
        {
            get
            {
                return instance;
            }
        }
        #endregion

        PasoRadioPupila()
        {
            base.Fase = FaseLocalizacionPupila.Instance;
            base.Nombre = "Encontrar radio de la pupila";
            base.Descripcion = "Se determina el radio de la pupila sumando el número de píxeles negros consecutivos que hay encima, debajo, a izquierda y a derecha del centro de la pupila y dividiendo el resultado obtenido entre 4.";
            base.PasoSiguiente = PasoCirculosExternoInterno.Instance;
        }

        protected override void Logica()
        {
            Bitmap imagenBinarizada = Estado.Instance.ImagenBinarizada;

            int filas = imagenBinarizada.Height;
            int columnas = imagenBinarizada.Width;

            int fila = Estado.Instance.CentroPupila.Y;
            int columna = Estado.Instance.CentroPupila.X;

            while (columna < columnas)
            {
                if (imagenBinarizada.GetPixel(columna, fila).R > 0) break;
                columna++;
            }

            int columnaDerecha = columna;

            columna = Estado.Instance.CentroPupila.X;

            while (columna >= 0)
            {
                if (imagenBinarizada.GetPixel(columna, fila).R > 0) break;
                columna--;
            }

            int columnaIzquierda = columna;

            columna = Estado.Instance.CentroPupila.X;

            while (fila < filas)
            {
                if (imagenBinarizada.GetPixel(columna, fila).R > 0) break;
                fila++;
            }

            int filaAbajo = fila;

            fila = Estado.Instance.CentroPupila.Y;

            while (fila >= 0)
            {
                if (imagenBinarizada.GetPixel(columna, fila).R > 0) break;
                fila--;
            }

            int filaArriba = fila;

            int distancia1 = Math.Abs(Estado.Instance.PuntoPupila.X - columnaDerecha);
            int distancia2 = Math.Abs(Estado.Instance.PuntoPupila.X - columnaIzquierda);
            int distancia3 = Math.Abs(Estado.Instance.PuntoPupila.Y - filaAbajo);
            int distancia4 = Math.Abs(Estado.Instance.PuntoPupila.Y - filaArriba);

            Estado.Instance.RadioPupila = (distancia1 + distancia2 + distancia3 + distancia4) / 4;

        }

        protected override void Presentacion()
        {
            Estado.Instance.ImagenAMostrar = Estado.Instance.ImagenBinarizada.Clone() as Bitmap;

            //marcamos el círculo que delimita la pupila
            Pintor.MarcaCirculoPupila();
            Pintor.MarcaCentroPupila();
        }
    }
}
