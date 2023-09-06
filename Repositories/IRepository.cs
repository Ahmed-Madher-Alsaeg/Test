namespace g13lec6.Repositories
{
    public interface IRepository<T> where T : class
    {
        T Add(T entity);
        T Update(T entity);
        T Delete(int id);
        T GetById(int id);
        List<T> GetAll();
    }
}
