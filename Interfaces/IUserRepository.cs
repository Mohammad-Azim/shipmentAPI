using ShipmentAPI.Model;
namespace ShipmentAPI.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> GetUser(UserLoginDTO userInput);
    }

}