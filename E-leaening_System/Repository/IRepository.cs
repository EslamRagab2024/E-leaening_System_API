namespace E_leaening_System.Repository
{
    public interface IRepository<T> where T :class,IDeletable
    {
        public void insert(T obj);
        public void update(T obj);
        public void delete(T obj);
        public List<T> GetAll();
        public T Get(Func<T, bool> predicate);
        public int save();
    }
}
