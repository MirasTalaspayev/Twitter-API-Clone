using TwitterAPIClone.Models;

namespace TwitterAPIClone.Services;
public interface IBaseService<T> where T : BaseModel
{
    public T Get(int id);
    public int Add(T entity);
    public void Update(T entity);
    public void Delete(int id);
    public T EntityExist(int id);
    public IEnumerable<T> GetAll();
}

