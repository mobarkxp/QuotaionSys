using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Quotaion.Client.Services;
using Quotaion.Shared.Models;
using Quotaion.Shared.TemoModels;
using Radzen;

namespace Quotaion.Client.Pages.Offers
{
    public partial class CreateNewOffer
    {
        [Inject]
        NotificationService? NotificationService { get; set; }
        [Inject]
        public IRepositoryInterface<Product> _product_service { get; set; }
        [Inject]
        public IRepositoryInterface<Customer> _customer_service { get; set; }
        [Inject]
        public IRepositoryInterface<OfferNumber> _OfferNumber_service { get; set; }
        public List<Product> _Products { get; set; } = new();
        public List<Customer> _customers { get; set; } = new();

        public List<Offer> Cart { get; set; } = new();
        public List<TempProduct> tempproducts { get; set; } = new();
        Customer CustomerChoosed=new();
        string periodTime = "10 يوم عمل";
       
      
        
        protected override async Task OnInitializedAsync()
        {
            
            _Products =await _product_service.GetAll("api/Products/GetAll");
            _customers =await _customer_service.GetAll("api/Customers/GetAll");
            
        }

        private void AddToCart(int id)
        {
            var temp=_Products.Where(x=>x.Id == id).FirstOrDefault();
            if(tempproducts.Where(x=>x.Id == id).Any())
            {
                var adding=tempproducts.Where(x=>x.Id==id).FirstOrDefault();
                adding.Qountity += 1;
                adding.NewPrice += temp.NormalPrice;
            }
            else
            {
                TempProduct tempPoduct = new TempProduct
                {
                    Id = temp.Id,
                    Name = temp.Name,
                    NewPrice = temp.NormalPrice,
                    Img = temp.Img,
                    Qountity = 1,

                };
                tempproducts.Add(tempPoduct);
                CaculateSummery();
            }
          
        }
        private async void SavaQuotion()
        {
            if(CustomerChoosed.Id !=0 && periodTime!=null)
            {
                OfferNumber offerNumber = new OfferNumber
                {
                    UserAdded = 1,
                    Customer_id = CustomerChoosed.Id,
                    PeroudTime = periodTime
                };

                await _OfferNumber_service.AddNew(offerNumber, "api/Offers/AddNew");
                NotificationService.Notify(NotificationSeverity.Success, "اشعار", "تم انشاء العرض بنجاح");

            }
            else
            {
                NotificationService.Notify(NotificationSeverity.Warning, "اشعار", "الرجاء اختيار العميل ووقت التسليم");
            }
            
        }


        private void DeleteFromCart(int id)
        {
            tempproducts.Remove(tempproducts.Where(x=>x.Id == id).FirstOrDefault());    
        }
       
      private void CaculateSummery()
        {
            foreach(var item in tempproducts)
            {
              
               

            }

        }
    }
}
