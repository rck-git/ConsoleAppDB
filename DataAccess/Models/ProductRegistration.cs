

namespace DataAccess.Models
{
    public class ProductRegistration
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
    }

    //public CategoryEntity Category { get; set; } = null!;

}
