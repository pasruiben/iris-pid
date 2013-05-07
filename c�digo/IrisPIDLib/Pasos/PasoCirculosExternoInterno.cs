using AForge.Imaging.Filters;
using System.Drawing;
using System;
using IrisPIDLib.Util;

namespace IrisPIDLib
{
    public class PasoCirculosExternoInterno : Paso
    {

        readonly int SumaRadioPupila = 10;
        readonly int RestaCirculoExterno = 1;

        #region Singleton
        static readonly PasoCirculosExternoInterno instance = new PasoCirculosExternoInterno();

        static PasoCirculosExternoInterno()
        {
        }

        public static PasoCirculosExternoInterno Instance
        {
            get
            {
                return instance;
            }
        }
        #endregion

        PasoCirculosExternoInterno()
        {
            base.Fase = FaseLimiteIrisEsclerotica.Instance;
            base.Nombre = "Círculos interno y externo";
            base.Descripcion = "Se determinan dos círculos que delimitarán la region de la imagen en la que puede encontrarse el círculo del iris. Ambos están centrados en el centro de la pupila. El círculo externo es ligeramente superior al círculo de la pupila y el círculo externo es el más grande posible tal que esté contenido en los límites de la imagen.";
            base.PasoSiguiente = PasoFiltroGaussiano.Instance;
        }

        protected override void Logica()
        {
            Bitmap imagenInicial = Estado.Instance.ImagenInicial;

            //el círculo interno es inmediato
            Estado.Instance.RadioCirculoInterno = Estado.Instance.RadioPupila + SumaRadioPupila;

            //el círculo externo es el más grande posible con centro en la pupila y que quepa en la imagen
            int filas = imagenInicial.Height;
            int columnas = imagenInicial.Width;

            int distanciaHorizontal1 = Estado.Instance.CentroPupila.X;
            int distanciaVertical1 = Estado.Instance.CentroPupila.Y;
            int distanciaHorizontal2 = columnas - Estado.Instance.CentroPupila.X;
            int distanciaVertical2 = filas - Estado.Instance.CentroPupila.Y;

            int radioMinimo = Math.Min(Math.Min(distanciaHorizontal1, distanciaHorizontal2), Math.Min(distanciaVertical1, distanciaVertical2));
            
            Estado.Instance.RadioCirculoExterno = radioMinimo - RestaCirculoExterno;

        }

        protected override void Presentacion()
        {
            Estado.Instance.ImagenAMostrar = Estado.Instance.ImagenInicial.Clone() as Bitmap;

            //marcamos los dos círculos en la imagen a mostrar
            Pintor.MarcaCirculoInterno();
            Pintor.MarcaCirculoExterno();
        }
    }
}