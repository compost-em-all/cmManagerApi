using CustomerMatterManagementAPI;

public class MatterDTO 
{
    public int MatterId { get; set; }
    public int CustomerId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }

    public static implicit operator Matter(MatterDTO dto)
    {
        return new Matter
        {
            MatterId = dto.MatterId,
            CustomerId = dto.CustomerId,
            Title = dto.Title,
            Description = dto.Description
        };
    }

    public static explicit operator MatterDTO(Matter v)
    {
        return new MatterDTO
        {
            MatterId = v.MatterId,
            CustomerId = v.CustomerId,
            Title = v.Title,
            Description = v.Description
        };
    }
}