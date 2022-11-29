using ShipmentAPI.Model;
using ShipmentAPI.EfCore;
using ShipmentAPI.Interfaces;

namespace ShipmentAPI.Repositories
{
    public class CarrierRepository : GenericRepository<Carrier>, ICarrierRepository
    {
        public CarrierRepository(EF_DataContext context) : base(context) { }
    }



}