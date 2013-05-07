using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using IrisPIDLib.Util;

namespace IrisPIDLib
{
    class PasoFiltroCandidatosGradiente : Paso
    {
        readonly int NumeroMinimoPuntosSup = 50;
        readonly int NumeroMinimoPuntosInf = 50;
        readonly int Limite = 5;

        #region Singleton
        static readonly PasoFiltroCandidatosGradiente instance = new PasoFiltroCandidatosGradiente();

        static PasoFiltroCandidatosGradiente()
        {
        }

        public static PasoFiltroCandidatosGradiente Instance
        {
            get
            {
                return instance;
            }
        }
        #endregion

        PasoFiltroCandidatosGradiente()
        {
            base.Fase = FaseLocalizacionParpados.Instance;
            base.Nombre = "Filtro a los candidatos con gradiente significativo";
            base.Descripcion = "De los puntos seleccionados anteriormente nos quedaremos sólo con aquellos que cumplan las tres condiciones siguientes:\r\n1. Tener un valor de intensidad inferior a 120.\r\n2. Tener un valor de intensidad muy similar al de alguno de sus vecinos situados a su izquierda.\r\n3. Tener un valor de intensidad muy similar al de alguno de sus vecinos situados a su derecha.";
            base.PasoSiguiente = PasoParabolaMinimosCuadradosSuperior.Instance;
        }

        protected override void Logica()
        {
            bool criterio1, criterio2, criterio3;

            Bitmap imagen = Estado.Instance.ImagenFiltroGaussiano;

            List<Vector2> nuevaListaPuntosSup = new List<Vector2>();
            List<Vector2> nuevaListaPuntosInf = new List<Vector2>();

            foreach (Vector2 p in Estado.Instance.PuntosGradienteParpadoSuperior)
            {
                byte intensidad = imagen.GetPixel(p.X, p.Y).R;

                criterio1 = intensidad < 130;

                byte? intensidadVecino1 = AyudanteImagenes.Intensidad(imagen, p.X - 1, p.Y - 1);
                byte? intensidadVecino2 = AyudanteImagenes.Intensidad(imagen, p.X - 1, p.Y);
                byte? intensidadVecino3 = AyudanteImagenes.Intensidad(imagen, p.X - 1, p.Y + 1);

                criterio2 = Similar(intensidad, intensidadVecino1) || Similar(intensidad, intensidadVecino2) || Similar(intensidad, intensidadVecino3);

                byte? intensidadVecino4 = AyudanteImagenes.Intensidad(imagen, p.X + 1, p.Y - 1);
                byte? intensidadVecino5 = AyudanteImagenes.Intensidad(imagen, p.X + 1, p.Y);
                byte? intensidadVecino6 = AyudanteImagenes.Intensidad(imagen, p.X + 1, p.Y + 1);

                criterio3 = Similar(intensidad, intensidadVecino4) || Similar(intensidad, intensidadVecino5) || Similar(intensidad, intensidadVecino6);

                if(criterio1 && criterio2 && criterio3) nuevaListaPuntosSup.Add(p);
            }

            
            foreach (Vector2 p in Estado.Instance.PuntosGradienteParpadoInferior)
            {
                byte intensidad = imagen.GetPixel(p.X, p.Y).R;

                criterio1 = intensidad < 160;

                byte? intensidadVecino1 = AyudanteImagenes.Intensidad(imagen, p.X - 1, p.Y - 1);
                byte? intensidadVecino2 = AyudanteImagenes.Intensidad(imagen, p.X - 1, p.Y);
                byte? intensidadVecino3 = AyudanteImagenes.Intensidad(imagen, p.X - 1, p.Y + 1);

                criterio2 = Similar(intensidad, intensidadVecino1) || Similar(intensidad, intensidadVecino2) || Similar(intensidad, intensidadVecino3);

                byte? intensidadVecino4 = AyudanteImagenes.Intensidad(imagen, p.X + 1, p.Y - 1);
                byte? intensidadVecino5 = AyudanteImagenes.Intensidad(imagen, p.X + 1, p.Y);
                byte? intensidadVecino6 = AyudanteImagenes.Intensidad(imagen, p.X + 1, p.Y + 1);

                criterio3 = Similar(intensidad, intensidadVecino4) || Similar(intensidad, intensidadVecino5) || Similar(intensidad, intensidadVecino6);

                if (criterio1 && criterio2 && criterio3) nuevaListaPuntosInf.Add(p);
            }

            Estado.Instance.PuntosGradienteParpadoSuperior = nuevaListaPuntosSup;
            Estado.Instance.PuntosGradienteParpadoInferior = nuevaListaPuntosInf;


            //si hay suficientes puntos, se ha detectado el párpado
            if (nuevaListaPuntosSup.Count > NumeroMinimoPuntosSup)
                Estado.Instance.ParpadoSuperiorEncontrado = true;
            else 
                Estado.Instance.ParpadoSuperiorEncontrado = false;

            if (nuevaListaPuntosInf.Count > NumeroMinimoPuntosInf)
                Estado.Instance.ParpadoInferiorEncontrado = true;
            else 
                Estado.Instance.ParpadoInferiorEncontrado = false;
        }

        protected override void Presentacion()
        {
            Estado.Instance.ImagenAMostrar = Estado.Instance.ImagenFiltroGaussiano.Clone() as Bitmap;

            Pintor.MarcaParabolaVirtualInferior();
            Pintor.MarcaParabolaVirtualSuperior();
            Pintor.MarcaCirculoIris();

            Pintor.MarcaPuntosGradienteParpadoSuperior();
            Pintor.MarcaPuntosGradienteParpadoInferior();
        }

        private bool Similar(byte intensidad1, byte? intensidad2)
        {
            bool res = false;

            if (intensidad2 != null)
            {
                res = Math.Abs(intensidad2.Value - intensidad1) < Limite;
            }

            return res;
        }
    }
}
