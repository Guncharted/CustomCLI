using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GunchartedCLI.Tools;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Guncharted.DownloadsWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly DownloadsFolderCleaner _downloadsFolderCleaner;

        public DateTime LastCheckDate { get; set; }

        public Worker(ILogger<Worker> logger, DownloadsFolderCleaner downloadsFolderCleaner)
        {
            _logger = logger;
            _downloadsFolderCleaner = downloadsFolderCleaner;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                if (IsTimeToCheck())
                {
                    _downloadsFolderCleaner.StartMove();
                    LastCheckDate = DateTime.UtcNow;
                }
            }
        }

        private bool IsTimeToCheck()
        {
            if (LastCheckDate == default)
                return true;

            var timespan = DateTime.UtcNow - LastCheckDate;

            return timespan > TimeSpan.FromDays(1) ? true : false;
        }
    }
}
