using System;
using System.Net;
using System.Threading.Tasks;
using Titanium.Web.Proxy;
using Titanium.Web.Proxy.EventArguments;
using Titanium.Web.Proxy.Models;

namespace TitaniumWebProxyTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var proxyServer = new ProxyServer();

            var httpEP = new ExplicitProxyEndPoint(IPAddress.Any, 14300, false);
            var httpsEP = new ExplicitProxyEndPoint(IPAddress.Any, 14301, true);

            proxyServer.AddEndPoint(httpEP);
            proxyServer.AddEndPoint(httpsEP);

            proxyServer.BeforeRequest += OnRequest;

            proxyServer.Start();

            Console.WriteLine("Press a key when you're done.");
            Console.Read();

            proxyServer.BeforeRequest -= OnRequest;
            proxyServer.Stop();
        }

        static async Task OnRequest(object sender, SessionEventArgs args)
        {
            Console.WriteLine($"Request URL = {args.WebSession.Request.RequestUri}");
        }
    }
}
