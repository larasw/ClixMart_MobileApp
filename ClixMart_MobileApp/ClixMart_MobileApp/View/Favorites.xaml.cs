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
	public partial class Favorites : ContentPage
	{
        private ClixMart clixMart;
        List<Item> favItems;
        List<string> favorites;

        public Favorites() { }

        public Favorites (ClixMart clixMart)
		{
			InitializeComponent ();
            this.clixMart = clixMart;

            //favSearch.SearchButtonPressed += FavSearch_SearchButtonPressed;
            favSearch.TextChanged += FavSearch_TextChanged;
            favList.ItemSelected += FavList_ItemSelected;
		}

        protected override void OnAppearing()
        {
            favItems = null;
            favorites = null;
            favList.ItemsSource = null;
            initList();
        }

        async void FavList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            for (int i = 0; i < favorites.Count(); i++)
            {
                if (e.SelectedItem.Equals(favItems[i].ItemName))
                {
                    await Navigation.PushModalAsync(new NavigationPage(new ItemPage(clixMart, favItems[i])));
                }
            }
        }

        private void FavSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue == string.Empty)
            {
                favList.ItemsSource = favorites;
            }
            else
            {
                String keyword = e.NewTextValue;
                IEnumerable<string> result = favorites.Where(name => name.ToLower().Contains(keyword.ToLower()));
                favList.ItemsSource = result;
            }
        }

        //Search Button
        //private void FavSearch_SearchButtonPressed(object sender, EventArgs e)
        //{
        //    String keyword = favSearch.Text;
        //    IEnumerable<string> result = favorites.Where(name => name.ToLower().Contains(keyword.ToLower()));
        //    favList.ItemsSource = result;
        //}

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