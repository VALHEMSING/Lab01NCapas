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
        Task<ActionResult> DeleteAsync(int id);
    }
}
