namespace ReadyToRead
{
    partial class FrmMenuAdmin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMenuAdmin));
            this.btnProdotti = new System.Windows.Forms.Button();
            this.btnGeneri = new System.Windows.Forms.Button();
            this.btnAutori = new System.Windows.Forms.Button();
            this.btnCasaEditrice = new System.Windows.Forms.Button();
            this.flpSideBar = new System.Windows.Forms.FlowLayoutPanel();
            this.btnOrdini = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.lblTitolo = new System.Windows.Forms.Label();
            this.flpSideBar.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // btnProdotti
            // 
            this.btnProdotti.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(242)))), ((int)(((byte)(229)))));
            this.btnProdotti.Font = new System.Drawing.Font("Coolvetica", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProdotti.Image = ((System.Drawing.Image)(resources.GetObject("btnProdotti.Image")));
            this.btnProdotti.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnProdotti.Location = new System.Drawing.Point(5, 326);
            this.btnProdotti.Margin = new System.Windows.Forms.Padding(5);
            this.btnProdotti.Name = "btnProdotti";
            this.btnProdotti.Size = new System.Drawing.Size(171, 97);
            this.btnProdotti.TabIndex = 234;
            this.btnProdotti.Text = "Prodotti";
            this.btnProdotti.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnProdotti.UseVisualStyleBackColor = false;
            this.btnProdotti.Click += new System.EventHandler(this.btnProdotti_Click);
            // 
            // btnGeneri
            // 
            this.btnGeneri.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(242)))), ((int)(((byte)(229)))));
            this.btnGeneri.Font = new System.Drawing.Font("Coolvetica", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGeneri.Image = ((System.Drawing.Image)(resources.GetObject("btnGeneri.Image")));
            this.btnGeneri.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnGeneri.Location = new System.Drawing.Point(5, 219);
            this.btnGeneri.Margin = new System.Windows.Forms.Padding(5);
            this.btnGeneri.Name = "btnGeneri";
            this.btnGeneri.Size = new System.Drawing.Size(171, 97);
            this.btnGeneri.TabIndex = 233;
            this.btnGeneri.Text = "Generi";
            this.btnGeneri.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnGeneri.UseVisualStyleBackColor = false;
            this.btnGeneri.Click += new System.EventHandler(this.btnGeneri_Click);
            // 
            // btnAutori
            // 
            this.btnAutori.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(242)))), ((int)(((byte)(229)))));
            this.btnAutori.Font = new System.Drawing.Font("Coolvetica", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAutori.Image = ((System.Drawing.Image)(resources.GetObject("btnAutori.Image")));
            this.btnAutori.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnAutori.Location = new System.Drawing.Point(5, 112);
            this.btnAutori.Margin = new System.Windows.Forms.Padding(5);
            this.btnAutori.Name = "btnAutori";
            this.btnAutori.Size = new System.Drawing.Size(171, 97);
            this.btnAutori.TabIndex = 232;
            this.btnAutori.Text = "Autori";
            this.btnAutori.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAutori.UseVisualStyleBackColor = false;
            this.btnAutori.Click += new System.EventHandler(this.btnAutori_Click);
            // 
            // btnCasaEditrice
            // 
            this.btnCasaEditrice.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(242)))), ((int)(((byte)(229)))));
            this.btnCasaEditrice.Font = new System.Drawing.Font("Coolvetica", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCasaEditrice.Image = ((System.Drawing.Image)(resources.GetObject("btnCasaEditrice.Image")));
            this.btnCasaEditrice.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnCasaEditrice.Location = new System.Drawing.Point(5, 5);
            this.btnCasaEditrice.Margin = new System.Windows.Forms.Padding(5);
            this.btnCasaEditrice.Name = "btnCasaEditrice";
            this.btnCasaEditrice.Size = new System.Drawing.Size(171, 97);
            this.btnCasaEditrice.TabIndex = 231;
            this.btnCasaEditrice.Text = "Casa Editrice";
            this.btnCasaEditrice.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCasaEditrice.UseVisualStyleBackColor = false;
            this.btnCasaEditrice.Click += new System.EventHandler(this.btnCasaEditrice_Click);
            // 
            // flpSideBar
            // 
            this.flpSideBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.flpSideBar.Controls.Add(this.btnCasaEditrice);
            this.flpSideBar.Controls.Add(this.btnAutori);
            this.flpSideBar.Controls.Add(this.btnGeneri);
            this.flpSideBar.Controls.Add(this.btnProdotti);
            this.flpSideBar.Controls.Add(this.btnOrdini);
            this.flpSideBar.Dock = System.Windows.Forms.DockStyle.Left;
            this.flpSideBar.Location = new System.Drawing.Point(0, 60);
            this.flpSideBar.Margin = new System.Windows.Forms.Padding(11, 10, 11, 10);
            this.flpSideBar.Name = "flpSideBar";
            this.flpSideBar.Size = new System.Drawing.Size(190, 681);
            this.flpSideBar.TabIndex = 236;
            // 
            // btnOrdini
            // 
            this.btnOrdini.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(242)))), ((int)(((byte)(229)))));
            this.btnOrdini.Font = new System.Drawing.Font("Coolvetica", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOrdini.Image = ((System.Drawing.Image)(resources.GetObject("btnOrdini.Image")));
            this.btnOrdini.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnOrdini.Location = new System.Drawing.Point(5, 433);
            this.btnOrdini.Margin = new System.Windows.Forms.Padding(5);
            this.btnOrdini.Name = "btnOrdini";
            this.btnOrdini.Size = new System.Drawing.Size(171, 97);
            this.btnOrdini.TabIndex = 235;
            this.btnOrdini.Text = "Ordini";
            this.btnOrdini.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnOrdini.UseVisualStyleBackColor = false;
            this.btnOrdini.Click += new System.EventHandler(this.btnOrdini_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.flowLayoutPanel1.Controls.Add(this.pbLogo);
            this.flowLayoutPanel1.Controls.Add(this.lblTitolo);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1454, 60);
            this.flowLayoutPanel1.TabIndex = 237;
            // 
            // pbLogo
            // 
            this.pbLogo.Image = ((System.Drawing.Image)(resources.GetObject("pbLogo.Image")));
            this.pbLogo.Location = new System.Drawing.Point(3, 3);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(113, 50);
            this.pbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbLogo.TabIndex = 7;
            this.pbLogo.TabStop = false;
            // 
            // lblTitolo
            // 
            this.lblTitolo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitolo.Font = new System.Drawing.Font("Coolvetica", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitolo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(84)))), ((int)(((byte)(97)))));
            this.lblTitolo.Location = new System.Drawing.Point(122, 0);
            this.lblTitolo.Name = "lblTitolo";
            this.lblTitolo.Size = new System.Drawing.Size(1320, 53);
            this.lblTitolo.TabIndex = 7;
            this.lblTitolo.Text = "Menu Admin";
            this.lblTitolo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrmMenuAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(242)))), ((int)(((byte)(229)))));
            this.ClientSize = new System.Drawing.Size(1454, 741);
            this.Controls.Add(this.flpSideBar);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Font = new System.Drawing.Font("Coolvetica", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IsMdiContainer = true;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MaximumSize = new System.Drawing.Size(1920, 1080);
            this.MinimumSize = new System.Drawing.Size(1470, 780);
            this.Name = "FrmMenuAdmin";
            this.Text = "FrmMenuAdmin";
            this.Load += new System.EventHandler(this.FrmMenuAdmin_Load);
            this.flpSideBar.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnProdotti;
        private System.Windows.Forms.Button btnGeneri;
        private System.Windows.Forms.Button btnAutori;
        private System.Windows.Forms.Button btnCasaEditrice;
        private System.Windows.Forms.FlowLayoutPanel flpSideBar;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.PictureBox pbLogo;
        private System.Windows.Forms.Label lblTitolo;
        private System.Windows.Forms.Button btnOrdini;
    }
}