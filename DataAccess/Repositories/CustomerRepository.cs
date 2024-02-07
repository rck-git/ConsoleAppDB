using DataAccess.Entities;
using Microsoft.Data.SqlClient;
using Dapper;
using DataAccess.Contexts;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace DataAccess.Repositories;

public class CustomerRepository : Repository<CustomerEntity>
{
    private readonly DataContext _context;


    public CustomerRepository(DataContext context) : base(context)
    {

    }
    

}
