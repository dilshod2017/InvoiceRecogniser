using Azure.AI.FormRecognizer.Training;
using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Domain.Models
{
    public class Model :IModel
    {
        [PrimaryKey, Identity]
        public int ModelId { get; set; }
        public string RawModelId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int FieldsId { get; set; }
        public string RwaModel{ get; set; }
        public int Status { get; set; }
        public DateTimeOffset TrainingCompletedOn { get; set; }
    }
    public interface IModel{}
}
