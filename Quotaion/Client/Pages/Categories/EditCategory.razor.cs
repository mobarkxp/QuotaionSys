using Microsoft.AspNetCore.Components;
using Quotaion.Client.Services;
using Quotaion.Shared.Models;

namespace Quotaion.Client.Pages.Categories
{
    public partial class EditCategory
    {
        [Parameter]
        public int Id { get; set; }
        [Inject]
        public NavigationManager manager { get; set; }
        public Category category { get; set; } = new();

        [Inject]
        public IRepositoryInterface<Category> _castegory_service { get; set; }
      
       
        protected override async Task OnInitializedAsync()
        {

            category = await _castegory_service.GetById($"api/Categories/GetById/{Id}");
        }
        private async void EditCategoryFunction()
        {
            try
            {
                await _castegory_service.Update(category, $"api/Categories/Update/{Id}");
                manager.NavigateTo("IndexCategory");
            }
            catch (Exception ex)
            {

            }

            
        }
    }

}
