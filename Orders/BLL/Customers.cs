using BLL.Exeptions;
using DAL;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
namespace BLL
{
    public class Customers
    {
        /*-------------------------------------------------------------------------------------------------------------------------------*/
        
        //Tarea asincrona para crear al cliente si no existe y validacion
        public async Task<Customer> CreateAsync(Customer customer)
        {
            Customer customerResult = null;
            using (var repository = RepositoryFactory.CreateRepository())
            {
                //Buscar si el nombre del cliente existe
                Customer customerSearch = await repository.RetrieveAsync<Customer>(c => c.FirstName == customer.FirstName);
                if(customerSearch == null)
                {
                    //No existe, podemos crearlo
                    customerResult = await repository.CreateAsync(customer);
                }
                else
                {
                    /*
                     * Podriamos lanzar una excepcion
                     * para modificar que el cliente ya existe.
                     * podriamos incluso crear una capa de Excepciones
                     * personalizada y consumirla desde otras capas.
                    */
                    CustomersExecptions.ThrowCustomerAlreadyExistException(customerSearch.FirstName, customerSearch.LastName);
                }
                
            }
            // Verificar si customerResult sigue siendo nulo y lanzar una excepción en caso afirmativo
            if (customerResult == null)
            {
                throw new InvalidOperationException("El cliente no pudo ser creado.");
            }
            return customerResult;

            
        }

        /*-------------------------------------------------------------------------------------------------------------------------------*/
        
        //Tarea asincrona para obtener todos los clientes
        public async Task<List<Customer>> RetrieveAllAsync()
        {
            List<Customer> result = null;
            using(var r = RepositoryFactory.CreateRepository())
            {
                /*
                 * Define le criterio de filtro para obtener todolos los clientes
                 */
                Expression<Func<Customer, bool>> allCustomersCriteria = x => true;
                result = await r.FilterAsync<Customer>(allCustomersCriteria);
            }
            return result;

            //return result ?? new List<Customer>(); // Asegurarse de que no se retorne un valor nulo
            
            /*
             * Al devolver result, utilizo el operador de coalescencia nula (??) para asegurar que si result es nulo, se devuelva una nueva
             * lista vacía (new List<Customer>()).
             */
        }

        /*-------------------------------------------------------------------------------------------------------------------------------*/
        //Tara asincora de llamar al cliente por Id
        public async Task<Customer> RetrieveByIDAsync(int id)
        {
            using (var repository = RepositoryFactory.CreateRepository())
            {
                Customer customer = await repository.RetrieveAsync<Customer>(c => c.Id == id);
                // Check if the customer was found
                if (customer == null)
                {
                    // Throw an exception if not found
                    CustomersExecptions.ThrowInvalidCustomerIdException(id);
                }

                return customer;
            }
        }



        /*-------------------------------------------------------------------------------------------------------------------------------*/

        //Tarea asincrona para actualizar o modificar
        public async Task<bool> UpdateAsync(Customer customer)
        {
            bool result = false;
            using (var repository = RepositoryFactory.CreateRepository())
            {
                //Validar que el nombre no exista
                Customer customerSearch = await repository.RetrieveAsync<Customer>(c => c.FirstName == customer.FirstName && c.Id != customer.Id);
                if( customerSearch == null)
                {
                    //No existe
                    result = await repository.UpdateAsync(customer);
                }
                else
                {
                    /*
                     * Podemos implementar alguna logica para
                     * indicar que no se pudo modificar
                     */
                    CustomersExecptions.ThrowCustomerAlreadyExistException(customerSearch.FirstName, customerSearch.LastName);
                }
            }
            return result;
        }
        /*-------------------------------------------------------------------------------------------------------------------------------*/
        
        //Tarea asincrona para eliminar
        public async Task<bool> DeleteAsync(int id)
        {
            bool result = false;
            //Buscar un cliente para ver si tiene Orders(Ordenes de compra)
            Customer customer = await RetrieveByIDAsync(id);
            if(customer != null)
            {
                //Eliminar al cliete
                using (var repository = RepositoryFactory.CreateRepository())
                {
                    result = await repository.DeleteAsync(customer);
                }
            }
            else
            {
                /*
                 * Podemos implementar alguna logica para
                 * indicar que el producto no existe
                 */
                CustomersExecptions.ThrowInvalidCustomerIdException(id);
            }
            return result;
        }

        /*-------------------------------------------------------------------------------------------------------------------------------*/

    }
}
