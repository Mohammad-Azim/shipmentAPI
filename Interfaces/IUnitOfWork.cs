namespace ShipmentAPI.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {

        IUserRepository User
        {
            get;
        }
        IShipmentRepository Shipment
        {
            get;
        }

        ICarrierRepository Carrier
        {
            get;
        }

        ICarrierServiceRepository CarrierService
        {
            get;
        }





        int Save();
    }
}
