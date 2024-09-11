using Microsoft.AspNetCore.Components;
using Quotaion.Client.Services;
using Quotaion.Shared.Models;
using Radzen;

namespace Quotaion.Client.Pages.Categories
{
    public partial class IndexCategory
    {
        [Inject]
        public NavigationManager _manager { get; set; }
        [Inject]
        public DialogService? DialogService { get; set; }
        [Inject]
        NotificationService? NotificationService { get; set; }
        [Inject]
        public IRepositoryInterface<Category> _categoryService { get; set; }
        public List<Category> _categories { get; set; } = new List<Category>();
        private bool _load=false;
        protected override async Task OnInitializedAsync()
        { 
            _categories =await _categoryService.GetAll("api/Categories/GetAll");
            _load = true;
        }
        private void OnAddClick()
        {
            _manager.NavigateTo("/CreateCategory");
        }
        private async Task OnRemoveClick(int id)
        {
            //  if (id == 0) return;
            try
            {

                var customer = _categories.FirstOrDefault(x => x.Id == id);
                if (customer == null) return;
                var result = await DialogService.Confirm($"هل تريد حذف {_categories.Where(x => x.Id == id).FirstOrDefault().Name} ب الفعل", "حذف الصنف ", new ConfirmOptions() { OkButtonText = "نعم", CancelButtonText = "لا" });
                if (result.Value)
                {
                    await _categoryService.DeleteById($"api/Categories/Delete/{id}");
                    _categories.Remove(_categories.Where(x => x.Id == id).FirstOrDefault());

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
    }
}
