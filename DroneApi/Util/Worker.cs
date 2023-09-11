using DroneApi.Infrastructure;
using DroneApi.Models;
using DroneCore.Entities;

namespace DroneApi.Util
{
    public class Worker : BackgroundService
    {
        private readonly ILogger _logger;
        private readonly IServiceScopeFactory _scopeFactory;
        public IConfiguration Configuration { get; }
        
        public Worker(ILogger<Worker> logger, IServiceScopeFactory scopeFactory, IConfiguration configuration)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
            Configuration = configuration;
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var _context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                    List<Drone> drones = _context.Drone.ToList();
                    foreach (var item in drones)
                    {
                        _logger.LogInformation("Battery level for the drone {0} is {1}%", item.SerialNumber, item.BatteryCapacity);
                    }

                }
                // Only 5 minutes test, so that the changes can be seen
                await Task.Delay(TimeSpan.FromSeconds(int.Parse(Configuration["TimeCheckDroneBattery"])), stoppingToken);                
            }
            
        }
    }
}
