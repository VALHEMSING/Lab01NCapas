using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SLC
{
    public interface ICustomerService
    {
        Task<ActionResult<Customer>> CreateAsync([FromBody] Customer toCreate);
        Task<IActionResult> DeleteAsync(int id);
        Task<ActionResult<List<Customer>>> GetAllAsync();
        Task<ActionResult<Customer>> RetrieveAsync(int id);
        Task<IActionResult> UpdateAsync(int id, [FromBody] Customer toUpdate);
    }
}

