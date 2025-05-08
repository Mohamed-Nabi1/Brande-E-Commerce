

namespace E_Commerce.DAL
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        private readonly E_CommerceContext _context;

        public GenericRepo(E_CommerceContext context)
        {
            _context = context;
        }
        public List<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public T? GetById(Guid id)
        {
            return _context.Set<T>().Find(id);
        }
        public void Add(T item)
        {
            _context.Set<T>().Add(item);
        }


        public void Update(T item)
        {
            //Empty
        }

        public void Delete(T item)
        {
            _context.Set<T>().Remove(item);
        }
    }

}
