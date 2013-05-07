using System.Drawing;
using System.Reflection;
using System;
using IrisPIDLib;
using IrisPIDLib.Util;


namespace IrisPID
{
    partial class Iris
    {
        readonly String ParametrosXML = "Parámetros.xml";

        public Iris()
        {
            InitializeComponent();
            
            CargarParametros();
        }

        private void CargarParametros()
        {
            try
            {
                Parametros.Instance = Serializacion.DesSerializar<Parametros>(ParametrosXML);
                Estado.Instance.ParametrosCargados = true;

                infoToolStripStatusLabel.Text = "Parámetros cargados";
            }
            catch (Exception) 
            { 
                Estado.Instance.ParametrosCargados = false;
                infoToolStripStatusLabel.Text = "Parámetros por defecto";

            }
        }

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.menuPrincipal = new System.Windows.Forms.MenuStrip();
            this.archivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.abrirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ayudaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.acercaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.controlPrincipal = new System.Windows.Forms.GroupBox();
            this.aplicarCompletoButton = new System.Windows.Forms.Button();
            this.descTextBox = new System.Windows.Forms.TextBox();
            this.pasoTextBox = new System.Windows.Forms.TextBox();
            this.faseTextBox = new System.Windows.Forms.TextBox();
            this.aplicarButton = new System.Windows.Forms.Button();
            this.descLabel = new System.Windows.Forms.Label();
            this.pasoLabel = new System.Windows.Forms.Label();
            this.faseLabel = new System.Windows.Forms.Label();
            this.statusPrincipal = new System.Windows.Forms.StatusStrip();
            this.infoToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.menuPrincipal.SuspendLayout();
            this.controlPrincipal.SuspendLayout();
            this.statusPrincipal.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox
            // 
            this.pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox.Location = new System.Drawing.Point(545, 41);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(320, 280);
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            // 
            // menuPrincipal
            // 
            this.menuPrincipal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archivoToolStripMenuItem,
            this.ayudaToolStripMenuItem});
            this.menuPrincipal.Location = new System.Drawing.Point(0, 0);
            this.menuPrincipal.Name = "menuPrincipal";
            this.menuPrincipal.Size = new System.Drawing.Size(904, 24);
            this.menuPrincipal.TabIndex = 1;
            this.menuPrincipal.Text = "Menu";
            // 
            // archivoToolStripMenuItem
            // 
            this.archivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.abrirToolStripMenuItem,
            this.salirToolStripMenuItem});
            this.archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            this.archivoToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.archivoToolStripMenuItem.Text = "Archivo";
            // 
            // abrirToolStripMenuItem
            // 
            this.abrirToolStripMenuItem.Name = "abrirToolStripMenuItem";
            this.abrirToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.abrirToolStripMenuItem.Text = "Abrir";
            this.abrirToolStripMenuItem.Click += new System.EventHandler(this.abrirToolStripMenuItem_Click);
            // 
            // salirToolStripMenuItem
            // 
            this.salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            this.salirToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.salirToolStripMenuItem.Text = "Salir";
            this.salirToolStripMenuItem.Click += new System.EventHandler(this.salirToolStripMenuItem_Click);
            // 
            // ayudaToolStripMenuItem
            // 
            this.ayudaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.acercaToolStripMenuItem});
            this.ayudaToolStripMenuItem.Name = "ayudaToolStripMenuItem";
            this.ayudaToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.ayudaToolStripMenuItem.Text = "Ayuda";
            // 
            // acercaToolStripMenuItem
            // 
            this.acercaToolStripMenuItem.Name = "acercaToolStripMenuItem";
            this.acercaToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.acercaToolStripMenuItem.Text = "Acerca";
            this.acercaToolStripMenuItem.Click += new System.EventHandler(this.acercaToolStripMenuItem_Click);
            // 
            // controlPrincipal
            // 
            this.controlPrincipal.Controls.Add(this.aplicarCompletoButton);
            this.controlPrincipal.Controls.Add(this.descTextBox);
            this.controlPrincipal.Controls.Add(this.pasoTextBox);
            this.controlPrincipal.Controls.Add(this.faseTextBox);
            this.controlPrincipal.Controls.Add(this.aplicarButton);
            this.controlPrincipal.Controls.Add(this.descLabel);
            this.controlPrincipal.Controls.Add(this.pasoLabel);
            this.controlPrincipal.Controls.Add(this.faseLabel);
            this.controlPrincipal.Location = new System.Drawing.Point(12, 27);
            this.controlPrincipal.Name = "controlPrincipal";
            this.controlPrincipal.Size = new System.Drawing.Size(494, 308);
            this.controlPrincipal.TabIndex = 2;
            this.controlPrincipal.TabStop = false;
            // 
            // aplicarCompletoButton
            // 
            this.aplicarCompletoButton.Location = new System.Drawing.Point(291, 266);
            this.aplicarCompletoButton.Name = "aplicarCompletoButton";
            this.aplicarCompletoButton.Size = new System.Drawing.Size(96, 28);
            this.aplicarCompletoButton.TabIndex = 10;
            this.aplicarCompletoButton.Text = "Aplicar Completo";
            this.aplicarCompletoButton.UseVisualStyleBackColor = true;
            this.aplicarCompletoButton.Click += new System.EventHandler(this.aplicarCompletoButton_Click);
            // 
            // descTextBox
            // 
            this.descTextBox.BackColor = System.Drawing.Color.White;
            this.descTextBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.descTextBox.Location = new System.Drawing.Point(106, 92);
            this.descTextBox.Multiline = true;
            this.descTextBox.Name = "descTextBox";
            this.descTextBox.ReadOnly = true;
            this.descTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.descTextBox.Size = new System.Drawing.Size(358, 145);
            this.descTextBox.TabIndex = 9;
            // 
            // pasoTextBox
            // 
            this.pasoTextBox.BackColor = System.Drawing.Color.White;
            this.pasoTextBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.pasoTextBox.Location = new System.Drawing.Point(106, 54);
            this.pasoTextBox.Name = "pasoTextBox";
            this.pasoTextBox.ReadOnly = true;
            this.pasoTextBox.Size = new System.Drawing.Size(358, 20);
            this.pasoTextBox.TabIndex = 8;
            // 
            // faseTextBox
            // 
            this.faseTextBox.BackColor = System.Drawing.Color.White;
            this.faseTextBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.faseTextBox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.faseTextBox.Location = new System.Drawing.Point(106, 16);
            this.faseTextBox.Name = "faseTextBox";
            this.faseTextBox.ReadOnly = true;
            this.faseTextBox.Size = new System.Drawing.Size(358, 20);
            this.faseTextBox.TabIndex = 7;
            // 
            // aplicarButton
            // 
            this.aplicarButton.Location = new System.Drawing.Point(137, 266);
            this.aplicarButton.Name = "aplicarButton";
            this.aplicarButton.Size = new System.Drawing.Size(96, 28);
            this.aplicarButton.TabIndex = 1;
            this.aplicarButton.Text = "Aplicar Paso";
            this.aplicarButton.UseVisualStyleBackColor = true;
            this.aplicarButton.Click += new System.EventHandler(this.AplicarButton_Click);
            // 
            // descLabel
            // 
            this.descLabel.AutoSize = true;
            this.descLabel.Location = new System.Drawing.Point(22, 92);
            this.descLabel.Name = "descLabel";
            this.descLabel.Size = new System.Drawing.Size(66, 13);
            this.descLabel.TabIndex = 2;
            this.descLabel.Text = "Descripción:";
            // 
            // pasoLabel
            // 
            this.pasoLabel.AutoSize = true;
            this.pasoLabel.Location = new System.Drawing.Point(22, 54);
            this.pasoLabel.Name = "pasoLabel";
            this.pasoLabel.Size = new System.Drawing.Size(34, 13);
            this.pasoLabel.TabIndex = 1;
            this.pasoLabel.Text = "Paso:";
            // 
            // faseLabel
            // 
            this.faseLabel.AutoSize = true;
            this.faseLabel.Location = new System.Drawing.Point(23, 16);
            this.faseLabel.Name = "faseLabel";
            this.faseLabel.Size = new System.Drawing.Size(33, 13);
            this.faseLabel.TabIndex = 0;
            this.faseLabel.Text = "Fase:";
            // 
            // statusPrincipal
            // 
            this.statusPrincipal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.infoToolStripStatusLabel});
            this.statusPrincipal.Location = new System.Drawing.Point(0, 340);
            this.statusPrincipal.Name = "statusPrincipal";
            this.statusPrincipal.Size = new System.Drawing.Size(904, 22);
            this.statusPrincipal.TabIndex = 3;
            // 
            // toolStripStatusLabel1
            // 
            this.infoToolStripStatusLabel.Name = "infoToolStripStatusLabel";
            this.infoToolStripStatusLabel.Size = new System.Drawing.Size(118, 17);
            this.infoToolStripStatusLabel.Text = "";
            // 
            // Iris
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(904, 362);
            this.Controls.Add(this.statusPrincipal);
            this.Controls.Add(this.controlPrincipal);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.menuPrincipal);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuPrincipal;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Iris";
            this.Text = "IrisPID";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.menuPrincipal.ResumeLayout(false);
            this.menuPrincipal.PerformLayout();
            this.controlPrincipal.ResumeLayout(false);
            this.controlPrincipal.PerformLayout();
            this.statusPrincipal.ResumeLayout(false);
            this.statusPrincipal.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        
        private System.Windows.Forms.PictureBox pictureBox;

        private System.Windows.Forms.Label descLabel;
        private System.Windows.Forms.Label pasoLabel;
        private System.Windows.Forms.Label faseLabel;
        
        private System.Windows.Forms.TextBox descTextBox;
        private System.Windows.Forms.TextBox pasoTextBox;
        private System.Windows.Forms.TextBox faseTextBox;

        private System.Windows.Forms.Button aplicarButton;
        private System.Windows.Forms.Button aplicarCompletoButton;

        private System.Windows.Forms.GroupBox controlPrincipal;
        private System.Windows.Forms.StatusStrip statusPrincipal;
        private System.Windows.Forms.MenuStrip menuPrincipal;

        private System.Windows.Forms.ToolStripMenuItem archivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem abrirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ayudaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem acercaToolStripMenuItem;

        private System.Windows.Forms.ToolStripStatusLabel infoToolStripStatusLabel;
        
    }
}

