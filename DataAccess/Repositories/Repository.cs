using Dapper;
using DataAccess.Contexts;
using DataAccess.Entities;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Diagnostics;
using System.Linq.Expressions;

namespace DataAccess.Repositories;

public abstract class Repository<TEntity> where TEntity : class
{

    private readonly DataContext _context;

    protected Repository(DataContext context)
    {
        _context = context;
    }

    public virtual TEntity Create(TEntity entity)
    {
        try
        {
            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
            return entity;

        }
        catch (Exception ex)
        {
            Debug.WriteLine("error msg:" + ex.Message);
        }
        return null!;
    }

    public virtual IEnumerable<TEntity> ReadAllEntities()
    {
        try
        {
            return _context.Set<TEntity>().ToList();

        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        return null!;
    }
    public virtual IEnumerable<TEntity> ReadAllEntities(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            var result = _context.Set<TEntity>().Where(predicate);
            if (result != null)
            {
                return result;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        return null!;
    }
    public virtual TEntity ReadOneEntity(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            var result = _context.Set<TEntity>().FirstOrDefault(predicate);

            if (result != null)
            {
                return result;
            }
            if (result == null)
            {
                return null!;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        return null!;
    }


    public virtual TEntity Update(TEntity entity)
    {
        try
        {
            var entityToUpdate = _context.Set<TEntity>().FirstOrDefault(entity);

            if (entityToUpdate != null)
            {
                _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
                _context.SaveChanges();
                return entityToUpdate;
            }

        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        return null!;
    }
    public virtual TEntity Update(Expression<Func<TEntity, bool>> predicate, TEntity entity)
    {
        try
        {
            var entityToUpdate = _context.Set<TEntity>().FirstOrDefault(predicate);

            if (entityToUpdate != null)
            {
                _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
                _context.SaveChanges();
                return entityToUpdate;
            }

        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        return null!;
    }
    public virtual bool Delete(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            var entityToDelete = _context.Set<TEntity>().FirstOrDefault(predicate);

            if (entityToDelete != null)
            {
                _context.Set<TEntity>().Remove(entityToDelete);
                _context.SaveChanges();

                return true;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        return false!;
    }
    public virtual bool Exists(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            var result = _context.Set<TEntity>().Any(predicate);
            return result;

        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        return false;

    }
}