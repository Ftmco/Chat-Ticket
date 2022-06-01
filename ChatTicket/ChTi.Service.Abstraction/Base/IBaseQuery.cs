﻿using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ChTi.Service.Abstraction.Base;

public interface IBaseQuery<TEntity, TContext> where TEntity : class where TContext : DbContext
{
    Task<IEnumerable<TEntity>> GetAllAsync();

    Task<IEnumerable<TEntity>> GetAllAsync(int skip, int take);

    Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> where);

    Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> where,int skip,int take);

    Task<TEntity?> GetAsync(object? id);

    Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> firstOrDefault);

    Task<int> CountAsync();

    Task<int> CountAsync(Expression<Func<TEntity, bool>> count);

    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> any);

    Task<TEntity?> MaxAsync<TKey>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TKey>> max);
}