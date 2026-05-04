namespace ReadyToRead
{
    partial class FrmMain
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

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.flpSideBar = new System.Windows.Forms.FlowLayoutPanel();
            this.lblMsgBenvenuto = new System.Windows.Forms.Label();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.flpContentUtente = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.lblTitolo = new System.Windows.Forms.Label();
            this.flpCarrello = new System.Windows.Forms.FlowLayoutPanel();
            this.flpLibriOrdinati = new System.Windows.Forms.FlowLayoutPanel();
            this.label10 = new System.Windows.Forms.Label();
            this.lblTotaleParziale = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lblPrezzoSpedizione = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.tbCodiceSconto = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.btnAcquistaOra = new System.Windows.Forms.Button();
            this.pbUtente = new System.Windows.Forms.PictureBox();
            this.btnDashboard = new System.Windows.Forms.Button();
            this.btnEsplora = new System.Windows.Forms.Button();
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.btnMenuProfilo = new System.Windows.Forms.Button();
            this.btnCarrello = new System.Windows.Forms.Button();
            this.btnWishList = new System.Windows.Forms.Button();
            this.btnProfilo = new System.Windows.Forms.Button();
            this.btnDisconnetti = new System.Windows.Forms.Button();
            this.flpSideBar.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.flpContentUtente.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.flpCarrello.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbUtente)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // flpSideBar
            // 
            this.flpSideBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.flpSideBar.Controls.Add(this.pbUtente);
            this.flpSideBar.Controls.Add(this.lblMsgBenvenuto);
            this.flpSideBar.Controls.Add(this.btnDashboard);
            this.flpSideBar.Controls.Add(this.btnEsplora);
            this.flpSideBar.Dock = System.Windows.Forms.DockStyle.Left;
            this.flpSideBar.Location = new System.Drawing.Point(0, 60);
            this.flpSideBar.Margin = new System.Windows.Forms.Padding(11, 10, 11, 10);
            this.flpSideBar.Name = "flpSideBar";
            this.flpSideBar.Size = new System.Drawing.Size(190, 681);
            this.flpSideBar.TabIndex = 15;
            // 
            // lblMsgBenvenuto
            // 
            this.lblMsgBenvenuto.Font = new System.Drawing.Font("Coolvetica", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMsgBenvenuto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(84)))), ((int)(((byte)(97)))));
            this.lblMsgBenvenuto.Location = new System.Drawing.Point(3, 106);
            this.lblMsgBenvenuto.Name = "lblMsgBenvenuto";
            this.lblMsgBenvenuto.Size = new System.Drawing.Size(182, 64);
            this.lblMsgBenvenuto.TabIndex = 13;
            this.lblMsgBenvenuto.Text = "Benritrovato, \r\nNome Utente";
            this.lblMsgBenvenuto.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.flowLayoutPanel3.Controls.Add(this.btnMenuProfilo);
            this.flowLayoutPanel3.Controls.Add(this.btnCarrello);
            this.flowLayoutPanel3.Controls.Add(this.btnWishList);
            this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.flowLayoutPanel3.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(1099, 3);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(351, 50);
            this.flowLayoutPanel3.TabIndex = 17;
            // 
            // flpContentUtente
            // 
            this.flpContentUtente.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flpContentUtente.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.flpContentUtente.Controls.Add(this.btnProfilo);
            this.flpContentUtente.Controls.Add(this.btnDisconnetti);
            this.flpContentUtente.Location = new System.Drawing.Point(1251, 60);
            this.flpContentUtente.Name = "flpContentUtente";
            this.flpContentUtente.Size = new System.Drawing.Size(203, 112);
            this.flpContentUtente.TabIndex = 18;
            this.flpContentUtente.Visible = false;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.flowLayoutPanel1.Controls.Add(this.pbLogo);
            this.flowLayoutPanel1.Controls.Add(this.lblTitolo);
            this.flowLayoutPanel1.Controls.Add(this.flowLayoutPanel3);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1454, 60);
            this.flowLayoutPanel1.TabIndex = 16;
            // 
            // lblTitolo
            // 
            this.lblTitolo.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblTitolo.Font = new System.Drawing.Font("Coolvetica", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitolo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(84)))), ((int)(((byte)(97)))));
            this.lblTitolo.Location = new System.Drawing.Point(122, 0);
            this.lblTitolo.Name = "lblTitolo";
            this.lblTitolo.Size = new System.Drawing.Size(971, 56);
            this.lblTitolo.TabIndex = 7;
            this.lblTitolo.Text = "Dashboard";
            this.lblTitolo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // flpCarrello
            // 
            this.flpCarrello.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.flpCarrello.Controls.Add(this.flpLibriOrdinati);
            this.flpCarrello.Controls.Add(this.label10);
            this.flpCarrello.Controls.Add(this.lblTotaleParziale);
            this.flpCarrello.Controls.Add(this.label12);
            this.flpCarrello.Controls.Add(this.lblPrezzoSpedizione);
            this.flpCarrello.Controls.Add(this.label15);
            this.flpCarrello.Controls.Add(this.tbCodiceSconto);
            this.flpCarrello.Controls.Add(this.label11);
            this.flpCarrello.Controls.Add(this.label14);
            this.flpCarrello.Controls.Add(this.btnAcquistaOra);
            this.flpCarrello.Dock = System.Windows.Forms.DockStyle.Right;
            this.flpCarrello.Location = new System.Drawing.Point(1132, 60);
            this.flpCarrello.Name = "flpCarrello";
            this.flpCarrello.Size = new System.Drawing.Size(322, 681);
            this.flpCarrello.TabIndex = 20;
            this.flpCarrello.Visible = false;
            // 
            // flpLibriOrdinati
            // 
            this.flpLibriOrdinati.AutoScroll = true;
            this.flpLibriOrdinati.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(242)))), ((int)(((byte)(229)))));
            this.flpLibriOrdinati.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flpLibriOrdinati.Location = new System.Drawing.Point(3, 3);
            this.flpLibriOrdinati.Name = "flpLibriOrdinati";
            this.flpLibriOrdinati.Size = new System.Drawing.Size(312, 481);
            this.flpLibriOrdinati.TabIndex = 21;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(242)))), ((int)(((byte)(229)))));
            this.label10.Location = new System.Drawing.Point(3, 487);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(128, 23);
            this.label10.TabIndex = 22;
            this.label10.Text = "Totale Parziale";
            // 
            // lblTotaleParziale
            // 
            this.lblTotaleParziale.BackColor = System.Drawing.Color.Transparent;
            this.lblTotaleParziale.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(242)))), ((int)(((byte)(229)))));
            this.lblTotaleParziale.Location = new System.Drawing.Point(137, 487);
            this.lblTotaleParziale.Name = "lblTotaleParziale";
            this.lblTotaleParziale.Size = new System.Drawing.Size(168, 23);
            this.lblTotaleParziale.TabIndex = 26;
            this.lblTotaleParziale.Text = "Prezzo €";
            this.lblTotaleParziale.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(242)))), ((int)(((byte)(229)))));
            this.label12.Location = new System.Drawing.Point(3, 510);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(128, 22);
            this.label12.TabIndex = 24;
            this.label12.Text = "Spedizione";
            // 
            // lblPrezzoSpedizione
            // 
            this.lblPrezzoSpedizione.BackColor = System.Drawing.Color.Transparent;
            this.lblPrezzoSpedizione.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(242)))), ((int)(((byte)(229)))));
            this.lblPrezzoSpedizione.Location = new System.Drawing.Point(137, 510);
            this.lblPrezzoSpedizione.Name = "lblPrezzoSpedizione";
            this.lblPrezzoSpedizione.Size = new System.Drawing.Size(170, 22);
            this.lblPrezzoSpedizione.TabIndex = 27;
            this.lblPrezzoSpedizione.Text = "Prezzo €";
            this.lblPrezzoSpedizione.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(242)))), ((int)(((byte)(229)))));
            this.label15.Location = new System.Drawing.Point(3, 532);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(198, 23);
            this.label15.TabIndex = 30;
            this.label15.Text = "Codice Sconto";
            // 
            // tbCodiceSconto
            // 
            this.tbCodiceSconto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.tbCodiceSconto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbCodiceSconto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(242)))), ((int)(((byte)(229)))));
            this.tbCodiceSconto.Location = new System.Drawing.Point(207, 535);
            this.tbCodiceSconto.MaxLength = 8;
            this.tbCodiceSconto.Name = "tbCodiceSconto";
            this.tbCodiceSconto.Size = new System.Drawing.Size(112, 30);
            this.tbCodiceSconto.TabIndex = 29;
            this.tbCodiceSconto.Text = "ABCDXXXX";
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Coolvetica", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(242)))), ((int)(((byte)(229)))));
            this.label11.Location = new System.Drawing.Point(3, 568);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(128, 44);
            this.label11.TabIndex = 23;
            this.label11.Text = "Totale";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Font = new System.Drawing.Font("Coolvetica", 21.75F);
            this.label14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(242)))), ((int)(((byte)(229)))));
            this.label14.Location = new System.Drawing.Point(137, 568);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(176, 37);
            this.label14.TabIndex = 28;
            this.label14.Text = "Prezzo €";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnAcquistaOra
            // 
            this.btnAcquistaOra.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(84)))), ((int)(((byte)(97)))));
            this.btnAcquistaOra.Font = new System.Drawing.Font("Coolvetica", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAcquistaOra.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.btnAcquistaOra.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAcquistaOra.Location = new System.Drawing.Point(11, 622);
            this.btnAcquistaOra.Margin = new System.Windows.Forms.Padding(11, 10, 11, 10);
            this.btnAcquistaOra.Name = "btnAcquistaOra";
            this.btnAcquistaOra.Size = new System.Drawing.Size(304, 50);
            this.btnAcquistaOra.TabIndex = 17;
            this.btnAcquistaOra.Text = "Acquista Ora";
            this.btnAcquistaOra.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAcquistaOra.UseVisualStyleBackColor = false;
            // 
            // pbUtente
            // 
            this.pbUtente.BackColor = System.Drawing.Color.Transparent;
            this.pbUtente.Image = ((System.Drawing.Image)(resources.GetObject("pbUtente.Image")));
            this.pbUtente.Location = new System.Drawing.Point(3, 3);
            this.pbUtente.Name = "pbUtente";
            this.pbUtente.Size = new System.Drawing.Size(182, 100);
            this.pbUtente.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbUtente.TabIndex = 5;
            this.pbUtente.TabStop = false;
            // 
            // btnDashboard
            // 
            this.btnDashboard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(242)))), ((int)(((byte)(229)))));
            this.btnDashboard.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.btnDashboard.Image = ((System.Drawing.Image)(resources.GetObject("btnDashboard.Image")));
            this.btnDashboard.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDashboard.Location = new System.Drawing.Point(11, 180);
            this.btnDashboard.Margin = new System.Windows.Forms.Padding(11, 10, 11, 10);
            this.btnDashboard.Name = "btnDashboard";
            this.btnDashboard.Size = new System.Drawing.Size(179, 50);
            this.btnDashboard.TabIndex = 1;
            this.btnDashboard.Text = "Dashboard";
            this.btnDashboard.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDashboard.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDashboard.UseVisualStyleBackColor = false;
            this.btnDashboard.Click += new System.EventHandler(this.btnDashboard_Click);
            // 
            // btnEsplora
            // 
            this.btnEsplora.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(242)))), ((int)(((byte)(229)))));
            this.btnEsplora.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.btnEsplora.Image = ((System.Drawing.Image)(resources.GetObject("btnEsplora.Image")));
            this.btnEsplora.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEsplora.Location = new System.Drawing.Point(11, 250);
            this.btnEsplora.Margin = new System.Windows.Forms.Padding(11, 10, 11, 10);
            this.btnEsplora.Name = "btnEsplora";
            this.btnEsplora.Size = new System.Drawing.Size(179, 50);
            this.btnEsplora.TabIndex = 7;
            this.btnEsplora.Text = "Esplora";
            this.btnEsplora.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEsplora.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEsplora.UseVisualStyleBackColor = false;
            this.btnEsplora.Click += new System.EventHandler(this.btnEsplora_Click);
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
            // btnMenuProfilo
            // 
            this.btnMenuProfilo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMenuProfilo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(242)))), ((int)(((byte)(229)))));
            this.btnMenuProfilo.Image = ((System.Drawing.Image)(resources.GetObject("btnMenuProfilo.Image")));
            this.btnMenuProfilo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMenuProfilo.Location = new System.Drawing.Point(159, 3);
            this.btnMenuProfilo.Name = "btnMenuProfilo";
            this.btnMenuProfilo.Size = new System.Drawing.Size(189, 50);
            this.btnMenuProfilo.TabIndex = 10;
            this.btnMenuProfilo.Text = "Nome Utente";
            this.btnMenuProfilo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMenuProfilo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnMenuProfilo.UseVisualStyleBackColor = false;
            this.btnMenuProfilo.Click += new System.EventHandler(this.btnMenuProfilo_Click);
            // 
            // btnCarrello
            // 
            this.btnCarrello.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCarrello.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(242)))), ((int)(((byte)(229)))));
            this.btnCarrello.Image = ((System.Drawing.Image)(resources.GetObject("btnCarrello.Image")));
            this.btnCarrello.Location = new System.Drawing.Point(103, 3);
            this.btnCarrello.Name = "btnCarrello";
            this.btnCarrello.Size = new System.Drawing.Size(50, 50);
            this.btnCarrello.TabIndex = 8;
            this.btnCarrello.UseVisualStyleBackColor = false;
            this.btnCarrello.Click += new System.EventHandler(this.btnCarrello_Click);
            // 
            // btnWishList
            // 
            this.btnWishList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnWishList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(242)))), ((int)(((byte)(229)))));
            this.btnWishList.Image = ((System.Drawing.Image)(resources.GetObject("btnWishList.Image")));
            this.btnWishList.Location = new System.Drawing.Point(47, 3);
            this.btnWishList.Name = "btnWishList";
            this.btnWishList.Size = new System.Drawing.Size(50, 50);
            this.btnWishList.TabIndex = 9;
            this.btnWishList.UseVisualStyleBackColor = false;
            // 
            // btnProfilo
            // 
            this.btnProfilo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(242)))), ((int)(((byte)(229)))));
            this.btnProfilo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.btnProfilo.Image = ((System.Drawing.Image)(resources.GetObject("btnProfilo.Image")));
            this.btnProfilo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnProfilo.Location = new System.Drawing.Point(11, 10);
            this.btnProfilo.Margin = new System.Windows.Forms.Padding(11, 10, 11, 10);
            this.btnProfilo.Name = "btnProfilo";
            this.btnProfilo.Size = new System.Drawing.Size(174, 34);
            this.btnProfilo.TabIndex = 14;
            this.btnProfilo.Text = "Area Privata";
            this.btnProfilo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnProfilo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnProfilo.UseVisualStyleBackColor = false;
            this.btnProfilo.Click += new System.EventHandler(this.btnProfilo_Click);
            // 
            // btnDisconnetti
            // 
            this.btnDisconnetti.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(242)))), ((int)(((byte)(229)))));
            this.btnDisconnetti.ForeColor = System.Drawing.Color.Red;
            this.btnDisconnetti.Image = ((System.Drawing.Image)(resources.GetObject("btnDisconnetti.Image")));
            this.btnDisconnetti.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDisconnetti.Location = new System.Drawing.Point(11, 64);
            this.btnDisconnetti.Margin = new System.Windows.Forms.Padding(11, 10, 11, 10);
            this.btnDisconnetti.Name = "btnDisconnetti";
            this.btnDisconnetti.Size = new System.Drawing.Size(174, 34);
            this.btnDisconnetti.TabIndex = 15;
            this.btnDisconnetti.Text = "Disconnetti";
            this.btnDisconnetti.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDisconnetti.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDisconnetti.UseVisualStyleBackColor = false;
            this.btnDisconnetti.Click += new System.EventHandler(this.btnDisconnetti_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1454, 741);
            this.Controls.Add(this.flpCarrello);
            this.Controls.Add(this.flpSideBar);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.flpContentUtente);
            this.Font = new System.Drawing.Font("Coolvetica", 14.25F);
            this.IsMdiContainer = true;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MaximumSize = new System.Drawing.Size(1920, 1280);
            this.MinimumSize = new System.Drawing.Size(1470, 780);
            this.Name = "FrmMain";
            this.Text = "FrmMain";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.flpSideBar.ResumeLayout(false);
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flpContentUtente.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flpCarrello.ResumeLayout(false);
            this.flpCarrello.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbUtente)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flpSideBar;
        private System.Windows.Forms.PictureBox pbUtente;
        private System.Windows.Forms.Label lblMsgBenvenuto;
        private System.Windows.Forms.Button btnDashboard;
        private System.Windows.Forms.Button btnEsplora;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.Button btnMenuProfilo;
        private System.Windows.Forms.Button btnCarrello;
        private System.Windows.Forms.Button btnWishList;
        private System.Windows.Forms.FlowLayoutPanel flpContentUtente;
        private System.Windows.Forms.Button btnProfilo;
        private System.Windows.Forms.Button btnDisconnetti;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.PictureBox pbLogo;
        private System.Windows.Forms.FlowLayoutPanel flpCarrello;
        private System.Windows.Forms.FlowLayoutPanel flpLibriOrdinati;
        private System.Windows.Forms.Label lblTitolo;
        private System.Windows.Forms.Button btnAcquistaOra;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblTotaleParziale;
        private System.Windows.Forms.Label lblPrezzoSpedizione;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox tbCodiceSconto;
        private System.Windows.Forms.Label label14;
    }
}

