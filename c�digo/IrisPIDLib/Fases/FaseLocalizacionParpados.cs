namespace IrisPIDLib
{
    class FaseLocalizacionParpados : Fase
    {
        #region Singleton
        static readonly FaseLocalizacionParpados instance = new FaseLocalizacionParpados();

        static FaseLocalizacionParpados()
        {
        }

        FaseLocalizacionParpados()
        {
            Nombre = "Localización de párpados";
        }

        public static FaseLocalizacionParpados Instance
        {
            get
            {
                return instance;
            }
        }
        #endregion

    }
}