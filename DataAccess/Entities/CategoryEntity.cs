using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace DataAccess.Entities;


public class CategoryEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "NVARCHAR(100)")]
    public string CategoryName { get; set; } = null!;
}
