using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyServer.Interfaces
{
    public interface ICustomerProxy
    {
        /*
         * Metodo Asincrono para CREAR un cliente
         */
        Task<Customer> CreateAsync(Customer customer);

        /*
         * Metodo Asincrono para ELIMINAR un cliente
         */
        Task<bool> DeleteAsync(int id);


        /*
         * Metodo Asincrono para OBTERNER TODOS los clientes
         */
        Task<List<Customer>> GetAllAsync();

        /*
         * Metodo Asincrono para Obtener un cliente por Id
         */
        Task<Customer> GetByIdAsync(int id);

        /*
         * Metodo Asincrono para Actualizar un cliente
         */

        Task<bool> UpdateAsync(int id, Customer customer);
    }
}
