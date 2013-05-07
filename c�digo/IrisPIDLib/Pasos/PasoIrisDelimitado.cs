using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using IrisPIDLib.Util;

namespace IrisPIDLib
{
    public class PasoIrisDelimitado : Paso
    {        
        #region Singleton
        static readonly PasoIrisDelimitado instance = new PasoIrisDelimitado();

        static PasoIrisDelimitado()
        {
        }

        public static PasoIrisDelimitado Instance
        {
            get
            {
                return instance;
            }
        }
        #endregion

        PasoIrisDelimitado()
        {
            base.Fase = FaseLocalizacionParpados.Instance;
            base.Nombre = "Delimitación completa del iris (salvo pestañas)";
            base.Descripcion = "Llegados a este punto ya conocemos la localización de la pupila, el iris y los párpados. Eliminamos el resto de la imagen pues contiene información que no nos interesa.";
            base.PasoSiguiente = PasoPosiblesPestanas.Instance;
        }

        protected override void Logica()
        {
            Bitmap imagenIrisDelimitado = Estado.Instance.ImagenInicial.Clone() as Bitmap;

            //hay que poner en blanco todos los puntos que no pertenezcan al iris
            //para ello usaremos los círculos de pupila e iris, y las parábolas de los párpados

            for (int x = 0; x < imagenIrisDelimitado.Width; x++)
            {
                for (int y = 0; y < imagenIrisDelimitado.Height; y++)
                {
                    //si está dentro del círculo de la pupila lo eliminamos
                    if (
                        DentroCirculoPupila(x, y) ||
                        FueraCirculoIris(x, y) ||
                        SobreParpadoSuperior(x, y) ||
                        BajoParpadoInferior(x, y)
                        )
                        imagenIrisDelimitado.SetPixel(x, y, Color.White);
                }
            }

            Estado.Instance.ImagenIrisDelimitado = imagenIrisDelimitado;
        }

        protected override void Presentacion()
        {
            Estado.Instance.ImagenAMostrar = Estado.Instance.ImagenIrisDelimitado.Clone() as Bitmap;

            Pintor.MarcaCirculoPupila();
            Pintor.MarcaCirculoIris();
            Pintor.MarcaParabolaParpadoSuperior();
            Pintor.MarcaParabolaParpadoInferior();
        }

        private bool DentroCirculoPupila(int x, int y)
        {
            Vector2 punto = new Vector2(x, y);
            double distanciaCentro = Estado.Instance.CentroPupila.Distancia(punto);

            return distanciaCentro < Estado.Instance.RadioPupila;
        }

        private bool FueraCirculoIris(int x, int y)
        {
            Vector2 punto = new Vector2(x, y);
            double distanciaCentro = Estado.Instance.CentroPupila.Distancia(punto);

            return distanciaCentro > Estado.Instance.RadioIris;
        }

        private bool SobreParpadoSuperior(int x, int y)
        {
            if (!Estado.Instance.ParabolaMinimosCuadradosSuperiorValida)
                return false;

            Vector2 punto = new Vector2(x, y);

            double yParpado = Estado.Instance.ParabolaParpadoSuperior.Valor(x);

            return y < yParpado;
        }

        private bool BajoParpadoInferior(int x, int y)
        {
            if (!Estado.Instance.ParabolaMinimosCuadradosInferiorValida)
                return false;

            Vector2 punto = new Vector2(x, y);

            double yParpado = Estado.Instance.ParabolaParpadoInferior.Valor(x);

            return y > yParpado;
        }
    }
}
