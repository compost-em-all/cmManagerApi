public class UserSignUpDTO
{
    public string Email { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Password { get; set; } = null!; // plain text, only for transport
    public string FirmName { get; set; } = null!;
}