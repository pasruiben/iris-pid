using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using IrisPIDLib.Util;

namespace IrisPIDLib
{
    class PasoParabolaVirtualInferior : PasoParabolaVirtual
    {

        #region Singleton
        static readonly PasoParabolaVirtualInferior instance = new PasoParabolaVirtualInferior();

        static PasoParabolaVirtualInferior()
        {
        }

        public static PasoParabolaVirtualInferior Instance
        {
            get
            {
                return instance;
            }
        }
        #endregion

        PasoParabolaVirtualInferior()
        {
            base.Fase = FaseLocalizacionParpados.Instance;
            base.Nombre = "Detección de la parábola virtual Inferior";
            base.Descripcion = "La parábola virtual inferior pasa por el punto (CentroPupila.X - RadioIris, CentroPupila.Y) así como por el punto (CentroPupila.X + RadioIris, CentroPupila.Y). Tiene su vértice en el punto (CentroPupila.X, CentroPupila.Y + RadioPupila + " + incrementoVerticeParabola.ToString() + "). Buscaremos el párpado inferior en la región comprendida entre esta parábola y el semicírculo inferior del iris.";
            base.PasoSiguiente = PasoPuntosGradienteParpadoSuperior.Instance;
        }

        protected override void InicializaPuntos()
        {
            puntoParabola1 = new Vector2();
            puntoParabola2 = new Vector2();
            verticeParabola = new Vector2();

            puntoParabola1.X = Estado.Instance.CentroPupila.X + (int)Estado.Instance.RadioIris;
            puntoParabola1.Y = Estado.Instance.CentroPupila.Y;

            puntoParabola2.X = Estado.Instance.CentroPupila.X - (int)Estado.Instance.RadioIris;
            puntoParabola2.Y = Estado.Instance.CentroPupila.Y;

            verticeParabola.X = Estado.Instance.CentroPupila.X;
            verticeParabola.Y = Estado.Instance.CentroPupila.Y + (int)Estado.Instance.RadioPupila + incrementoVerticeParabola;
        }

        protected override void AsignaParabolaAEstado()
        {
            Estado.Instance.ParabolaVirtualInferior = parabola;
        }

        protected override void Presentacion()
        {
            base.Presentacion();

            Pintor.MarcaParabolaVirtualSuperior();
            Pintor.MarcaParabolaVirtualInferior();
        }
    }
}
