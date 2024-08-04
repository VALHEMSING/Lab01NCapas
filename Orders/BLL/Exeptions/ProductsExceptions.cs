using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Exeptions
{
    public class ProductsExceptions : Exception
    {
        private ProductsExceptions(string message) : base(message)
        {

        }

        public static void ThrowProductAlreadyExistException(string productName)
        {
            throw new ProductsExceptions($"A supplier with the name already exist {productName}");
        }
        public static void ThrowInvalidProductDataException(string message)
        {
            throw new ProductsExceptions(message);
        }
        public static void ThrowInvalidProductIdException(int id)
        {
            throw new ProductsExceptions($"El ID del cliente '{id}' no es válido.");
        }
    }
}
