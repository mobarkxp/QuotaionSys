using Microsoft.AspNetCore.Components;
using Quotaion.Client.Services;
using Quotaion.Shared.Models;

namespace Quotaion.Client.Pages.Products
{
    public partial class ProductsIndex
    {
        [Inject]
        public NavigationManager _manager { get; set; }
        [Inject]
        public IRepositoryInterface<Product> _product_service { get; set; }
        [Inject]
        public IRepositoryInterface<Category> _category_service { get; set; }
        public List<Product> products { get; set; } = new();
        public List<Category> categories { get; set; } = new();

        Category Choosedcategory { get; set; }=new();
        protected override async Task OnInitializedAsync()
        {
            products = await _product_service.GetAll("api/Products/GetAll");
            categories = await _category_service.GetAll("api/Categories/GetAll");
        }
        private void OnAddClick()
        {
            _manager.NavigateTo("/AddProduct");
        }

    }
}
