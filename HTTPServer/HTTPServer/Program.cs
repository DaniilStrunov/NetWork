using System;
using System.Net;
using System.IO;

namespace NetConsoleApp
{
    class Program
    {
        public static void ShowInfirmaition(HttpListenerContext context, string str)
        {
            HttpListenerResponse response = context.Response;
            string responseStr = str;
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseStr);
            response.ContentLength64 = buffer.Length;
            Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            output.Close(); 
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Ожидание подключений...");
            while (true)
            {
                HttpListener listener = new HttpListener();

                listener.Prefixes.Add("http://127.0.0.1:8888/id/");
                listener.Prefixes.Add("http://127.0.0.1:8888/1/");
                listener.Prefixes.Add("http://127.0.0.1:8888/2/");
                listener.Prefixes.Add("http://127.0.0.1:8888/3/");
                listener.Prefixes.Add("http://127.0.0.1:8888/4/");
                listener.Start();
                HttpListenerContext context = listener.GetContext();
                string str = context.Request.Url.ToString();
                switch (str)
                {
                    case "http://127.0.0.1:8888/id/":
                        ShowInfirmaition(context, @"[{""name"":""Dasha"",""id"":""4/""},{ ""name"":""Natasha"",""id"":""2/""},{""name"":""Katya"", ""id"":""1/""},{""name"":""Nastya"",""id"":""3/""}]");
                        listener.Close();
                        break;
                    case "http://127.0.0.1:8888/1/":
                        ShowInfirmaition(context, @"{""name"":""Katya"",""id"":""1/"", ""eyeColour"":""Rad""}");
                        listener.Close();
                        break;
                    case "http://127.0.0.1:8888/2/":
                        ShowInfirmaition(context, @"{""name"":""Natasha"",""id"":""2/"", ""eyeColour"":""Gray""}");
                        listener.Close();
                        break;
                    case "http://127.0.0.1:8888/3/":
                        ShowInfirmaition(context, @"{""name"":""Nastya"",""id"":""3/"", ""eyeColour"":""Green""}");
                        listener.Close();
                        break;
                    case "http://127.0.0.1:8888/4/":
                        ShowInfirmaition(context, @"{""name"":""Dasha"",""id"":""4/"", ""eyeColour"":""Blue""}");
                        listener.Close();
                        break;
                }
            }
        }
    }
}