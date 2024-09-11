using Microsoft.AspNetCore.Components;
using Quotaion.Client.Services;
using Quotaion.Shared.Models;
using Radzen;

namespace Quotaion.Client.Pages.Products
{
    public partial class DetailsProducts
    {
        [Parameter]
        public int Id { get; set; }
        [Inject]
        public DialogService DialogService { get; set; }
        [Inject]
        public NavigationManager Manager { get; set; }
        [Inject]
        public NotificationService NotificationService { get; set; }
        [Inject]
        public IRepositoryInterface<Product> produc_service { get; set; }

        public Product product { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            
            product =await produc_service.GetById($"api/Products/GetById/{Id}");
        }


        private async Task OnRemoveClick(int id)
        {
            //  if (id == 0) return;
            try
            {
                var result = await DialogService.Confirm($"هل تريد حذف {product.Name} ب الفعل", "حذف منتج ", new ConfirmOptions() { OkButtonText = "نعم", CancelButtonText = "لا" });
                if (result.Value)
                {
                    await produc_service.DeleteById($"api/Products/Delete/{id}");
                    NotificationService.Notify(NotificationSeverity.Success, "حذف", "تم الحذف بنجاح");
                    Manager.NavigateTo("/ProductsIndex", true);

                }

            }
            catch
            {

            }

        }
    }
}
