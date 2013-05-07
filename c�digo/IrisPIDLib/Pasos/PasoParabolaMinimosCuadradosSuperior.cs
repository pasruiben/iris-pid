using System;
using System.Collections.Generic;
using System.Numerics;
using IrisPIDLib.Util;

namespace IrisPIDLib
{
    class PasoParabolaMinimosCuadradosSuperior : PasoParabolaMinimosCuadrados
    {

        #region Singleton
        static readonly PasoParabolaMinimosCuadradosSuperior instance = new PasoParabolaMinimosCuadradosSuperior();

        static PasoParabolaMinimosCuadradosSuperior()
        {
        }

        public static PasoParabolaMinimosCuadradosSuperior Instance
        {
            get
            {
                return instance;
            }
        }
        #endregion

        PasoParabolaMinimosCuadradosSuperior()
        {
            base.Fase = FaseLocalizacionParpados.Instance;
            base.Nombre = "Parábola mínimos cuadrados superior";
            base.Descripcion = "Si una cantidad considerable de puntos situados en la zona superior del iris pasaron el filtro anterior, se calcula la parábola de mínimos cuadrados para estos puntos. El resultado será la aproximación al párpado superior.";
            base.PasoSiguiente = PasoParabolaMinimosCuadradosInferior.Instance;
        }

        protected override void Presentacion()
        {
            if (Estado.Instance.ParpadoSuperiorEncontrado && Estado.Instance.ParabolaMinimosCuadradosSuperiorValida)
                Pintor.MarcaParabolaParpadoSuperior();
        }

        protected override bool ParpadoEncontrado()
        {
            return Estado.Instance.ParpadoSuperiorEncontrado;
        }

        protected override List<Vector2> MisPuntosGradiente()
        {
            return Estado.Instance.PuntosGradienteParpadoSuperior;
        }

        protected override void ActualizaParabola(Parabola parabola)
        {
            if (parabola.a >= 0)
            {
                Estado.Instance.ParabolaParpadoSuperior = parabola;
                Estado.Instance.ParabolaMinimosCuadradosSuperiorValida = true;
            }
        }
    }
}
