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
            _bll =  bll; // Asigna el servicio inyectado a la variable de instancia
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
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred");
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
                    return NotFound("Customer not found or deletion failed.");
                }
                return NoContent();
            }
            catch (SuppliersExceptions ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred");
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<Supplier>>> GetAllAsync()
        {
            try
            {
                var result = await _bll.RetrieveAllAsync();
                return Ok(result);
            }
            catch (SuppliersExceptions ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred");
            }
        }

        [HttpGet("{id}", Name = "RetrieveSuppliersAsync")]
        public async Task<ActionResult<Supplier>> RetrieveAsync(int id)
        {
            try
            {
                var supplier = await _bll.RetrieveByIdAsync(id);
                if (supplier == null)
                {
                    return NotFound("Customer not found.");
                }
                return Ok(supplier);
            }
            catch (SuppliersExceptions ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred");
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
            catch (SuppliersExceptions ex)
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