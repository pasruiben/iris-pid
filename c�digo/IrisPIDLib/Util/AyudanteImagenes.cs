using System.Drawing;
using System.Drawing.Imaging;

namespace IrisPIDLib.Util
{
    public static class AyudanteImagenes
    {
        public static Bitmap PasarANoIndexada(Bitmap b)
        {
            Bitmap newBmp = new Bitmap(b.Width, b.Height, PixelFormat.Format32bppRgb);
            Graphics.FromImage(newBmp).DrawImage(b, 0, 0);

            return newBmp;
        }

        public static byte? Intensidad(Bitmap imagen, int x, int y)
        {
            byte? res = null;
            int width = imagen.Width;
            int height = imagen.Height;

            if (x >= 0 && x < imagen.Width && y >= 0 && y < imagen.Height) res = imagen.GetPixel(x, y).R;

            return res;
        }
    }
}
