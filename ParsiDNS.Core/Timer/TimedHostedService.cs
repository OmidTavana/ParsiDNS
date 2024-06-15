using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ParsiDNS.Core.Repository;
using ParsiDNS.DataLayer.Context;

namespace ParsiDNS.Core.Timer
{
    public class TimedHostedService : IHostedService, IDisposable
    {
        private System.Threading.Timer _weeklyTimer;
        private System.Threading.Timer _monthlyTimer;

        private IServiceProvider _serviceProvider;

        public TimedHostedService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            // تنظیم Timer برای اجرای هفتگی
            _weeklyTimer = new System.Threading.Timer(DoWeeklyWork, null, TimeSpan.Zero,
                TimeSpan.FromDays(7));

            // تنظیم Timer برای اجرای ماهانه
            _monthlyTimer = new System.Threading.Timer(DoMonthlyWork, null, TimeSpan.Zero,
                TimeSpan.FromDays(30));

            return Task.CompletedTask;
        }

        private void DoWeeklyWork(object state)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var scopedContext = scope.ServiceProvider.GetRequiredService<ParsiDnsContext>();
                var WeeklyLikes = scopedContext.DnsSoftware.Where(like => like.LastWeekLikeCount != 0);

                foreach (var item in WeeklyLikes)
                {
                    item.LastWeekLikeCount = 0;
                }

                scopedContext.SaveChanges();
            }
        }

        private void DoMonthlyWork(object state)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var scopedContext = scope.ServiceProvider.GetRequiredService<ParsiDnsContext>();
                var MonthlyLikes = scopedContext.DnsSoftware.Where(like => like.LastMonthLikeCount != 0);

                foreach (var item in MonthlyLikes)
                {
                    item.LastMonthLikeCount = 0;
                }

                scopedContext.SaveChanges();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _weeklyTimer?.Change(Timeout.Infinite, 0);
            _monthlyTimer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _weeklyTimer?.Dispose();
            _monthlyTimer?.Dispose();
        }
    }
}
