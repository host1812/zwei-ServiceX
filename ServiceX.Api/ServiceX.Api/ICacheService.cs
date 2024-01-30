// <copyright file="ICacheService.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

namespace ServiceX.Api;

/// <summary>
/// Interface for <see cref="./CacheService.cs"/> 
/// </summary>
public interface ICacheService
{
    /// <summary>
    /// Get a cache entry
    /// </summary>
    /// <typeparam name="T">The generic type for the data</typeparam>
    /// <param name="key">The cache key</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    public Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get a cache entry
    /// </summary>
    /// <typeparam name="T">The generic type for the data</typeparam>
    /// <param name="key">The cache key</param>
    /// <param name="provider">A provider to pull the value if not found in cache</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    public Task<T?> GetAsync<T>(string key, Func<Task<T?>> provider, CancellationToken cancellationToken = default);

    /// <summary>
    /// Set cache entry
    /// </summary>
    /// <typeparam name="T">The generic type for the data</typeparam>
    /// <param name="key">The cache key</param>
    /// <param name="value">The value to save in cache</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task SetAsync<T>(string key, T value, CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete a cache entry
    /// </summary>
    /// <param name="key">The cache key</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    public Task DeleteKey(string key, CancellationToken cancellationToken = default);
}
