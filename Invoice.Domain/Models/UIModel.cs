using System.Collections.Generic;
using Invoice.Domain.Models;

namespace Invoice.Domain.Models
{
    public class UIModel : Model
    {
        public IEnumerable<Fields> Fields { get; set; }
        public string Status { get; set; }
    }
}