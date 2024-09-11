using Microsoft.AspNetCore.Components;
using Quotaion.Client.Services;
using Quotaion.Shared.Models;
using Radzen;

namespace Quotaion.Client.Pages.Customers
{
    public partial class CustomerInfo
    {
        [Inject]
        NotificationService? NotificationService { get; set; }

        [Inject]
        public DialogService? DialogService { get; set; }
        [Inject]
        public NavigationManager Manager { get; set; }
        [Inject]
        public IRepositoryInterface<Customer> _customer_service { get; set; }
        public Customer Customer { get; set; }=new();
        public List<OfferNumber> offerNumbers { get; set; } = new();
        [Parameter]
        public int Id { get; set; } 
        protected override async Task OnInitializedAsync()
        {
           
            Customer =await _customer_service.GetById($"api/Customers/GetById/{Id}");
        }

        private async Task OnRemoveClick(int id)
        {
            //  if (id == 0) return;
            try
            {
                var result = await DialogService.Confirm($"هل تريد حذف {Customer.Name} ب الفعل", "حذف عميل ", new ConfirmOptions() { OkButtonText = "نعم", CancelButtonText = "لا" });
                if (result.Value)
                {
                    await _customer_service.DeleteById($"api/Customers/Delete/{id}");              
                    NotificationService.Notify(NotificationSeverity.Success, "حذف", "تم الحذف بنجاح");
                    Manager.NavigateTo("/CustomerIndex",true);
                    
                }

            }
            catch
            {

            }

        }
    }
}
