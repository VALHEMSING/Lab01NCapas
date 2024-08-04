using BLL.Exeptions;
using DAL;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Suppliers
    {
        public async Task<Supplier> CreateAsync(Supplier supplier)
        {
            Supplier supplierResult = null;
            using (var repo = RepositoryFactory.CreateRepository())
            {
                Supplier supplierSearch = await repo.RetrieveAsync<Supplier>(s => s.CompanyName == supplier.CompanyName);
                if(supplierSearch == null)
                {
                    supplierResult = await repo.CreateAsync(supplier);
                }
                else
                {
                    SuppliersExceptions.ThrowSupplierAlreadyExistException(supplierSearch.CompanyName);
                }
            }
            if(supplierResult == null)
            {
                throw new InvalidOperationException("El cliente no pudo ser creado.");
            }
            return supplierResult;
        }



        public async Task<List<Supplier>> RetrieveAllAsync()
        {
            List<Supplier> suppliersResult = null;
            using (var repo = RepositoryFactory.CreateRepository())
            {
                Expression<Func<Supplier, bool>> allSuppliersCriteria = x => true;
                suppliersResult = await repo.FilterAsync<Supplier>(allSuppliersCriteria);

            }
            return suppliersResult;
        }


        public async Task<Supplier> RetrieveByIdAsync(int id)
        {
            using (var repo = RepositoryFactory.CreateRepository())
            {
                Supplier supplier = await repo.RetrieveAsync<Supplier>(s => s.Id == id);
                if(supplier == null)
                {
                    SuppliersExceptions.ThrowInvalidSupplierIdException(id);
                }
                return supplier;
            }
        }

        public async Task<bool> UpdateAsync(Supplier supplier)
        {
            bool result = false;
            using (var repo = RepositoryFactory.CreateRepository())
            {
                Supplier supplierSearch = await repo.RetrieveAsync<Supplier>(s => s.CompanyName == supplier.CompanyName && s.Id != supplier.Id);
                if(supplierSearch == null)
                {
                    result = await repo.UpdateAsync(supplier);
                }
                else
                {
                    SuppliersExceptions.ThrowSupplierAlreadyExistException(supplierSearch.CompanyName);
                }
            }
            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            bool result = false;
            Supplier supplier = await RetrieveByIdAsync(id);
            if(supplier != null)
            {
                using (var repo = RepositoryFactory.CreateRepository())
                {
                    result = await repo.DeleteAsync(supplier);
                }
            }
            else
            {
                SuppliersExceptions.ThrowInvalidSupplierIdException(id);
            }
            return result;
        }





    }
}
