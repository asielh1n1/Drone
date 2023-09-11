using DroneApi.Models;
using DroneCore.Entities;
using DroneCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DroneApi.Infrastructure
{
	public class DroneRepository:Repository<Drone>,IDroneRepository
	{
		public DroneRepository(ApplicationDbContext context) : base(context)
		{
		}

		public List<Drone> Find(string state)
		{
			IQueryable<Drone> query = dbSet;
			return query.Where(x=> x.State == state).ToList();
		}
	}
}
