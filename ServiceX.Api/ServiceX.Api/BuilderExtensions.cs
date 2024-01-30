// <copyright file="BuilderExtensions.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

namespace ServiceX.Api.Extensions;

public static class BuilderExtensions
{
    public static ILogger<T> CreateSimpleLogger<T>(this WebApplicationBuilder builder)
    {
        using var loggerFactory = LoggerFactory.Create(config =>
        {
            config.AddConsole()
            .AddDebug()
            .AddAzureWebAppDiagnostics();
        });

        return loggerFactory.CreateLogger<T>();
    }
}
