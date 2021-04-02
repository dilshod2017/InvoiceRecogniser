using Invoice.Domain.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceAPIMethods
{
    public class MainCachingPipes
    {
        public ReplaySubject<IEnumerable<Model>> ModelListTransport { get; } = new();
        public ReplaySubject<TrainingModelPayloadType> TrainModelTransport { get; } = new();
        public ReplaySubject<Model> TrainingComplete { get; } = new();
        public ReplaySubject<TrainingModelPayloadType> SetTrainingUri { get; } = new();

        public ReplaySubject<IEnumerable<Model>> ErrorListTransport { get; } = new();

    }
}
