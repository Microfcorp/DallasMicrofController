using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace DallasMicrofOperator
{
    public class Network
    {
        public static string SendRequest(string ip, string data, int port = 3322)
        {
            try
            {
                TcpClient tcpClient = new TcpClient();
                tcpClient.ReceiveTimeout = 1000;
                tcpClient.SendTimeout = 1000;
                
                tcpClient.BeginConnect(ip, port, null, null);
                for (int i = 0; i < 1000; i++)
                {
                    System.Threading.Thread.Sleep(1);
                    if (tcpClient.Connected) break;
                }
                
                if (!tcpClient.Connected) return "";
                NetworkStream stream = tcpClient.GetStream();
                stream.ReadTimeout = 2000;
                stream.WriteTimeout = 2000;
                byte[] dataarr = System.Text.Encoding.UTF8.GetBytes(data + "\r\n\r\n");
                stream.Write(dataarr, 0, dataarr.Length);
                
                byte[] dataread = new byte[1024];
                int bytes = stream.Read(dataread, 0, dataread.Length); // получаем количество считанных байтов
                string message = Encoding.UTF8.GetString(dataread, 0, bytes);
                return message;
            }
            catch { return ""; }
        }

        public static void SearchServers(EventHandler @event)
        {
            UdpClient sender = new UdpClient(8979); // создаем UdpClient для отправки
            sender.JoinMulticastGroup(IPAddress.Parse("235.9.1.34"), 20);           
            //sender.
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("235.9.1.34"), 8978);
            IPEndPoint remoteIp = null;
            try
            {
                byte[] data = { 0xac, 0xdc, 0xff };
                sender.Send(data, data.Length, endPoint); // отправка
                System.Threading.Thread.Sleep(100);
                if (sender.Available > 0)
                {
                    byte[] dataa = sender.Receive(ref remoteIp);
                    var text = Encoding.UTF8.GetString(dataa);
                    @event?.Invoke(remoteIp.Address.ToString(), new EventArgs());
                    foreach (var item in text.Split('|'))
                        if (item != "")
                            @event?.Invoke(item, new EventArgs());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sender.Close();
            }
        }

        public static string GetServerName(string ip)
        {
            var t = SendRequest(ip, "ping");
            return t.Split('-').LastOrDefault();
        }
        public static void SetServerName(string ip, string name)
        {
            SendRequest(ip, "sn"+name);
        }
        public static string GetServerTemepature(string ip, string tid)
        {
            return SendRequest(ip, "rd"+ tid).Split(':').LastOrDefault();
        }
        public static string GetServerInfo(string ip)
        {
            return SendRequest(ip, "rm");
        }
        public static byte GetServerNemberTermometrs(string ip)
        {
            var t = SendRequest(ip, "ct");
            if(t != "")
                return byte.Parse(t.Split('-').LastOrDefault());
            return 0;
        }
        public static void SetServerResol(string ip, string resol, string tid)
        {
            SendRequest(ip, "sr"+tid + "-" + resol);
        }
    }
}
