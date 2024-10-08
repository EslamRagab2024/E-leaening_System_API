using E_leaening_System.Models;

namespace E_leaening_System.Repository
{
    public class Repository<T> : IRepository<T> where T : class,IDeletable
    {
        public readonly MyData context;
        public Repository(MyData context)
        {
          this.context = context;
        }
        public void insert(T obj)
        {
            context.Set<T>().Add(obj);
        }
        public void delete(T obj)
        {
            obj.IsDeleted = true;
            update(obj);
        }
        public void update(T obj) 
        {
            context.Set<T>().Update(obj);
        }
        public List<T> GetAll()
        {
            return context.Set<T>().Where(x => !x.IsDeleted).ToList();   
        }
        public T Get(Func<T,bool>predicate)
        {
            var entites=context.Set<T>().Where(x=>!x.IsDeleted).ToList();
            return entites.FirstOrDefault(predicate);
        }
        public int save()
        {
            return context.SaveChanges();
        }
    }
}
