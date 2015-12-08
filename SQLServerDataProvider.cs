using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Linq.Expressions;
using ServiceStack.OrmLite;

namespace RevStack.SQL
{
    public class SQLServerDataProvider
    {
        protected OrmLiteConnectionFactory DbFactory;

        private SQLServerDataProvider() { }
        
        public SQLServerDataProvider(string connectionString)
        {
            Init(connectionString);
        }

        OrmLiteConnectionFactory Init(string connStr)
        {
            ConnectionString = connStr;
            IOrmLiteDialectProvider dialectProvider = new ServiceStack.OrmLite.SqlServer.SqlServerOrmLiteDialectProvider();
            DbFactory = new OrmLiteConnectionFactory(ConnectionString, dialectProvider);
            return DbFactory;
        }

        public string ConnectionString { get; set; }

        public void Insert<TEntity>(TEntity entity)
        {
            using (var db = DbFactory.OpenDbConnection())
            {
                //db.CreateTableIfNotExists<TEntity>();
                db.Save<TEntity>(entity, references: true);
            }
        }

        public void Update<TEntity>(TEntity entity)
        {
            using (var db = DbFactory.OpenDbConnection())
            {
                //db.CreateTableIfNotExists<TEntity>();
                db.Save<TEntity>(entity, references: true);
            }
        }

        public void Delete<TEntity>(TEntity entity)
        {
            Type type = typeof(TEntity);
            PropertyInfo property = type.GetProperty("Id");
            object id = property.GetValue(entity);
            using (var db = DbFactory.OpenDbConnection())
            {
                db.DeleteById<TEntity>(id);
            }
        }

        public TEntity Get<TEntity>(object id)
        {
            TEntity entity;

            using (var db = DbFactory.OpenDbConnection())
            {
                entity = db.LoadSingleById<TEntity>(id);
            }

            return entity;
        }

        public void Execute(string command)
        {
            using (var db = DbFactory.OpenDbConnection())
            {
                db.ExecuteNonQuery(command);
            }
        }
        
        public IQueryable<TEntity> Find<TEntity>(Expression<Func<TEntity, bool>> expression)
        {
            IQueryable<TEntity> query;

            using (var db = DbFactory.OpenDbConnection())
            {
                var q = db.LoadSelect<TEntity>(expression);
                query =  q.AsQueryable();
            }

            return query;
        }

        public IQueryable<TEntity> Find<TEntity>()
        {
            IQueryable<TEntity> query;

            using (var db = DbFactory.OpenDbConnection())
            {
                var q = db.LoadSelect<TEntity>();
                query = q.AsQueryable();
            }

            return query;
        }
    }
}
