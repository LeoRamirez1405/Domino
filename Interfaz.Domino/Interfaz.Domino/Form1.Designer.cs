namespace Interfaz.Domino
{
    partial class Form1
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
            this.btCrearJuego = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.nUBNumeroJugadores = new System.Windows.Forms.NumericUpDown();
            this.labelcantidadJugadores = new System.Windows.Forms.Label();
            this.domainUpDown1 = new System.Windows.Forms.DomainUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.nUBNumeroJugadores)).BeginInit();
            this.SuspendLayout();
            // 
            // btCrearJuego
            // 
            this.btCrearJuego.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btCrearJuego.Location = new System.Drawing.Point(476, 682);
            this.btCrearJuego.Name = "btCrearJuego";
            this.btCrearJuego.Size = new System.Drawing.Size(197, 43);
            this.btCrearJuego.TabIndex = 0;
            this.btCrearJuego.Text = "Crear Juego";
            this.btCrearJuego.UseVisualStyleBackColor = true;
            this.btCrearJuego.Click += new System.EventHandler(this.btCrearJuego_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(45, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(814, 595);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            // 
            // nUBNumeroJugadores
            // 
            this.nUBNumeroJugadores.Location = new System.Drawing.Point(31, 186);
            this.nUBNumeroJugadores.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.nUBNumeroJugadores.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nUBNumeroJugadores.Name = "nUBNumeroJugadores";
            this.nUBNumeroJugadores.Size = new System.Drawing.Size(120, 22);
            this.nUBNumeroJugadores.TabIndex = 2;
            this.nUBNumeroJugadores.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // labelcantidadJugadores
            // 
            this.labelcantidadJugadores.Location = new System.Drawing.Point(28, 155);
            this.labelcantidadJugadores.Name = "labelcantidadJugadores";
            this.labelcantidadJugadores.Size = new System.Drawing.Size(163, 28);
            this.labelcantidadJugadores.TabIndex = 3;
            this.labelcantidadJugadores.Text = "Cantidad de jugadores";
            // 
            // domainUpDown1
            // 
            this.domainUpDown1.Items.Add("Clasico");
            this.domainUpDown1.Items.Add("Quincena");
            this.domainUpDown1.Items.Add("Otros");
            this.domainUpDown1.Location = new System.Drawing.Point(418, 186);
            this.domainUpDown1.Name = "domainUpDown1";
            this.domainUpDown1.Size = new System.Drawing.Size(158, 22);
            this.domainUpDown1.TabIndex = 4;
            this.domainUpDown1.Text = "Reglas del juego";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1202, 737);
            this.Controls.Add(this.domainUpDown1);
            this.Controls.Add(this.labelcantidadJugadores);
            this.Controls.Add(this.nUBNumeroJugadores);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btCrearJuego);
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nUBNumeroJugadores)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btCrearJuego;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nUBNumeroJugadores;
        private System.Windows.Forms.Label labelcantidadJugadores;
        private System.Windows.Forms.DomainUpDown domainUpDown1;
    }
}

