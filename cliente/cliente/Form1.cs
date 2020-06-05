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

namespace WindowsFormsApplication1
{
    
    public partial class Form1 : Form
    {
        Socket server;
        Thread atender;
        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false; //Necesario para que los elementos de los formularios puedan ser
            //accedidos desde threads diferentes a los que los crearon
        }
        private void AtenderServidor()
        {
            while (true)
            {
                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                string mensaje = Encoding.ASCII.GetString(msg2).TrimEnd('\0');
                string[] trozos = mensaje.Split('/');
                int codigo = Convert.ToInt32(trozos[0]);
                //string mensaje = mensaje = trozos[1].Split('\0')[0];
               

                switch (codigo)
                {
                    case 1:
                        MessageBox.Show(trozos[1]);
                        break;
                    case 2:
                        MessageBox.Show(mensaje);
                        break;
                    case 3:
                        MessageBox.Show(mensaje);
                        break;
                    case 4:
                        MessageBox.Show(mensaje);
                        break;
                    case 5:
                        MessageBox.Show(mensaje);
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
                        MessageBox.Show("Has recibido una invitación de partida. Haz clic en aceptar para aceptarla");
                        break;

                    case 11:
                        MessageBox.Show("La partida ha comenzado");
                        break;

                       
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            IPAddress direc = IPAddress.Parse("192.168.1.127"); // 192.168.56.102 192.168.1.127 mia 192.168.56.101  o 147.83.117.22
            IPEndPoint ipep = new IPEndPoint(direc, 50007); //9001


            //Creamos el socket 
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Connect(ipep);//Intentamos conectar el socket
                this.BackColor = Color.Green;
                MessageBox.Show("Conectado");

            }
            catch (SocketException ex)
            {
                //Si hay excepcion imprimimos error y salimos del programa con return 
                MessageBox.Show("No he podido conectar con el servidor");
                return;
            }
            ThreadStart ts = delegate { AtenderServidor(); };
            atender = new Thread(ts);
            atender.Start();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void iniciars_Click(object sender, EventArgs e)
        {
            string mensaje = "1/" + user.Text + "/" + pass.Text;
            // Enviamos al servidor el nombre tecleado
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

        }

        private void register_Click(object sender, EventArgs e)
        {
            string mensaje = "2/" + user.Text + "/" + pass.Text;
            // Enviamos al servidor el nombre tecleado
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (ganador.Checked)
            {
                string mensaje = "3/" + user.Text + "/" + pass.Text;
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

               
            }
            if (datos.Checked)
            {
                string mensaje = "4/" + user.Text + "/" + pass.Text;
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

         
            }
            if (recovery.Checked)
            {
                string mensaje = "5/" + nombre.Text; 
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

            }
        }
            
        private void ganador_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void datos_CheckedChanged(object sender, EventArgs e)
        {
        
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void desconectar_Click(object sender, EventArgs e)
        {
            //Mensaje de desconexión
            string mensaje = "0/";

            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            // Nos desconectamos
            atender.Abort();
            this.BackColor = Color.Gray;
            server.Shutdown(SocketShutdown.Both);
            server.Close();
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            string mensaje = "10/";

            foreach (DataGridViewRow r in ListaConectados.SelectedRows)
            {

                mensaje += r.Cells[0].Value.ToString() + "/";
                
            }

            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string mensaje = "11/";

            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string mensaje = "12/";

            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
        }
    }
}
