namespace ReadyToRead
{
    partial class usLibroNelCarrello
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(usLibroNelCarrello));
            this.nudQuantita = new System.Windows.Forms.NumericUpDown();
            this.lblPrezzoLibroSuggerito = new System.Windows.Forms.Label();
            this.lblLibro = new System.Windows.Forms.Label();
            this.pbCancellaOrdine = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantita)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCancellaOrdine)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // nudQuantita
            // 
            this.nudQuantita.Font = new System.Drawing.Font("Coolvetica", 14.25F);
            this.nudQuantita.Location = new System.Drawing.Point(121, 70);
            this.nudQuantita.Name = "nudQuantita";
            this.nudQuantita.Size = new System.Drawing.Size(93, 30);
            this.nudQuantita.TabIndex = 88;
            // 
            // lblPrezzoLibroSuggerito
            // 
            this.lblPrezzoLibroSuggerito.BackColor = System.Drawing.Color.Transparent;
            this.lblPrezzoLibroSuggerito.Font = new System.Drawing.Font("Coolvetica", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrezzoLibroSuggerito.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.lblPrezzoLibroSuggerito.Location = new System.Drawing.Point(116, 110);
            this.lblPrezzoLibroSuggerito.Name = "lblPrezzoLibroSuggerito";
            this.lblPrezzoLibroSuggerito.Size = new System.Drawing.Size(112, 34);
            this.lblPrezzoLibroSuggerito.TabIndex = 84;
            this.lblPrezzoLibroSuggerito.Text = "Prezzo€";
            // 
            // lblLibro
            // 
            this.lblLibro.Font = new System.Drawing.Font("Coolvetica", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLibro.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.lblLibro.Location = new System.Drawing.Point(116, 2);
            this.lblLibro.Name = "lblLibro";
            this.lblLibro.Size = new System.Drawing.Size(147, 38);
            this.lblLibro.TabIndex = 85;
            this.lblLibro.Text = "Titolo Libro";
            this.lblLibro.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pbCancellaOrdine
            // 
            this.pbCancellaOrdine.Image = ((System.Drawing.Image)(resources.GetObject("pbCancellaOrdine.Image")));
            this.pbCancellaOrdine.Location = new System.Drawing.Point(260, 3);
            this.pbCancellaOrdine.Name = "pbCancellaOrdine";
            this.pbCancellaOrdine.Size = new System.Drawing.Size(24, 24);
            this.pbCancellaOrdine.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbCancellaOrdine.TabIndex = 87;
            this.pbCancellaOrdine.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Black;
            this.pictureBox1.Location = new System.Drawing.Point(4, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(106, 141);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 86;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Coolvetica", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.label1.Location = new System.Drawing.Point(118, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 11);
            this.label1.TabIndex = 89;
            this.label1.Text = "quantità:";
            // 
            // usLibroNelCarrello
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nudQuantita);
            this.Controls.Add(this.pbCancellaOrdine);
            this.Controls.Add(this.lblPrezzoLibroSuggerito);
            this.Controls.Add(this.lblLibro);
            this.Controls.Add(this.pictureBox1);
            this.Name = "usLibroNelCarrello";
            this.Size = new System.Drawing.Size(288, 147);
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantita)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCancellaOrdine)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NumericUpDown nudQuantita;
        private System.Windows.Forms.PictureBox pbCancellaOrdine;
        private System.Windows.Forms.Label lblPrezzoLibroSuggerito;
        private System.Windows.Forms.Label lblLibro;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
    }
}
