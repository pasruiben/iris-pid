namespace IrisPIDLib
{
    class FaseEliminacionPestanas : Fase
    {
        #region Singleton
        static readonly FaseEliminacionPestanas instance = new FaseEliminacionPestanas();

        static FaseEliminacionPestanas()
        {
        }

        FaseEliminacionPestanas()
        {
            Nombre = "Eliminación de pestañas";
        }

        public static FaseEliminacionPestanas Instance
        {
            get
            {
                return instance;
            }
        }
        #endregion

    }
}