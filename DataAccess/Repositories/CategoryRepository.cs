
using DataAccess.Contexts;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace DataAccess.Repositories;

//inheriting the two methods from the abstract class "repository"
//
public class CategoryRepository : Repository<CategoryEntity>
{
    private readonly DataContext _context;
    

    public CategoryRepository(DataContext context) : base(context)
    {

    }
 

    //public override IEnumerable<ProductEntity> ReadAllEntities()
    //{
    //    try
    //    {
    //        var result = _context.Products.Include(x => x.Category).ToList();

    //        if (result != null)
    //        {
    //            return result;
    //        }
    //        if (result == null)
    //        {
    //            return null!;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Debug.WriteLine(ex.Message);
    //    }
    //    return null!;

    //}

    //public override ProductEntity ReadOneEntity(Expression<Func<ProductEntity, bool>> predicate)
    //{
    //    try
    //    {
    //        var result = _context.Products.Include(x => x.Category).FirstOrDefault(predicate);

    //        if (result != null)
    //        {
    //            return result;
    //        }
    //        if (result == null)
    //        {
    //            return null!;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Debug.WriteLine(ex.Message);
    //    }
    //    return null!;
    //}

}
