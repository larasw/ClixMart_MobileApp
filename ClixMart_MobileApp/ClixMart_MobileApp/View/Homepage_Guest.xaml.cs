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
    public partial class Homepage_Guest : TabbedPage
    {
        private ClixMart clixMart = new ClixMart();
        public Homepage_Guest (ClixMart clixMart)
        {
            InitializeComponent();
            this.Children.Add(new CategoriesPage(clixMart));
        }
    }
}