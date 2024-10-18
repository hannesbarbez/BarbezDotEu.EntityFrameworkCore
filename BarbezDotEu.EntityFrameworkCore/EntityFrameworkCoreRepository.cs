// Copyright (c) Hannes Barbez. All rights reserved.

using System;
using System.Linq;
using BarbezDotEu.Domain;
using Microsoft.EntityFrameworkCore;

namespace BarbezDotEu.EntityFrameworkCore
{
    /// <summary>
    /// Exposes a smaller subset of the repository logic already present in <see cref="DbContext"/>.
    /// </summary>
    /// <remarks>
    /// This class basically isolates the repository logic from a <see cref="DbContext"/>.
    /// </remarks>
    /// <param name="context">An instance of <see cref="DbContext"/>.</param>
    public class EntityFrameworkCoreRepository<TEntity, TKey>(DbContext context) : IBaseRepository<TEntity, TKey>
        where TEntity : class, IBaseAggregateRoot
        where TKey : IComparable<TKey>, IEquatable<TKey>, IUtf8SpanFormattable
    {
        /// <inheritdoc/>
        public IQueryable<TEntity> Entities => context.Set<TEntity>();

        /// <inheritdoc/>
        public void Add(IBaseEntity<TKey> entity)
        {
            context.Add(entity);
        }

        /// <inheritdoc/>
        public void Remove(IBaseEntity<TKey> entity)
        {
            context.Remove(entity);
        }
    }
}
