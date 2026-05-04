namespace ReadyToRead
{
    partial class usRecensione
    {
        /// <summary> 
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione componenti

        /// <summary> 
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare 
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(usRecensione));
            this.rtbTestoRecensione = new System.Windows.Forms.RichTextBox();
            this.pbValutazioneRecensione = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblNomeUtente = new System.Windows.Forms.Label();
            this.pbFotoProfilo = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbValutazioneRecensione)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbFotoProfilo)).BeginInit();
            this.SuspendLayout();
            // 
            // rtbTestoRecensione
            // 
            this.rtbTestoRecensione.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(242)))), ((int)(((byte)(229)))));
            this.rtbTestoRecensione.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbTestoRecensione.Font = new System.Drawing.Font("Coolvetica", 12F);
            this.rtbTestoRecensione.Location = new System.Drawing.Point(7, 91);
            this.rtbTestoRecensione.Name = "rtbTestoRecensione";
            this.rtbTestoRecensione.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtbTestoRecensione.Size = new System.Drawing.Size(452, 78);
            this.rtbTestoRecensione.TabIndex = 78;
            this.rtbTestoRecensione.Text = resources.GetString("rtbTestoRecensione.Text");
            // 
            // pbValutazioneRecensione
            // 
            this.pbValutazioneRecensione.Image = ((System.Drawing.Image)(resources.GetObject("pbValutazioneRecensione.Image")));
            this.pbValutazioneRecensione.Location = new System.Drawing.Point(7, 60);
            this.pbValutazioneRecensione.Name = "pbValutazioneRecensione";
            this.pbValutazioneRecensione.Size = new System.Drawing.Size(125, 25);
            this.pbValutazioneRecensione.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbValutazioneRecensione.TabIndex = 80;
            this.pbValutazioneRecensione.TabStop = false;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Coolvetica", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.label4.Location = new System.Drawing.Point(63, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(360, 25);
            this.label4.TabIndex = 81;
            this.label4.Text = "11/11/2025";
            this.label4.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // lblNomeUtente
            // 
            this.lblNomeUtente.Font = new System.Drawing.Font("Coolvetica", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNomeUtente.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.lblNomeUtente.Location = new System.Drawing.Point(63, 4);
            this.lblNomeUtente.Name = "lblNomeUtente";
            this.lblNomeUtente.Size = new System.Drawing.Size(360, 25);
            this.lblNomeUtente.TabIndex = 79;
            this.lblNomeUtente.Text = "Francesco Stasi";
            // 
            // pbFotoProfilo
            // 
            this.pbFotoProfilo.Image = ((System.Drawing.Image)(resources.GetObject("pbFotoProfilo.Image")));
            this.pbFotoProfilo.Location = new System.Drawing.Point(7, 4);
            this.pbFotoProfilo.Name = "pbFotoProfilo";
            this.pbFotoProfilo.Size = new System.Drawing.Size(50, 50);
            this.pbFotoProfilo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbFotoProfilo.TabIndex = 77;
            this.pbFotoProfilo.TabStop = false;
            // 
            // usRecensione
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(242)))), ((int)(((byte)(229)))));
            this.Controls.Add(this.rtbTestoRecensione);
            this.Controls.Add(this.pbValutazioneRecensione);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblNomeUtente);
            this.Controls.Add(this.pbFotoProfilo);
            this.Name = "usRecensione";
            this.Size = new System.Drawing.Size(467, 172);
            ((System.ComponentModel.ISupportInitialize)(this.pbValutazioneRecensione)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbFotoProfilo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbTestoRecensione;
        private System.Windows.Forms.PictureBox pbValutazioneRecensione;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblNomeUtente;
        private System.Windows.Forms.PictureBox pbFotoProfilo;
    }
}
