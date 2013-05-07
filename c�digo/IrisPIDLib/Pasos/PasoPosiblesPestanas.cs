using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using IrisPIDLib.Util;

namespace IrisPIDLib
{
    public class PasoPosiblesPestanas : Paso
    {
        #region Singleton
        static readonly PasoPosiblesPestanas instance = new PasoPosiblesPestanas();

        static PasoPosiblesPestanas()
        {
        }

        public static PasoPosiblesPestanas Instance
        {
            get
            {
                return instance;
            }
        }
        #endregion

        List<Vector2> puntosPosiblesPestanas;
        readonly int LimiteOscuro = 100;

        PasoPosiblesPestanas()
        {
            base.Fase = FaseEliminacionPestanas.Instance;
            base.Nombre = "Localizar píxeles oscuros (posibles pestañas)";
            base.Descripcion = "Se localizan aquellos píxeles con una intensidad inferior a " + LimiteOscuro.ToString() + ", que serán candidatos a pertenecer a una pestaña, ya que las pestañas son más oscuras que el iris.";
            base.PasoSiguiente = PasoEliminaPestanas.Instance;            
        }

        protected override void Logica()
        {
            Bitmap imagenIrisDelimitado = Estado.Instance.ImagenIrisDelimitado;
            puntosPosiblesPestanas = new List<Vector2>();

            for (int x = 0; x < imagenIrisDelimitado.Width; x++)
            {
                for (int y = 0; y < imagenIrisDelimitado.Height; y++)
                {
                    Vector2 punto = new Vector2(x, y);

                    //si es lo suficientemente oscuro, lo marcamos como posible pestaña
                    if (imagenIrisDelimitado.GetPixel(x, y).R < LimiteOscuro)
                        puntosPosiblesPestanas.Add(punto);
                }
            }

            Estado.Instance.PuntosPosiblesPestanas = puntosPosiblesPestanas;
        }

        protected override void Presentacion()
        {
            Estado.Instance.ImagenAMostrar = Estado.Instance.ImagenIrisDelimitado.Clone() as Bitmap;

            Pintor.MarcaPosiblesPestanas();
        }
    }
}
