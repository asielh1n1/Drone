using DroneCore.Entities;
using DroneCore.Interfaces;

namespace DroneApi.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        public IRepository<Drone> Drone { get; set; }
        public IRepository<Medication> Medication { get; set; }
        public IRepository<Delivery> Delivery { get; set; }
        public IRepository<DeliveryDetail> DeliveryDetail { get; set; }

        private ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Drone = new Repository<Drone>(context);
            Medication = new Repository<Medication>(context);
            Delivery = new Repository<Delivery>(context);
            DeliveryDetail = new Repository<DeliveryDetail>(context);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void BeginTransaction()
        {
            _context.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _context.Database.CommitTransaction();
        }

        public void RollbackTransaction()
        {
            _context.Database.RollbackTransaction();
        }
    }
}
