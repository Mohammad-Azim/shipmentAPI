using ShipmentAPI.Interfaces;

namespace ShipmentAPI.EfCore
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly EF_DataContext context;

        public GenericRepository(EF_DataContext context)
        {
            this.context = context;
        }
        public IQueryable<T> GetAll()
        {
            return context.Set<T>();
        }
        public void Add(T entity)
        {
            context.Set<T>().Add(entity);
        }

    }
}