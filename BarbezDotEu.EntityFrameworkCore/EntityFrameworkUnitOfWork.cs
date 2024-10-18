// Copyright (c) Hannes Barbez. All rights reserved.

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BarbezDotEu.Domain;
using Microsoft.EntityFrameworkCore;

namespace BarbezDotEu.EntityFrameworkCore
{
    /// <summary>
    /// Exposes a smaller subset of the unit of work logic already present in <see cref="DbContext"/>.
    /// </summary>
    /// <remarks>
    /// This class basically isolates the Unit of Work logic from a <see cref="DbContext"/>.
    /// </remarks>
    /// <param name="context">An instance of <see cref="DbContext"/>.</param>
    public class EntityFrameworkUnitOfWork(DbContext context) : IBaseUnitOfWork<RootEntity>
    {
        /// <inheritdoc/>
        /// <remarks>Return objects are not tracked by EF Core.</remarks>
        public async Task<IEnumerable<TEntity>> RawQueryAsync<TEntity>(string sql, params object[] parameters)
            where TEntity : class
        {
            return await context.Set<TEntity>().FromSqlRaw(sql, parameters).AsNoTracking().ToListAsync();
        }

        /// <inheritdoc/>
        public async Task AddRangeAsync(IEnumerable<RootEntity> entities, CancellationToken cancellationToken = default)
        {
            await context.AddRangeAsync(entities, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await context.SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public int SaveChanges()
        {
            return context.SaveChanges();
        }
    }
}
