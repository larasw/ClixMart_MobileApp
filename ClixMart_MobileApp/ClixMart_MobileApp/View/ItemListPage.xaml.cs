using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClixMart_MobileApp.Controller;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ClixMart_MobileApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ItemListPage : ContentPage
	{
        private ClixMart clixMart;
        private List<Item> itemList;

		public ItemListPage (ClixMart clixMart, Category category)
		{
			InitializeComponent ();
            this.clixMart = clixMart;
            setData(category);
		}

        private void setData(Category category)
        {
            CategoryController controller = new CategoryController();
            itemList = controller.GetListOfItems(category);

            int size = itemList.Count();
            int row = 0;
            if (size % 2 == 0) row = size / 2;
            else row = (size + 1) / 2;

            int listNum = 0;
            for(int i = 0; i < row; i++)
            {
                gridLayout.RowDefinitions.Add(new RowDefinition() { Height = 150 });
                for (int j = 0; j<2; j++)
                {
                    if(i == (row-1) && j == 1 && (size % 2) != 0)
                    {
                        
                    } else
                    {
                        Image image = new Image{
                            Source = itemList[listNum].ItemPicture,
                            ClassId = itemList[listNum].ItemId
                        };
                        TapGestureRecognizer onTap = new TapGestureRecognizer();
                        onTap.Tapped += OnTap_Tapped;
                        image.GestureRecognizers.Add(onTap);

                        gridLayout.Children.Add(image, j , i);
                        listNum++;
                    }
                }
            }
            
        }

        async void OnTap_Tapped(object sender, EventArgs e)
        {
            string itemId = ((Image)sender).ClassId;
            Item item = null;
            for(int i=0; i<itemList.Count(); i++)
            {
                if (itemId.Equals(itemList[i].ItemId))
                    item = itemList[i];
            }
            await Navigation.PushModalAsync(new NavigationPage(new ItemPage(clixMart, item)));
        }
    }
}