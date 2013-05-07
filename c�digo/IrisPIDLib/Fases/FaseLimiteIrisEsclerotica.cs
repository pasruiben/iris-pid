namespace IrisPIDLib
{
    class FaseLimiteIrisEsclerotica : Fase
    {
        #region Singleton
        static readonly FaseLimiteIrisEsclerotica instance = new FaseLimiteIrisEsclerotica();

        static FaseLimiteIrisEsclerotica()
        {
        }

        FaseLimiteIrisEsclerotica()
        {
            Nombre = "Detección del límite iris-esclerótica";
        }

        public static FaseLimiteIrisEsclerotica Instance
        {
            get
            {
                return instance;
            }
        }
        #endregion

    }
}
