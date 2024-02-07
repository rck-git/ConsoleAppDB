using DataAccess.Contexts;
using DataAccess.Entities;
using System.Diagnostics;
using System;
using System.Linq.Expressions;
using static Dapper.SqlMapper;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

//inheriting methods from the generic class "repository"

public class ProductRepository : Repository<ProductEntity>
{

    private readonly DataContext _context;

    public ProductRepository(DataContext context) : base(context)
    {
    }
}
