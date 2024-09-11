using Blazorise;
using Blazorise.States;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Quotaion.Client.Services;
using Quotaion.Shared.DtoModels;
using Quotaion.Shared.Models;
using Radzen;
using Radzen.Blazor;

namespace Quotaion.Client.Pages.Products
{
    public partial class AddProduct
    {
        IBrowserFile browserFile=null;
        string imgename=string.Empty;
        public ProductDto productDto { get; set; } = new();
        [Inject]
        public DialogService dialogService { get; set; }
        public List<Category> categories { get; set; } = new();
        [Inject]
        public IRepositoryInterface<Category> category_service { get; set; }

        [Inject]
        public IRepositoryInterface<ProductDto> productDto_service { get; set; }
        [Inject]
        public NavigationManager navigation { get; set; }
        bool busy;
        protected override async Task OnInitializedAsync()
        {
                categories = await category_service.GetAll("api/Categories/GetAll");
           
        }
        private async Task SaveNewAsync()
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
                busy = true;
                await productDto_service.AddNew(productDto, "api/Products/AddNew");
                navigation.NavigateTo("ProductsIndex");
                busy = false;
            }
            catch (Exception ex)
            {
                throw new Exception();
            }

        }
        private async Task HandleImageUpload(InputFileChangeEventArgs e)
        {
            browserFile=e.File;
            var buffer=new byte[browserFile.Size];
            await browserFile.OpenReadStream().ReadAsync(buffer);
            string igmtype = browserFile.ContentType;
            imgename = $"data:{igmtype};Base64,{Convert.ToBase64String(buffer)}";
           StateHasChanged();
        }


        private async Task showAddCatAsync()
        {
          
         
        }
        async Task OnBusyClick()
        {
            busy = true;
            await Task.Delay(2000);
            busy = false;
        }

    }
}
