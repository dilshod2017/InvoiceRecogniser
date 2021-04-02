using Invoice.Domain.Models;
using System;
using System.Collections.Generic;
using Invoice.Domain;
using LinqToDB;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceRepository
{
    public abstract class Repository<T> : IInvoiceRepository<T> where T : class
    {
        public Type _type { get; } = typeof(T);

        public abstract Task<T> Get(Func<T, bool> predicate);

        public async Task<IEnumerable<T>> GetAll()
        {
            using var db = Database.NewDatabaseInstance.Value;
            var method = typeof(DataExtensions).GetMethods()
              .FirstOrDefault(m => m.Name == nameof(DataExtensions.GetTable));
            var genericMethod = method.MakeGenericMethod(_type);
            var list = await((ITable<T>)genericMethod.Invoke(null, new object[] { db })).ToListAsync();
            return list;
        }

        public async Task<T> InsertOrUpdate(T item)
        {
            try
            {
                var list = await GetAll();
                var oldModel = list.FirstOrDefault(Getelement(item));
                using var db = Database.NewDatabaseInstance.Value;
                var methodName = oldModel is null
                    ? nameof(DataExtensions.InsertWithInt32Identity)
                    : nameof(DataExtensions.Update);
                var returnObj = Invoke(db, item, methodName);
                return default;
            }
            catch (Exception e)
            {

                throw;
            }

        }

        public async Task<bool> Remove(T item)
        {
            try
            {
                using var db = Database.NewDatabaseInstance.Value;
                var removed = await RemoveAsync(db, Getelement(item));
                return removed;
            }
            catch (Exception e)
            {

                throw;
            }
        }
        protected static Func<T, bool> Getelement(T item)
        {
            return (T x) =>
            {
                var p = typeof(T).GetProperties();
                var pId = p.FirstOrDefault(x => x.Name.EndsWith("Id"));
                if (pId is null) throw new Exception("missing field");
                var xId = (int)pId.GetValue(x);
                var itemId = (int)pId.GetValue(item);
                var pp = xId == itemId;
                return pp;
            };
        }
        protected async Task<bool> RemoveAsync(Database db, Func<T, bool> expression)
        {
            var itemList = await GetAll();
            var item = itemList.FirstOrDefault(expression);
            if (item is null) throw new Exception("Item not found");
            var p = (int)Invoke(db, item, nameof(DataExtensions.Delete));
            return p == 1;
        }
        protected static object? Invoke(Database db, T item, string methodName)
        {
            var method = typeof(DataExtensions).GetMethods()
                .FirstOrDefault(m => m.Name == methodName);
            var genericMethod = method.MakeGenericMethod(typeof(T));
            var p = genericMethod
                .Invoke(null, new object[] { db, item, null, null, null, null });
            return p;
        }
    }
}
