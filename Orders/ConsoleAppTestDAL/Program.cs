using DAL;
using Entities.Models;
using System.Data.Common;
using System.Linq.Expressions;

//CreateAsync().GetAwaiter().GetResult();
//RetreiveAsync().GetAwaiter().GetResult();   
UpdateAsync().GetAwaiter().GetResult();

Console.ReadLine();

static async Task CreateAsync()
{
    //Add Customer 
    Customer customer = new Customer() 
    {
        FirstName = "Julian",
        LastName = "Quintero",
        City = "Bogotá",
        Country = "Clombia",
        Phone = "2123232315"
    };

    using (var repository = RepositoryFactory.CreateRepository())
    {
        try
        {
            var createdCustomer = await repository.CreateAsync(customer);
            //Console.WriteLine(createdCustomer.FirstName);  
            Console.WriteLine($"Added Customer: {createdCustomer.LastName}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

}

static async Task RetreiveAsync()
{
    using (var repository = RepositoryFactory.CreateRepository())
    {
        try
        {
            Expression<Func<Customer, bool>> Criteria = c => c.FirstName == "Julian" && c.LastName == "Quintero";
            var customer = await repository.RetrieveAsync(Criteria);
            if(customer != null)
            {
                Console.WriteLine($"Retrived customer: {customer.FirstName}\t{customer.LastName}\tCity: {customer.City}\tCountry: {customer.Country}");
            }
            Console.WriteLine($"Customer not exist");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
    
static async Task UpdateAsync()
{
    //Supuesto: Existe el objeto a modificar
    //Pre-Requisito --> Que exista el objeto

    //var customerToUpdate = await
    using (var repository = RepositoryFactory.CreateRepository())
    {

        //Averiguar si el objeto o registro ¡Existe!!!!
        var customerToUpdate = await repository.RetrieveAsync<Customer>(c => c.Id == 78);
        if(customerToUpdate != null)//Si es distinto de NULL, es porque existe
        {
            customerToUpdate.FirstName = "Liu";
            customerToUpdate.LastName = "Wong";
            customerToUpdate.City = "Toronto";
            customerToUpdate.Country = "Canada";
            customerToUpdate.Phone = "+14337 6353039";
        }

        try
        {
            bool update = await repository.UpdateAsync(customerToUpdate);
            if (update)
            {
                Console.WriteLine($"Customer update succesfully...");
            }
            else
            {
                Console.WriteLine($"Customer update failed");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

    }

}
