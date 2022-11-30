using ShipmentAPI.EfCore;
using ShipmentAPI.Model;
using ShipmentAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ShipmentAPI.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(EF_DataContext context) : base(context) { }

        public async Task<User?> GetUser(UserLoginDTO userInput)
        {
            var user = await this.context.Users!.FirstOrDefaultAsync(u => u.Email == userInput.Email && u.Password == userInput.Password);
            return user;
        }
    }
}