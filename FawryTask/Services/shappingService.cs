using FawryTask.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FawryTask.Services
{
    public class shappingService
    {
        public List<IShipping> ShippableProducts { get; set; } = new List<IShipping>();
        //here i have two cases 
        // 1-  the first  case is the shipplefee is constant per each order 
        // 2- the second case is the shipplefee is calculated based on the weight of the product (i will chosse this option)
       
        public const double ShippingFeePerOrderperKg = 5; 
        public double CalculateShippingFees(List<IShipping> ShippableProducts)
        {
            double totalWeight = ShippableProducts.Sum(c=>c.GetWeight());  
   
            return totalWeight * ShippingFeePerOrderperKg; 
        }

        public void ShippedItem(List<IShipping> ShippableProducts)
        {
            if (ShippableProducts == null || ShippableProducts.Count == 0)
            {
                Console.WriteLine("No shippable products available.");
                return;
            }
            Console.WriteLine("** Shipment notice ** ");

            foreach (var product in ShippableProducts)
            {
                Console.WriteLine($" {product.GetName()}     Weight: {product.GetWeight()} kg");
            }
            Console.WriteLine($"total wight   =  {ShippableProducts.Sum(c=>c.GetWeight())}");
        }
    } 

}