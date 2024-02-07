using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities;

//entity should reflect/represent the table in the database
//while the model should reflect the object in the program
public class ProductEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "NVARCHAR (MAX)")]
    public string Title { get; set; } = null!;

    [Required]
    [Column(TypeName = "money")]
    public decimal Price { get; set; }

    [Required]
    [ForeignKey(nameof(CategoryEntity))]
    public int CategoryId { get; set; }

    public CategoryEntity Category { get; set; } = null!;
}
