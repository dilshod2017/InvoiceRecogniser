using System;
using System.Linq;
using System.Threading.Tasks;
using Invoice.Domain;
using LinqToDB;

namespace InvoiceRepository
{
    public class FieldRepository<Fields> : Repository<Fields> where Fields : Invoice.Domain.Models.Fields
    {
        public override async Task<Fields> Get(Func<Fields, bool> predicate)
        {
            using var db = Database.NewDatabaseInstance.Value;
            var responce = await db.Fields.FirstOrDefaultAsync(f => predicate.Invoke((Fields) f));
            return (Fields)responce;
        }
    }
}
