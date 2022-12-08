using ShipmentAPI.EfCore;
using ShipmentAPI.Model;
using ShipmentAPI.Interfaces;
namespace ShipmentAPI.Repositories
{
    public class CarrierServiceRepository : GenericRepository<CarrierService>, ICarrierServiceRepository
    {
        public CarrierServiceRepository(EF_DataContext context) : base(context) { }
    }
}