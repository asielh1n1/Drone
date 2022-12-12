using DroneCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneCore.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public IRepository<Drone> Drone { get; }
        public IRepository<Medication> Medication { get; }
        public IRepository<Delivery> Delivery { get; }
        public IRepository<DeliveryDetail> DeliveryDetail { get; }
        public Task Save();
        public void BeginTransaction();
        public void CommitTransaction();
        public void RollbackTransaction();
    }
}
