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
	public partial class CategoriesPage : ContentPage
	{
        private ClixMart clixMart;
        private List<Category> cats;
        private List<string> categories;

        public CategoriesPage() { }

		public CategoriesPage (ClixMart clixMart)
		{
			InitializeComponent ();
            this.clixMart = clixMart;
            initList();

            //catSearch.SearchButtonPressed += CatSearch_SearchButtonPressed;
            catSearch.TextChanged += CatSearch_TextChanged;
            catList.ItemSelected += CatList_ItemSelected;
		}

        async void CatList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            for(int i = 0; i<categories.Count(); i++)
            {
                if (e.SelectedItem.Equals(cats[i].CategoryName)){
                    await Navigation.PushModalAsync(new NavigationPage(new ItemListPage(clixMart, cats[i])));
                }
            }
        }

        private void CatSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(e.NewTextValue == string.Empty)
            {
                catList.ItemsSource = categories;
            } else
            {
                String keyword = e.NewTextValue;
                IEnumerable<string> result = categories.Where(name => name.ToLower().Contains(keyword.ToLower()));
                catList.ItemsSource = result;
            }
        }

        //Search Button
        //private void CatSearch_SearchButtonPressed(object sender, EventArgs e)
        //{
        //    String keyword = catSearch.Text;
        //    IEnumerable<string> result = categories.Where(name => name.ToLower().Contains(keyword.ToLower()));
        //    catList.ItemsSource = result;
        //}

        private void initList()
        {
            cats = clixMart.GetListOfCategories();
            categories = new List<string>();
            foreach (Category cat in cats)
            {
                categories.Add(cat.CategoryName);
            }
            categories.Sort();
            catList.ItemsSource = categories;
        }
    }
}