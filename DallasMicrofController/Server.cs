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
        public bool IsClose = false;
        Thread th;
        public void Start()
        {
            Start(3322);
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

        // Запуск сервера
        public void Start(int Port)
        {            
            // Создаем "слушателя" для указанного порта
            Listener = new TcpListener(IPAddress.Any, Port);
            Listener.Start(); // Запускаем его
           
            th = new Thread(() =>
            {
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
                            SendClient(stream, "temperature-" + ds.Termometrs[nd].Temperature);
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
                            tmp += ds.DevicesOfLines.ToString() + "\n";
                            for (byte i = 0; i < ds.DevicesOfLines; i++)
                            {
                                tmp += ds.Termometrs[i].IsError.ToString() + "\n";
                                tmp += ds.Termometrs[i].Address + "\n";
                                tmp += ((byte)ds.Termometrs[i].CurrentResolution).ToString() + "\n";
                            }
                            
                            SendClient(stream, tmp);
                        }
                        else
                        {
                            SendClient(stream, "ServerDallasMicrof-" + Setting.Name);
                        }

                        // Закроем соединение
                        Client.Close();
                    });
                    // И запускаем этот поток, передавая ему принятого клиента
                    Thread.Start();
                }
            });
            th.IsBackground = true;
            th.Start();
        }

        static void SendClient(NetworkStream client, string text)
        {
            // Приведем строку к виду массива байт
            byte[] Buffer1 = Encoding.UTF8.GetBytes(text);
            // Отправим его клиенту
            client.Write(Buffer1, 0, Buffer1.Length);
        }

        public Server()
        {
        }

        // Остановка сервера
        ~Server()
        {
            IsClose = true;
            // Если "слушатель" был создан
            if (Listener != null)
            {
                // Остановим его
                Listener.Stop();
            }
        }
        public void Dispose()
        {
            IsClose = true;
            try
            {
                //th.Abort();
            }
            catch (Exception ex) { }
        }
    }
}
