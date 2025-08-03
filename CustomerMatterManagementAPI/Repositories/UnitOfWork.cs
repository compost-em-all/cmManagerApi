using System.Threading.Tasks;
using CustomerMatterManagementAPI;
using CustomerMatterManagementAPI.Data.Repositories;

namespace CustomerMatterManagementAPI.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CustomerMatterManagementAPIContext _context;
        public ICustomerRepository Customers { get; }
        public IMatterRepository Matters { get; }

        public UnitOfWork(CustomerMatterManagementAPIContext context)
        {
            _context = context;
            Customers = new CustomerRepository(_context);
            Matters = new MatterRepository(_context);
        }

        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();

        public void Dispose() => _context.Dispose();
    }
}
