using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities;

//entity should reflect/represent the table in the database
//while the model should reflect the object in the program
public class AdressEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string StreetName { get; set; } = null!;

    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string City { get; set; } = null!;

    [Required]
    [Column(TypeName = "varchar(6)")]
    public string PostalCode { get; set; }=null!;


    //create a one to many relationship
    //Customer can have 1 Adress while i.e
    // Adress can have multiple users
    //public virtual ICollection<AdressEntity> Adress { get; set; } = new HashSet<AdressEntity>();
}
