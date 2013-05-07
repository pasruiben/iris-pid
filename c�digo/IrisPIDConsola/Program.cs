using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using IrisPIDLib;
using IrisPIDLib.Util;
using System.Drawing;
using System.Diagnostics;

namespace IrisPIDConsola
{
    class Program
    {
        static readonly String Salida = "salida";
        static readonly String ParametrosXML = "Parámetros.xml";

        static void Main(string[] args)
        {

            Console.WriteLine("Introduce el nombre de la carpeta: ");
            string directorio = Console.ReadLine();

            if (EsCarpeta(directorio))
            {
                CargarParametros();

                Console.WriteLine("Comienza el análisis completo de " + directorio);

                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\" + Salida);

                Console.WriteLine("Carpeta con resultados: " + Salida);
                Console.WriteLine();

                LocalizaRecu(Directory.GetCurrentDirectory() + "\\" + directorio);
            }

            Console.WriteLine("Fin");
            Console.ReadLine();

        }

        static void LocalizaIris(string imagen)
        {
            Bitmap img = null;
            try
            {
                img = Image.FromFile(imagen) as Bitmap;
                img = AyudanteImagenes.PasarANoIndexada(img);
            }
            catch (Exception) { }

            if (img == null) return;

            Console.WriteLine("Procesando: " + Path.GetFileName(imagen));

            Estado.Instance.Reset();

            Estado.Instance.ImagenInicial = img.Clone() as Bitmap;
            Estado.Instance.ImagenFinal = Estado.Instance.ImagenInicial.Clone() as Bitmap;

            Estado.Instance.PasoActual = PasoFiltroMedia.Instance;

            while (!Estado.Instance.FinEjecucion)
            {
                Estado.Instance.PasoActual.EjecutarConsola();
                Estado.Instance.PasoActual = Estado.Instance.PasoActual.PasoSiguiente;
            }

            Image b = new Bitmap(img.Width * 2, img.Height);
            Graphics grfx = Graphics.FromImage(b);

            grfx.DrawImage(img, 0, 0);
            grfx.DrawImage(Estado.Instance.ImagenFinal, img.Width, 0);

            b.Save(Directory.GetCurrentDirectory() + "\\" + Salida + "\\" + Path.GetFileName(imagen) + ".png");
        }


        public static bool EsCarpeta(string ruta)
        {
            bool res = false;

            try
            {
                res = ((File.GetAttributes(ruta) & FileAttributes.Directory) == FileAttributes.Directory);
            }
            catch (Exception) { }

            return res;
        }

        static void LocalizaRecu(string ruta)
        {
            foreach (string file in Directory.GetFiles(ruta))
            {
                LocalizaIris(file);
            }

            foreach (string dir in Directory.GetDirectories(ruta))
            {
                if (!dir.Contains(".svn")) LocalizaRecu(dir);
            }
        }

        private static void CargarParametros()
        {
            /*Parametros.Instance.CorteBinarizacion = 100;
            Parametros.Instance.sigmaGauss = 10;
            Parametros.Instance.tamanoKernelGauss = 27;
            Serializacion.Serializar<Parametros>(ParametrosXML, Parametros.Instance);*/

            try
            {
                Parametros.Instance = Serializacion.DesSerializar<Parametros>(ParametrosXML);

                Estado.Instance.ParametrosCargados = true;

                Console.WriteLine("Parámetros cargados...");
            }
            catch (Exception) 
            {
                Console.WriteLine("Usando parámetros por defecto...");
                Estado.Instance.ParametrosCargados = false; 
            }

        }


    }
}
