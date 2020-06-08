namespace RuletaGame
{
    partial class Main
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.btn_connect = new System.Windows.Forms.Button();
            this.textbox_username = new System.Windows.Forms.TextBox();
            this.label_username = new System.Windows.Forms.Label();
            this.label_password = new System.Windows.Forms.Label();
            this.textbox_password = new System.Windows.Forms.TextBox();
            this.btn_login = new System.Windows.Forms.Button();
            this.btn_register = new System.Windows.Forms.Button();
            this.radiobtn_scores = new System.Windows.Forms.RadioButton();
            this.btn_send = new System.Windows.Forms.Button();
            this.radiobtn_data = new System.Windows.Forms.RadioButton();
            this.radiobtn_recovery = new System.Windows.Forms.RadioButton();
            this.textbox_recovery_password = new System.Windows.Forms.TextBox();
            this.ListaConectados = new System.Windows.Forms.DataGridView();
            this.column_username = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.column_score = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_invite = new System.Windows.Forms.Button();
            this.btn_accept = new System.Windows.Forms.Button();
            this.btn_reject = new System.Windows.Forms.Button();
            this.btn_random = new System.Windows.Forms.Button();
            this.label_game_no_started = new System.Windows.Forms.Label();
            this.btn_disconnect = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ListaConectados)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_connect
            // 
            this.btn_connect.Location = new System.Drawing.Point(33, 32);
            this.btn_connect.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_connect.Name = "btn_connect";
            this.btn_connect.Size = new System.Drawing.Size(190, 78);
            this.btn_connect.TabIndex = 0;
            this.btn_connect.Text = "Conectarse";
            this.btn_connect.UseVisualStyleBackColor = true;
            this.btn_connect.Click += new System.EventHandler(this.button1_Click);
            // 
            // textbox_username
            // 
            this.textbox_username.Enabled = false;
            this.textbox_username.Location = new System.Drawing.Point(258, 75);
            this.textbox_username.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textbox_username.Name = "textbox_username";
            this.textbox_username.Size = new System.Drawing.Size(195, 31);
            this.textbox_username.TabIndex = 3;
            // 
            // label_username
            // 
            this.label_username.AutoSize = true;
            this.label_username.Enabled = false;
            this.label_username.Location = new System.Drawing.Point(253, 42);
            this.label_username.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_username.Name = "label_username";
            this.label_username.Size = new System.Drawing.Size(92, 25);
            this.label_username.TabIndex = 4;
            this.label_username.Text = "Usuario:";
            // 
            // label_password
            // 
            this.label_password.AutoSize = true;
            this.label_password.Enabled = false;
            this.label_password.Location = new System.Drawing.Point(253, 134);
            this.label_password.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_password.Name = "label_password";
            this.label_password.Size = new System.Drawing.Size(129, 25);
            this.label_password.TabIndex = 5;
            this.label_password.Text = "Contraseña:";
            // 
            // textbox_password
            // 
            this.textbox_password.Enabled = false;
            this.textbox_password.Location = new System.Drawing.Point(258, 164);
            this.textbox_password.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textbox_password.Name = "textbox_password";
            this.textbox_password.Size = new System.Drawing.Size(195, 31);
            this.textbox_password.TabIndex = 6;
            this.textbox_password.UseSystemPasswordChar = true;
            // 
            // btn_login
            // 
            this.btn_login.Enabled = false;
            this.btn_login.Location = new System.Drawing.Point(258, 216);
            this.btn_login.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_login.Name = "btn_login";
            this.btn_login.Size = new System.Drawing.Size(195, 52);
            this.btn_login.TabIndex = 7;
            this.btn_login.Text = "Iniciar sesión";
            this.btn_login.UseVisualStyleBackColor = true;
            this.btn_login.Click += new System.EventHandler(this.iniciars_Click);
            // 
            // btn_register
            // 
            this.btn_register.Enabled = false;
            this.btn_register.Location = new System.Drawing.Point(258, 282);
            this.btn_register.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_register.Name = "btn_register";
            this.btn_register.Size = new System.Drawing.Size(195, 51);
            this.btn_register.TabIndex = 8;
            this.btn_register.Text = "Registrarse";
            this.btn_register.UseVisualStyleBackColor = true;
            this.btn_register.Click += new System.EventHandler(this.register_Click);
            // 
            // radiobtn_scores
            // 
            this.radiobtn_scores.AutoSize = true;
            this.radiobtn_scores.Enabled = false;
            this.radiobtn_scores.Location = new System.Drawing.Point(511, 42);
            this.radiobtn_scores.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radiobtn_scores.Name = "radiobtn_scores";
            this.radiobtn_scores.Size = new System.Drawing.Size(238, 29);
            this.radiobtn_scores.TabIndex = 9;
            this.radiobtn_scores.TabStop = true;
            this.radiobtn_scores.Text = "Puntuacion más alta";
            this.radiobtn_scores.UseVisualStyleBackColor = true;
            // 
            // btn_send
            // 
            this.btn_send.Enabled = false;
            this.btn_send.Location = new System.Drawing.Point(567, 278);
            this.btn_send.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_send.Name = "btn_send";
            this.btn_send.Size = new System.Drawing.Size(149, 58);
            this.btn_send.TabIndex = 12;
            this.btn_send.Text = "Enviar";
            this.btn_send.UseVisualStyleBackColor = true;
            this.btn_send.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // radiobtn_data
            // 
            this.radiobtn_data.AutoSize = true;
            this.radiobtn_data.Enabled = false;
            this.radiobtn_data.Location = new System.Drawing.Point(511, 93);
            this.radiobtn_data.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radiobtn_data.Name = "radiobtn_data";
            this.radiobtn_data.Size = new System.Drawing.Size(200, 29);
            this.radiobtn_data.TabIndex = 13;
            this.radiobtn_data.TabStop = true;
            this.radiobtn_data.Text = "Datos jugadores";
            this.radiobtn_data.UseVisualStyleBackColor = true;
            // 
            // radiobtn_recovery
            // 
            this.radiobtn_recovery.AutoSize = true;
            this.radiobtn_recovery.Enabled = false;
            this.radiobtn_recovery.Location = new System.Drawing.Point(511, 145);
            this.radiobtn_recovery.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radiobtn_recovery.Name = "radiobtn_recovery";
            this.radiobtn_recovery.Size = new System.Drawing.Size(250, 29);
            this.radiobtn_recovery.TabIndex = 14;
            this.radiobtn_recovery.TabStop = true;
            this.radiobtn_recovery.Text = "Recordar contraseña:";
            this.radiobtn_recovery.UseVisualStyleBackColor = true;
            // 
            // textbox_recovery_password
            // 
            this.textbox_recovery_password.Enabled = false;
            this.textbox_recovery_password.Location = new System.Drawing.Point(511, 202);
            this.textbox_recovery_password.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textbox_recovery_password.Name = "textbox_recovery_password";
            this.textbox_recovery_password.Size = new System.Drawing.Size(242, 31);
            this.textbox_recovery_password.TabIndex = 15;
            // 
            // ListaConectados
            // 
            this.ListaConectados.AllowUserToAddRows = false;
            this.ListaConectados.AllowUserToDeleteRows = false;
            this.ListaConectados.AllowUserToResizeColumns = false;
            this.ListaConectados.AllowUserToResizeRows = false;
            this.ListaConectados.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.ListaConectados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ListaConectados.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.column_username,
            this.column_score});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ListaConectados.DefaultCellStyle = dataGridViewCellStyle1;
            this.ListaConectados.Enabled = false;
            this.ListaConectados.Location = new System.Drawing.Point(33, 370);
            this.ListaConectados.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.ListaConectados.Name = "ListaConectados";
            this.ListaConectados.ReadOnly = true;
            this.ListaConectados.RowHeadersVisible = false;
            this.ListaConectados.RowHeadersWidth = 82;
            this.ListaConectados.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ListaConectados.ShowEditingIcon = false;
            this.ListaConectados.Size = new System.Drawing.Size(750, 489);
            this.ListaConectados.TabIndex = 7;
            // 
            // column_username
            // 
            this.column_username.HeaderText = "Usuario";
            this.column_username.MinimumWidth = 10;
            this.column_username.Name = "column_username";
            this.column_username.ReadOnly = true;
            // 
            // column_score
            // 
            this.column_score.HeaderText = "Puntos";
            this.column_score.MinimumWidth = 10;
            this.column_score.Name = "column_score";
            this.column_score.ReadOnly = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel1.Location = new System.Drawing.Point(841, 42);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(820, 820);
            this.panel1.TabIndex = 17;
            // 
            // btn_invite
            // 
            this.btn_invite.Enabled = false;
            this.btn_invite.Location = new System.Drawing.Point(33, 892);
            this.btn_invite.Name = "btn_invite";
            this.btn_invite.Size = new System.Drawing.Size(176, 58);
            this.btn_invite.TabIndex = 18;
            this.btn_invite.Text = "Invitar";
            this.btn_invite.UseVisualStyleBackColor = true;
            this.btn_invite.Click += new System.EventHandler(this.button1_Click_2);
            // 
            // btn_accept
            // 
            this.btn_accept.Enabled = false;
            this.btn_accept.Location = new System.Drawing.Point(318, 892);
            this.btn_accept.Name = "btn_accept";
            this.btn_accept.Size = new System.Drawing.Size(169, 58);
            this.btn_accept.TabIndex = 19;
            this.btn_accept.Text = "Aceptar";
            this.btn_accept.UseVisualStyleBackColor = true;
            this.btn_accept.Click += new System.EventHandler(this.button2_Click);
            // 
            // btn_reject
            // 
            this.btn_reject.Enabled = false;
            this.btn_reject.Location = new System.Drawing.Point(579, 892);
            this.btn_reject.Name = "btn_reject";
            this.btn_reject.Size = new System.Drawing.Size(204, 58);
            this.btn_reject.TabIndex = 20;
            this.btn_reject.Text = "Rechazar";
            this.btn_reject.UseVisualStyleBackColor = true;
            this.btn_reject.Click += new System.EventHandler(this.button3_Click);
            // 
            // btn_random
            // 
            this.btn_random.Enabled = false;
            this.btn_random.Location = new System.Drawing.Point(1145, 892);
            this.btn_random.Name = "btn_random";
            this.btn_random.Size = new System.Drawing.Size(205, 58);
            this.btn_random.TabIndex = 21;
            this.btn_random.Text = "Número aleatorio";
            this.btn_random.UseVisualStyleBackColor = true;
            this.btn_random.Click += new System.EventHandler(this.btn_random_Click);
            // 
            // label_game_no_started
            // 
            this.label_game_no_started.AutoSize = true;
            this.label_game_no_started.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_game_no_started.Location = new System.Drawing.Point(820, 450);
            this.label_game_no_started.Name = "label_game_no_started";
            this.label_game_no_started.Size = new System.Drawing.Size(837, 55);
            this.label_game_no_started.TabIndex = 22;
            this.label_game_no_started.Text = "La partida todavía no ha comenzado";
            // 
            // btn_disconnect
            // 
            this.btn_disconnect.Enabled = false;
            this.btn_disconnect.Location = new System.Drawing.Point(33, 120);
            this.btn_disconnect.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_disconnect.Name = "btn_disconnect";
            this.btn_disconnect.Size = new System.Drawing.Size(190, 78);
            this.btn_disconnect.TabIndex = 2;
            this.btn_disconnect.Text = "Desconectarse";
            this.btn_disconnect.UseVisualStyleBackColor = true;
            this.btn_disconnect.Click += new System.EventHandler(this.desconectar_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2013, 1129);
            this.Controls.Add(this.label_game_no_started);
            this.Controls.Add(this.btn_random);
            this.Controls.Add(this.btn_reject);
            this.Controls.Add(this.btn_accept);
            this.Controls.Add(this.btn_invite);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ListaConectados);
            this.Controls.Add(this.textbox_recovery_password);
            this.Controls.Add(this.radiobtn_recovery);
            this.Controls.Add(this.radiobtn_data);
            this.Controls.Add(this.btn_send);
            this.Controls.Add(this.radiobtn_scores);
            this.Controls.Add(this.btn_register);
            this.Controls.Add(this.btn_login);
            this.Controls.Add(this.textbox_password);
            this.Controls.Add(this.label_password);
            this.Controls.Add(this.label_username);
            this.Controls.Add(this.textbox_username);
            this.Controls.Add(this.btn_disconnect);
            this.Controls.Add(this.btn_connect);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(2039, 1200);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(2039, 1200);
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ruleta";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Main_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.ListaConectados)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_connect;
        private System.Windows.Forms.TextBox textbox_username;
        private System.Windows.Forms.Label label_username;
        private System.Windows.Forms.Label label_password;
        private System.Windows.Forms.TextBox textbox_password;
        private System.Windows.Forms.Button btn_login;
        private System.Windows.Forms.Button btn_register;
        private System.Windows.Forms.RadioButton radiobtn_scores;
        private System.Windows.Forms.Button btn_send;
        private System.Windows.Forms.RadioButton radiobtn_data;
        private System.Windows.Forms.RadioButton radiobtn_recovery;
        private System.Windows.Forms.TextBox textbox_recovery_password;
        private System.Windows.Forms.DataGridView ListaConectados;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_invite;
        private System.Windows.Forms.Button btn_accept;
        private System.Windows.Forms.Button btn_reject;
        private System.Windows.Forms.DataGridViewTextBoxColumn column_username;
        private System.Windows.Forms.DataGridViewTextBoxColumn column_score;
        private System.Windows.Forms.Button btn_random;
        private System.Windows.Forms.Label label_game_no_started;
        private System.Windows.Forms.Button btn_disconnect;
    }
}

