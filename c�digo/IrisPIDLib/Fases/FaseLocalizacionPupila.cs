namespace IrisPIDLib
{
    class FaseLocalizacionPupila : Fase
    {
        #region Singleton
        static readonly FaseLocalizacionPupila instance = new FaseLocalizacionPupila();

        static FaseLocalizacionPupila()
        {
        }

        FaseLocalizacionPupila()
        {
            Nombre = "Localización de la pupila";
        }

        public static FaseLocalizacionPupila Instance
        {
            get
            {
                return instance;
            }
        }
        #endregion

    }
}
