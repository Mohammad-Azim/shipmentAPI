namespace ShipmentAPI.Interfaces
{

    public interface IGenericRepository<T> where T : class
    {

        IQueryable<T> GetAll();
        void Add(T entity);

    }


}

