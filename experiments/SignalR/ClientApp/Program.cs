using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace ClientApp
{
    class Program
    {
        HubConnection connection;
        static void Main(string[] args)
        {
            Program program = new Program();
            program.InitializeConnection();
        }

        private void InitializeConnection()
        {
            Console.WriteLine("Establishing connection to hub..");
            connection = new HubConnectionBuilder()
                            .WithUrl("http://localhost:53353/ChatHub")
                            .Build();

            connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await connection.StartAsync();
            };
        }
    }
}
