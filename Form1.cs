using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

using System.Net;
using System.Net.Sockets;

namespace K_Chat_app
{
    public partial class Form1 : Form
    {
        Socket sck;
        EndPoint eplocal, epremote;
        private bool mousedown;
        private Point posisiterakhir;
        byte[] reqcon = new byte[1800];
        bool backgroundworkstart = true;
        
        private string readlocalip()
        {
            IPHostEntry myhost;
            myhost = Dns.GetHostEntry(Dns.GetHostName());

            foreach (IPAddress ip in myhost.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }

            }
            return "0.0.0.0";
        }

        private void callmessage(IAsyncResult aresult)
        {
            try
            {
                int ukuran = sck.EndReceiveFrom(aresult, ref epremote);
                if(ukuran>0)
                {
                    byte[] dataterima = new byte[2000];
                    dataterima = (byte[])aresult.AsyncState;
                    ASCIIEncoding asciiencod = new ASCIIEncoding();
                    string pesanterkirim = asciiencod.GetString(dataterima);

                    //= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
                    if (dataterima == reqcon)
                    {
                        string requestchat = asciiencod.GetString(reqcon);
                        listMessage.Items.Add(requestchat);
                    }
                    else

                    listMessage.Items.Add(friendip_tb.Text + "\t:\t" + pesanterkirim);
                    
                    

                }
                byte[] buffer = new byte[1800];
                sck.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref epremote, new AsyncCallback(callmessage), buffer);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public Form1()
        {
            InitializeComponent();
            requestbckwork.RunWorkerAsync();

            sck = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            sck.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress,true);
            //=================================================================================
            youip_tb.Text = readlocalip();
        }

        private void Startbtn_Click(object sender, EventArgs e)
        {
            try
            {
                ASCIIEncoding Renc = new ASCIIEncoding();
                eplocal = new IPEndPoint(IPAddress.Parse(youip_tb.Text), Convert.ToInt32(youport_tb.Text));
                epremote = new IPEndPoint(IPAddress.Parse(friendip_tb.Text), Convert.ToInt32(friendport_tb.Text));
                sck.Bind(eplocal);
                sck.Connect(epremote);
                byte[] buffer = new byte[1800];
                sck.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref epremote, new AsyncCallback(callmessage), buffer);

                startbtn.Text = "Connected!";
                startbtn.BackColor = Color.Green;
                startbtn.Enabled = false;
                Sendbtn.Enabled = true;
                tbmessage.Focus();

                string requestconnect = "You are connected with " + friendip_tb.Text;
                reqcon = Renc.GetBytes(requestconnect);
                sck.Send(reqcon);
                listMessage.Items.Add("Request to chat has been send to" + friendip_tb.Text);
                backgroundworkstart = false;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Fill the correct IP and port in the white box", "ERROR 01 = " + ex.ToString());
            }

        }
        
        private void Sendbtn_Click(object sender, EventArgs e)
        {
            

            ASCIIEncoding aenc = new ASCIIEncoding();
            byte[] pesan = new byte[1800];
            pesan = aenc.GetBytes(tbmessage.Text);
            
            sck.Send(pesan);
            listMessage.Items.Add("You\t\t:\t" + tbmessage.Text);
            
            tbmessage.Clear();
        }

        private void Panel1_MouseDown(object sender, MouseEventArgs e)
        {
            mousedown = true;
            posisiterakhir = e.Location;
        }

        private void Panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if(mousedown==true)
            {
                this.Location=new Point((this.Location.X-posisiterakhir.X)+ e.X, (this.Location.Y-posisiterakhir.Y)+e.Y);
                this.Update();
            }
        }

        private void Panel1_MouseUp(object sender, MouseEventArgs e)
        {
            mousedown = false;
        }

        private void Requestbckwork_DoWork(object sender, DoWorkEventArgs e)
        {
            while(true)
            {
                if (backgroundworkstart)
                {
                    byte[] start = new byte[1800];

                    sck.BeginReceiveFrom(start, 0, start.Length, SocketFlags.None, ref epremote, new AsyncCallback(callmessage), start);
                }
                if (!backgroundworkstart)
                {
                    requestbckwork.CancelAsync();
                    break;
                }
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
