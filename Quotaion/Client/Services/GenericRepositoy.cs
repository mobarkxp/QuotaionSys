using Microsoft.AspNetCore.Components;
using Quotaion.Shared.Models;
using System.Net.Http.Json;

namespace Quotaion.Client.Services
{
    public class GenericRepositoy<T> : IRepositoryInterface<T> where T : class
    {
        private readonly HttpClient _httpClient;
        public NavigationManager manager { get; set; }
        public GenericRepositoy(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<List<T>> GetAll(string ApiName)
        {
            return _httpClient.GetFromJsonAsync<List<T>>(ApiName);
        }

        public async Task<T> GetById(string ApiName)
        {
            var resposne=await _httpClient.GetAsync(ApiName);
            if (!resposne.IsSuccessStatusCode)
            {
                if(resposne.StatusCode==System.Net.HttpStatusCode.NotFound)
                {
                    manager.NavigateTo("/PageNotFound");
                }
            }
            return await _httpClient.GetFromJsonAsync<T>(ApiName); 
        }
        public async Task<T> AddNew(T entity, string ApiName)
        {
            var result = await _httpClient.PostAsJsonAsync(ApiName, entity);
            if(!result.IsSuccessStatusCode)
            {
                var resultmsg=await result.Content.ReadAsStringAsync();
                throw new Exception(resultmsg);
            }
            var res = result.IsSuccessStatusCode;
            return null;
        }
        public async Task DeleteById(string apiId)
        {
            _httpClient.DeleteAsync(apiId);
        }

        public async Task Update(T entity, string ApiName)
        {
            await _httpClient.PutAsJsonAsync(ApiName, entity);
        }
    }
}
