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
	public partial class ItemPage : ContentPage
	{
        private ClixMart clixMart;
        List<PriceTemp> PriceList;
        Item item;
       
        public ItemPage(ClixMart clixMart, Item item)
        {
            InitializeComponent();
            this.clixMart = clixMart;
            this.item = item;
            setData();

            if(clixMart.User != null)
            {
                setFavorite(item.ItemId);
                btnFavorite.Clicked += BtnFavorite_Clicked;
            } else
            {
                btnFavorite.Icon = null;
            }
        }

        private void BtnFavorite_Clicked(object sender, EventArgs e)
        {
            if (btnFavorite.Icon == "heart_white_outline.png")
            {
                clixMart.AddFavoriteItem(item.ItemId);
                btnFavorite.Icon = "heart_red.png";
            }
            else
            {
                clixMart.DeleteFavoriteItem(item.ItemId);
                btnFavorite.Icon = "heart_white_outline.png";
            }
        }

        private void setFavorite(String itemId)
        {
            List<string> itemIdList = clixMart.GetFavoriteItemID();
            for(int i = 0; i<itemIdList.Count(); i++)
            {
                if(itemId == itemIdList[i])
                {
                    btnFavorite.Icon = "heart_red.png";
                }
            }
        }

        private void setData()
        {
            textItem.Text = item.ItemName;
            imageItem.Source = item.ItemPicture;
            
            PriceList = clixMart.GetItemPriceList(item.ItemId);

            double[] cheapest = clixMart.GetCheapestPrice(PriceList);
            ahBestPrice.Text = cheapest[0].ToString();
            jumboBestPrice.Text = cheapest[1].ToString();
            lidlBestPrice.Text = cheapest[2].ToString();

            Comparison.ItemsSource = PriceList;
        }
    }
}