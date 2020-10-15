using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ControlsTesterClient
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClassObjectTest : ContentPage
    {
        MyClass _objValSend = new MyClass();
        MyClass _objValReceive = new MyClass();
        HubConnection hubConnection;
        public ClassObjectTest()
        {
            InitializeComponent();
        }
       
        protected async override void OnAppearing()
        {
            hubConnection = new HubConnectionBuilder().WithUrl("https://mobomedia.in/sendhub").Build();


            hubConnection.On<MyClass>("MyClassReceiveNewMessage", (value) =>
            {
                if (value.UserId == Global.UserId)
                {
                    _objValReceive = value;
                    TxtFirstName.Text = TxtLastName.Text = "";
                    TxtFirstName.Text = value.FirstName;
                    TxtLastName.Text = value.LastName;
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

        private async void btnSendMesage_Clicked(object sender, EventArgs e)
        {
            try
            {
                var action = await DisplayActionSheet("Message: Send to?", "Cancel", null, "ManojKumar", "IshuSharma", "PawanSharma");
                switch (action)
                {
                    case "ManojKumar":
                        _objValSend.UserId = "1";
                        break;
                    case "IshuSharma":
                        _objValSend.UserId = "2";
                        break;
                    case "PawanSharma":
                        _objValSend.UserId = "3";
                        break;
                    default:
                        break;
                }
                _objValSend.FirstName = TxtFirstName.Text.Trim();
                _objValSend.LastName = TxtLastName.Text.Trim();
                if (_objValReceive != _objValSend)
                {
                    // TxtUserName.Text = e.NewTextValue;
                    if (hubConnection.State == HubConnectionState.Connected)
                      await  hubConnection.SendAsync("MyClassSendMessageFromServer", _objValSend);
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}