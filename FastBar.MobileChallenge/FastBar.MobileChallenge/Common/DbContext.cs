using System;
using System.Linq;
using System.Reflection;
using SQLite.Net;

namespace FastBar.MobileChallenge.Common
{
    /// <summary>
    ///     Wrapper for SQLiteConnection to simulate EF's DbContext
    /// </summary>
    public abstract class DbContext : IDisposable
    {
        private readonly SQLiteConnection _connection;

        protected DbContext(SQLiteConnection connection)
        {
            _connection = connection;
            Init();
        }

        public void Delete(object obj) => _connection.Delete(obj);

        public void DeleteAll<T>() => _connection.DeleteAll<T>();

        public void Dispose() => _connection.Dispose();

        public void Insert(object obj) => _connection.Insert(obj);

        public void Update(object obj) => _connection.Update(obj);

        internal void Init()
        {
            foreach (var tableProperty in
                GetType()
                    .GetRuntimeProperties()
                    .Where(p => p.PropertyType.GetGenericTypeDefinition() == typeof (TableQuery<>)))
            {
                var propertyType = tableProperty.PropertyType;
                var entryType = propertyType.GetTypeInfo().GenericTypeArguments[0];

                _connection.CreateTable(entryType);

                var tableQuery = Activator.CreateInstance(propertyType, _connection.Platform, _connection);
                tableProperty.SetValue(this, tableQuery);
            }
        }
    }
}