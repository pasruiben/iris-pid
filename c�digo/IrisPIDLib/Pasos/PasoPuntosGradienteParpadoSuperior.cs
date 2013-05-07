using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using IrisPIDLib.Util;

namespace IrisPIDLib
{
    class PasoPuntosGradienteParpadoSuperior : PasoPuntosGradienteParpado
    {
        #region Singleton
        static readonly PasoPuntosGradienteParpadoSuperior instance = new PasoPuntosGradienteParpadoSuperior();

        static PasoPuntosGradienteParpadoSuperior()
        {
        }

        public static PasoPuntosGradienteParpadoSuperior Instance
        {
            get
            {
                return instance;
            }
        }
        #endregion

        PasoPuntosGradienteParpadoSuperior()
        {
            base.Fase = FaseLocalizacionParpados.Instance;
            base.Nombre = "Puntos gradiente parpado superior";
            base.Descripcion = "Se localizan los puntos de mayor gradiente de intensidad en la región delimitada entre la parábola virtual superior y el semicírculo superior del iris. El cálculo del gradiente de intensidad se lleva a cabo en dirección vertical.";
            base.PasoSiguiente = PasoPuntosGradienteParpadoInferior.Instance;

            limiteInferior = 4;
        }

        protected override void Logica()
        {
            puntosGradiente = new List<Vector2>();

            Vector2 centroIris = Estado.Instance.CentroPupila;
            float radioIris = Estado.Instance.RadioIris;

            for (int puntoX = (int)(centroIris.X - radioIris); puntoX <= centroIris.X + radioIris; puntoX += 2)
            {
                int parabolaY = (int)MiParabolaVirtual().Valor(puntoX);

                float distancia = (float)Math.Sqrt(Math.Pow(radioIris, 2) - Math.Pow(puntoX - centroIris.X, 2));
                int circuloIrisY = centroIris.Y - (int)distancia;

                for (int puntoY = circuloIrisY + 1 ; puntoY <= parabolaY - 1 ; puntoY++)
                {
                    EncuentraPuntosGradiente(puntoX, puntoY);
                }
            }

            Estado.Instance.PuntosGradienteParpadoSuperior = puntosGradiente;
        }

        protected override Parabola MiParabolaVirtual()
        {
            return Estado.Instance.ParabolaVirtualSuperior;
        }

        protected override void Presentacion()
        {
            Estado.Instance.ImagenAMostrar = Estado.Instance.ImagenFiltroGaussiano.Clone() as Bitmap;

            Pintor.MarcaParabolaVirtualInferior();
            Pintor.MarcaParabolaVirtualSuperior();
            Pintor.MarcaCirculoIris();
            Pintor.MarcaPuntosGradienteParpadoSuperior();
        }
    }
}
