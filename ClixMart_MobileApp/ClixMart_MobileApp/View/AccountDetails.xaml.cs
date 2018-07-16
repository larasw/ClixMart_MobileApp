using ClixMart_MobileApp.Controller;
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
	public partial class AccountDetails : ContentPage
	{
        private ClixMart clixMart;
        List<Item> favItems;
        List<string> favorites;
        private User user;
        private bool editUser, editEmail, editPass;
        private UserController userController = new UserController();

		public AccountDetails (ClixMart clixMart)
		{
			InitializeComponent ();
            this.clixMart = clixMart;
            this.user = clixMart.User;
            initUser();
            initList();

            editUser = false;
            editEmail = false;
            editPass = false;

            btnUserName.Clicked += BtnUserName_Clicked;
            btnEmail.Clicked += BtnEmail_Clicked;
            btnPass.Clicked += BtnPass_Clicked;
		}

        private void BtnPass_Clicked(object sender, EventArgs e)
        {
            if(editPass == false)
            {
                editPass = true;
                textPass.IsEnabled = true;
                btnPass.Text = "Save";
            }
            else
            {
                if (textPass.Text != user.Password) userController.updateUser(user, "password", textPass.Text);
                clixMart.UpdateUserData("password", textPass.Text);
                editPass = false;
                textPass.IsEnabled = false;
                btnPass.Text = "Change";
            }
        }

        private void BtnEmail_Clicked(object sender, EventArgs e)
        {
            if (editEmail == false)
            {
                editEmail = true;
                textEmail.IsEnabled = true;
                btnEmail.Text = "Save";
            }
            else
            {
                if (textEmail.Text != user.Email) userController.updateUser(user, "email", textEmail.Text);
                editEmail = false;
                textEmail.IsEnabled = false;
                btnEmail.Text = "Change";
            }
        }

        private void BtnUserName_Clicked(object sender, EventArgs e)
        {
            if (editUser == false)
            {
                editUser = true;
                textUserName.IsEnabled = true;
                btnUserName.Text = "Save";
            }
            else
            {
                if(textUserName.Text != user.Username) userController.updateUser(user, "username", textUserName.Text);
                clixMart.UpdateUserData("username", textUserName.Text);
                editUser = false;
                textUserName.IsEnabled = false;
                btnUserName.Text = "Change";
            }
        }

        private void initUser()
        {
            textUserName.Text = user.Username;
            textPass.Text = user.Password;
            textEmail.Text = user.Email;
        }

        private void initList()
        {
            favItems = clixMart.GetFavoriteItemList();
            favorites = new List<string>();

            foreach (Item item in favItems)
            {
                favorites.Add(item.ItemName);
            }
            favorites.Sort();
            favList.ItemsSource = favorites;
        }
	}
}