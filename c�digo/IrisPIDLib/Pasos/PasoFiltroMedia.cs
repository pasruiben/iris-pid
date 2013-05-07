using AForge.Imaging.Filters;
using System.Drawing;

namespace IrisPIDLib
{
    public class PasoFiltroMedia : Paso
    {

        #region Singleton
        static readonly PasoFiltroMedia instance = new PasoFiltroMedia();

        static PasoFiltroMedia()
        {
        }

        public static PasoFiltroMedia Instance
        {
            get
            {
                return instance;
            }
        }
        #endregion

        PasoFiltroMedia()
        {
            base.Fase = FaseLocalizacionPupila.Instance;
            base.Nombre = "Filtro de la media";
            base.Descripcion = "Se realiza un filtro de media con el objetivo de suavizar ligeramente la imagen.";
            base.PasoSiguiente = PasoPuntoPupila.Instance;
        }

        protected override void Logica()
        {
            Mean filter = new Mean();
            Bitmap ImagenFiltroMedia = filter.Apply(Estado.Instance.ImagenInicial);
            Estado.Instance.ImagenFiltroMedia = ImagenFiltroMedia;            
        }

        protected override void Presentacion()
        {
            Estado.Instance.ImagenAMostrar = Estado.Instance.ImagenFiltroMedia.Clone() as Bitmap;
        }
    }
}
