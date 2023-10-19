using Asp.netCoreDemo.DataContext;
using Asp.netCoreDemo.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Asp.netCoreDemo.Controllers 
{
    [ApiController]
    [Route("api/[Controller]/[action]")]
    public class CustomerController : Controller
    {
        private readonly DapperContext dbContext;

        public CustomerController(DapperContext dbContext)
        {
            this.dbContext = dbContext;          // initializes the private dappercontext
        }


        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {

            return Ok(await dbContext.customer.ToListAsync());


        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetCustomer([FromRoute] int id)
        {
            var customer = await dbContext.customer.FindAsync(id);
            if(customer == null) 
            {
                return NotFound();
            }

            return Ok(customer);


        }


        [HttpPost]
		public async Task<IActionResult> AddCustomer(AddCustomerRequest addCustomerRequest)
        {
            var customer = new Customer()
            {
                CustomerId = new Random().Next(),                      // generate a new student id as appropriate
				CustomerName = addCustomerRequest.CustomerName,
				Phone = addCustomerRequest.Phone,
				Address = addCustomerRequest.Address,
                

            };
           await dbContext.customer.AddAsync(customer);
           await dbContext.SaveChangesAsync();

			return Ok(customer);
        }


        [HttpPut]
        [Route("{id:int}")]

        public async Task<IActionResult> UpdateCustomer([FromRoute] int id,  UpdateCustomerRequest updateCustomerRequest)
        {
            var customer =  await dbContext.customer.FindAsync(id);
            if (customer != null)
            {
				customer.CustomerName = updateCustomerRequest.CustomerName;
				customer.Phone = updateCustomerRequest.Phone;
				customer.Address = updateCustomerRequest.Address;
                

                await dbContext.SaveChangesAsync();
                return Ok(customer);
            }
            return NotFound();
        }


        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteCustomer([FromRoute] int id)
        {
            var customer = await dbContext.customer.FindAsync(id);
            if (customer != null)
            {
				dbContext.Remove(customer);
				await dbContext.SaveChangesAsync();
                return Ok(customer);
            }
            return NotFound();
        }


    }
}
