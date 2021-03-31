using Invoice.Domain.Models;
using System;

namespace InvoiceAPIMethods
{
    public static class InvoiceTrainingService
    {
        public static IObservable<Model> GetModels(Func<Model,bool> predicate = null)
        {
            return null;
        }
    }
}
