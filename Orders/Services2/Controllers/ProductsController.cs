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
    public class ProductsController : ControllerBase, IProductsServices
    {
        private readonly Products _bll;

        public ProductsController(Products bll)
        {
            _bll = bll;
        }

        [HttpPost]
        public async  Task<ActionResult<Product>> CreateAsync([FromBody] Product toCreate)
        {
            try
            {
                var product = await _bll.CreateAsync(toCreate);
                return CreatedAtRoute("RetrieveProductAsync", new { id = product.Id }, product);
            }
            catch (ProductsExceptions ex)
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
            catch (ProductsExceptions ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error ocurred");
            }
        }
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetAllAsync()
        {
            try
            {
                var result = await _bll.RetrieveAllAsync();
                return Ok(result);
            }
            catch (ProductsExceptions ex)//Catch specific business log exceptions
            {
                return BadRequest(ex.Message); //Return 400 Bad Request with error message
            }
            catch (Exception)
            {
                //Log the exception
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error ocurred");
            }
        }

        [HttpGet("{id}", Name = "RetrieveProductAsync")]
        public async Task<ActionResult<Product>> RetrieveAsync(int id)
        {
            try
            {
                Product product = await _bll.RetrieveByIDAsync(id);

                if (product == null)
                {
                    return NotFound("Customer not fuond.");
                }
                return Ok(product);
            }
            catch (ProductsExceptions ce)
            {
                return BadRequest(ce.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error ocurred");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] Product toUpdate)
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
            catch (ProductsExceptions ex)
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
