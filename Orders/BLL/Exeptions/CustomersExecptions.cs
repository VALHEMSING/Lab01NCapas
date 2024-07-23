using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Exeptions
{
    public class CustomersExecptions : Exception
    {
        /// Agregar mas metodos estaticos
        
        //Constructor
        private CustomersExecptions(string message) : base(message)
        {
            //Opcional: agregar constructo logico para logueo o manejo de errores del cliente
        }
        /*-------------------------------------------------------------------------------------------------------------------------------*/
        public static void ThrowCustomerAlreadyExistException(string firstName, string lastName)
        {
            throw new CustomersExecptions($"A client with the name already exist {firstName} {lastName}");
        }
        /*-------------------------------------------------------------------------------------------------------------------------------*/
        public static void ThrowInvalidCusrtomerDataException(int id)
        {
            throw new CustomersExecptions($"El ID del cliente '{id}' no es válido.");
        }
        /*-------------------------------------------------------------------------------------------------------------------------------*/
        public static void ThrowInvalididCustomerIdException(int id)
        {
            throw new CustomersExecptions($"El ID del cliente '{id}' no es válido.\"");
        }
    }
}
