using System.Drawing;
using IrisPIDLib.Util;
using AForge.Imaging.Filters;

namespace IrisPIDLib
{
    public class PasoPuntoPupila : Paso
    {

        #region Singleton
        static readonly PasoPuntoPupila instance = new PasoPuntoPupila();

        static PasoPuntoPupila()
        {
        }

        public static PasoPuntoPupila Instance
        {
            get
            {
                return instance;
            }
        }
        #endregion

        int CorteBinarizacion = 70;

        PasoPuntoPupila()
        {
            base.Fase = FaseLocalizacionPupila.Instance;
            base.Nombre = "Encontrar punto dentro de la pupila";
            base.Descripcion = "Se binariza la imagen y se localiza un punto cualquiera del interior de la pupila. La componente X de dicho punto se corresponde con la columna que más píxeles negros tiene en la imagen binarizada. La componente Y de dicho punto se corresponde con la fila que más píxeles negros tiene en la imagen binarizada.";
            base.PasoSiguiente = PasoCentroPupila.Instance;

            if (Estado.Instance.ParametrosCargados)
            {
                CorteBinarizacion = Parametros.Instance.CorteBinarizacion;
            }
        }

        protected override void Logica()
        {
            Threshold filter = new Threshold(CorteBinarizacion);
            Bitmap imagenBinarizada = filter.Apply(Grayscale.CommonAlgorithms.RMY.Apply(Estado.Instance.ImagenFiltroMedia));
            imagenBinarizada = AyudanteImagenes.PasarANoIndexada(imagenBinarizada);
            Estado.Instance.ImagenBinarizada = imagenBinarizada;

            int filas = imagenBinarizada.Height;
            int columnas = imagenBinarizada.Width;

            int filaSumaMinima, columnaSumaMinima, sumaActual, sumaMinima;

            sumaMinima = int.MaxValue;
            filaSumaMinima = 0;

            for (int fila = 0; fila < filas; fila++)
            {
                sumaActual = 0;
                for (int columna = 0; columna < columnas; columna++)
                {
                    //como es una imagen en blanco y negro nos da igual tomar la componente R, G o B
                    sumaActual += imagenBinarizada.GetPixel(columna, fila).R;
                }

                if (sumaActual < sumaMinima)
                {
                    sumaMinima = sumaActual;
                    filaSumaMinima = fila;
                }
            }

            sumaMinima = int.MaxValue;
            columnaSumaMinima = 0;

            for (int columna = 0; columna < columnas; columna++)
            {
                sumaActual = 0;
                for (int fila = 0; fila < filas; fila++)
                {
                    sumaActual += imagenBinarizada.GetPixel(columna, fila).R;
                }

                if (sumaActual < sumaMinima)
                {
                    sumaMinima = sumaActual;
                    columnaSumaMinima = columna;
                }
            }

            //ya tenemos un punto dentro de la pupila
            Estado.Instance.PuntoPupila = new Vector2(columnaSumaMinima, filaSumaMinima);
        }

        protected override void Presentacion()
        {
            Estado.Instance.ImagenAMostrar = Estado.Instance.ImagenBinarizada.Clone() as Bitmap;

            //marcamos el punto dentro de la pupila
            Pintor.MarcaPuntoPupila();
        }
    }
}