using System;
using System.Collections.Generic;
using System.Numerics;
using IrisPIDLib.Util;

namespace IrisPIDLib
{
    class PasoParabolaMinimosCuadradosInferior : PasoParabolaMinimosCuadrados
    {

        #region Singleton
        static readonly PasoParabolaMinimosCuadradosInferior instance = new PasoParabolaMinimosCuadradosInferior();

        static PasoParabolaMinimosCuadradosInferior()
        {
        }

        public static PasoParabolaMinimosCuadradosInferior Instance
        {
            get
            {
                return instance;
            }
        }
        #endregion

        PasoParabolaMinimosCuadradosInferior()
        {
            base.Fase = FaseLocalizacionParpados.Instance;
            base.Nombre = "Parabola mínimos cuadrados";
            base.Descripcion = "Si una cantidad considerable de puntos situados en la zona inferior del iris pasaron el filtro anterior, se calcula la parábola de mínimos cuadrados para estos puntos. El resultado será la aproximación al párpado inferior.";
            base.PasoSiguiente = PasoIrisDelimitado.Instance;
        }

        protected override void Presentacion()
        {
            if (Estado.Instance.ParpadoInferiorEncontrado && Estado.Instance.ParabolaMinimosCuadradosInferiorValida)
                Pintor.MarcaParabolaParpadoInferior();
        }

        protected override bool ParpadoEncontrado()
        {
            return Estado.Instance.ParpadoInferiorEncontrado;
        }

        protected override List<Vector2> MisPuntosGradiente()
        {
            return Estado.Instance.PuntosGradienteParpadoInferior;
        }

        protected override void ActualizaParabola(Parabola parabola)
        {
            if (parabola.a <= 0)
            {
                Estado.Instance.ParabolaParpadoInferior = parabola;
                Estado.Instance.ParabolaMinimosCuadradosInferiorValida = true;
            }
        }
    }
}
