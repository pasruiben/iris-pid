using System.Drawing;
using AForge.Imaging.Filters;
using System;
using IrisPIDLib.Util;

namespace IrisPIDLib
{
    public class PasoCentroPupila : Paso
    {
        readonly double ErrorMaximoPermitido = Math.Sqrt(2);
        readonly int NumIteracionesMax = 500;

        #region Singleton
        static readonly PasoCentroPupila instance = new PasoCentroPupila();

        static PasoCentroPupila()
        {
        }

        public static PasoCentroPupila Instance
        {
            get
            {
                return instance;
            }
        }
        #endregion

        PasoCentroPupila()
        {
            base.Fase = FaseLocalizacionPupila.Instance;
            base.Nombre = "Encontrar centro de la pupila (método iterativo)";
            base.Descripcion = "Se localiza el centro de la pupila utilizando un método iterativo. Se parte del punto obtenido en el apartado anterior. En cada iteración, se actualiza la componente X del punto de manera que tenga el mismo número de píxeles negros consecutivos a izquierda y derecha. Se hace lo mismo con la componente Y. El método converge en el punto centro de la pupila.";
            base.PasoSiguiente = PasoRadioPupila.Instance;
        }

        protected override void Logica()
        {
            Vector2 puntoActual, puntoNuevo;
            double distanciaEntrePuntos;

            puntoActual = Estado.Instance.PuntoPupila;

            int numIter = 0;
            do
            {
                puntoNuevo = CalculaPuntoNuevo(puntoActual);
                distanciaEntrePuntos = puntoNuevo.Distancia(puntoActual);
                puntoActual = puntoNuevo;

                numIter++;
            }
            while ((distanciaEntrePuntos > ErrorMaximoPermitido) && (numIter < NumIteracionesMax));
            
            Estado.Instance.CentroPupila = puntoActual;

        }

        protected override void Presentacion()
        {
            Estado.Instance.ImagenAMostrar = Estado.Instance.ImagenBinarizada.Clone() as Bitmap;
            //marcamos el centro de la pupila
            Pintor.MarcaCentroPupila();
            Pintor.MarcaPuntoPupila();
        }

        private Vector2 CalculaPuntoNuevo(Vector2 puntoActual)
        {
            Bitmap imagenBinarizada = Estado.Instance.ImagenBinarizada;

            int filas = imagenBinarizada.Height;
            int columnas = imagenBinarizada.Width;

            //calculamos la distancia a los límites de la zona que ha quedado negra tras la binarización
            int fila = puntoActual.Y;
            int columna = puntoActual.X;

            while(columna < columnas)
            {
                if (imagenBinarizada.GetPixel(columna, fila).R > 0) break; 
                columna++;
            }

            int columnaDerecha = columna;

            columna = puntoActual.X;

            while (columna >= 0)
            {
                if (imagenBinarizada.GetPixel(columna, fila).R > 0)  break;
                columna--;
            }

            int columnaIzquierda = columna;

            columna = puntoActual.X;

            while (fila < filas)
            {
                if (imagenBinarizada.GetPixel(columna, fila).R > 0) break;
                fila++;
            }

            int filaAbajo = fila;

            fila = puntoActual.Y;

            while (fila >= 0)
            {
                if (imagenBinarizada.GetPixel(columna, fila).R > 0)  break;
                fila--;
            }

            int filaArriba = fila;

            //y nos aproximamos al punto central de la zona que ha quedado negra tras la binarización
            int mediaColumna = (columnaDerecha + columnaIzquierda) / 2;
            int mediaFila = (filaAbajo + filaArriba) / 2;

            return new Vector2(mediaColumna, mediaFila);
        }
    }
}