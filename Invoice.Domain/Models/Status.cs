using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Domain.Models
{
    public class Status
    {
        [PrimaryKey, Identity]
        public int StatusId { get; set; }
        public string StatusLabel { get; set; }
    }
}
