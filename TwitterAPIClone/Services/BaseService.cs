using Microsoft.EntityFrameworkCore;
using System.Linq;
using TwitterAPIClone.Data;
using TwitterAPIClone.Models;

namespace TwitterAPIClone.Services;
public class BaseService<T> : IBaseService<T> where T : BaseModel
{
    protected AppDbContext _dbContext;
    public BaseService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public int Add(T entity)
    {
        _dbContext.Set<T>().AddAsync(entity);
        _dbContext.SaveChanges();
        return entity.Id;
    }

    public void Delete(int id)
    {
        var ent = EntityExist(id);
        ent.IsDeleted = true;
        _dbContext.SaveChanges();
    }

    public T EntityExist(int id)
    {
        var ent = _dbContext.Set<T>().Find(id);
        if (ent is null)
        {
            ThrowDoesNotExistException();
        }
        return ent;
    }

    public T Get(int id)
    {
        return EntityExist(id);
    }

    public IEnumerable<T> GetAll()
    {
        return _dbContext.Set<T>().Where(z => !z.IsDeleted).ToList();
    }

    public void Update(T entity)
    {
        EntityExist(entity.Id);
        _dbContext.Set<T>().Update(entity);
        _dbContext.SaveChanges();
    }
    protected void ThrowDoesNotExistException()
    {
        string s = typeof(T).Name;
        var exception = new Exception($"{s} does not exist.");
        exception.Data["StatusCode"] = 404;
        throw exception;
    }
}

