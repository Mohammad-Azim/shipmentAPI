namespace ShipmentAPI.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
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
