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
            catch (Exception ex)
            {
                // Registrar excepción aquí
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocurrió un error inesperado.");
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
                    return NotFound("Producto no encontrado o la eliminación falló.");
                }
                return NoContent();
            }
            catch (ProductsExceptions ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                // Registrar excepción aquí
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocurrió un error inesperado.");
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
            catch (Exception ex)
            {
                // Registrar excepción aquí
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocurrió un error inesperado.");
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
                    return NotFound("Producto no encontrado.");
                }
                return Ok(product);
            }
            catch (ProductsExceptions ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                // Registrar excepción aquí
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocurrió un error inesperado.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] Product toUpdate)
        {
            if (toUpdate == null)
            {
                return BadRequest("Los datos del producto son nulos.");
            }

            if (id != toUpdate.Id)
            {
                return BadRequest("ID de producto no coincide.");
            }

            try
            {
                var result = await _bll.UpdateAsync(toUpdate);
                if (!result)
                {
                    return NotFound("Producto no encontrado o la actualización falló.");
                }
                return NoContent();
            }
            catch (ProductsExceptions ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                // Registrar excepción aquí
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocurrió un error inesperado.");
            }
        }
    }
}
}
