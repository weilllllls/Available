using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;


namespace AvailablePC
{
    delegate void NetStringHandler(string str);

    [Obsolete]
    class Backgroud
    {
        /**
         <summary>绑定该事件以处理受到的信息</summary>
         */
        public event NetStringHandler GetNetString; 

        private const int port = 1234;
        private const int MAXSIZE = 50;
        //private const string ip = "127.0.0.1";
        private bool active = true;

        TcpListener listener = new TcpListener(IPAddress.Any,port);
        
        //List<TcpClient> tcps;
        public Backgroud()
        {
            new Thread(() =>
            {
                listener.Start(MAXSIZE);
                while (active)
                {
                    var connection = listener.AcceptTcpClient();
                    //tcps.Add(connection);
                    Receive(connection);
                }
            }).Start();         
        }


        private void Receive(TcpClient client)
        {
            new Thread(()=> { 
                NetworkStream stream = client.GetStream();
                StreamReader reader = new StreamReader(stream);
                string str = reader.ReadToEnd();
                GetNetString?.Invoke(str);
                client.Close();
            }).Start();
        }


        private void NetString(string str)
        {
            Util.AppendFile("",str);
        }

        public void Close()
        {
            active = false;
        }

    }
}
