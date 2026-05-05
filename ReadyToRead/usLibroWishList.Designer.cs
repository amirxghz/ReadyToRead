namespace ReadyToRead
{
    partial class usLibroWishList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(usLibroWishList));
            this.btnAggiungiAlCarrello = new System.Windows.Forms.Button();
            this.pbCancellaOrdine = new System.Windows.Forms.PictureBox();
            this.lblPrezzoLibroSuggerito = new System.Windows.Forms.Label();
            this.lblLibro = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbCancellaOrdine)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAggiungiAlCarrello
            // 
            this.btnAggiungiAlCarrello.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAggiungiAlCarrello.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(84)))), ((int)(((byte)(97)))));
            this.btnAggiungiAlCarrello.Font = new System.Drawing.Font("Coolvetica", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAggiungiAlCarrello.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.btnAggiungiAlCarrello.Image = ((System.Drawing.Image)(resources.GetObject("btnAggiungiAlCarrello.Image")));
            this.btnAggiungiAlCarrello.Location = new System.Drawing.Point(224, 102);
            this.btnAggiungiAlCarrello.Name = "btnAggiungiAlCarrello";
            this.btnAggiungiAlCarrello.Size = new System.Drawing.Size(35, 44);
            this.btnAggiungiAlCarrello.TabIndex = 81;
            this.btnAggiungiAlCarrello.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAggiungiAlCarrello.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAggiungiAlCarrello.UseVisualStyleBackColor = false;
            // 
            // pbCancellaOrdine
            // 
            this.pbCancellaOrdine.Image = ((System.Drawing.Image)(resources.GetObject("pbCancellaOrdine.Image")));
            this.pbCancellaOrdine.Location = new System.Drawing.Point(238, 5);
            this.pbCancellaOrdine.Name = "pbCancellaOrdine";
            this.pbCancellaOrdine.Size = new System.Drawing.Size(24, 24);
            this.pbCancellaOrdine.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbCancellaOrdine.TabIndex = 82;
            this.pbCancellaOrdine.TabStop = false;
            // 
            // lblPrezzoLibroSuggerito
            // 
            this.lblPrezzoLibroSuggerito.BackColor = System.Drawing.Color.Transparent;
            this.lblPrezzoLibroSuggerito.Font = new System.Drawing.Font("Coolvetica", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrezzoLibroSuggerito.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.lblPrezzoLibroSuggerito.Location = new System.Drawing.Point(115, 112);
            this.lblPrezzoLibroSuggerito.Name = "lblPrezzoLibroSuggerito";
            this.lblPrezzoLibroSuggerito.Size = new System.Drawing.Size(112, 34);
            this.lblPrezzoLibroSuggerito.TabIndex = 78;
            this.lblPrezzoLibroSuggerito.Text = "Prezzo€";
            // 
            // lblLibro
            // 
            this.lblLibro.Font = new System.Drawing.Font("Coolvetica", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLibro.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.lblLibro.Location = new System.Drawing.Point(115, 4);
            this.lblLibro.Name = "lblLibro";
            this.lblLibro.Size = new System.Drawing.Size(147, 38);
            this.lblLibro.TabIndex = 79;
            this.lblLibro.Text = "Titolo Libro";
            this.lblLibro.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Black;
            this.pictureBox1.Location = new System.Drawing.Point(3, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(106, 141);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 80;
            this.pictureBox1.TabStop = false;
            // 
            // usLibroWishList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnAggiungiAlCarrello);
            this.Controls.Add(this.pbCancellaOrdine);
            this.Controls.Add(this.lblPrezzoLibroSuggerito);
            this.Controls.Add(this.lblLibro);
            this.Controls.Add(this.pictureBox1);
            this.Name = "usLibroWishList";
            this.Size = new System.Drawing.Size(264, 150);
            ((System.ComponentModel.ISupportInitialize)(this.pbCancellaOrdine)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAggiungiAlCarrello;
        private System.Windows.Forms.PictureBox pbCancellaOrdine;
        private System.Windows.Forms.Label lblPrezzoLibroSuggerito;
        private System.Windows.Forms.Label lblLibro;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}
