using Microsoft.AspNetCore.Components;
using Quotaion.Client.Services;
using Quotaion.Shared.Models;
using Radzen;
using System;
using System.Net.Http.Json;

namespace Quotaion.Client.Pages.Customers
{
    public partial class CustomerIndex
    {
        [Inject]
        public NavigationManager? _manager { get; set; }
        [Inject]
        public IRepositoryInterface<Customer> _service { get; set; }
        [Inject]
        public DialogService? DialogService { get; set; }
        [Inject]
        NotificationService? NotificationService { get; set; }

        public List<Customer> _customers = new();
        private bool _load = false;
        protected override  async Task OnInitializedAsync()
        {
           await GetDataAsync();
            _load = true;
        }
        
        private void OnAddClick()
        {
            _manager.NavigateTo("/AddCustomer");
        }
        private async Task OnRemoveClick(int id)
        {
          //  if (id == 0) return;
            try
            {
                
               var customer = _customers.FirstOrDefault(x => x.Id == id);
                if (customer == null) return;
            var result=await DialogService.Confirm($"هل تريد حذف {_customers.Where(x => x.Id == id).FirstOrDefault().Name} ب الفعل", "حذف عميل ", new ConfirmOptions() { OkButtonText = "نعم", CancelButtonText = "لا" });
                if (result.Value)
                {
                    await _service.DeleteById($"api/Customers/Delete/{id}");
                    _customers.Remove(_customers.Where(x => x.Id == id).FirstOrDefault());

                    NotificationService.Notify(NotificationSeverity.Success, "حذف", "تم الحذف بنجاح");
                   // deliAsync(id);
                   //await GetDataAsync();
                   // StateHasChanged();
                }
             
              
             
            }
            catch
            {

            }
          
        }
        
        private async Task GetDataAsync()
        {
            try
            {
                _customers = await _service.GetAll("api/Customers/GetAll");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

       
    }
}
