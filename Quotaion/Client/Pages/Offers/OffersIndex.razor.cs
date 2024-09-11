using Microsoft.AspNetCore.Components;
using Quotaion.Client.Services;
using Quotaion.Shared.Models;

namespace Quotaion.Client.Pages.Offers
{
    public partial class OffersIndex
    {
        [Inject]
        public NavigationManager  manager { get; set; }

        [Inject]
        public IRepositoryInterface<OfferNumber> ofernumbaer_service { get; set; }
        public List<OfferNumber> offerNumbers { get; set; } = new();
        private bool _isloaded=false;
        protected override async Task OnInitializedAsync()
        {
            
            offerNumbers =await ofernumbaer_service.GetAll("api/Offers/GetAll");
            _isloaded = true;
        }
        private void OnAddClick()
        {
            manager.NavigateTo("/CreateNewOffer");
        }

    }
}
