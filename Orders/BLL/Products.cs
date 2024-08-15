using BLL.Exeptions;
using DAL;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Products
    {
        public async Task<Product> CreateAsync (Product product)
        {
            Product productResult = null;
            using (var repo = RepositoryFactory.CreateRepository())
            {
                Product productSearch = await repo.RetrieveAsync<Product>(p => p.ProductName == product.ProductName);
                if(productSearch == null)
                {
                    productResult = await repo.CreateAsync(product);
                }
                else
                {
                    ProductsExceptions.ThrowProductAlreadyExistException(product.ProductName);
                }
            }
            if(productResult == null)
            {
                throw new InvalidOperationException("El producto no puede ser creado");
            }
            return productResult;
        }

        public async Task<List<Product>> RetrieveAllAsync()
        {
            List<Product> result = null;
            using (var r = RepositoryFactory.CreateRepository())
            {
                Expression<Func<Product, bool>> allProductsCriteria = x => true;
                result = await r.FilterAsync<Product>(allProductsCriteria);
            }
            return result;
        }

        public async Task<Product> RetrieveByIDAsync(int id)
        {
            using (var repo = RepositoryFactory.CreateRepository())
            {
                Product product = await repo.RetrieveAsync<Product>(p => p.Id == id);
                // Check if the customer was found
                if (product == null)
                {
                    // Throw an exception if not found
                    CustomersExecptions.ThrowInvalidCustomerIdException(id);
                }

                return product;
            }
        }

        public async Task<bool> UpdateAsync(Product product)
        {
            bool result = false;
            using (var repository = RepositoryFactory.CreateRepository())
            {
                
                Product productSearch = await repository.RetrieveAsync<Product>(p => p.ProductName == product.ProductName && p.Id != product.Id);
                if (productSearch == null)
                {
                    result = await repository.UpdateAsync(product);
                }
                else
                {
                  
                    ProductsExceptions.ThrowProductAlreadyExistException(productSearch.ProductName);
                }
            }
            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            bool result = false;
            Product product = await RetrieveByIDAsync(id);
            if (product != null)
            {
               
                using (var repository = RepositoryFactory.CreateRepository())
                {
                    result = await repository.DeleteAsync(product);
                }
            }
            else
            {
                CustomersExecptions.ThrowInvalidCustomerIdException(id);
            }
            return result;
        }


    }
}
