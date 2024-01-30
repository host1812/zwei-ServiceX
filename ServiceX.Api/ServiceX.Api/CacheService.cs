// <copyright file="CacheService.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace ServiceX.Api;

/// <summary>
/// Cache service
/// </summary>
public class CacheService : ICacheService
{
    private readonly IDistributedCache distributedCache;

    /// <summary>
    /// Constructor for <see cref="./CacheService.cs"/>
    /// </summary>
    /// <param name="distributedCache">Distributed cache</param>
    public CacheService(IDistributedCache distributedCache)
    {
        this.distributedCache = distributedCache;
    }

    /// <summary>
    /// Delete a cache entry
    /// </summary>
    /// <param name="key">The cache key</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    public async Task DeleteKey(string key, CancellationToken cancellationToken = default)
    {
        await distributedCache.RemoveAsync(key, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Get a cache entry
    /// </summary>
    /// <typeparam name="T">The generic type for the data</typeparam>
    /// <param name="key">The cache key</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
    {
        var cacheValue = await distributedCache.GetStringAsync(key, cancellationToken).ConfigureAwait(false);
        if (string.IsNullOrEmpty(cacheValue))
        {
            return default;
        }

        return JsonConvert.DeserializeObject<T>(cacheValue);
    }

    /// <summary>
    /// Get a cache entry
    /// </summary>
    /// <typeparam name="T">The generic type for the data</typeparam>
    /// <param name="key">The cache key</param>
    /// <param name="provider">A provider to pull the value if not found in cache</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    public async Task<T?> GetAsync<T>(string key, Func<Task<T?>> provider, CancellationToken cancellationToken = default)
    {
        var cacheValue = await GetAsync<T>(key, cancellationToken).ConfigureAwait(false);
        if (cacheValue is not null)
        {
            return cacheValue;
        }

        var valueFromProvider = await provider().ConfigureAwait(false);

        if (valueFromProvider is null)
        {
            return default;
        }

        await SetAsync(key, valueFromProvider, cancellationToken).ConfigureAwait(false);
        return valueFromProvider;
    }

    /// <summary>
    /// Set cache entry
    /// </summary>
    /// <typeparam name="T">The generic type for the data</typeparam>
    /// <param name="key">The cache key</param>
    /// <param name="value">The value to save in cache</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task SetAsync<T>(string key, T value, CancellationToken cancellationToken = default)
    {
        var stringValue = JsonConvert.SerializeObject(value);
        await distributedCache.SetStringAsync(key, stringValue, cancellationToken).ConfigureAwait(false);
    }
}
