using ASP6.Models;

namespace ASP6.Services.Interfaces
{
    public interface IOrderService
    {
        bool ValidateUserAge(int age);
        List<Product> InitializeOrderForms(int quantity);
    }


}
