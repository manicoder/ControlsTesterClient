using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ControlsTesterClient
{
    public partial class MainPage : ContentPage
    {
        HubConnection hubConnection;
        public string ReceiveValue = string.Empty;
        public MainPage()
        {
            InitializeComponent();
        }
        int i = 0;
        protected async override void OnAppearing()
        {
            hubConnection = new HubConnectionBuilder().WithUrl("https://mobomedia.in/sendhub").Build();

            hubConnection.On<string>("ReceiveNewMessage", (value) =>
            {
                ReceiveValue = value;
                if (ReceiveValue == "buttonClickIshu")
                {
                    lblButton.Text = "Button Clicked ";
                }
                else
                {
                    TxtUserName.Text = value;
                }
            });
            if (hubConnection.State == HubConnectionState.Disconnected)
            {
                try
                {
                    await hubConnection.StartAsync();
                }
                catch (System.Exception ex)
                {
                    throw;
                }
            }
            // view1.SetOnTouchListener(this);
        }


        private void TxtUserName_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (ReceiveValue != e.NewTextValue)
                {
                    // TxtUserName.Text = e.NewTextValue;
                    if (hubConnection.State == HubConnectionState.Connected)
                        hubConnection.SendAsync("SendMessageFromServer", e.NewTextValue);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (hubConnection.State == HubConnectionState.Connected)
                hubConnection.SendAsync("SendMessageFromServer", "buttonClickIshu");
        }
    }
}
