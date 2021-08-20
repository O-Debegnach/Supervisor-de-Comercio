using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Business_Layer.Server
{
    public class Server : INotifyPropertyChanged
    {
        /*        
            TcpListener--------> Espera la conexion del Cliente.        
            TcpClient----------> Proporciona la Conexion entre el Servidor y el Cliente.        
            NetworkStream------> Se encarga de enviar mensajes atravez de los sockets.        
        */

        public TcpListener server;
        private TcpClient client = new TcpClient();
        public readonly IPEndPoint ipendpoint;
        private readonly List<Connection> list = new List<Connection>();
        private readonly string nick = "srv";
        private Connection con;
        private string _recibido;

        public event PropertyChangedEventHandler PropertyChanged;

        private delegate void ServerIniciado(bool s);
        private event ServerIniciado SrvIniciado;

        public delegate void DelegadoInformacionRecibida(string info);
        public event DelegadoInformacionRecibida InformacionRecibida;


        public string Recibido
        {
            get => _recibido;
            set
            {
                _recibido = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Recibido)));
            }
        }

        private struct Connection
        {
            public NetworkStream stream;
            public StreamWriter streamw;
            public StreamReader streamr;
            public string nick;
        }

        public Server(string host)
        {
            ipendpoint = new IPEndPoint(IPAddress.Parse(host), 8000);
            SrvIniciado += Server_SrvIniciado;
            Thread t = new Thread(Inicio);
            t.Start();
            //Inicio();
        }

        public Server()
        {
            ipendpoint = new IPEndPoint(IPAddress.Parse(GetIP()), 8000);
            SrvIniciado += Server_SrvIniciado;
            Thread t = new Thread(Inicio);
            t.Start();
        }

        ~Server()
        {
            IsStarted = false;
            server.Stop();
        }
        public string GetIP()
        {
            IPHostEntry host;
            string localIP = "";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    localIP = ip.ToString();
                    break;
                }
            }
            return localIP;
        }

        private void Server_SrvIniciado(bool s)
        {
            IsStarted = true;
        }

        public void Detener()
        {
            IsStarted = false;
            server.Stop();
            threads.ForEach((x) => x.Abort(this));
        }

        List<Thread> threads = new List<Thread>();
        public void Inicio()
        {

            Console.WriteLine("Servidor OK!");

            server = new TcpListener(ipendpoint);
            server.Start();
            SrvIniciado?.Invoke(true);
            while (IsStarted)
            {
                try
                {
                    client = server.AcceptTcpClient();

                    con = new Connection
                    {
                        stream = client.GetStream()
                    };
                    con.streamr = new StreamReader(con.stream);
                    con.streamw = new StreamWriter(con.stream);

                    con.nick = con.streamr.ReadLine();

                    list.Add(con);
                    Console.WriteLine(con.nick + " se a conectado.");



                    Thread t = new Thread(Escuchar_conexion);
                    t.Start();
                    threads.Add(t);
                }
                catch { }
            }


        }

        public bool IsStarted = false;


        private void Escuchar_conexion()
        {
            Connection hcon = con;

            do
            {
                try
                {
                    string tmp = hcon.streamr.ReadLine();
                    System.Diagnostics.Debug.WriteLine(tmp);
                    InformacionRecibida?.Invoke(tmp);
                    Console.WriteLine(tmp);
                    foreach (Connection c in list)
                    {
                        try
                        {
                            c.streamw.WriteLine(tmp);
                            c.streamw.Flush();
                        }
                        catch
                        {
                        }
                    }
                }
                catch
                {
                    list.Remove(hcon);
                    Console.WriteLine(con.nick + " se a desconectado.");
                    break;
                }
            } while (IsStarted);
        }

    }

}
