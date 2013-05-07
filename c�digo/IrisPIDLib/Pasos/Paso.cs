using System.Drawing;
namespace IrisPIDLib
{
    public abstract class Paso 
    {

        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public Paso PasoSiguiente { get; set; }
        public Fase Fase { get; set; }

        public void Ejecutar()
        {
            Logica();
            Presentacion();
        }

        public void EjecutarConsola()
        {
            Logica();
        }

        protected abstract void Logica();
        protected abstract void Presentacion();
    }
}
