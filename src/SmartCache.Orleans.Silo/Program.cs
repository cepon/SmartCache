﻿using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

IHostBuilder builder = Host.CreateDefaultBuilder(args)
    .UseOrleans(silo =>
    {
        silo.UseLocalhostClustering()
            .ConfigureLogging(logging => logging.AddConsole());
        silo.AddMemoryGrainStorageAsDefault();
        silo.AddMemoryGrainStorage("store");
    })
    .UseConsoleLifetime();

using IHost host = builder.Build();

await host.RunAsync();