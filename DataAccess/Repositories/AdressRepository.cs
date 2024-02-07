using DataAccess.Entities;
using Dapper;
using Microsoft.Data.SqlClient;
using DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;
using static Dapper.SqlMapper;
using System.Linq;
using System;
using System.Xml.Linq;

namespace DataAccess.Repositories;

// Ska utföra crud bara mot adressen (en entitet) /  single responsibility 
// servicen kommer göra flera saker typ ta adress och customer o göra crud (använda flera entiteter tillsammans)
//tar hand om hela processen med att skicka och ta emot entiteter
public class AdressRepository : Repository<AdressEntity>
{
    private readonly DataContext _context;
    

    public AdressRepository(DataContext context) : base(context)
    {

    }


    public override bool Exists(Expression<Func<AdressEntity, bool>> predicate)
    {
        try
        {
            var result = _context.Set<AdressEntity>().Any(predicate);
            return result;

        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        return false;

    }
   
}