using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneCore.Util
{
    public class DroneException : Exception
    {
        public DroneException(string message) : base(message) { }

        public DroneException(string message, Exception inner) : base(message, inner) { }
    }
}
