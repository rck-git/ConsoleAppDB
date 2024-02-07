
namespace DataAccess.Models;

public class CustomerRegistration

{
    public string FirstName = null!;
    public string Lastname { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? PhoneNumber { get; set; } = null!;

    public int adressId { get; set; }
    public string StreetName { get; set; } = null!;
    public string City { get; set; } = null!;
    public string PostalCode { get; set; } = null!;

    public int RoleId { get; set; }
    public string RoleName { get; set; } = null!;

}
