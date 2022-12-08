using Microsoft.EntityFrameworkCore;
using ShipmentAPI.EfCore;
using ShipmentAPI.Model;
using ShipmentAPI.Interfaces;
using ShipmentAPI.UnitOfWorks;


namespace ShipmentAPI.Test;
public class ShipmentAPITestBase : IDisposable
{
    protected EF_DataContext _context;
    protected IUnitOfWork _unitOfWork;

    public ShipmentAPITestBase()
    {
        var options = new DbContextOptionsBuilder<EF_DataContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

        _context = new EF_DataContext(options);
        _context.Database.EnsureCreated();
        Seed(_context);
        _unitOfWork = new UnitOfWork(_context);
    }

    public virtual void Seed(EF_DataContext _context)
    {
        var carrier = new[]{
            new Carrier {Id = 1, Name = "fedex" , dimentions_type = "cm" , weight_type="kg"},
            new Carrier {Id = 2, Name = "ups" , dimentions_type = "cm" , weight_type="kg"}
        };

        var carrierService = new[]{
            new CarrierService {Id = 1 , Name = "fedexAIR", CarrierId = 1},
            new CarrierService {Id = 2 , Name = "upsAIR", CarrierId = 2}

        };

        var shipment = new[]{
        new Shipment {Id = 1,width = 5,height = 5, weight = 5,CarrierId = 1 ,CarrierServiceId=1},
        new Shipment {Id = 2,width = 5,height = 5, weight = 5, CarrierId = 1 ,CarrierServiceId=1},
        new Shipment {Id = 3,width = 5,height = 5, weight = 5, CarrierId = 2 ,CarrierServiceId=2},
        };

        _context.Carrier!.AddRange(carrier);
        _context.CarrierService!.AddRange(carrierService);
        _context.Shipments!.AddRange(shipment);

        _context.SaveChanges();
    }

    public void Dispose() // ?????? no need ?
    {
        _unitOfWork.Dispose();
    }
}