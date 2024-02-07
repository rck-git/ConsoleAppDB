
using DataAccess.Entities;
using Dapper;
using Microsoft.Data.SqlClient;
using DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;



namespace DataAccess.Repositories;

public class RoleRepository : Repository<RoleEntity>
{   // Ska utföra crud bara mot Role (en entitet) /  single responsibility 
    private readonly DataContext _context;


    public RoleRepository(DataContext context) : base(context)
    {

    }
}
