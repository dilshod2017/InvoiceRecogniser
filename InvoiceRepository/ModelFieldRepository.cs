using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceRepository
{
    public class ModelFieldRepository<ModelField> : Repository<ModelField> where ModelField : class
    {
        public override Task<ModelField> Get(Func<ModelField, bool> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
