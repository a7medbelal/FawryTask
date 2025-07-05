using FawryTask.Interfaces;
using FawryTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FawryTask.Services
{
    public class CheckoutService
    {
        public shappingService ShippingService { get; set; } = new shappingService();
        public CheckoutService() { }

        public void Checkout(Customer Customer)
        {
            if(Customer == null)
            {
                throw new ArgumentNullException("Customer cannot be null.");
            }
            if (Customer.Carts == null || !Customer.Carts.Any())
            {
                Console.WriteLine("Cart is empty. Please add items to the cart before checking out.");
                return;
            }

            #region PriceOfTheCart
            var Subtotal = Customer.Carts.Sum(c => c.GetTotalPrice());

            var itemShipped = GetShippableProducts(Customer.Carts);

            var Fees = ShippingService.CalculateShippingFees(itemShipped);

            var TotalPrice = Subtotal + Fees;

            //here we check if the customer has enough balance to complete the process or not
            if (!Customer.CheckTheBalance(TotalPrice))
            {
                return;
            } 
            #endregion
            ShippingService.ShippedItem(itemShipped);
            Console.WriteLine($"----------------------");

            PrintItems(Customer.Carts);
            Console.WriteLine($"----------------------");
            CheckouReceipt(Subtotal, Fees, TotalPrice, Customer.GetBalance());
            Customer.ClearCart();
        }

        // here we get the shippable products from the cart of the customer and cast as it shipple
        private List<IShipping> GetShippableProducts(List<CartItem> cart)
        {
            if (cart == null)
            {
                return new List<IShipping>();
            }
            return cart
                .Where(c => c.Product.IsShippable)
                .Select(c => c.Product as IShipping)
                .ToList();
        }


        private void PrintItems(List<CartItem> cart)
        {
            if (cart == null || !cart.Any())
            {
                Console.WriteLine("Cart is empty.");
                return;
            }
            Console.WriteLine("Items in the cart:");
            foreach (var item in cart)
            {
                Console.WriteLine($"{item.QuantityToOrder} * {item.Product.Name}     Total Price: {item.GetTotalPrice():C}");
            }
        }


        private void CheckouReceipt(double SubTotal , double fees , double TotalPrice , double CustomerBalance)
        {
            Console.WriteLine("** Checkout Receipt: **");
            Console.WriteLine($"Subtotal: {SubTotal:C}");
            Console.WriteLine($"Shipping Fees: {fees:C}");
            Console.WriteLine($"Total Price: {TotalPrice:C}");
            Console.WriteLine($"Customer Balance: {CustomerBalance:C}");
            Console.WriteLine("Thank you for your purchase!");

        }

    }
}
