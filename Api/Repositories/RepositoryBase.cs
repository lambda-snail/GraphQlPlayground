using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using Api.Models;

namespace Api.Repositories;
public class RepositoryBase<T> : IOrderRepositoryBase<T> where T : class
{
    protected NorthwindContext _context;
    protected DbSet<T> _dbSet;

    public RepositoryBase(NorthwindContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public virtual Task<List<T>> GetAll()
    {
        return _dbSet.ToListAsync();
    }

    public async virtual Task<T> Add(T newEntity)
    {
        await _dbSet.AddAsync(newEntity);
        _context.SaveChanges();
        return newEntity;
    }
}

public interface IOrderRepositoryBase<T> where T : class
{
    Task<List<T>> GetAll();
}