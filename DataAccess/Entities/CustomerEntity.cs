using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace DataAccess.Entities;

//entity should reflect/represent the table in the database
//while the model should reflect the object in the program
public class CustomerEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "NVARCHAR(50)")]
    public string FirstName { get; set; } = null!;

    [Required]
    [Column(TypeName = "NVARCHAR(50)")]
    public string Lastname { get; set; } = null!;

    [Required]
    
    [Column(TypeName = "NVARCHAR(100)")]
    public string Email { get; set; } = null!;

    [AllowNull]
    [Column(TypeName = "VARCHAR(10)")]
    public string? PhoneNumber { get; set; }

    [Required]
    [ForeignKey(nameof(AdressEntity))]
    public int AdressId { get; set; }

    
    public AdressEntity Adress { get; set; } = null!;


    [Required]
    [ForeignKey(nameof(RoleEntity))]
    public int RoleId { get; set; }


    public RoleEntity Role { get; set; } = null!;
}
