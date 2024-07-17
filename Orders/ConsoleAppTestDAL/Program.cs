using DAL;
using Entities.Models;
using System.Linq.Expressions;

CreateAsync().GetAwaiter().GetResult();

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
    

