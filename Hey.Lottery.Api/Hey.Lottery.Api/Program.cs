using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Hey.Lottery.Api
{
    class Program
    {
        static void Main(string[] args)
        {
            string baseAddress = "http://localhost:9000/";
            // Start OWIN host 
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                var client = new HttpClient();
                Console.WriteLine("服务已打开，");
                Console.WriteLine("正在打开浏览器。。。");
                Process.Start(baseAddress);
                while (true)
                {
                    Console.WriteLine("请输入T退出");
                    if (Console.ReadLine() == "T")
                    {
                        Environment.Exit(0);
                    }
                }
            }
        }
    }
}
