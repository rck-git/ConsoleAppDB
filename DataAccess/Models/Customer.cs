
namespace ConsoleAppD.Models;

//models are used to send and receive data
//while entities should reflect whats inside the database
internal class Customer
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;
    public string Lastname { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? PhoneNumber { get; set; } = null!;

    public string StreetName { get; set; } = null!;
    public string City { get; set; } = null!;
    public string PostalCode { get; set; } = null!;

}
