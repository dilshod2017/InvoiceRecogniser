using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Domain.Models
{
    public class Fields
    {
        [PrimaryKey, Identity]
        public int FieldsId { get; set; }
        public string Name { get; set; }
        public float? Accuracy { get; set; }
        public string Label { get; set; }
        public string FieldType { get; set; }
        public string Text { get; set; }
    }
}
