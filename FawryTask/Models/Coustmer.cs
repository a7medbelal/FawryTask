using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FawryTask.Models
{
    public class Customer
    {
        public string Name { get; set; }

        //here i use encapsulation to prevent the customer from changing the balance directly
        private double Balance { get; set; } = 0;
        public double GetBalance() => Balance;

        public  List<CartItem> Carts { get; set; } = new List<CartItem>();
       
        public Customer(string name, double balance)
        {
            Name = name;
            Balance = balance;
        }


        // here we add the product to the cart and applay some validation
        // here we use encapsulation to prevent the customer from changing the cart directly and compostion 
        public void AddToCart(Product product, int Quantity)
        {

            if (product == null)
            {
                throw new ArgumentNullException(nameof(product), "Product cannot be null.");
            }
            if (Quantity <= 0)
            {
                //throw new ArgumentException("Quantity must be greater than zero.");
                Console.WriteLine("Quantity must be greater than zero.");
                return;
            }
            if (Quantity > product.QuantityAvailable)
            {
                throw new InvalidOperationException("Insufficient quantity available for the product.");
            }

            Carts.Add(new CartItem(product, Quantity));
            Console.WriteLine($"item {product.Name} added to cart sucessfully with quantity {Quantity}");
        }

        //this used when the customer want to remove item from the cart 
        public void ClearCart()
        {
            Carts.Clear();
            Console.WriteLine("Cart cleared successfully.");

        }

        // This method checks if the customer has enough balance to complete the process or not here
        // we appaly encapusaltion of the price i can use this methoed any where 
        public bool CheckTheBalance(double totalPrice)
        {
            if (totalPrice > Balance)
            {
                Console.WriteLine("Insufficient balance to complete the purchase.");
                return false;
            }
            else
            {
                Balance -= totalPrice;
               // Console.WriteLine($"Purchase successful. Remaining balance: {Balance}");
                return true;
            }
        }
    }
}
