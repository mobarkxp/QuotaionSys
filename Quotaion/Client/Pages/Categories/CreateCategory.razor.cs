using Microsoft.AspNetCore.Components;
using Quotaion.Client.Services;
using Quotaion.Shared.Models;

namespace Quotaion.Client.Pages.Categories
{
    public partial class CreateCategory
    {
        [Inject]
        public NavigationManager manager { get; set; }
        [Inject]
        public IRepositoryInterface<Category> categoryService { get; set; }
        public Category category { get; set; } = new();

        private async void SaveNew()
        {
            await categoryService.AddNew(category, "api/Categories/AddNew");
            manager.NavigateTo("IndexCategory");
        }
    }
}
