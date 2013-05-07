using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace IrisPIDLib.Util
{
    public static class Pintor
    {

        public static void MarcaPuntoPupila()
        {
            MarcaPuntoCirculo(Estado.Instance.ImagenAMostrar, Estado.Instance.PuntoPupila, Color.Red);
        }

        public static void MarcaCentroPupila()
        {
            MarcaPuntoCirculo(Estado.Instance.ImagenAMostrar, Estado.Instance.CentroPupila, Color.Yellow);
        }

        public static void MarcaCirculoPupila()
        {
            MarcaCirculo(Estado.Instance.ImagenAMostrar, Estado.Instance.CentroPupila, Estado.Instance.RadioPupila, Color.Red);
        }

        public static void MarcaCirculoIris()
        {
            MarcaCirculo(Estado.Instance.ImagenAMostrar, Estado.Instance.CentroPupila, Estado.Instance.RadioIris, Color.DarkOrange);
        }

        public static void MarcaCirculoInterno()
        {
            MarcaCirculo(Estado.Instance.ImagenAMostrar, Estado.Instance.CentroPupila, Estado.Instance.RadioCirculoInterno, Color.Green);
        }

        public static void MarcaCirculoExterno()
        {
            MarcaCirculo(Estado.Instance.ImagenAMostrar, Estado.Instance.CentroPupila, Estado.Instance.RadioCirculoExterno, Color.Blue);
        }

        public static void MarcaParabolaVirtualSuperior()
        {
            for (int i = 0; i < Estado.Instance.ImagenAMostrar.Width; i++)
            {
                int y = (int)Estado.Instance.ParabolaVirtualSuperior.Valor(i);
                MarcaPuntoReal(Estado.Instance.ImagenAMostrar, new Vector2(i, y), Color.LemonChiffon);
            }
        }

        public static void MarcaParabolaVirtualInferior()
        {
            for (int i = 0; i < Estado.Instance.ImagenAMostrar.Width; i++)
            {
                int y = (int)Estado.Instance.ParabolaVirtualInferior.Valor(i);
                MarcaPuntoReal(Estado.Instance.ImagenAMostrar, new Vector2(i, y), Color.LemonChiffon);
            }
        }

        public static void MarcaParabolaParpadoSuperior()
        {
            if (Estado.Instance.ParabolaParpadoSuperior == null) return;

            for (int i = 0; i < Estado.Instance.ImagenAMostrar.Width; i++)
            {
                int y = (int)Estado.Instance.ParabolaParpadoSuperior.Valor(i);
                MarcaPuntoReal(Estado.Instance.ImagenAMostrar, new Vector2(i, y), Color.Green);
            }
        }

        public static void MarcaParabolaParpadoInferior()
        {
            if (Estado.Instance.ParabolaParpadoInferior == null) return;

            for (int i = 0; i < Estado.Instance.ImagenAMostrar.Width; i++)
            {
                int y = (int)Estado.Instance.ParabolaParpadoInferior.Valor(i);
                MarcaPuntoReal(Estado.Instance.ImagenAMostrar, new Vector2(i, y), Color.Green);
            }
        }

        public static void MarcaPuntosGradienteParpadoSuperior()
        {
            foreach (Vector2 punto in Estado.Instance.PuntosGradienteParpadoSuperior)
                MarcaPuntoReal(Estado.Instance.ImagenAMostrar, punto, Color.Blue);
        }

        public static void MarcaPuntosGradienteParpadoInferior()
        {
            foreach (Vector2 punto in Estado.Instance.PuntosGradienteParpadoInferior)
                MarcaPuntoReal(Estado.Instance.ImagenAMostrar, punto, Color.Blue);
        }

        public static void MarcaPosiblesPestanas()
        {
            foreach (Vector2 punto in Estado.Instance.PuntosPosiblesPestanas)
                MarcaPuntoReal(Estado.Instance.ImagenAMostrar, punto, Color.Yellow);
        }

        #region Helpers

        public static void MarcaPuntoCirculo(Bitmap imagen, Vector2 punto, Color color)
        {
            int radio = 2;
            Graphics.FromImage(imagen).DrawEllipse(new Pen(color), punto.X - radio, punto.Y - radio, radio * 2, radio * 2);
        }

        public static void MarcaCirculo(Bitmap imagen, Vector2 centro, float radio, Color color)
        {
            Graphics.FromImage(imagen).DrawEllipse(new Pen(color), centro.X - radio, centro.Y - radio, radio * 2, radio * 2);
        }

        public static void MarcaPuntoReal(Bitmap imagen, Vector2 punto, Color color)
        {
            if (punto.X >= 0 && punto.X < imagen.Width && punto.Y >= 0 && punto.Y < imagen.Height)
                imagen.SetPixel(punto.X, punto.Y, color);
        }

        #endregion

    }
}
