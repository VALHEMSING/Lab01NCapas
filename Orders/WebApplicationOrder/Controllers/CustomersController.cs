﻿using Entities.Models;
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

        public async Task<IActionResult> Index()
        {
            var customers = await _proxy.GetAllAsync();
            return View(customers);
        }

        /*
         * Create
        */
        //GET: Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,City,Country,Phone")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _proxy.CreateAsync(customer);
                    if (result == null)
                    {
                        return RedirectToAction("Error", new { message = "El cliente con el mismo nombre y aprellido ya existe." });
                    }
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Error", new { message = ex.Message });
                }
            }
            return View(customer);
        }



























    }
}
