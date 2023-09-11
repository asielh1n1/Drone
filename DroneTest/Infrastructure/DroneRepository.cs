using DroneCore.Entities;
using DroneCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneTest.Infrastructure
{
	public class DroneRepository : Repository<Drone>, IDroneRepository
	{
		public DroneRepository(ApplicationDbContext context) : base(context)
		{
		}

		public List<Drone> Find(string state)
		{
			IQueryable<Drone> query = dbSet;
			return query.Where(x => x.State == state).ToList();
		}
	}
}
