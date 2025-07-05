using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FawryTask.Models
{
    public class CartItem
    {
        public Product Product { get; set; }
        public int QuantityToOrder { get; set; }
    
       public CartItem(Product product, int quantityToOrder)
        {
            //here we put some validation for the product and quantity to prevent the appliction from Exciptions
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product), "Product cannot be null.");
            }
            if (quantityToOrder <= 0)
            {
                throw new ArgumentException("Quantity to order must be greater than zero.", nameof(quantityToOrder));
            }
            if (quantityToOrder > product.QuantityAvailable)
            {
                throw new InvalidOperationException("Insufficient quantity available for the product.");
            }
            Product = product;
            QuantityToOrder = quantityToOrder;
        }

        // This method calculates the total price for the cart item based on the product price and quantity to order
        public double GetTotalPrice()
        {
            return Product.Price * QuantityToOrder;
        }
    }
}
