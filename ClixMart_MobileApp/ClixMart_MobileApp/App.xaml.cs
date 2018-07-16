using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace ClixMart_MobileApp
{
	public partial class App : Application
	{
        private ClixMart clixMart = new ClixMart();
		public App ()
		{
			InitializeComponent();

            //MainPage = new NavigationPage(new Homepage());
            MainPage = new LoginPage(clixMart);
        }

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
