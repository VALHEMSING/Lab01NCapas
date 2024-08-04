using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Exeptions
{
    public class SuppliersExceptions :Exception
    {
        private SuppliersExceptions(string message) : base(message)
        {

        }
        public static void ThrowSupplierAlreadyExistException(string companyName)
        {
            throw new SuppliersExceptions($"A supplier with the name already exist {companyName}");
        }
        public static void ThrowInvalidSupplaierDataException(string message)
        {
            throw new SuppliersExceptions(message);
        }
        public static void ThrowInvalidSupplierIdException(int id)
        {
            throw new SuppliersExceptions($"El ID del cliente '{id}' no es válido.");
        }
    }
}
