using Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace Services.Controllers
{
    public interface ICustomerController
    {
        Task<ActionResult<Customer>> CreateAsync([FromBody] Customer toCreate);
        Task<IActionResult> DeleteAsync(int id);
        Task<ActionResult<List<Customer>>> GetResultAsync();
        Task<ActionResult<Customer>> RetrieveAsync(int id);
        Task<IActionResult> UpdateAsync(int id, [FromBody] Customer toUpdate);
    }
}