using System;
using Entities.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

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
