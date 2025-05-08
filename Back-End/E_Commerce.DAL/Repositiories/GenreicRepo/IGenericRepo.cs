  

namespace E_Commerce.DAL
{
    public interface IGenericRepo<T> where T : class
    {
        List<T> GetAll();
        T? GetById(Guid id);
        void Add (T item);
        void Update (T item);
        void Delete (T item);
    }
}
