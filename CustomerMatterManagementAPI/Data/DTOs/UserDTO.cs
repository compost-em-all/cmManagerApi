namespace CustomerMatterManagementAPI.Data.DTOs;

public class UserSignUpDTO
{
    public string Email { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Password { get; set; } = null!; // plain text, only for transport
    public string FirmName { get; set; } = null!;
}

public class UserDetailDTO
{
    public string EmailAddr { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string FirmName { get; set; } = null!;

    public static explicit operator UserDetailDTO(User v)
    {
        return new UserDetailDTO
        {
            EmailAddr = v.EmailAddr,
            FirstName = v.FirstName,
            LastName = v.LastName,
            FirmName = v.FirmName
        };
    }
}

public class UserLoginDto
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!; // plain text, only for transport
}