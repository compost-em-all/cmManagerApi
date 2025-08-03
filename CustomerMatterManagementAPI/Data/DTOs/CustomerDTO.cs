
namespace CustomerMatterManagementAPI.Data.DTOs;

public class CustomerDTO
{
    public int CustomerId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string PhoneNum { get; set; } = string.Empty;

    public static implicit operator Customer(CustomerDTO dto)
    {
        return new Customer
        {
            CustomerId = dto.CustomerId,
            Name = dto.Name,
            PhoneNum = dto.PhoneNum
        };
    }

    public static explicit operator CustomerDTO(Customer v)
    {
        return new CustomerDTO
        {
            CustomerId = v.CustomerId,
            Name = v.Name,
            PhoneNum = v.PhoneNum
        };
    }
}