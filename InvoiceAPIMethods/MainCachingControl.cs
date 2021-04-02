using Azure.AI.FormRecognizer.Training;
using Invoice.Domain;
using Invoice.Domain.Models;
using LinqToDB.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceAPIMethods
{
    public class TrainingModelPayloadType
    {
        public FormTrainingClient trainingClient { get; set; }
        public Uri trainingUri  { get; set; }
    }
    public class MainCachingControl
    {

        public IObservable<TrainingModelPayloadType> trainingUri { get; private set; }
        private MainCachingPipes _pipes { get; }
        public IObservable<IEnumerable<Model>> Models { get; private set; }
        public IObservable<Model> TainedModel { get; private set; }
        
        public IObservable<IEnumerable<Model>> Errormodels { get; private set; }
        public MainCachingControl(MainCachingPipes pipes)
        {
            _pipes = pipes;
            Errormodels = _pipes.ErrorListTransport.Publish().RefCount();
            Models = _pipes.ModelListTransport.Select(list => {
                var errorList = list.Where(m => m is null || m.Status == CustomFormModelStatus.Invalid);
                _pipes.ErrorListTransport.OnNext(errorList);
                return list.Where(m => m?.Fields?.Count > 0 && m?.Status == CustomFormModelStatus.Ready);
            }).Publish().RefCount();
            trainingUri = _pipes.SetTrainingUri;
            TainedModel = _pipes.TrainModelTransport.Select(payload =>
            {
                try
                {
                    var trainingClient = payload.trainingClient;
                    var trainingUri = payload.trainingUri;
                    return InvoiceTrainingService.TrainingModel(trainingClient, trainingUri)
                    .Do(model =>
                    {
                        using var db = new Database();
                        db.Model.Append(model);
                    });
                }
                catch (Exception)
                {

                    throw;
                }
            }).Switch().Publish().RefCount();
        }
    }
}
