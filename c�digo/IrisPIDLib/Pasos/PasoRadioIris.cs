using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using IrisPIDLib.Util;

namespace IrisPIDLib
{
    public class PasoRadioIris : Paso
    {
        #region Singleton
        static readonly PasoRadioIris instance = new PasoRadioIris();

        static PasoRadioIris()
        {
        }

        public static PasoRadioIris Instance
        {
            get
            {
                return instance;
            }
        }
        #endregion

        readonly int AnchoBanda = 16;

        PasoRadioIris()
        {
            base.Fase = FaseLimiteIrisEsclerotica.Instance;
            base.Nombre = "Detección del radio del iris";
            base.Descripcion = "Se calcula la banda de anchura " + AnchoBanda.ToString() + " que agrupa mayor cantidad de puntos de gradiente significativo. El radio del iris será la media de las distancias de los puntos comprendidos en la banda al centro de la pupila.";
            base.PasoSiguiente = PasoParabolaVirtualSuperior.Instance;
        }

        protected override void Logica()
        {
            int radioMinimo = (int)Math.Round(Estado.Instance.RadioCirculoInterno);
            int radioMaximo = (int)Math.Round(Estado.Instance.RadioCirculoExterno);

            List<Vector2> puntos = Estado.Instance.PuntosGradienteIrisEsclerotica;

            if (puntos.Count == 0)
            {
                Estado.Instance.FinEjecucion = true;
                return;
            }

            int sumaMaxima = 0, radioSumaMaxima = 0;

            for (int i = radioMinimo; i < radioMaximo; i++)
            {
                int suma = NumeroValoresEnBanda(puntos, i);

                if (suma > sumaMaxima)
                {
                    sumaMaxima = suma;
                    radioSumaMaxima = i;
                }
            }

            int radioIris = MediaValoresEnBanda(puntos, radioSumaMaxima);

            Estado.Instance.RadioIris = radioIris;

        }

        protected override void Presentacion()
        {
            if (Estado.Instance.FinEjecucion)
            {
                Estado.Instance.MensajeParada = "No se detectaron puntos con gradiente significativo que puedan delimitar el iris, el proceso no puede continuar.";
                return;
            }

            Estado.Instance.ImagenAMostrar = Estado.Instance.ImagenInicial.Clone() as Bitmap;

            Pintor.MarcaCirculoPupila();
            Pintor.MarcaCirculoInterno();
            Pintor.MarcaCirculoExterno();
            Pintor.MarcaCirculoIris();
        }

        //cuenta el número de valores en 'lista' que entran en la banda centrada en 'centroBanda'
        private int NumeroValoresEnBanda(List<Vector2> lista, int centroBanda)
        {
            int min = centroBanda - AnchoBanda / 2;
            int max = centroBanda + AnchoBanda / 2;

            int cuenta = 0;

            foreach (Vector2 p in lista)
            {
                int distancia = (int) p.Distancia(Estado.Instance.CentroPupila);
                if (distancia >= min && distancia <= max)
                    cuenta++;
            }

            return cuenta;
        }

        //calcula la media de los valores en 'lista' que entran en la banda centrada en 'centroBanda'
        private int MediaValoresEnBanda(List<Vector2> lista, int centroBanda)
        {
            int min = centroBanda - AnchoBanda / 2;
            int max = centroBanda + AnchoBanda / 2;

            int cuenta = 0, suma = 0;

            foreach (Vector2 p in lista)
            {
                int distancia = (int)p.Distancia(Estado.Instance.CentroPupila);
                if (distancia >= min && distancia <= max)
                {
                    cuenta++;
                    suma += distancia;
                }
            }

            return (int)Math.Round((double)suma / cuenta);
        }
    }
}
