using BLL;
using BLL.Exeptions;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SLC;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public async Task<ActionResult<Product>> CreateAsync([FromBody] Product toCreate)
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
                    return NotFound("Product not found or deletion failed.");
                }
                return NoContent();
            }
            catch (ProductsExceptions ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred");
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
            catch (ProductsExceptions ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred");
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
                    return NotFound("Product not found.");
                }
                return Ok(product);
            }
            catch (ProductsExceptions ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] Product toUpdate)
        {
            if (toUpdate == null)
            {
                return BadRequest("Product data is null.");
            }

            if (id != toUpdate.Id)
            {
                return BadRequest("Mismatched product ID.");
            }

            try
            {
                var result = await _bll.UpdateAsync(toUpdate);
                if (!result)
                {
                    return NotFound("Product not found or update failed.");
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
