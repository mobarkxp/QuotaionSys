
using Microsoft.EntityFrameworkCore;
using Quotaion.Server.Data;
namespace Quotaion.Server.Repository
{
    public class MainRepository<T> : IMainRepository<T> where T : class
    {
        private readonly QuotaionDbContext _quotaionDbContext;

        public MainRepository(QuotaionDbContext quotaionDbContext)
        {
            _quotaionDbContext = quotaionDbContext;
        }

        public bool Add(T item)
        {
            try
            {
                _quotaionDbContext.Add(item);
                _quotaionDbContext.SaveChanges();
                return true;
            }catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var modal=_quotaionDbContext.Set<T>().Find(id);
                if (modal != null)
                {
                    _quotaionDbContext.Remove(modal);
                    _quotaionDbContext.SaveChanges() ;
                    return true;
                }else
                {
                    return false;
                }
            }catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteAll()
        {
            throw new NotImplementedException();
        }

        public List<T> GetAll()
        {
            try
            {
                return _quotaionDbContext.Set<T>().ToList();
            }catch (Exception ex)
            {
                return null;
            }
        }

        public T GetById(int id)
        {
          return _quotaionDbContext.Set<T>().Find(id);
         
        }
        

        public bool Update(int id, T item)
        {
            DbSet<T> dbset=_quotaionDbContext.Set<T>();
            dbset.Attach(item);
            _quotaionDbContext.Entry(item).State = EntityState.Modified;
            _quotaionDbContext.SaveChanges();
            return true;
            
        }
    }
}
