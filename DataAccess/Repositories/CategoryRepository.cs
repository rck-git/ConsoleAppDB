
using DataAccess.Contexts;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace DataAccess.Repositories;

//inheriting the two methods from the abstract class "repository"

public class CategoryRepository : Repository<CategoryEntity>
{
    private readonly DataContext _context;


    public CategoryRepository(DataContext context) : base(context)
    {

    }

}
