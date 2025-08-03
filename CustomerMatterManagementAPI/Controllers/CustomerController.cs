using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CustomerMatterManagementAPI.Data.Repositories;
using CustomerMatterManagementAPI.Data.DTOs;

namespace CustomerMatterManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET /api/customers → Retrieve a list of customers
        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            var customers = await _unitOfWork.Customers.GetAllAsync();
            return Ok(customers);
        }

        // POST /api/customers → Create a new customer (name, phone)
        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerDTO customerDto)
        {
            // Map DTO to entity so we can get the CustomerId after creation
            var customer = new Customer
            {
                Name = customerDto.Name,
                PhoneNum = customerDto.PhoneNum
            };

            await _unitOfWork.Customers.AddAsync(customer);
            await _unitOfWork.SaveChangesAsync();

            // Retrieve the CustomerId after saving to ensure it's populated
            var savedCustomer = await _unitOfWork.Customers.GetByIdAsync(customer.CustomerId);

            return CreatedAtAction(nameof(GetCustomerById), new { customer_id = savedCustomer?.CustomerId }, savedCustomer);
        }

        // GET /api/customers/{customer_id} → Retrieve details of a customer
        [HttpGet("{customer_id}")]
        public async Task<IActionResult> GetCustomerById(int customer_id)
        {
            var customer = await _unitOfWork.Customers.GetByIdAsync(customer_id);
            if (customer == null) return NotFound();
            return Ok((CustomerDTO)customer);
        }

        // PUT /api/customers/{customer_id} → Update a customer
        [HttpPut("{customer_id}")]
        public async Task<IActionResult> UpdateCustomer(int customer_id, [FromBody] CustomerDTO customerDto)
        {
            var customer = await _unitOfWork.Customers.GetByIdAsync(customer_id);
            if (customer == null) return NotFound();

            customer.Name = customerDto.Name;
            customer.PhoneNum = customerDto.PhoneNum;

            _unitOfWork.Customers.Update(customer);
            await _unitOfWork.SaveChangesAsync();
            return Ok(customer);
        }

        // DELETE /api/customers/{customer_id} → Delete a customer
        [HttpDelete("{customer_id}")]
        public async Task<IActionResult> DeleteCustomer(int customer_id)
        {
            var customer = await _unitOfWork.Customers.GetByIdAsync(customer_id);
            if (customer == null) return NotFound();
            
            _unitOfWork.Customers.Delete(customer);
            await _unitOfWork.SaveChangesAsync();
            return Ok();
        }

        // GET /api/customers/{customer_id}/matters → Retrieve matters for a customer
        [HttpGet("{customer_id}/matters")]
        public async Task<IActionResult> GetMattersForCustomer(int customer_id)
        {
            var matters = await _unitOfWork.Matters.GetByCustomerIdAsync(customer_id);
            return Ok(matters);
        }

        // POST /api/customers/{customer_id}/matters → Create a matter
        [HttpPost("{customer_id}/matters")]
        public async Task<IActionResult> CreateMatterForCustomer(int customer_id, [FromBody] MatterDTO matterDto)
        {
            var matter = new Matter
            {
                CustomerId = customer_id,
                Title = matterDto.Title,
                Description = matterDto.Description
            };

            await _unitOfWork.Matters.AddAsync(matter);
            await _unitOfWork.SaveChangesAsync();

            var savedMatter = await _unitOfWork.Matters.GetByIdAsync(customer_id, matter.MatterId);
            
            return CreatedAtAction(nameof(GetMatterDetails), new { customer_id = savedMatter?.CustomerId, matter_id = savedMatter?.MatterId }, savedMatter);
        }

        // GET /api/customers/{customer_id}/matters/{matter_id} → Retrieve matter details
        [HttpGet("{customer_id}/matters/{matter_id}")]
        public async Task<IActionResult> GetMatterDetails(int customer_id, int matter_id)
        {
            var matter = await _unitOfWork.Matters.GetByIdAsync(customer_id, matter_id);
            if (matter == null) return NotFound();
            return Ok(matter);
        }
    }
}