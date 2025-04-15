using Storage.Layer.Service;
using Storage.Layer.Service.Repositories.Interfaces;
using Storage.Layer.Service.Repositories;
using Microsoft.EntityFrameworkCore;
using Storage.Layer.Service.Data;
using Storage.Layer.Service.Services;
using Storage.Layer.Service.Domain;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddHostedService<Worker>();
        services.AddSingleton<MessageProcess>();

        services.AddDbContext<MeterFarmDbContext>(options =>
            options.UseMySql(
                context.Configuration.GetConnectionString("DefaultConnection"),
                ServerVersion.AutoDetect(context.Configuration.GetConnectionString("DefaultConnection"))
            ));

        services.AddScoped<ICommandRepository, CommandRepository>();
        services.AddSingleton<IPublisherService, RabbitMQPublisherService>();
        services.AddHostedService<RabbitMQConsumerService>();
    })
    .Build();

host.Run();
