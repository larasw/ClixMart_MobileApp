using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ClixMart_MobileApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SignUp : ContentPage
	{
        private ClixMart clixMart;
		public SignUp (ClixMart clixMart)
		{
			InitializeComponent ();
            this.clixMart = clixMart;
            btnSignUp.Clicked += BtnSignUp_Clicked;
		}

        async void BtnSignUp_Clicked(object sender, EventArgs e)
        {
            if(validateInput() == true){
                User user = new User(username.Text, email.Text, password.Text);
                if (clixMart.CheckValidity(user))
                {
                    clixMart.SignUp(user);
                    App.Current.MainPage = new NavigationPage(new Homepage(clixMart));
                } else
                {
                    DisplayAlert("Incorrect User", "User is already exist", "OK");
                }
            }
        }

        private bool validateInput()
        {
            Regex emailRegex = new Regex(@"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$");
            if (username.Text == "" || email.Text == "" || password.Text == "" || confpassword.Text == ""
                || username.Text == null || email.Text == null || password.Text == null || confpassword.Text == null)
            {
                DisplayAlert("Input is not complete", "Some field is still empty", "OK");
                return false;
            } else if (!emailRegex.IsMatch(email.Text))
            {
                email.Text = "";
                DisplayAlert("Incorrect email address", "Your Email Address is incorrect", "OK");
                return false;
            } else if (!password.Text.Equals(confpassword.Text))
            {
                password.Text = "";
                confpassword.Text = "";
                DisplayAlert("Incorrect Password", "Your Password and Confirm Password does not match", "OK");
                return false;
            }
            return true;
        }
    }
}