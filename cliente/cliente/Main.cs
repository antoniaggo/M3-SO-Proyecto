using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace RuletaGame
{
 
    public partial class Main : Form
    {
        Socket serverSocket;
        Thread serverThread;

        public Main()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false; //Necesario para que los elementos de los formularios puedan ser
            //accedidos desde threads diferentes a los que los crearon
        }
        private void AtenderServidor()
        {
            try
            {
                while (true)
                {
                    byte[] buffer = new byte[512];
                    serverSocket.Receive(buffer);

                    string mensaje = Encoding.ASCII.GetString(buffer).TrimEnd('\0');
                    string[] trozos = mensaje.Split('/');

                    switch (Convert.ToInt32(trozos[0]))
                    {
                        case 1:

                            switch (Convert.ToInt32(trozos[1])) {

                                case 0:

                                    MessageBox.Show("El usuario no existe");

                                break;

                                case 1:

                                    label_username.Enabled = false;
                                    label_password.Enabled = false;
                                    textbox_username.Enabled = false;
                                    textbox_password.Enabled = false;
                                    btn_login.Enabled = false;
                                    btn_register.Enabled = false;

                                    ListaConectados.Enabled = true;
                                    btn_invite.Enabled = true;
                                    btn_delete_account.Enabled = true;

                                    MessageBox.Show("Has iniciado sesión");

                                break;

                                case 2:

                                    MessageBox.Show("La contraseña es incorrecta");

                                break;

                            }

                        break;

                        case 2:

                            switch (Convert.ToInt32(trozos[1]))
                            {
                                case 0:

                                    MessageBox.Show("Ya existe un usuario con ese nombre");

                                break;

                                case 1:

                                    MessageBox.Show("Te has registrado correctamente");

                                break;

                                case 2:

                                    MessageBox.Show("Tu nombre de usuario debe tener entre 4 y 20 caracteres");

                                break;

                                case 3:

                                    MessageBox.Show("Tu contraseña debe tener entre 4 y 20 caracteres");

                                break;

                            }

                        break;

                        case 3:

                            MessageBox.Show(trozos[1]);

                       break;

                        case 4:

                            MessageBox.Show(trozos[1]);

                        break;

                        case 5:

                            switch (Convert.ToInt32(trozos[1]))
                            {
                                case 0:

                                    MessageBox.Show("No existe ningún usuario con ese nombre");

                                break;

                                case 1:

                                    MessageBox.Show("La contraseña es " + trozos[2]);

                                break;

                            }

                        break;

                        case 6:

                            ListaConectados.Rows.Clear();

                            ListaConectados.Rows.Add(trozos[2], trozos[3]);

                            for (int i = 1; i < Convert.ToInt32(trozos[1]); i++)
                            {
                                ListaConectados.Rows.Add(trozos[i + 3], trozos[i + 4]);

                            }

                        break;

                        case 8: //Chat

                            listbox_chat.Items.Add(trozos[1] + ": " + trozos[2]);

                        break;

                        case 9: //Account deleted

                            MessageBox.Show("Tu cuenta se ha borrado");

                            this.Close();

                        break;

                        case 10: //Invitation received

                            MessageBox.Show(trozos[1] + " te ha invitado a una partida. Haz clic en el botón aceptar para aceptar la invitación");
                            btn_invite.Enabled = false;
                            btn_accept.Enabled = true;
                            btn_reject.Enabled = true;
                            
                        break;

                        case 11:

                            MessageBox.Show("La partida ha comenzado");

                            label_game_no_started.Visible = false;

                            btn_invite.Enabled = false;
                            btn_accept.Enabled = false;
                            btn_reject.Enabled = false;

                            btn_chat.Enabled = true;
                            listbox_chat.Enabled = true;
                            textbox_chat.Enabled = true;
                            label_chat.Enabled = true;

                        break;

                        case 12:

                            MessageBox.Show("Uno de los usuarios ha rechazado tu invitación");

                            btn_invite.Enabled = true;

                        break;

                        case 13:

                            switch (Convert.ToInt32(trozos[1]))
                            {
                                case 0:

                                    MessageBox.Show("No has acertado. Pierdes " + trozos[2] + " puntos");

                                break;

                                case 1:

                                    MessageBox.Show("Has acertado. Ganas " + trozos[2] + " puntos");

                                break;
      
                            }

                            label_turn.Text = "No es tu turno";
                            btn_red.Enabled = false;
                            btn_black.Enabled = false;

                            break;

                        case 14: // Turn change

                            switch (Convert.ToInt32(trozos[1]))
                            {
                                case 0:

                                    label_turn.Text = "No es tu turno";
                                    btn_red.Enabled = false;
                                    btn_black.Enabled = false;

                                    break;

                                case 1:

                                    label_turn.Text = "Tu turno";
                                    btn_red.Enabled = true;
                                    btn_black.Enabled = true;

                                    break;

                            }

                        break;

                        case 15: // Final

                            MessageBox.Show("La partida ha terminado! El jugador con más puntuación es el ganador!");

                        break;
                        

                    }
                }

            } catch {

                this.Hide();

                System.Windows.Forms.MessageBox.Show("Se ha perdido la conexión con el servidor");

                this.Close();

            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            IPEndPoint ip_endpoint = new IPEndPoint(IPAddress.Parse("192.168.1.127"), 50007); 

            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                serverSocket.Connect(ip_endpoint);
                this.BackColor = Color.Green;

                btn_connect.Enabled = false;

                textbox_username.Enabled = true;
                textbox_password.Enabled = true;
                btn_login.Enabled = true;
                btn_register.Enabled = true;
                label_username.Enabled = true;
                label_password.Enabled = true;

                btn_send.Enabled = true;
                textbox_recovery_password.Enabled = true;
                radiobtn_data.Enabled = true;
                radiobtn_recovery.Enabled = true;
                radiobtn_scores.Enabled = true;

                btn_disconnect.Enabled = true;

            }
            catch (SocketException)
            {
                MessageBox.Show("No se ha podido conectar con el servidor");
                return;

            }

            ThreadStart ts = delegate { AtenderServidor(); };
            serverThread = new Thread(ts);
            serverThread.Start();

        }

        private void iniciars_Click(object sender, EventArgs e)
        {
            string mensaje = "1/" + textbox_username.Text + "/" + textbox_password.Text;

            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            serverSocket.Send(msg);

        }

        private void register_Click(object sender, EventArgs e)
        {
            string mensaje = "2/" + textbox_username.Text + "/" + textbox_password.Text;

            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            serverSocket.Send(msg);

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (radiobtn_scores.Checked)
            {
                string mensaje = "3/" + textbox_username.Text + "/" + textbox_password.Text;

                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                serverSocket.Send(msg);

            }

            if (radiobtn_data.Checked)
            {
                string mensaje = "4/" + textbox_username.Text + "/" + textbox_password.Text;

                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                serverSocket.Send(msg);

         
            }

            if (radiobtn_recovery.Checked)
            {
                string mensaje = "5/" + textbox_recovery_password.Text; 

                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                serverSocket.Send(msg);

            }
        }

        private void desconectar_Click(object sender, EventArgs e)
        {

            this.Close();
        }

        private void button1_Click_2(object sender, EventArgs e)
        {

            btn_invite.Enabled = false;

            string mensaje = "10/";

            mensaje += ListaConectados.SelectedRows.Count + "/";

            foreach (DataGridViewRow r in ListaConectados.SelectedRows)
            {

                mensaje += r.Cells[0].Value.ToString() + "/";
                
            }

            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            serverSocket.Send(msg);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string mensaje = "11/";

            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            serverSocket.Send(msg);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string mensaje = "12/";

            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            serverSocket.Send(msg);
        }
        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (serverThread != null) {

                serverThread.Abort();

            }

            if (serverSocket != null) {

                serverSocket.Shutdown(SocketShutdown.Both);
                serverSocket.Close();

            }

            Environment.Exit(0);
        }

        private void btn_random_Click(object sender, EventArgs e)
        {

            string mensaje = "13/";

            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            serverSocket.Send(msg);

        }

        private void btn_delete_account_Click(object sender, EventArgs e)
        {
            string mensaje = "9/";

            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            serverSocket.Send(msg);

        }

        private void btn_chat_Click(object sender, EventArgs e)
        {
            string mensaje = "8/" + textbox_chat.Text + "/";

            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            serverSocket.Send(msg);

            textbox_chat.Text = "";
        }

        private void textbox_username_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (e.KeyChar == '/');
        }

        private void textbox_password_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (e.KeyChar == '/');
        }

        private void textbox_recovery_password_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (e.KeyChar == '/');
        }

        private void textbox_chat_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (e.KeyChar == '/');
        }

        private void btn_black_Click(object sender, EventArgs e)
        {
            string mensaje = "14/";

            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            serverSocket.Send(msg);
        }
    }
}
