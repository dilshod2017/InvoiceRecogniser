using Azure.AI.FormRecognizer;
using Azure.AI.FormRecognizer.Models;
using Azure.AI.FormRecognizer.Training;
using Invoice.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;

namespace InvoiceAPIMethods
{
    public static class InvoiceTrainingService
    {
        public static IObservable<Model> AnalizeForm(FormRecognizerClient formRecognizer, Uri invoiceURI)
        {
            var form = _getRecognizedForm(formRecognizer, invoiceURI).ToObservable();

            return Observable.Return(new Model());
        }
        private static async Task<FormPageCollection> _getRecognizedForm(FormRecognizerClient formRecognizer, Uri invoiceUri)
        {
            var form = await formRecognizer.StartRecognizeContentFromUri(invoiceUri).WaitForCompletionAsync();
            return form;
        }
        public static IObservable<IEnumerable<Model>> GetModels(FormTrainingClient trainingClient)
        {
            var pages = trainingClient.GetCustomModels();
            var list = pages.Select(fm => new Model()
            {
                Status = fm.Status,
                RawModelId = fm.ModelId,
                TrainingCompletedOn = fm.TrainingCompletedOn
            });
            return Observable.Return(list);
        }
        public static IObservable<Model> GetModel(FormTrainingClient trainingClient, string modelId)
        {
            var rawModel = trainingClient.GetCustomModel(modelId);
            var model = rawModel.Value;
            return Observable.Return(new Model()
            {
                Fields = model.Submodels[0].Fields
            });
        }
        public static IObservable<Model> TrainingModel(FormTrainingClient trainingClient, Uri trainingUri)
        {
            var model = trainingClient.StartTrainingAsync(trainingUri, true).WaitForCompletionAsync();
            model.Wait();
            var result = model.Result;
            var value = result.Value;
            Fields fields = (Fields)(value.Submodels[0]?.Fields);
            using var db = Invoice.Domain.Database.NewDatabaseInstance.Value;
            
            return Observable.Return(new Model()
            {
                FieldsId = 
                Status = value.Status,
                RawModelId = value.ModelId,
                CreatedDateTime = value.TrainingCompletedOn.UtcDateTime,
                UserId = 1
            });
        }
 
    }
}
