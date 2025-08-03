using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CustomerMatterManagementAPI;

namespace CustomerMatterManagementAPI.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerMatterManagementAPIContext _context;
        public CustomerRepository(CustomerMatterManagementAPIContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Customer>> GetAllAsync() => await _context.Customers.ToListAsync();
        public async Task<Customer?> GetByIdAsync(int customerId) => await _context.Customers.FindAsync(customerId);
        public async Task AddAsync(Customer customer) => await _context.Customers.AddAsync(customer);
        public void Update(Customer customer) => _context.Customers.Update(customer);
        public void Delete(Customer customer) => _context.Customers.Remove(customer);
    }

    public class MatterRepository : IMatterRepository
    {
        private readonly CustomerMatterManagementAPIContext _context;
        public MatterRepository(CustomerMatterManagementAPIContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Matter>> GetByCustomerIdAsync(int customerId) => await _context.Matters.Where(m => m.CustomerId == customerId).ToListAsync();
        public async Task<Matter?> GetByIdAsync(int customerId, int matterId) => await _context.Matters.FirstOrDefaultAsync(m => m.CustomerId == customerId && m.MatterId == matterId);
        public async Task AddAsync(Matter matter) => await _context.Matters.AddAsync(matter);
        public void Update(Matter matter) => _context.Matters.Update(matter);
        public void Delete(Matter matter) => _context.Matters.Remove(matter);
    }
}
