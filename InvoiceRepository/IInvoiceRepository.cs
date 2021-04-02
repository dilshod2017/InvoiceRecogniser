using Invoice.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceRepository
{
    public interface IInvoiceRepository<T>
    {
        Task<T> Get(Func<T, bool> predicate);
        Task<IEnumerable<T>> GetAll();
        Task<T> InsertOrUpdate(T newModel);
        Task<bool> Remove(T model);
    }
}
