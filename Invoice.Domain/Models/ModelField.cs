using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Domain.Models
{
    public class ModelField
    {
        [PrimaryKey, Identity]
        public int ModelFieldId { get; set; }
        public int FieldId { get; set; }
        public int ModelId { get; set; }
    }
}
