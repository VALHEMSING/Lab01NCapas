using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SLC
{
    public interface ISuppliersServices
    {
        Task<ActionResult<Supplier>> CreateAsync([FromBody] Supplier toCreate);
        Task<IActionResult> DeleteAsync(int id);
        Task<ActionResult<List<Supplier>>> GetAllAsync();
        Task<ActionResult<Supplier>> RetrieveAsync(int id);
        Task<IActionResult> UpdateAsync(int id, [FromBody] Supplier toUpdate);
    }
}