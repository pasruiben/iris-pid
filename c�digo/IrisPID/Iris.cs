using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Resources;
using System.Reflection;
using IrisPIDLib.Util;
using IrisPIDLib;


namespace IrisPID
{
    public partial class Iris : Form
    {
        Bitmap img = null;

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Imágenes|*.bmp;*.png;*.jpg;*.gif";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                img = null;
                try
                {
                    img = Image.FromFile(dlg.FileName) as Bitmap;
                    img = AyudanteImagenes.PasarANoIndexada(img);
                }
                catch (Exception) { return; }

                this.infoToolStripStatusLabel.Text = "Imágen cargada";
                this.Text = "IrisPID - " + dlg.SafeFileName;

                Estado.Instance.Reset();

                Estado.Instance.ImagenInicial = img.Clone() as Bitmap;
                Estado.Instance.ImagenAMostrar = img.Clone() as Bitmap;

                Estado.Instance.PasoActual = PasoFiltroMedia.Instance;
                     
                ActualizarInfo();
            }
        }

        private void ActualizarInfo()
        {
            pictureBox.Image = Estado.Instance.ImagenAMostrar;

            if (!Estado.Instance.FinEjecucion)
            {
                faseTextBox.Text = Estado.Instance.PasoActual.Fase.Nombre;
                pasoTextBox.Text = Estado.Instance.PasoActual.Nombre;
                descTextBox.Text = Estado.Instance.PasoActual.Descripcion;
            }
            
            if (Estado.Instance.MensajeParada != null) infoToolStripStatusLabel.Text = Estado.Instance.MensajeParada;
        }


        private void AplicarButton_Click(object sender, EventArgs e)
        {
            if (Estado.Instance.FinEjecucion || img == null) return;

            Estado.Instance.PasoActual.Ejecutar();
            Estado.Instance.PasoActual = Estado.Instance.PasoActual.PasoSiguiente;

            ActualizarInfo();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void acercaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("IrisPID:\r\n"
                            + "Aplicación para localización del iris en imágenes en escala de gris usando los valores de intensidad.\r\n"
                            + "\r\nDesarrollada por:\r\n"
                            + "\tJuan Antonio Cano Salado\r\n"
                            + "\tBorja Moreno Fernández\r\n"
                            + "\tPascual Javier Ruiz Benítez\r\n");
        }

        private void aplicarCompletoButton_Click(object sender, EventArgs e)
        {
            if (Estado.Instance.FinEjecucion || img == null) return;

            while (!Estado.Instance.FinEjecucion)
            {
                Estado.Instance.PasoActual.Ejecutar();
                Estado.Instance.PasoActual = Estado.Instance.PasoActual.PasoSiguiente;
            } 
           
            ActualizarInfo();
        }
    }
}
