﻿using FindIt.Domain.Common;
using FindIt.Persistence.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace FindIt.Persistence.Repositories.Classes
{
    public class UnitOfWork(StoreDbContext _dbContext) : IUnitOfWork
    {   
        private readonly ConcurrentDictionary<string, object> _repositories = new();
        public IGenericRepository<T> Repository<T>() where T : BaseEntity
        {
            var type = typeof(T).Name;
            return (IGenericRepository<T>)_repositories.GetOrAdd(type, _ => new GenericRepository<T>(_dbContext));
        }

        public async Task<int> CompleteAsync()=> await _dbContext.SaveChangesAsync();


        public void Dispose()=>_dbContext.Dispose();


        public async ValueTask DisposeAsync()=> await _dbContext.DisposeAsync();
        
    }
}