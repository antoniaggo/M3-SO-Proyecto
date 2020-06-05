namespace WindowsFormsApplication1
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.conectar = new System.Windows.Forms.Button();
            this.desconectar = new System.Windows.Forms.Button();
            this.user = new System.Windows.Forms.TextBox();
            this.usuario = new System.Windows.Forms.Label();
            this.contraseña = new System.Windows.Forms.Label();
            this.pass = new System.Windows.Forms.TextBox();
            this.iniciars = new System.Windows.Forms.Button();
            this.register = new System.Windows.Forms.Button();
            this.ganador = new System.Windows.Forms.RadioButton();
            this.enviar = new System.Windows.Forms.Button();
            this.datos = new System.Windows.Forms.RadioButton();
            this.recovery = new System.Windows.Forms.RadioButton();
            this.nombre = new System.Windows.Forms.TextBox();
            this.ListaConectados = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ListaConectados)).BeginInit();
            this.SuspendLayout();
            // 
            // conectar
            // 
            this.conectar.Location = new System.Drawing.Point(33, 32);
            this.conectar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.conectar.Name = "conectar";
            this.conectar.Size = new System.Drawing.Size(149, 78);
            this.conectar.TabIndex = 0;
            this.conectar.Text = "conectar";
            this.conectar.UseVisualStyleBackColor = true;
            this.conectar.Click += new System.EventHandler(this.button1_Click);
            // 
            // desconectar
            // 
            this.desconectar.Location = new System.Drawing.Point(493, 32);
            this.desconectar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.desconectar.Name = "desconectar";
            this.desconectar.Size = new System.Drawing.Size(172, 78);
            this.desconectar.TabIndex = 2;
            this.desconectar.Text = "desconectar";
            this.desconectar.UseVisualStyleBackColor = true;
            this.desconectar.Click += new System.EventHandler(this.desconectar_Click);
            // 
            // user
            // 
            this.user.Location = new System.Drawing.Point(268, 52);
            this.user.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.user.Name = "user";
            this.user.Size = new System.Drawing.Size(148, 31);
            this.user.TabIndex = 3;
            // 
            // usuario
            // 
            this.usuario.AutoSize = true;
            this.usuario.Location = new System.Drawing.Point(264, 14);
            this.usuario.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.usuario.Name = "usuario";
            this.usuario.Size = new System.Drawing.Size(83, 25);
            this.usuario.TabIndex = 4;
            this.usuario.Text = "usuario";
            this.usuario.Click += new System.EventHandler(this.label1_Click);
            // 
            // contraseña
            // 
            this.contraseña.AutoSize = true;
            this.contraseña.Location = new System.Drawing.Point(264, 109);
            this.contraseña.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.contraseña.Name = "contraseña";
            this.contraseña.Size = new System.Drawing.Size(119, 25);
            this.contraseña.TabIndex = 5;
            this.contraseña.Text = "contraseña";
            // 
            // pass
            // 
            this.pass.Location = new System.Drawing.Point(268, 141);
            this.pass.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pass.Name = "pass";
            this.pass.Size = new System.Drawing.Size(148, 31);
            this.pass.TabIndex = 6;
            // 
            // iniciars
            // 
            this.iniciars.Location = new System.Drawing.Point(33, 228);
            this.iniciars.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.iniciars.Name = "iniciars";
            this.iniciars.Size = new System.Drawing.Size(195, 88);
            this.iniciars.TabIndex = 7;
            this.iniciars.Text = "Iniciar Sesion";
            this.iniciars.UseVisualStyleBackColor = true;
            this.iniciars.Click += new System.EventHandler(this.iniciars_Click);
            // 
            // register
            // 
            this.register.Location = new System.Drawing.Point(547, 141);
            this.register.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.register.Name = "register";
            this.register.Size = new System.Drawing.Size(141, 91);
            this.register.TabIndex = 8;
            this.register.Text = "Registrate";
            this.register.UseVisualStyleBackColor = true;
            this.register.Click += new System.EventHandler(this.register_Click);
            // 
            // ganador
            // 
            this.ganador.AutoSize = true;
            this.ganador.Location = new System.Drawing.Point(268, 222);
            this.ganador.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ganador.Name = "ganador";
            this.ganador.Size = new System.Drawing.Size(166, 29);
            this.ganador.TabIndex = 9;
            this.ganador.TabStop = true;
            this.ganador.Text = "TOP SCORE";
            this.ganador.UseVisualStyleBackColor = true;
            this.ganador.CheckedChanged += new System.EventHandler(this.ganador_CheckedChanged);
            // 
            // enviar
            // 
            this.enviar.Location = new System.Drawing.Point(609, 294);
            this.enviar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.enviar.Name = "enviar";
            this.enviar.Size = new System.Drawing.Size(149, 58);
            this.enviar.TabIndex = 12;
            this.enviar.Text = "enviar";
            this.enviar.UseVisualStyleBackColor = true;
            this.enviar.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // datos
            // 
            this.datos.AutoSize = true;
            this.datos.Location = new System.Drawing.Point(253, 281);
            this.datos.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.datos.Name = "datos";
            this.datos.Size = new System.Drawing.Size(200, 29);
            this.datos.TabIndex = 13;
            this.datos.TabStop = true;
            this.datos.Text = "Datos jugadores";
            this.datos.UseVisualStyleBackColor = true;
            // 
            // recovery
            // 
            this.recovery.AutoSize = true;
            this.recovery.Location = new System.Drawing.Point(165, 342);
            this.recovery.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.recovery.Name = "recovery";
            this.recovery.Size = new System.Drawing.Size(254, 29);
            this.recovery.TabIndex = 14;
            this.recovery.TabStop = true;
            this.recovery.Text = "Recovery Passwor -->";
            this.recovery.UseVisualStyleBackColor = true;
            // 
            // nombre
            // 
            this.nombre.Location = new System.Drawing.Point(428, 342);
            this.nombre.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.nombre.Name = "nombre";
            this.nombre.Size = new System.Drawing.Size(148, 31);
            this.nombre.TabIndex = 15;
            this.nombre.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // ListaConectados
            // 
            this.ListaConectados.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.ListaConectados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ListaConectados.ColumnHeadersVisible = false;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ListaConectados.DefaultCellStyle = dataGridViewCellStyle2;
            this.ListaConectados.Location = new System.Drawing.Point(45, 520);
            this.ListaConectados.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.ListaConectados.Name = "ListaConectados";
            this.ListaConectados.ReadOnly = true;
            this.ListaConectados.RowHeadersWidth = 82;
            this.ListaConectados.Size = new System.Drawing.Size(670, 489);
            this.ListaConectados.TabIndex = 16;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel1.BackgroundImage = global::WindowsFormsApplication1.Properties.Resources.roulette;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel1.Location = new System.Drawing.Point(807, 52);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(820, 820);
            this.panel1.TabIndex = 17;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(52, 438);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(176, 58);
            this.button1.TabIndex = 18;
            this.button1.Text = "Invitar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_2);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(284, 438);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(169, 58);
            this.button2.TabIndex = 19;
            this.button2.Text = "Aceptar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(511, 438);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(204, 58);
            this.button3.TabIndex = 20;
            this.button3.Text = "Rechazar";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2013, 1129);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ListaConectados);
            this.Controls.Add(this.nombre);
            this.Controls.Add(this.recovery);
            this.Controls.Add(this.datos);
            this.Controls.Add(this.enviar);
            this.Controls.Add(this.ganador);
            this.Controls.Add(this.register);
            this.Controls.Add(this.iniciars);
            this.Controls.Add(this.pass);
            this.Controls.Add(this.contraseña);
            this.Controls.Add(this.usuario);
            this.Controls.Add(this.user);
            this.Controls.Add(this.desconectar);
            this.Controls.Add(this.conectar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(2039, 1200);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(2039, 1200);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ruleta";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ListaConectados)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button conectar;
        private System.Windows.Forms.Button desconectar;
        private System.Windows.Forms.TextBox user;
        private System.Windows.Forms.Label usuario;
        private System.Windows.Forms.Label contraseña;
        private System.Windows.Forms.TextBox pass;
        private System.Windows.Forms.Button iniciars;
        private System.Windows.Forms.Button register;
        private System.Windows.Forms.RadioButton ganador;
        private System.Windows.Forms.Button enviar;
        private System.Windows.Forms.RadioButton datos;
        private System.Windows.Forms.RadioButton recovery;
        private System.Windows.Forms.TextBox nombre;
        private System.Windows.Forms.DataGridView ListaConectados;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}

