using AForge.Imaging.Filters;
using System.Drawing;
using IrisPIDLib.Util;

namespace IrisPIDLib
{
    public class PasoFiltroGaussiano : Paso
    {
        int tamanoKernel = 27;
        int sigma = 10;

        #region Singleton
        static readonly PasoFiltroGaussiano instance = new PasoFiltroGaussiano();

        static PasoFiltroGaussiano()
        {
        }

        public static PasoFiltroGaussiano Instance
        {
            get
            {
                return instance;
            }
        }
        #endregion

        PasoFiltroGaussiano()
        {
            base.Fase = FaseLimiteIrisEsclerotica.Instance;
            base.Nombre = "Filtro Gaussiano";
            base.Descripcion = "Se aplica un filtro gaussiano a la imagen para suavizarla, eliminando los detalles.";
            base.PasoSiguiente = PasoPuntosGradienteIris.Instance;

            if (Estado.Instance.ParametrosCargados)
            {
                tamanoKernel = Parametros.Instance.tamanoKernelGauss;
                sigma = Parametros.Instance.sigmaGauss;
            }
        }

        protected override void Logica()
        {
            GaussianBlur filter = new GaussianBlur(tamanoKernel, sigma);
            Bitmap ImagenFiltroGaussiano = filter.Apply(Estado.Instance.ImagenInicial);
            Estado.Instance.ImagenFiltroGaussiano = ImagenFiltroGaussiano;
        }

        protected override void Presentacion()
        {
            Estado.Instance.ImagenAMostrar = Estado.Instance.ImagenFiltroGaussiano.Clone() as Bitmap;

            //marcamos la información relevante
            Pintor.MarcaCirculoInterno();
            Pintor.MarcaCirculoExterno();
        }
    }
}