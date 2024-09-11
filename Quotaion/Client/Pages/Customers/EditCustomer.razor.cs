using Microsoft.AspNetCore.Components;
using Quotaion.Client.Services;
using Quotaion.Shared.Models;

namespace Quotaion.Client.Pages.Customers
{
    public partial class EditCustomer
    {

        [Parameter]
        public int Id { get; set; }

        public Customer customer { get; set; } = new();

        [Inject]
        public NavigationManager manager { get; set; }
       

        [Inject]
        public IRepositoryInterface<Customer> _customer_service { get; set; }


        protected override async Task OnInitializedAsync()
        {

            customer = await _customer_service.GetById($"api/Customers/GetById/{Id}");
        }
        private async void EditCustomerFunction()
        {


            await _customer_service.Update(customer, $"api/Customers/Update/{Id}");
            manager.NavigateTo("CustomerIndex");
        }
    }
}
