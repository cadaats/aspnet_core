using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrdersClient
{
    public partial class Form1 : Form
    {
        HubConnection connection;
        public Form1()
        {
            Random random = new Random();
            InitializeComponent();
            lblUser.Text = "User" + random.Next();

            Connect2Hub();
        }

        private async void Connect2Hub()
        {
            connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:50975/orders")
                .Build();

            connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await connection.StartAsync();
            };

            connection.On<string, string>("NotifyOrderUpdates", (user, message) =>
            {
                var newMessage = $"{user}: {message}";
                messagesList.Items.Add(newMessage);
            });

            try
            {
                await connection.StartAsync();
                messagesList.Items.Add("Connection started");
            }
            catch (Exception ex)
            {
                messagesList.Items.Add(ex.Message);
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            connection.InvokeAsync("SendMessage", lblUser.Text, textBox2.Text);
        }
    }
}
