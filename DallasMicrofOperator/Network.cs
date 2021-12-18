using System;
using System.Collections.Generic;
using System.Linq;
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
                tcpClient.Connect(ip, port);
                NetworkStream stream = tcpClient.GetStream();
                byte[] dataarr = System.Text.Encoding.UTF8.GetBytes(data + "\r\n\r\n");
                stream.Write(dataarr, 0, dataarr.Length);

                byte[] dataread = new byte[1024];
                int bytes = stream.Read(dataread, 0, dataread.Length); // получаем количество считанных байтов
                string message = Encoding.UTF8.GetString(dataread, 0, bytes);
                return message;
            }
            catch { return ""; }
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
            return SendRequest(ip, "rd"+ tid).Split('-').LastOrDefault();
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
