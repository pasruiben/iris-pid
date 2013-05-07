using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IrisPIDLib
{
    [Serializable]
    public class Parametros
    {
        #region Singleton
        static Parametros instance = new Parametros();

        static Parametros() { }
        Parametros() { }

        public static Parametros Instance
        {
            get
            {
                return instance;
            }

            set
            {
                instance = value;
            }
        }
        #endregion

        public int CorteBinarizacion { get; set; }
        public int tamanoKernelGauss { get; set; }
        public int sigmaGauss { get; set; }

    }
}
