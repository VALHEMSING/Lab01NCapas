using BLL;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using SLC;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;
using BLL.Exeptions;
using Microsoft.VisualBasic;

namespace Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    
    public class CustomerController : ControllerBase, ICustomerService
    {
        /*-------------------------------------------------------------------------------------------------------------------------------*/
        
        private readonly Customers _bll;//Inyeccion de dependencia
        
        /*-------------------------------------------------------------------------------------------------------------------------------*/
        
        //Contructor del controlador
        public CustomerController(Customers bll)
        {
            _bll = bll;
        }
        /*-------------------------------------------------------------------------------------------------------------------------------*/

        /*
         * Metodo para BUSCAR clientes por su ID
         */

        public async Task<ActionResult<Customer>> RetrieveAsync(int id)
        {
            try
            {
                var customer = await _bll.RetrieveByIDAsync(id);

                if(customer == null)
                {
                    return NotFound("Customer not fuond.");
                }
                return Ok(customer);
            }
            catch(CustomersExecptions ce)
            {
                return BadRequest(ce.Message);
            }
            catch(Exception )
            {
                 return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error ocurred");
            }
        }

        /*-------------------------------------------------------------------------------------------------------------------------------*/

        /*
         * Metodo para crear Customers(clientes)
         */
        public async Task<ActionResult<Customer>> CreateAsync([FromBody] Customer toCreate)
        {
            try
            {
                var customer = await _bll.CreateAsync(toCreate);
                return CreatedAtRoute("Retrieve", new { id = customer.Id }, customer);
            }
            catch(CustomersExecptions ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error ocurred");
            }
        }

        /*-------------------------------------------------------------------------------------------------------------------------------*/

        /*
         * Metodo para actualizar Customers
         */
        public async Task<IActionResult> UpdateAsync (int id, [FromBody] Customer toUpdate )
        {
            toUpdate.Id = id;
            try
            {
                var result = await _bll.UpdateAsync(toUpdate);
                if(!result)
                {
                    return NotFound("Customer not found or update failes");
                }
                return NoContent();
            }
            catch(CustomersExecptions ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error ocurred");
            }
        }

        /*-------------------------------------------------------------------------------------------------------------------------------*/
        
        /*
         * Metodo para eliminar un Customer (clinete) por su Id
         */
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var result = await _bll.DeleteAsync(id);
                if(!result)
                {
                    return NotFound("Customer not founf or deletion failed.");
                }
                return NoContent();
            }
            catch (CustomersExecptions ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error ocurred");
            }
        }
        /*-------------------------------------------------------------------------------------------------------------------------------*/

        //Metodo para Obtener todos los Customers


        


        public async Task<ActionResult<List<Customer>>> GetAllAsync()
        {
            try
            {
                var result = await _bll.RetrieveAllAsync();
                return Ok(result);//Usamos IActionResult for more flexibility (200 OK)
            }
            catch (CustomersExecptions ex)//Catch specific business log exceptions
            {
                return BadRequest(ex.Message); //Return 400 Bad Request with error message
            }
            catch(Exception)
            {
                //Log the exception
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error ocurred");
            }
        }
        /*-------------------------------------------------------------------------------------------------------------------------------*/
    }
}
