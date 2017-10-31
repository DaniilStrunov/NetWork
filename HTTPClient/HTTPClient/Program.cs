using System;
using System.Collections.Generic;
using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace HTTPClient
{
    public class Human
    {
        public string name { get; set; }
        public string id { get; set; }
        public string eyecolour { get; set; }
      
    }
     class Program
    {
        public static string GetHtmlCode(Uri uri)
        { 
                HttpWebRequest proxy_request = (HttpWebRequest)WebRequest.Create(uri);
                proxy_request.Method = "GET";
                proxy_request.KeepAlive = true;
                HttpWebResponse resp = proxy_request.GetResponse() as HttpWebResponse;
                string html = resp.ToString();
                using (StreamReader sr = new StreamReader(resp.GetResponseStream(), Encoding.GetEncoding(1251)))
                html = sr.ReadToEnd();
                html = html.Trim();
                return html;
        }
        static void Main(string[] args)
        {
            Uri uri = new Uri("http://127.0.0.1:8888/id/");
            Uri uriNEXT = new Uri("http://127.0.0.1:8888/");

            string Json = GetHtmlCode(uri);

            List<Human> listhuman = JsonConvert.DeserializeObject<List<Human>>(Json);

            string Json1 = "[";
            foreach (Human person in listhuman)
            {
                Json1 += GetHtmlCode(new Uri(uriNEXT, person.id));
                Json1 += ",";
            }
            Json1 += "]";

            listhuman = JsonConvert.DeserializeObject<List<Human>>(Json1);
            foreach (Human person in listhuman)
            {
                Console.WriteLine(person.name + " " + person.id + " " + person.eyecolour);
            }
            Human girlWithRadEyes = listhuman.Where(p => p.eyecolour == "Rad").First();

            Console.WriteLine("Имя девушки с красными глазами: "+ girlWithRadEyes.name);
            Console.ReadKey();
        }      
    }
}
