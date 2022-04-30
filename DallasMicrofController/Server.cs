using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace DallasMicrofController
{
    class Server : IDisposable
    {
        public bool IsClose = true;

        /// <summary>
        /// Работает ли сервер
        /// </summary>
        public bool IsRun
        {
            get => !IsClose;
        }

        /// <summary>
        /// При изменении статуса сервера
        /// </summary>
        public event EventHandler<ServerStatusEventArg> StatusChange;

        Thread th;
        Thread UDPThread;
        public void Start()
        {          
            Start(3322);
        }

        public void StartUDP()
        {
            UDPThread = new Thread(new ThreadStart(ReceiveUDPMessage));
            UDPThread.Start();
        }

        public ServerSetting Setting
        {
            get;
            set;
        }

        public Dallas ds;

        public void Load()
        {
            if (Settings.IsFile(ServerSetting.Path))
            {
                Setting = ServerSetting.Load();
            }
            else
            {
                Setting = new ServerSetting("Основной сервер");
                Setting.Save();
            }
        }

        TcpListener Listener; // Объект, принимающий TCP-клиентов
        UdpClient receiver;

        // Запуск сервера
        public void Start(int Port)
        {
            IsClose = false;
            // Создаем "слушателя" для указанного порта
            Listener = new TcpListener(IPAddress.Any, Port);
            Listener.Start(); // Запускаем его
           
            th = new Thread(() =>
            {
                StatusChange?.Invoke(this, new ServerStatusEventArg(ServerStatus.ServerStart));
                // В бесконечном цикле
                while (!IsClose)
                {
                    // Принимаем нового клиента
                    TcpClient Client = Listener.AcceptTcpClient();
                    Client.SendTimeout = 5000;
                    // Создаем поток
                    Thread Thread = new Thread(() => 
                    {
                        // Объявим строку, в которой будет хранится запрос клиента
                        string Request = "";
                        // Буфер для хранения принятых от клиента данных
                        byte[] Buffer = new byte[1024];
                        // Переменная для хранения количества байт, принятых от клиента
                        int Count;
                        var stream = Client.GetStream();
                        // Читаем из потока клиента до тех пор, пока от него поступают данные
                        while ((Count = stream.Read(Buffer, 0, Buffer.Length)) > 0)
                        {
                            // Преобразуем эти данные в строку и добавим ее к переменной Request
                            Request += Encoding.UTF8.GetString(Buffer, 0, Count);
                            if (Request.IndexOf("\r\n\r\n") >= 0 || Request.Length > 4096)
                            {
                                break;
                            }
                        }
                        Request = Request.Trim();
                        //Console.WriteLine(Request);

                        if (Request.Contains("rd"))
                        {
                            var nd = int.Parse(Request.Substring(2));
                            SendClient(stream, "temperature:" + (ds.Termometrs.Length > 0 ? ds.Termometrs[nd].Temperature : 0f));
                        }
                        else if (Request.Contains("sr"))
                        {
                            var nd = int.Parse(Request.Substring(2).Split('-').FirstOrDefault());
                            ds.Setting.SetResolution((DallasResolution)byte.Parse(Request.Substring(2).Split('-').LastOrDefault()));
                            ds.Setting.Save();
                            ds.SendResolution();
                            SendClient(stream, "Resolution as " + ((byte)ds.Setting.Resolution).ToString());
                        }
                        else if (Request.Contains("sn"))
                        {
                            Setting.SetName(Request.Substring(2));
                            Setting.Save();
                            SendClient(stream, "Name as " + Setting.Name);
                        }
                        else if (Request.Contains("ct"))
                        {
                            SendClient(stream, "counttermometr-" + ds.DevicesOfLines);
                        }
                        else if (Request == "rm")
                        {
                            var tmp = ds.IsConnect.ToString() + "\n";
                            tmp += ds.SN.ToString() + "\n";
                            tmp += ds.DateProduct.ToString() + "\n";
                            tmp += ds.DevicesOfLines.ToString() + "\n";
                            for (byte i = 0; i < ds.DevicesOfLines; i++)
                            {
                                tmp += ds.Termometrs[i].IsError.ToString() + "\n";
                                tmp += ds.Termometrs[i].ParasitePowers.ToString() + "\n";
                                tmp += ds.Termometrs[i].Address + "\n";
                                tmp += ((byte)ds.Termometrs[i].CurrentResolution).ToString() + "\n";
                            }
                            
                            SendClient(stream, tmp);
                        }
                        else
                        {
                            SendClient(stream, "ServerDallasMicrof-" + Setting.Name);
                        }
                        StatusChange?.Invoke(this, new ServerStatusEventArg(ServerStatus.ServerReadData));
                        // Закроем соединение
                        Client.Close();
                    });
                    // И запускаем этот поток, передавая ему принятого клиента
                    Thread.Start();
                }
            });
            th.IsBackground = true;
            th.Start();

            StartUDP();
        }

        static void SendClient(NetworkStream client, string text)
        {
            // Приведем строку к виду массива байт
            byte[] Buffer1 = Encoding.UTF8.GetBytes(text);
            // Отправим его клиенту
            client.Write(Buffer1, 0, Buffer1.Length);
        }

        private void ReceiveUDPMessage()
        {
            receiver = new UdpClient(8978); // UdpClient для получения данных
            receiver.JoinMulticastGroup(IPAddress.Parse("235.9.1.34"), 20);
            IPEndPoint remoteIp = null;
            //string localAddress = LocalIPAddress();
            try
            {
                while (!IsClose)
                {
                    byte[] data = receiver.Receive(ref remoteIp); // получаем данные

                    if(data.SequenceEqual(new byte[] { 0xac, 0xdc, 0xff }))
                    {
                        SendUDPMessage(GetAllCurrentIP()+"|", remoteIp.Port);
                        Console.WriteLine("Discover");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                receiver.Close();
            }
        }

        private static void SendUDPMessage(string Message, int port)
        {
            UdpClient sender = new UdpClient(); // создаем UdpClient для отправки
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("235.9.1.34"), port);
            try
            {
                byte[] data = Encoding.UTF8.GetBytes(Message);
                sender.Send(data, data.Length, endPoint); // отправка
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

        public Server()
        {
        }

        // Остановка сервера
        ~Server()
        {
            Dispose();
        }
        public void Dispose()
        {
            IsClose = true;
            try
            {
                if(th != null)
                    th.Abort();
                if (UDPThread != null)
                    UDPThread.Abort();
                // Если "слушатель" был создан
                if (Listener != null)
                    Listener.Stop();

                if (receiver != null)
                    receiver.Close();

                StatusChange?.Invoke(this, new ServerStatusEventArg(ServerStatus.ServerStop));
            }
            catch (Exception ex) { }
        }

        public static string[] GetCurrentIP()
        {
            // Получение имени компьютера.
            string host = Dns.GetHostName();
            // Получение ip-адреса.
            return Dns.GetHostAddresses(host).Where(tmp => tmp.AddressFamily == AddressFamily.InterNetwork).Select(t => t.ToString()).ToArray();
        }

        public static string GetAllCurrentIP()
        {
            return string.Join("\n", GetCurrentIP());
        }
    }
}
