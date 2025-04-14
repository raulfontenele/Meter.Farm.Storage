using Meter.Farm.DTO.Repository;
using Storage.Layer.Service.Data;
using Storage.Layer.Service.Domain;
using Storage.Layer.Service.Interfaces;
using Storage.Layer.Service.Repositories.Interfaces;
using Storage.Layer.Service.Services;
using System.Security.Policy;

namespace Storage.Layer.Service
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IPublisherService _publisher;
        private IDataProcess _messageProcess;
        private const int DELAY_FOR_NEW_RETRY = 1000;

        public Worker(ILogger<Worker> logger, MessageProcess messageProcess, IPublisherService publisher)
        {
            _logger = logger;
            _publisher = publisher;
            _messageProcess = messageProcess;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if(!_messageProcess.IsAvailableRequestPackeage())
                {
                    await Task.Delay(DELAY_FOR_NEW_RETRY, stoppingToken);
                }

                //se for um comando de escrita no banco, chamar uma fun��o ou m�todo de uma classe. Talvez utilizar um padr�o de projeto
            }

                using (var scope = _scopeFactory.CreateScope())
            {
                var repository = scope.ServiceProvider.GetRequiredService<ICommandRepository>();

                var commandStorageObject = new CommandStorageObject
                {
                    CommandName = "Comando 1",
                    Id = 1,
                    MeterUUID = "adfsdfsd",
                    RxCommand = new byte[10],
                    TxCommand = new byte[10],
                };

                await repository.AddAsync(commandStorageObject);
                await repository.SaveChangesAsync();
            }

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                using (var scope = _scopeFactory.CreateScope())
                {
                    var repository = scope.ServiceProvider.GetRequiredService<ICommandRepository>();
                    var commands = await repository.GetAllAsync();

                    foreach (var command in commands)
                    {
                        _logger.LogInformation($"Command: {command.CommandName} | MeterUUID: {command.MeterUUID}");
                    }
                }

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}