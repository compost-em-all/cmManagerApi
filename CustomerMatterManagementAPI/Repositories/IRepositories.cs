using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerMatterManagementAPI;

namespace CustomerMatterManagementAPI.Data.Repositories
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllAsync();
        Task<Customer?> GetByIdAsync(int customerId);
        Task AddAsync(Customer customer);
        void Update(Customer customer);
        void Delete(Customer customer);
    }

    public interface IMatterRepository
    {
        Task<IEnumerable<Matter>> GetByCustomerIdAsync(int customerId);
        Task<Matter?> GetByIdAsync(int customerId, int matterId);
        Task AddAsync(Matter matter);
        void Update(Matter matter);
        void Delete(Matter matter);
    }

    public interface IUnitOfWork : IDisposable
    {
        ICustomerRepository Customers { get; }
        IMatterRepository Matters { get; }
        Task<int> SaveChangesAsync();
    }
}
