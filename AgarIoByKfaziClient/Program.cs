using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AgarIoByKfaziClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var playerName = ConfigurationManager.AppSettings["playerName"];
            var password = ConfigurationManager.AppSettings["password"];
            var hostname = ConfigurationManager.AppSettings["hostname"];
            var port = int.Parse(ConfigurationManager.AppSettings["port"]);

            if (string.IsNullOrWhiteSpace(playerName + password))
            {
                throw new ConfigurationErrorsException("PlayerName and password must be set in App.config to connect to server.");
            }

            using (var client = new TcpClient())
            {
                client.NoDelay = true;
                client.Connect(hostname, port);
                using (var writer = new StreamWriter(client.GetStream()))
                {
                    writer.AutoFlush = true;
                    using (var reader = new StreamReader(client.GetStream()))
                    {
                        var serverGateway = new AgarIoServerGateway(writer, reader);
                        serverGateway.Login(playerName, password);
                        serverGateway.JoinPlayer();

                        var agarIoPlayer = new AgarIoPlayer(serverGateway, playerName);

                        agarIoPlayer.GameLoop();
                    }
                }
            }

            Console.ReadLine();
        }
    }
}
