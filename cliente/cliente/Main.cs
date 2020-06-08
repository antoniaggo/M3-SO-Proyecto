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

                                    MessageBox.Show("Usuario o contraseña incorrectos");

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

                                    MessageBox.Show("Has iniciado sesión");

                                break;

                            }

                        break;

                        case 2:

                            switch (Convert.ToInt32(trozos[1]))
                            {
                                case 0:

                                    MessageBox.Show("Se ha producido un error durante el registro");

                                break;

                                case 1:

                                    MessageBox.Show("Te has registrado correctamente");

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

                            string[] res = mensaje.Split('/');

                            ListaConectados.Rows.Clear();
                            ListaConectados.RowCount = Convert.ToInt32(trozos[1]);
                            int i = 0;
                            int j = 2;

                            while (i < Convert.ToInt32(trozos[1]))
                            {

                                ListaConectados.Rows[i].Cells[0].Value = trozos[j];
                                i = i + 1;
                                j = j + 1;

                            }

                        break;

                        case 10:

                            MessageBox.Show("Has recibido una invitación de partida. Haz clic en el botón aceptar para aceptarla");
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

                            btn_random.Enabled = true;

                        break;

                        case 12:

                            MessageBox.Show("Uno de los usuarios ha rechazado tu invitación");

                            btn_invite.Enabled = true;

                        break;

                        case 13:

                            MessageBox.Show("Tu número es " + trozos[1]);

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
            IPEndPoint ip_endpoint = new IPEndPoint(IPAddress.Parse("192.168.56.102"), 9002);  //192.168.1.127 , 50007

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
            serverThread.Abort();
            serverSocket.Shutdown(SocketShutdown.Both);
            serverSocket.Close();
            Environment.Exit(0);
        }

        private void btn_random_Click(object sender, EventArgs e)
        {
            string mensaje = "13/";

            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            serverSocket.Send(msg);
        }
    }
}
