using ASP6.Models;
using ASP6.Services.Interfaces;

namespace ASP6.Services
{
    public class OrderService : IOrderService
    {
        public bool ValidateUserAge(int age)
        {
            return age >= 16;
        }

        public List<Product> InitializeOrderForms(int quantity)
        {
            List<Product> products = new List<Product>();
            for (int i = 0; i < quantity; i++)
            {
                products.Add(new Product());
            }
            return products;
        }
    }


}
