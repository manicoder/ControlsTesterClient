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
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
        }

        private async void btnLogin_Clicked(object sender, EventArgs e)
        {


            if (txtUserName.Text == "ManojKumar")
            {
                Global.UserId = "1";
            }
            else if (txtUserName.Text == "IshuSharma")
            {
                Global.UserId = "2";
            }
            else if (txtUserName.Text == "PawanSharma")
            {
                Global.UserId = "3";
            }
            else
            {
                await DisplayAlert("Alert", "Invalid UserName", "Ok");
                return;
            }

            await Navigation.PushAsync(new ClassObjectTest());
        }
    }
}