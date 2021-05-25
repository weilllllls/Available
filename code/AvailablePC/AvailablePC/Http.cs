using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Http;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;
using AvailablePC.Entity;
using System.Windows.Forms;
using System.Text.Json;

namespace AvailablePC
{
    public class Http
    {
        public event Action<Request> ReceiveMessage;

        readonly Action<HttpResponseMessage> MessageProcess = null;

        private readonly HttpClient client = new HttpClient();
        public string uri { get; private set; }
        public Thread HeartBeat { get; private set; }
        public Http(string uri)
        {
            MessageProcess = DefaultMessageProcess;

            if (!File.Exists(Program.INFO_FILE))
            {
                var stream = File.Create(Program.INFO_FILE);
                stream.Close();
            }
            this.uri = uri;
            HeartBeat = new Thread(() =>
            {

                //TODO 这里暂时为了测试将心跳线程循环取消，记得还原
                while (true)
                {
                Thread.Sleep(1000 * 5);
                /*Task task = new Task(() => Request(uri, MessageProcess));
                Task delay = Task.Delay(1000 * 60 * 5);
                Task.WaitAll(task, delay);*/
                Request(uri, MessageProcess);
                Thread.Sleep(1000 * 60);

                Request(uri, MessageProcess);
                
                }
            })
            {
                IsBackground = true
            };
            HeartBeat.Start();
            //client.
        }




        private async void Request(string uri, Action<HttpResponseMessage> action)
        {
            try
            {
                var task = client.GetAsync(uri);
                await task;
                var statusCode = task.Result.StatusCode;
                if (statusCode == HttpStatusCode.OK)
                    action(task.Result);
                else return;
            }
            catch (Exception)
            {

            }
        }

        public async void Post(string url, EntityClass entity = null, FileInfo file = null, FileInfo file2 = null)
        {
            string json = entity.ToJson();

            Console.WriteLine(json);

            var content = new MultipartFormDataContent();
            if (entity != null)
                content.Add(new StringContent(entity.ToJson(), Encoding.UTF8, "application/json"), "data");
            if (file != null)
                content.Add(new ByteArrayContent(File.ReadAllBytes(file.FullName)), "file", file.Name);
            if (file2 != null)
                content.Add(new ByteArrayContent(File.ReadAllBytes(file2.FullName)), "file2", file2.Name);


            //string str = await content.ReadAsStringAsync();
            //Console.WriteLine(str);

            try
            {
                HttpResponseMessage message = await client.PostAsync(url, content);
                Console.WriteLine(message);
                Console.WriteLine("发送完成!");
                Console.WriteLine($"目标地址:{url}");
            }
            catch (Exception)
            {
                //TODO 处理网络异常
            }




        }


        private async void DefaultMessageProcess(HttpResponseMessage message)
        {

            //TODO 还有好多要DO的……这里

            HttpContent content = message.Content;
            string str = await content.ReadAsStringAsync();
            if (string.IsNullOrWhiteSpace(str))
                return;
            JsonDocument jsonDocument = JsonDocument.Parse(str);
            JsonElement json = jsonDocument.RootElement;
            try
            {
                int i = json.GetArrayLength();
                for (int j = 0; j < i; ++j)
                {
                    string strJson = json[0].ToString();
                    Request request = EntityClass.FromJson(strJson, typeof(Request)) as Request;
                    var db = Database.Util.GetInstance();
                    db.Add(request);
                    ReceiveMessage?.Invoke(request);
                    //触发网络信息接收事件？
                    DateTime time = request.Request_Time.AddSeconds(1);
                    Util.AppendFile(Program.INFO_FILE, $"{time}\n");

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}
