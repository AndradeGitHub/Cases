using System;
using System.Data;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Bson;

using audatex.br.audabridge2.domain.model;
using audatex.br.audabridge2.infrastructure.persistence.interfaces;

namespace audatex.br.audabridge2.infrastructure.persistence
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : EntityBase
    {
        private static UnitOfWork _db;        
        private static IMongoCollection<TEntity> _collection;                

        public Repository(IUnitOfWork unitOfWork)
        {
            _db = unitOfWork as UnitOfWork;           
        }

        public Repository(IMongoDatabase repositoryBase, string collection)
        {
            _collection = repositoryBase.GetCollection<TEntity>(collection);         
        }

        public void Add(TEntity entity)
        {
            _db.Set<TEntity>().Add(entity);
        }

        public void Delete(TEntity entity)
        {
            _db.Set<TEntity>().Remove(entity);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _db.Set<TEntity>().ToList();
        }

        public TEntity GetById(int id)
        {
            return _db.Set<TEntity>().Where(x => x.Id.Equals(id)).FirstOrDefault();            
        }

        public void AddAsync(TEntity entity)
        {
            entity.Id = (_collection.Find(new BsonDocument(), null).SortByDescending(x => x.Id).FirstOrDefault().Id) + 1;

            var addAsync = AddAsyncById(entity);
        }
        private async Task AddAsyncById(TEntity entity)
        {            
            await _collection.InsertOneAsync(entity);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            await _collection.FindOneAndReplaceAsync(x => x.Id == entity.Id, entity, null);
        }

        public async Task DeleteAsync(TEntity entity)
        {
            await _collection.DeleteOneAsync(x => x.Id == entity.Id);
        }

        public IEnumerable<TEntity> GetAllAsync()
        {
            return _collection.Find(new BsonDocument(), null).ToListAsync<TEntity>().Result;
        }

        public TEntity GetByIdAsync(int id)
        {
            return _collection.Find(x => x.Id == id, null).ToListAsync<TEntity>().Result.FirstOrDefault();
        }
    }
}