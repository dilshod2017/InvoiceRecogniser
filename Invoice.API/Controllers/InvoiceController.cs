using Azure;
using Azure.AI.FormRecognizer;
using Azure.AI.FormRecognizer.Training;
using Invoice.Domain;
using InvoiceAPIMethods;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace Invoice.API.Controllers
{
    public class PostBody
    {
        public string trainingUri { get; set; }
    }
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceController : Controller
    {
        private MainCachingControl _mainControl { get; }
        private MainCachingPipes _pipes { get; }
        string trainingUri = "https://formrecognision.blob.core.windows.net/invoice?sp=racwdl&st=2021-03-31T18:43:30Z&se=2022-04-01T18:43:00Z&sv=2020-02-10&sr=c&sig=wPuRjb7mU4YiKGsoVgsW5a190ZGlwULKQ3VATFnehaQ%3D";

        public InvoiceController(MainCachingControl mainControl, MainCachingPipes pipes)
        {
            _mainControl = mainControl;
            _pipes = pipes;
            _pipes.SetTrainingUri.OnNext(new TrainingModelPayloadType()
            {
                trainingUri = new Uri(trainingUri)
            });
        }
        string endpoint = "https://extractformfields.cognitiveservices.azure.com/";
        string apiKey = "412702f6ae9d4064b72955b51551e91b";
        string invoiceUri = "https://formrecognision.blob.core.windows.net/invoice/test.png";

        private FormRecognizerClient FormrecognizerClient() => new(new Uri(endpoint), new AzureKeyCredential(apiKey));
        private FormTrainingClient TrainingClient() => new(new Uri(endpoint), new AzureKeyCredential(apiKey));

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var formRecogniserClient = FormrecognizerClient();
            var trainingClient = TrainingClient();


            var train = InvoiceTrainingService.TrainingModel(trainingClient, new Uri(trainingUri));
            var trainedModel = await train;
            var trainedModels = InvoiceTrainingService.GetModels(trainingClient);
            var models = await trainedModels;
            return Ok(trainedModel);
        }
        [HttpPost]
        public async Task<IActionResult> Train()
        {
            var trainingUri = await _pipes.SetTrainingUri.FirstAsync();

            _pipes.TrainModelTransport.OnNext(new TrainingModelPayloadType()
            {
                trainingClient = TrainingClient(),
                trainingUri = trainingUri.trainingUri
            });
            var trainedModel = await _mainControl.TainedModel.FirstAsync();
            return Ok(trainedModel);
        }
    }
}
