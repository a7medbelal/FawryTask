using FawryTask.Models;
using FawryTask.Services;

namespace FawryTask
{
    internal class Program
    {
        static void Main(string[] args)
        {
         var products = new List<Product>
          {
             // the last parameter in the product constructor is the expiration date 
            new Product("Cheese", 50, 100, true, 0.5 ,false, DateTime.Now.AddDays(1)),
            new Product("TV", 3000, 5, true, 10),
            new Product("Mobile Card", 20, 200, false)
           };

            var customer = new Customer("Ahmed", 500000);
            var checkoutService = new CheckoutService();

            //// here are simple test case like you give in the task 
            //customer.AddToCart(products[0], 2); 
            //customer.AddToCart(products[1], 1); 
            //customer.AddToCart(products[2], 220);
            //checkoutService.Checkout(customer); 


            //here you can test all the cases if it fails what happen you can try this also 
            while (true)
            {
                Console.WriteLine("\n Available Products:");
                for (int i = 0; i < products.Count; i++)
                {
                    var p = products[i];
                    Console.WriteLine($"{i + 1}. {p.Name} - {p.Price} EGP (In stock: {p.QuantityAvailable})");
                }
                Console.WriteLine("0. Proceed to Checkout");

                Console.Write("\nEnter product number to checkout enter 0 : ");
                if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 0 || choice > products.Count)
                {
                    Console.WriteLine("Invalid input , try again");
                    continue;
                }


                if (choice == 0)
                {
                    checkoutService.Checkout(customer);
                    break;
                }



                Product selectedProduct = products[choice - 1];

                if (selectedProduct.IsExpired())
                {
                    Console.WriteLine("Warning: This product is expired and cannot be added to cart.");
                    continue;
                }

                Console.Write($"Enter quantity for {selectedProduct.Name}: ");
                if (!int.TryParse(Console.ReadLine(), out int quantity) || quantity <= 0)
                {
                    Console.WriteLine("Invalid quantity");
                    continue;
                }

                if (quantity > selectedProduct.QuantityAvailable)
                {
                    Console.WriteLine(" Not enough stock");
                    continue;
                }


                customer.AddToCart(selectedProduct, quantity);
            }
        }
    }
    }
    

