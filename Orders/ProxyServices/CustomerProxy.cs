using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProxyServer.Interfaces;
using Entities.Models

using System.Net.Http.Headers;
using System.Text.Json;:

namespace ProxyServer
{
    public class CustomerProxy : ICustomerProxy
    {
        private readonly HttpClient _httpClient;

        //Constructor
        public CustomerProxy()
        {
            //Inicializamos 
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7045/api/Customer/")//Asegurarse de que conicidad con el servidor
            };
            //
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }



        public async Task<List<Customer>> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync(" ");
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<Customer>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            }
            catch (global::System.Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;

            }
        }

        public async Task<Customer> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{id}");
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Customer>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch (global::System.Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }

        public async Task<Customer> CreateAsync(Customer customer)
        {
            try
            {
                var json = JsonSerializer.Serialize(customer);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(" ", content);
                response.EnsureSuccessStatusCode();
                var responseJson = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Customer>(responseJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch (global::System.Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }
        /*
        public async Task<Customer> CeateAsync(Customer customer)
        {
            try
            {
                var json = JsonSerializer.Serialize(customer);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(" ", content);
                response.EnsureSuccessStatusCode();
                var responseJson = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Customer>(responseJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch (global::System.Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }

        }
        */

        public async Task<bool> UpdateAsync (int id, Customer customer)
        {
            try
            {
                var json = JsonSerializer.Serialize(customer);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"{id} ", content);
                return response.IsSuccessStatusCode;
            }
            catch (global::System.Exception ex)
            {
                throw;
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

        
    }
}
