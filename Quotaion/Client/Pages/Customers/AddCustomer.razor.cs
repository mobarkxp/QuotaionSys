using Microsoft.AspNetCore.Components;
using Quotaion.Client.Services;
using Quotaion.Shared.Models;

namespace Quotaion.Client.Pages.Customers
{
    public partial class AddCustomer
    {
        [Inject]
        public NavigationManager manager { get; set; }

        [Inject]
        public IRepositoryInterface<Customer> _customerService { get; set; }

        public Customer customer { get; set; } = new();
        bool busy=false;
        private async void SaveNewCustomer()
        {
            busy = true;
            customer.UserAddedId = 1;

           await _customerService.AddNew(customer, "api/Customers/AddNew");
            manager.NavigateTo("CustomerIndex");
            busy = false;
        }
    }
}
