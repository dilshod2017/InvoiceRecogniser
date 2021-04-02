using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Invoice.Domain;
using Invoice.Domain.Models;
using LinqToDB;

namespace InvoiceRepository
{
    public class ModelRepository<UIModel> : Repository<UIModel> where UIModel : Invoice.Domain.Models.UIModel, new()
    {
        public override async Task<UIModel> Get(Func<UIModel, bool> predicate)
        {
            using var db = Database.NewDatabaseInstance.Value;
            var q = from model in db.Model
                where predicate.Invoke((UIModel) model)
                let Status = db.Status.FirstOrDefault(s => s.StatusId == model.Status)
                let Fields = db.ModelField
                    .Where(m => m.ModelId == model.ModelId)
                    .InnerJoin(db.Fields,
                        ((modField, fields) => modField.FieldId == fields.FieldsId),
                        ((modField, fields) => new
                        {
                            fields.Accuracy,
                            fields.FieldType,
                            fields.Label,
                            fields.Name,
                            fields.Text
                        }))
                select new UIModel()
                {
                    ModelId = model.ModelId,
                    RawModelId = model.RawModelId,
                    RwaModel = model.RwaModel,
                    Status = Status.StatusLabel,
                    UserId = model.UserId,
                    TrainingCompletedOn = model.TrainingCompletedOn,
                    CreatedDateTime = model.CreatedDateTime,
                    Fields = (IEnumerable<Fields>) Fields
                };
            var list = await q.ToListAsync();
            return list.FirstOrDefault();
        }
    }
}
