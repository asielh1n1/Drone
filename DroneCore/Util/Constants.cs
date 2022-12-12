using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneCore.Util
{
    public static class Constants
    {
        public static class DroneModel
        {
            public const string Lightweight = "Lightweight";
            public const string Middleweight = "Middleweight";
            public const string Cruiserweight = "Cruiserweight";
            public const string Heavyweight = "Heavyweight";
        }

        public static class DroneState
        {
            public const string IDLE = "IDLE";
            public const string LOADING = "LOADING";
            public const string LOADED = "LOADED";
            public const string DELIVERING = "DELIVERING";
            public const string DELIVERED = "DELIVERED";
            public const string RETURNING = "RETURNING";
        }

        public static class DeliveryState
        {
            public const string LOADING = "LOADING";
            public const string DELIVERING = "DELIVERING";
            public const string COMPLETED = "COMPLETED";
        }
    }
}
