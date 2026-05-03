namespace ReadyToRead
{
    partial class FrmGeneri
    {
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
            this.pnlDetails = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.rtbDescrizione = new System.Windows.Forms.RichTextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.tbTarget = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.btnAnnulla = new System.Windows.Forms.Button();
            this.btnAggiungi = new System.Windows.Forms.Button();
            this.tbNome = new System.Windows.Forms.TextBox();
            this.lblDomanda = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lvLibri = new System.Windows.Forms.ListView();
            this.chISBN = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chAutore = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chGenere = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label3 = new System.Windows.Forms.Label();
            this.tbFiltroNome = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnVisualizza = new System.Windows.Forms.Button();
            this.btnElimina = new System.Windows.Forms.Button();
            this.btnModifica = new System.Windows.Forms.Button();
            this.pnlDetails.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlDetails
            // 
            this.pnlDetails.Controls.Add(this.label12);
            this.pnlDetails.Controls.Add(this.label13);
            this.pnlDetails.Controls.Add(this.rtbDescrizione);
            this.pnlDetails.Controls.Add(this.label23);
            this.pnlDetails.Controls.Add(this.label24);
            this.pnlDetails.Controls.Add(this.tbTarget);
            this.pnlDetails.Controls.Add(this.label15);
            this.pnlDetails.Controls.Add(this.label16);
            this.pnlDetails.Controls.Add(this.btnAnnulla);
            this.pnlDetails.Controls.Add(this.btnAggiungi);
            this.pnlDetails.Controls.Add(this.tbNome);
            this.pnlDetails.Controls.Add(this.lblDomanda);
            this.pnlDetails.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlDetails.Location = new System.Drawing.Point(855, 0);
            this.pnlDetails.Margin = new System.Windows.Forms.Padding(2);
            this.pnlDetails.Name = "pnlDetails";
            this.pnlDetails.Size = new System.Drawing.Size(425, 720);
            this.pnlDetails.TabIndex = 90;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Coolvetica", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Gray;
            this.label12.Location = new System.Drawing.Point(24, 237);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(102, 23);
            this.label12.TabIndex = 240;
            this.label12.Text = "Descrizione";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("Coolvetica", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(26, 266);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(34, 25);
            this.label13.TabIndex = 241;
            this.label13.Text = "💬";
            // 
            // rtbDescrizione
            // 
            this.rtbDescrizione.Location = new System.Drawing.Point(59, 266);
            this.rtbDescrizione.Name = "rtbDescrizione";
            this.rtbDescrizione.Size = new System.Drawing.Size(321, 96);
            this.rtbDescrizione.TabIndex = 239;
            this.rtbDescrizione.Text = "";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Coolvetica", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.ForeColor = System.Drawing.Color.Gray;
            this.label23.Location = new System.Drawing.Point(24, 178);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(63, 23);
            this.label23.TabIndex = 236;
            this.label23.Text = "Target";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.BackColor = System.Drawing.Color.Transparent;
            this.label24.Font = new System.Drawing.Font("Coolvetica", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.ForeColor = System.Drawing.Color.Black;
            this.label24.Location = new System.Drawing.Point(26, 207);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(32, 23);
            this.label24.TabIndex = 238;
            this.label24.Text = "🔞";
            // 
            // tbTarget
            // 
            this.tbTarget.Font = new System.Drawing.Font("Coolvetica", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbTarget.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.tbTarget.Location = new System.Drawing.Point(62, 204);
            this.tbTarget.Name = "tbTarget";
            this.tbTarget.Size = new System.Drawing.Size(314, 30);
            this.tbTarget.TabIndex = 237;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Coolvetica", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.Gray;
            this.label15.Location = new System.Drawing.Point(24, 116);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(55, 23);
            this.label15.TabIndex = 230;
            this.label15.Text = "Nome";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.label16.Font = new System.Drawing.Font("Coolvetica", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.Black;
            this.label16.Location = new System.Drawing.Point(23, 145);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(32, 23);
            this.label16.TabIndex = 232;
            this.label16.Text = "🪪";
            // 
            // btnAnnulla
            // 
            this.btnAnnulla.Font = new System.Drawing.Font("Coolvetica", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAnnulla.Location = new System.Drawing.Point(263, 373);
            this.btnAnnulla.Name = "btnAnnulla";
            this.btnAnnulla.Size = new System.Drawing.Size(117, 31);
            this.btnAnnulla.TabIndex = 225;
            this.btnAnnulla.Text = "↩️ Annulla";
            this.btnAnnulla.UseVisualStyleBackColor = true;
            // 
            // btnAggiungi
            // 
            this.btnAggiungi.Font = new System.Drawing.Font("Coolvetica", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAggiungi.Location = new System.Drawing.Point(25, 368);
            this.btnAggiungi.Name = "btnAggiungi";
            this.btnAggiungi.Size = new System.Drawing.Size(224, 36);
            this.btnAggiungi.TabIndex = 224;
            this.btnAggiungi.Text = "➕Aggiungi";
            this.btnAggiungi.UseVisualStyleBackColor = true;
            // 
            // tbNome
            // 
            this.tbNome.Font = new System.Drawing.Font("Coolvetica", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbNome.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.tbNome.Location = new System.Drawing.Point(55, 145);
            this.tbNome.Name = "tbNome";
            this.tbNome.Size = new System.Drawing.Size(321, 30);
            this.tbNome.TabIndex = 231;
            // 
            // lblDomanda
            // 
            this.lblDomanda.AutoSize = true;
            this.lblDomanda.Font = new System.Drawing.Font("Coolvetica", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDomanda.Location = new System.Drawing.Point(15, 41);
            this.lblDomanda.Name = "lblDomanda";
            this.lblDomanda.Size = new System.Drawing.Size(180, 38);
            this.lblDomanda.TabIndex = 192;
            this.lblDomanda.Text = "Crea Genere";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lvLibri);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.tbFiltroNome);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.btnVisualizza);
            this.panel2.Controls.Add(this.btnElimina);
            this.panel2.Controls.Add(this.btnModifica);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(835, 720);
            this.panel2.TabIndex = 91;
            // 
            // lvLibri
            // 
            this.lvLibri.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chISBN,
            this.chAutore,
            this.chGenere});
            this.lvLibri.Font = new System.Drawing.Font("Coolvetica", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvLibri.FullRowSelect = true;
            this.lvLibri.HideSelection = false;
            this.lvLibri.Location = new System.Drawing.Point(40, 105);
            this.lvLibri.Name = "lvLibri";
            this.lvLibri.Size = new System.Drawing.Size(666, 345);
            this.lvLibri.TabIndex = 229;
            this.lvLibri.UseCompatibleStateImageBehavior = false;
            this.lvLibri.View = System.Windows.Forms.View.Details;
            // 
            // chISBN
            // 
            this.chISBN.Text = "ID";
            this.chISBN.Width = 61;
            // 
            // chAutore
            // 
            this.chAutore.Text = "Nome";
            this.chAutore.Width = 432;
            // 
            // chGenere
            // 
            this.chGenere.Text = "Target";
            this.chGenere.Width = 156;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Coolvetica", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(44, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 23);
            this.label3.TabIndex = 228;
            this.label3.Text = "🔍";
            // 
            // tbFiltroNome
            // 
            this.tbFiltroNome.Font = new System.Drawing.Font("Coolvetica", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbFiltroNome.Location = new System.Drawing.Point(77, 68);
            this.tbFiltroNome.Name = "tbFiltroNome";
            this.tbFiltroNome.Size = new System.Drawing.Size(302, 30);
            this.tbFiltroNome.TabIndex = 227;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Coolvetica", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Gray;
            this.label4.Location = new System.Drawing.Point(36, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(198, 23);
            this.label4.TabIndex = 226;
            this.label4.Text = "Cerca per nome Genere";
            // 
            // btnVisualizza
            // 
            this.btnVisualizza.Font = new System.Drawing.Font("Coolvetica", 14.25F);
            this.btnVisualizza.Location = new System.Drawing.Point(712, 105);
            this.btnVisualizza.Name = "btnVisualizza";
            this.btnVisualizza.Size = new System.Drawing.Size(117, 34);
            this.btnVisualizza.TabIndex = 188;
            this.btnVisualizza.Text = "👁️Visualizza";
            this.btnVisualizza.UseVisualStyleBackColor = true;
            // 
            // btnElimina
            // 
            this.btnElimina.Font = new System.Drawing.Font("Coolvetica", 14.25F);
            this.btnElimina.Location = new System.Drawing.Point(712, 187);
            this.btnElimina.Name = "btnElimina";
            this.btnElimina.Size = new System.Drawing.Size(118, 34);
            this.btnElimina.TabIndex = 187;
            this.btnElimina.Text = "🗑️Elimina";
            this.btnElimina.UseVisualStyleBackColor = true;
            // 
            // btnModifica
            // 
            this.btnModifica.Font = new System.Drawing.Font("Coolvetica", 14.25F);
            this.btnModifica.Location = new System.Drawing.Point(712, 145);
            this.btnModifica.Name = "btnModifica";
            this.btnModifica.Size = new System.Drawing.Size(118, 34);
            this.btnModifica.TabIndex = 186;
            this.btnModifica.Text = "✍️Modifica";
            this.btnModifica.UseVisualStyleBackColor = true;
            // 
            // FrmGeneri
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(242)))), ((int)(((byte)(229)))));
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.Controls.Add(this.pnlDetails);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Coolvetica", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MaximumSize = new System.Drawing.Size(1920, 1080);
            this.MinimumSize = new System.Drawing.Size(1280, 720);
            this.Name = "FrmGeneri";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FrmGeneri";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.pnlDetails.ResumeLayout(false);
            this.pnlDetails.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlDetails;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox tbTarget;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button btnAnnulla;
        private System.Windows.Forms.Button btnAggiungi;
        private System.Windows.Forms.TextBox tbNome;
        private System.Windows.Forms.Label lblDomanda;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ListView lvLibri;
        private System.Windows.Forms.ColumnHeader chISBN;
        private System.Windows.Forms.ColumnHeader chAutore;
        private System.Windows.Forms.ColumnHeader chGenere;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbFiltroNome;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnVisualizza;
        private System.Windows.Forms.Button btnElimina;
        private System.Windows.Forms.Button btnModifica;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.RichTextBox rtbDescrizione;
    }
}