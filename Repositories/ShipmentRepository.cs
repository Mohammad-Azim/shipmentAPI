using ShipmentAPI.EfCore;
using ShipmentAPI.Model;
using ShipmentAPI.Interfaces;

namespace ShipmentAPI.Repositories
{
    public class ShipmentRepository : GenericRepository<Shipment>, IShipmentRepository
    {
        public ShipmentRepository(EF_DataContext context) : base(context) { }
    }

}