using BLL;
using BLL.Exeptions;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SLC;

namespace Services2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase, ISuppliersServices
    {
        private readonly Suppliers _bll;

        public SuppliersController(Suppliers bll)
        {
            _bll = bll;
        }


        [HttpPost]
        public async Task<ActionResult<Supplier>> CreateAsync([FromBody] Supplier toCreate)
        {
            try
            {
                var supplier = await _bll.CreateAsync(toCreate);
                return CreatedAtRoute("RetrieveSuppliersAsync", new { id = supplier.Id }, supplier);
            }
            catch (SuppliersExceptions ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error ocurred");
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var result = await _bll.DeleteAsync(id);
                if (!result)
                {
                    return NotFound("Customer not founf or deletion failed.");
                }
                return NoContent();
            }
            catch (SuppliersExceptions ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error ocurred");
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<Supplier>>> GetAllAsync()
        {
            try
            {
                var result = await _bll.RetrieveAllAsync();
                return Ok(result);//Usamos IActionResult for more flexibility (200 OK)
            }
            catch (SuppliersExceptions ex)//Catch specific business log exceptions
            {
                return BadRequest(ex.Message); //Return 400 Bad Request with error message
            }
            catch (Exception)
            {
                //Log the exception
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error ocurred");
            }
        }
        [HttpGet("{id}", Name = "RetrieveSuppliersAsync")]
        public async Task<ActionResult<Supplier>> RetrieveAsync(int id)
        {
            try
            {
                Supplier supplier = await _bll.RetrieveByIdAsync(id);

                if (supplier == null)
                {
                    return NotFound("Customer not fuond.");
                }
                return Ok(supplier);
            }
            catch (CustomersExecptions ce)
            {
                return BadRequest(ce.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error ocurred");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] Supplier toUpdate)
        {
            if (toUpdate == null)
            {
                return BadRequest("Customer data is null.");
            }

            if (id != toUpdate.Id)
            {
                return BadRequest("Mismatched customer ID.");
            }

            try
            {
                var result = await _bll.UpdateAsync(toUpdate);
                if (!result)
                {
                    return NotFound("Customer not found or update failed.");
                }
                return NoContent();
            }
            catch (CustomersExecptions ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
            }
        }
    }
}
