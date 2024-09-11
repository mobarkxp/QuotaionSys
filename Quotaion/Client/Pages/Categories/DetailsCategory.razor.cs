using Microsoft.AspNetCore.Components;
using Quotaion.Client.Services;
using Quotaion.Shared.Models;

namespace Quotaion.Client.Pages.Categories
{
    public partial class DetailsCategory
    {
        [Parameter]
        public int Id { get; set; }
        public Category category { get; set; } = new();
        public List<Product> products { get; set; } = new();
        private bool _load = false;
        [Inject]
        public IRepositoryInterface<Category> _castegory_service { get; set; }


        protected override async Task OnInitializedAsync()
        {
            // category =await category_service.GetById($"api/Categories/GetById/{Id}");
            category = await _castegory_service.GetById($"api/Categories/GetById/{Id}");
            _load = true;
        }
    }
}
