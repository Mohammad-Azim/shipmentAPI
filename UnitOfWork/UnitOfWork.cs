using ShipmentAPI.Interfaces;
using ShipmentAPI.EfCore;
using ShipmentAPI.Repositories;

namespace ShipmentAPI.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private EF_DataContext context;

        public UnitOfWork(EF_DataContext context)
        {
            this.context = context;
            User = new UserRepository(this.context);
            Shipment = new ShipmentRepository(this.context);
            Carrier = new CarrierRepository(this.context);
            CarrierService = new CarrierServiceRepository(this.context);
        }


        public IUserRepository User
        {
            get;
        }

        public IShipmentRepository Shipment
        {
            get;
        }

        public ICarrierRepository Carrier
        {
            get;
        }

        public ICarrierServiceRepository CarrierService
        {
            get;
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public int Save()
        {
            return context.SaveChanges();
        }

    }
}