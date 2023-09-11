using DroneCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DroneCore.Interfaces
{
    public interface IDroneRepository: IRepository<Drone>
	{
		public List<Drone> Find(string state);
	}
}
