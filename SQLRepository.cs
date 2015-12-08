using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RevStack.Pattern;
using System.Linq.Expressions;
using System.Collections.ObjectModel;

namespace RevStack.SQL
{
    public class SQLRepository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        private readonly SQLServerDataProvider _database;

        public SQLRepository(SQLDataContext context)
        {
            _database = context.DataProvider;
        }

        public IEnumerable<TEntity> Get()
        {
            return _database.Find<TEntity>().AsEnumerable<TEntity>();
        }

        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _database.Find<TEntity>(predicate);
        }

        public TEntity Add(TEntity entity)
        {
            _database.Insert<TEntity>(entity);
            return entity;
        }

        public TEntity Update(TEntity entity)
        {
            _database.Update<TEntity>(entity);
            return entity;
        }

        public void Delete(TEntity entity)
        {
            _database.Delete<TEntity>(entity);
        }

        public void Execute(string command)
        {
            _database.Execute(command);
        }
    }
}
