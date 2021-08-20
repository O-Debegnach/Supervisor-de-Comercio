using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConnectionTester
{
    public class Cliente
    {
        private static NetworkStream stream;
        private static StreamWriter streamw;
        private static StreamReader streamr;
        private static readonly TcpClient client = new TcpClient();
        private string _infoRecibida = "";
        private readonly string nick = "clt";
        public string Nick => nick;
        public Cliente(string nick)
        {
            this.nick = nick;
        }

        public bool Connected => client.Connected;

        public delegate void DelegadoInfoRecibida(string s);
        public event DelegadoInfoRecibida InfoRecibida;

        public string InformacionRecibida
        {
            get => _infoRecibida;
            set
            {
                if (value != _infoRecibida)
                {
                    _infoRecibida = value;
                }
            }
        }

        public void Post(string s)
        {
            streamw.WriteLine(nick + "þ" + s);
            streamw.Flush();
        }

        public void Stop()
        {
            client.Close();
        }
        public void Listen()
        {
            string s;
            while (client.Connected)
            {
                try
                {
                    s = streamr.ReadLine();
                    InfoRecibida?.Invoke(s);
                }
                catch
                {
                    InfoRecibida?.Invoke("");
                }
            }
        }

        public bool Conectar(string host)
        {
            try
            {
                client.Connect(host, 8000);
                if (client.Connected)
                {
                    Thread t = new Thread(Listen);

                    stream = client.GetStream();
                    streamw = new StreamWriter(stream);
                    streamr = new StreamReader(stream);

                    streamw.WriteLine(nick);
                    streamw.Flush();
                    t.Start();
                    Console.WriteLine("Conexion exitosa!");
                    return true;
                }
                else
                {
                    return false;
                    //MessageBox.Show("Servidor no Disponible");
                }
            }
            catch (SocketException se)
            {
                Console.WriteLine($"Error code {se.ErrorCode}: {se.Message}");
                return false;
            }
            catch (ObjectDisposedException oe)
            {
                Console.WriteLine($"Error code {oe.InnerException}: {oe.Message}");
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
                //MessageBox.Show("Servidor no Disponible");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Cliente cliente = new Cliente("tester");
            Console.Write("Ingrese la IP: ");
            string ip = Console.ReadLine();
            cliente.Conectar(ip);
            Console.Write("Presione cualquier tecla para salir");
            Console.ReadKey();
        }
    }
}
