using Azure.AI.FormRecognizer.Training;
using Invoice.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

namespace InvoiceAPIMethods
{
    public static class InvoiceTrainingService
    {
        public static IObservable<IEnumerable<RawModel>> GetModels(FormTrainingClient trainingClient)
        {
            var pages = trainingClient.GetCustomModels();
            var list = pages.Select(fm => new RawModel()
            {
                Status = fm.Status,
                RawModelId = fm.ModelId,
                TrainingCompletedOn = fm.TrainingCompletedOn
            }).ToList();
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

    }
}
