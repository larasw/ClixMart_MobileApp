using System;
using System.Collections.Generic;
using System.Text;

namespace ClixMart_MobileApp
{
    public class PriceTemp
    {
        public string brandName { get; set; }
        public double AHPrice { get; set; }
        public double JumboPrice { get; set; }
        public double LidlPrice { get; set; }
        public PriceTemp(string brand, double ah, double jumbo, double lidl)
        {
            this.brandName = brand;
            this.AHPrice = ah;
            this.JumboPrice = jumbo;
            this.LidlPrice = lidl;
        }
    }
}
