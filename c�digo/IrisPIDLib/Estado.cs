using System.Drawing;
using System.Collections.Generic;

namespace IrisPIDLib
{
    public sealed class Estado
    {

        #region Singleton
        static readonly Estado instance = new Estado();

        Estado()
        {
        }

        static Estado()
        {
        }

        public static Estado Instance
        {
            get
            {
                return instance;
            }
        }
        #endregion

        public void Reset()
        {
            instance.PasoActual = null;
            
            instance.ImagenInicial = null;
            instance.ImagenAMostrar = null;
            instance.ImagenFinal = null;

            instance.ImagenBinarizada = null;
            instance.ImagenFiltroMedia = null;
            instance.ImagenFiltroGaussiano = null;
            instance.ImagenIrisDelimitado = null;

            instance.PuntoPupila = null;
            instance.CentroPupila = null;

            instance.PuntosGradienteIrisEsclerotica = null;
            
            instance.ParabolaVirtualSuperior = null;
            instance.ParabolaVirtualInferior = null;

            instance.ParabolaParpadoSuperior = null;
            instance.ParabolaParpadoInferior = null;

            instance.PuntosGradienteParpadoSuperior = null;
            instance.PuntosGradienteParpadoInferior = null;

            instance.ParpadoSuperiorEncontrado = false;
            instance.ParpadoInferiorEncontrado = false;

            instance.PuntosPosiblesPestanas = null;

            instance.MensajeParada = null;
            instance.FinEjecucion = false;

            instance.ParabolaMinimosCuadradosInferiorValida = false;
            instance.ParabolaMinimosCuadradosSuperiorValida = false;
        }

        public Paso PasoActual { get; set; }

        public Bitmap ImagenInicial { get; set; }
        public Bitmap ImagenAMostrar { get; set; }
        public Bitmap ImagenFinal { get; set; }

        public Bitmap ImagenBinarizada { get; set; }
        public Bitmap ImagenFiltroMedia { get; set; }
        public Bitmap ImagenFiltroGaussiano { get; set; }
        public Bitmap ImagenIrisDelimitado { get; set; }

        public Vector2 PuntoPupila { get; set; }
        public Vector2 CentroPupila { get; set; }

        public float RadioPupila { get; set; }
        public float RadioCirculoInterno { get; set; }
        public float RadioCirculoExterno { get; set; }
        public float RadioIris { get; set; }

        public List<Vector2> PuntosGradienteIrisEsclerotica { get; set; }

        public Parabola ParabolaVirtualSuperior { get; set; }
        public Parabola ParabolaVirtualInferior { get; set; }

        public Parabola ParabolaParpadoSuperior { get; set; }
        public Parabola ParabolaParpadoInferior { get; set; }

        public List<Vector2> PuntosGradienteParpadoSuperior { get; set; }
        public List<Vector2> PuntosGradienteParpadoInferior { get; set; }

        public bool ParpadoSuperiorEncontrado { get; set; }
        public bool ParpadoInferiorEncontrado { get; set; }

        public List<Vector2> PuntosPosiblesPestanas { get; set; }

        public bool ParametrosCargados { get; set; }

        public bool ParabolaMinimosCuadradosInferiorValida { get; set; }
        public bool ParabolaMinimosCuadradosSuperiorValida { get; set; }

        public string MensajeParada { get; set; }
        public bool FinEjecucion { get; set; }
    }
}
