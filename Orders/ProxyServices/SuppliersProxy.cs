using Entities.Models;
using ProxyServer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace ProxyServer
{
    public class SuppliersProxy : ISuppliersProxy
    {
        private readonly HttpClient _httpClient;

        public SuppliersProxy()
        {
             
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7228/api/Customer/")
                //https://localhost:7045/api/Customer/
                //https://localhost:7228/swagger/index.html//Asegurarse de que conicidad con el servidor
            };
         
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


        }
        public async Task<Supplier> CreateAsync(Supplier supplier)
        {
            try
            {
                var json = JsonSerializer.Serialize(supplier);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(" ", content);
                response.EnsureSuccessStatusCode();
                var responseJson = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Supplier>(responseJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch (global::System.Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{id}");
                return response.IsSuccessStatusCode;
            }
            catch (global::System.Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        public async Task<List<Supplier>> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("");
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<Supplier>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }

        public async Task<Supplier> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{id}");
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Supplier>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch (global::System.Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> UpdateAsync(int id, Supplier supplier)
        {
            try
            {
                var json = JsonSerializer.Serialize(supplier);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"{id}", content);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                // Log the error, potentially with more context
                Console.WriteLine($"Error updating customer {id}: {ex.Message}");
                throw;
            }
        }
    }
}
