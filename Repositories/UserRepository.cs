using ShipmentAPI.EfCore;
using ShipmentAPI.Model;
using ShipmentAPI.Interfaces;

namespace ShipmentAPI.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(EF_DataContext context) : base(context) { }

    }
}