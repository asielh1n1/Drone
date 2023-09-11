using DroneApi.Models;
using DroneCore.Entities;
using DroneCore.Interfaces;

namespace DroneApi.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        public IDroneRepository Drone { get; set; }
        public IRepository<Medication> Medication { get; set; }
        public IRepository<Delivery> Delivery { get; set; }

        private ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Drone = new DroneRepository(context);
            Medication = new Repository<Medication>(context);
            Delivery = new Repository<Delivery>(context);
        }

        public void Save()
        {
            _context.SaveChanges();
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
