using Microsoft.AspNetCore.Mvc;
using ProxyServer;

namespace WebApplicationOrder.Controllers
{
    public class CustomersController : Controller 
    {

        //campo
        private readonly CustomerProxy _proxy;

        //Constructor
        public CustomersController()
        {
            this._proxy = new CustomerProxy();
        }
        public async Task<IActionResult>Index()
        {
            var customers = await _proxy.GetAllAsync();
            return View(customers);
        }
    }
}
