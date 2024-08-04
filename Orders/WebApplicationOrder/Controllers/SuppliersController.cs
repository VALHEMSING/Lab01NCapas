using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using ProxyServer;

namespace WebApplicationOrder.Controllers
{
    public class SuppliersController : Controller
    {
        private readonly SuppliersProxy _proxy;

        public SuppliersController()
        {
            this._proxy = new SuppliersProxy();
        }


        public async Task<IActionResult> Index()
        {
            var suppliers = await _proxy.GetAllAsync();
            return View(suppliers);
        } 

        //GET: Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CompanyName,ContactName,ContactTitle,City,Country,Phone,Fax")] Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _proxy.CreateAsync(supplier);
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
            return View(supplier);
        }

        // GET: CustomersController/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var supplier = await _proxy.GetByIdAsync(id);
            if (supplier == null)
            {
                return NotFound();
            }
            return View(supplier);
        }


        // POST: CustomersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CompanyName,ContactName,ContactTitle,City,Country,Phone,Fax")] Supplier supplier)
        {
            if (id != supplier.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _proxy.UpdateAsync(id, supplier);
                    if (!result)
                    {
                        return RedirectToAction("Error", new { message = "No se puede realizar la edición porque hay duplicidad de nombre con otro cliente" });
                    }
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Error", new { message = ex.Message });
                }
            }
            return View(supplier);
        }
        //Details
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Detail(int id)
        {
            var supplier = await _proxy.GetByIdAsync(id);
            if (supplier == null)
            {
                return NotFound();
            }
            return View(supplier);
        }


        //Delete
        public async Task<IActionResult> Delete(int id)
        {
            var supplier = await _proxy.GetByIdAsync(id);
            if (supplier == null)
            {
                return NotFound();
            }
            return View(supplier);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            try
            {
                var result = await _proxy.DeleteAsync(id);
                if (!result)
                {
                    return RedirectToAction("Error", new { message = "No se puede eliminar el cliente porque tiene facturas asociadas." });
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                throw;
            }

        }



        //Error

        public IActionResult Error(string message)
        {
            ViewBag.ErrorMessage = message;
            return View();
        }








    }
}
