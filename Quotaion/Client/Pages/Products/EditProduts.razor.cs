using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Quotaion.Client.Services;
using Quotaion.Shared.DtoModels;
using Quotaion.Shared.Models;

namespace Quotaion.Client.Pages.Products
{
    public partial class EditProduts
    {
        [Parameter]
        public int Id { get; set; }
        IBrowserFile browserFile = null;
        string imgename = string.Empty;
        public Product product { get; set; } = new();
        public ProductDto productDto { get; set; } = new();
        public List<Category> categories { get; set; } = new();

        [Inject]
        public IRepositoryInterface<Product> product_service { get; set; }

        [Inject]
        public IRepositoryInterface<ProductDto> productDto_service { get; set; }
        [Inject]
        public NavigationManager manager { get; set; }
        [Inject]
        public IRepositoryInterface<Category> category_service { get; set; }

        protected override async Task OnInitializedAsync()
        {
            categories = await category_service.GetAll("api/Categories/GetAll");

            product = await product_service.GetById($"api/Products/GetById/{Id}");
            // productDto = await productDto_service.GetById($"api/Products/GetById/{Id}");
            productDto = new ProductDto
            {
                Code = product.Code,
                Name = product.Name,
                Category_id = product.Category_id,
                CompanyName = product.CompanyName,
                Description = product.Description,
                Img = product.Img,
                NormalPrice = product.NormalPrice,

            };
           
        }
        private async void EditFuction() 
        {
            if (browserFile != null)
            {
                productDto.Img = browserFile.Name;
                using (var ms = new MemoryStream())
                {
                    await browserFile.OpenReadStream().CopyToAsync(ms);
                    productDto.ImageFile = ms.ToArray();
                    ms.Dispose();
                }
            }
            try
            {
                await productDto_service.Update(productDto, $"api/Products/Update/{Id}");
                manager.NavigateTo("ProductsIndex");

            }
            catch (Exception ex)
            {
                throw new Exception();
            }

           
        }
        private async Task HandleImageUpload(InputFileChangeEventArgs e)
        {
            browserFile = e.File;
            var buffer = new byte[browserFile.Size];
            await browserFile.OpenReadStream().ReadAsync(buffer);
            string igmtype = browserFile.ContentType;
            imgename = $"data:{igmtype};Base64,{Convert.ToBase64String(buffer)}";
            StateHasChanged();
        }

    }
}
