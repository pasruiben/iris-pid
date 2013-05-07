using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using IrisPIDLib.Util;

namespace IrisPIDLib
{
    public class PasoEliminaPestanas : Paso
    {
        #region Singleton
        static readonly PasoEliminaPestanas instance = new PasoEliminaPestanas();

        static PasoEliminaPestanas()
        {
        }

        public static PasoEliminaPestanas Instance
        {
            get
            {
                return instance;
            }
        }
        #endregion

        readonly int SuficientesPuntos = 10;

        PasoEliminaPestanas()
        {
            base.Fase = FaseEliminacionPestanas.Instance;
            base.Nombre = "Eliminar pestañas en caso de que hubiera";
            base.Descripcion = "Si se detectó un número de píxeles oscuros superior a " + SuficientesPuntos.ToString() + " se considera que la imagen original tenía pestañas sobre el iris y se procede a la eliminación de estos píxeles oscuros. Con esto concluye el proceso, el iris ha quedado completamente delimitado y libre de pestañas.";
            base.PasoSiguiente = null;
        }

        protected override void Logica()
        {
            Bitmap imagenIrisDelimitado = Estado.Instance.ImagenIrisDelimitado;

            int numeroPuntos = Estado.Instance.PuntosPosiblesPestanas.Count;

            //si hay suficientes puntos
            if (numeroPuntos > SuficientesPuntos)
            {
                //los borramos
                foreach (Vector2 punto in Estado.Instance.PuntosPosiblesPestanas)
                {
                    imagenIrisDelimitado.SetPixel(punto.X, punto.Y, Color.White);
                }
            }

            Estado.Instance.ImagenIrisDelimitado = imagenIrisDelimitado;
            Estado.Instance.ImagenFinal = imagenIrisDelimitado;

            Estado.Instance.FinEjecucion = true;
        }

        protected override void Presentacion()
        {
            Estado.Instance.ImagenAMostrar = Estado.Instance.ImagenIrisDelimitado.Clone() as Bitmap;

            Estado.Instance.MensajeParada = "Fin";
        }
    }
}
