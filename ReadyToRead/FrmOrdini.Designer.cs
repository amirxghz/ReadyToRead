namespace ReadyToRead
{
    partial class FrmOrdini
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
            this.lvOrdini = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chAutore = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chCasaEditrice = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chGenere = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label3 = new System.Windows.Forms.Label();
            this.tbFiltroCliente = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbStatoOrdine = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpData = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.nudMinPrezzo = new System.Windows.Forms.NumericUpDown();
            this.nudMaxPrezzo = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnApplicaFiltri = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.cbOrdina = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinPrezzo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxPrezzo)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvOrdini
            // 
            this.lvOrdini.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.chAutore,
            this.chCasaEditrice,
            this.chGenere,
            this.columnHeader3});
            this.lvOrdini.Font = new System.Drawing.Font("Coolvetica", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvOrdini.FullRowSelect = true;
            this.lvOrdini.HideSelection = false;
            this.lvOrdini.Location = new System.Drawing.Point(266, 112);
            this.lvOrdini.Name = "lvOrdini";
            this.lvOrdini.Size = new System.Drawing.Size(919, 525);
            this.lvOrdini.TabIndex = 233;
            this.lvOrdini.UseCompatibleStateImageBehavior = false;
            this.lvOrdini.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Nome";
            this.columnHeader1.Width = 116;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Cliente";
            this.columnHeader2.Width = 196;
            // 
            // chAutore
            // 
            this.chAutore.DisplayIndex = 3;
            this.chAutore.Text = "Stato";
            this.chAutore.Width = 113;
            // 
            // chCasaEditrice
            // 
            this.chCasaEditrice.DisplayIndex = 4;
            this.chCasaEditrice.Text = "Data";
            this.chCasaEditrice.Width = 189;
            // 
            // chGenere
            // 
            this.chGenere.DisplayIndex = 5;
            this.chGenere.Text = "Totale";
            this.chGenere.Width = 120;
            // 
            // columnHeader3
            // 
            this.columnHeader3.DisplayIndex = 2;
            this.columnHeader3.Text = "Contatto";
            this.columnHeader3.Width = 165;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Coolvetica", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(265, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 23);
            this.label3.TabIndex = 232;
            this.label3.Text = "🔍";
            // 
            // tbFiltroCliente
            // 
            this.tbFiltroCliente.Font = new System.Drawing.Font("Coolvetica", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbFiltroCliente.Location = new System.Drawing.Point(298, 76);
            this.tbFiltroCliente.Name = "tbFiltroCliente";
            this.tbFiltroCliente.Size = new System.Drawing.Size(302, 30);
            this.tbFiltroCliente.TabIndex = 231;
            this.tbFiltroCliente.TextChanged += new System.EventHandler(this.tbFiltroCliente_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Coolvetica", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Gray;
            this.label4.Location = new System.Drawing.Point(265, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(200, 23);
            this.label4.TabIndex = 230;
            this.label4.Text = "Cerca per Nome Cliente";
            // 
            // cbStatoOrdine
            // 
            this.cbStatoOrdine.FormattingEnabled = true;
            this.cbStatoOrdine.Location = new System.Drawing.Point(52, 112);
            this.cbStatoOrdine.Name = "cbStatoOrdine";
            this.cbStatoOrdine.Size = new System.Drawing.Size(208, 31);
            this.cbStatoOrdine.TabIndex = 234;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Coolvetica", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Gray;
            this.label1.Location = new System.Drawing.Point(48, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 23);
            this.label1.TabIndex = 235;
            this.label1.Text = "Stato Ordine";
            // 
            // dtpData
            // 
            this.dtpData.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpData.Location = new System.Drawing.Point(52, 188);
            this.dtpData.Name = "dtpData";
            this.dtpData.Size = new System.Drawing.Size(208, 30);
            this.dtpData.TabIndex = 236;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Coolvetica", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Gray;
            this.label2.Location = new System.Drawing.Point(48, 162);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 23);
            this.label2.TabIndex = 237;
            this.label2.Text = "Data Ordine";
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Coolvetica", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.label7.Location = new System.Drawing.Point(145, 262);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 30);
            this.label7.TabIndex = 242;
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Coolvetica", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.label6.Location = new System.Drawing.Point(49, 264);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 30);
            this.label6.TabIndex = 241;
            this.label6.Text = "Min";
            this.label6.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // nudMinPrezzo
            // 
            this.nudMinPrezzo.DecimalPlaces = 2;
            this.nudMinPrezzo.Increment = new decimal(new int[] {
            35,
            0,
            0,
            65536});
            this.nudMinPrezzo.Location = new System.Drawing.Point(78, 264);
            this.nudMinPrezzo.Name = "nudMinPrezzo";
            this.nudMinPrezzo.Size = new System.Drawing.Size(61, 30);
            this.nudMinPrezzo.TabIndex = 240;
            // 
            // nudMaxPrezzo
            // 
            this.nudMaxPrezzo.DecimalPlaces = 2;
            this.nudMaxPrezzo.Increment = new decimal(new int[] {
            35,
            0,
            0,
            65536});
            this.nudMaxPrezzo.Location = new System.Drawing.Point(198, 264);
            this.nudMaxPrezzo.Name = "nudMaxPrezzo";
            this.nudMaxPrezzo.Size = new System.Drawing.Size(62, 30);
            this.nudMaxPrezzo.TabIndex = 239;
            this.nudMaxPrezzo.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Coolvetica", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Gray;
            this.label8.Location = new System.Drawing.Point(48, 239);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(116, 23);
            this.label8.TabIndex = 243;
            this.label8.Text = "Prezzo Totale";
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Coolvetica", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.label9.Location = new System.Drawing.Point(165, 262);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(27, 30);
            this.label9.TabIndex = 244;
            this.label9.Text = "Max";
            this.label9.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnApplicaFiltri);
            this.flowLayoutPanel1.Controls.Add(this.btnReset);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(52, 300);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(208, 83);
            this.flowLayoutPanel1.TabIndex = 245;
            // 
            // btnApplicaFiltri
            // 
            this.btnApplicaFiltri.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApplicaFiltri.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(84)))), ((int)(((byte)(97)))));
            this.btnApplicaFiltri.Font = new System.Drawing.Font("Coolvetica", 14.25F);
            this.btnApplicaFiltri.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.btnApplicaFiltri.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnApplicaFiltri.Location = new System.Drawing.Point(3, 3);
            this.btnApplicaFiltri.Name = "btnApplicaFiltri";
            this.btnApplicaFiltri.Size = new System.Drawing.Size(205, 34);
            this.btnApplicaFiltri.TabIndex = 76;
            this.btnApplicaFiltri.Text = "Applica";
            this.btnApplicaFiltri.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnApplicaFiltri.UseVisualStyleBackColor = false;
            this.btnApplicaFiltri.Click += new System.EventHandler(this.btnApplicaFiltri_Click);
            // 
            // btnReset
            // 
            this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(242)))), ((int)(((byte)(229)))));
            this.btnReset.Font = new System.Drawing.Font("Coolvetica", 14.25F);
            this.btnReset.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.btnReset.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReset.Location = new System.Drawing.Point(3, 43);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(205, 33);
            this.btnReset.TabIndex = 91;
            this.btnReset.Text = "Reset";
            this.btnReset.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // cbOrdina
            // 
            this.cbOrdina.Font = new System.Drawing.Font("Coolvetica", 14.25F);
            this.cbOrdina.FormattingEnabled = true;
            this.cbOrdina.Items.AddRange(new object[] {
            "In evidenza"});
            this.cbOrdina.Location = new System.Drawing.Point(947, 71);
            this.cbOrdina.Name = "cbOrdina";
            this.cbOrdina.Size = new System.Drawing.Size(238, 31);
            this.cbOrdina.TabIndex = 246;
            this.cbOrdina.SelectedIndexChanged += new System.EventHandler(this.cbOrdina_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Coolvetica", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Gray;
            this.label5.Location = new System.Drawing.Point(943, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 23);
            this.label5.TabIndex = 248;
            this.label5.Text = "Ordina";
            // 
            // FrmOrdini
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(242)))), ((int)(((byte)(229)))));
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbOrdina);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.nudMaxPrezzo);
            this.Controls.Add(this.nudMinPrezzo);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpData);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbStatoOrdine);
            this.Controls.Add(this.lvOrdini);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbFiltroCliente);
            this.Controls.Add(this.label4);
            this.Font = new System.Drawing.Font("Coolvetica", 14.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MaximumSize = new System.Drawing.Size(1920, 1080);
            this.MinimumSize = new System.Drawing.Size(1280, 720);
            this.Name = "FrmOrdini";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmOrdini";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmOrdini_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudMinPrezzo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxPrezzo)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvOrdini;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader chAutore;
        private System.Windows.Forms.ColumnHeader chCasaEditrice;
        private System.Windows.Forms.ColumnHeader chGenere;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbFiltroCliente;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbStatoOrdine;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpData;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown nudMinPrezzo;
        private System.Windows.Forms.NumericUpDown nudMaxPrezzo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnApplicaFiltri;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.ComboBox cbOrdina;
        private System.Windows.Forms.Label label5;
    }
}