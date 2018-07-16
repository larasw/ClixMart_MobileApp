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
    public partial class Homepage : TabbedPage
    {
        private ClixMart clixMart;
        public Homepage (ClixMart clixMart)
        { 
            InitializeComponent();
            this.clixMart = clixMart;
            AddTabPages();

            btnAccDetail.Clicked += BtnAccDetail_Clicked;
            btnLogout.Clicked += BtnLogout_Clicked;
        }

        private void BtnLogout_Clicked(object sender, EventArgs e)
        {
            clixMart.User = null;
            clixMart.DeleteUserLocal();
            App.Current.MainPage = new LoginPage(clixMart);
        }

        private void AddTabPages()
        {
            this.Children.Add(new CategoriesPage(clixMart));
            this.Children.Add(new Favorites(clixMart));
        }

        async void BtnAccDetail_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new AccountDetails(clixMart)));
        }
    }
}