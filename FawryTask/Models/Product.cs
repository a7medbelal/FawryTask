using FawryTask.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FawryTask.Models
{
    public class Product : IShipping
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int QuantityAvailable { get; private set; }
        public bool IsShippable { get; }
        public double Weight { get; }
        public bool IsExpirable { get; }
        public DateTime? ExpiryDate { get; }

        public Product(string name, double price, int quantity, bool isShippable, double weight = 0, bool isExpirable = false, DateTime? expiertime = null)
        {
            Name = name;
            Price = price;
            QuantityAvailable = quantity;
            IsShippable = isShippable;
            Weight = isShippable ? weight : 0; 
            IsExpirable = isExpirable;
            ExpiryDate = isExpirable ? expiertime : null;

        }

        public void UpdateQuantity(int quantity)
        {
            if (quantity < 0)
            {
                throw new ArgumentException("Quantity cannot be negative.");
            }
            QuantityAvailable = quantity;
        }

        // here if some make the order 
        public void DecreaseQuantity(int quantity)
        {
            if (quantity < 0)
            {
                throw new ArgumentException("Quantity cannot be negative.");
            }
            if (quantity > QuantityAvailable)
            {
                throw new InvalidOperationException("Insufficient quantity available.");
            }
            QuantityAvailable -= quantity;
        }

        public string GetName()=> Name;

        //here gave the weight of the product if it is shippable
        public double GetWeight() => IsShippable ? Weight : 0;

        //check if the product is expired or not
        public bool IsExpired()
        {
            return IsExpirable && ExpiryDate.HasValue && ExpiryDate.Value < DateTime.Now;
        }
    }
}
