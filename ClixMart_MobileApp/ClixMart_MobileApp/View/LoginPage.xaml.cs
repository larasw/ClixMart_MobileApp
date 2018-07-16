using ClixMart_MobileApp.Controller;
using ClixMart_MobileApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ClixMart_MobileApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private ClixMart clixMart;
        public LoginPage(ClixMart clixMart)
        {
            InitializeComponent();
            this.clixMart = clixMart;
            
            btnLogin.Clicked += BtnLogin_Clicked;
            btnGuest.Clicked += BtnGuest_Clicked;
            
            TapGestureRecognizer tapSignUp = new TapGestureRecognizer();
            tapSignUp.Tapped += TapSignUp_Tapped;
            btnSignUp.GestureRecognizers.Add(tapSignUp);
        }

        async void TapSignUp_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new SignUp(clixMart));
        }

        async void BtnGuest_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new NavigationPage(new Homepage_Guest(clixMart));
        }

        async void BtnLogin_Clicked(object sender, EventArgs e)
        {
            string userName = textUser.Text;
            string pass = textPass.Text;
            LoginAsync(userName, pass);
        }

        protected override void OnAppearing()
        {
            UserLocal userLocal = clixMart.CheckData();
            if (userLocal != null)
            {
                LoginAsync(userLocal.userName, userLocal.password);
            }
        }

        async void LoginAsync(string username, string pass)
        {
            bool user = clixMart.Login(username, pass);
            if (user == true)
            {
                textUser.Text = "";
                textPass.Text = "";
                Console.WriteLine("Login Async Run");
                App.Current.MainPage = new NavigationPage(new Homepage(clixMart));
            }
            else
            {
                DisplayAlert("Incorrect input", "Incorrect Username or Password", "OK");
            }
        }
    }
}