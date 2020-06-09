using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyPersonalPlannerBackend.Service.IService;
using NCrontab;

namespace MyPersonalPlannerBackend.Service
{
    public class PlannerBackgroundService : BackgroundService
    {
        private CrontabSchedule _schedule;
        private DateTime _nextRun;
        private IServiceScopeFactory _scopeFactory;

        private  string Schedule = "0 0 0 * * 1";

        public PlannerBackgroundService(IServiceScopeFactory  scopeFactory)
        {
            _scopeFactory = scopeFactory;
            _schedule = CrontabSchedule.Parse(Schedule, new CrontabSchedule.ParseOptions { IncludingSeconds = true });
            _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            do
            {
                var now = DateTime.Now;
                var nextrun = _schedule.GetNextOccurrence(now);
                if (now > _nextRun)
                {
                    Process();
                    _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
                }
                await Task.Delay(5000, stoppingToken);
            }
            while (!stoppingToken.IsCancellationRequested);
        }
        
        private void Process()
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var plannerService = scope.ServiceProvider.GetService<IPlannerService>();
                plannerService.MarkAllItemsAsNotDone();
            }
        }
    }
}