using Azure;
using Azure.AI.FormRecognizer;
using Azure.AI.FormRecognizer.Training;
using Invoice.Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invoice.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceController : Controller
    {

        string endpoint = "https://extractformfields.cognitiveservices.azure.com/";
        string apiKey = "412702f6ae9d4064b72955b51551e91b";
        string trainingUri = "https://formrecognision.blob.core.windows.net/invoice?sp=racwdl&st=2021-03-31T18:43:30Z&se=2022-04-01T18:43:00Z&sv=2020-02-10&sr=c&sig=wPuRjb7mU4YiKGsoVgsW5a190ZGlwULKQ3VATFnehaQ%3D";
        string invoiceUri = "https://formrecognision.blob.core.windows.net/invoice/test.png";

        private FormRecognizerClient FormrecognizerClient() => new(new Uri(endpoint), new AzureKeyCredential(apiKey));
        private FormTrainingClient TrainingClient() => new(new Uri(endpoint), new AzureKeyCredential(apiKey));

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var formRecogniserClient = FormrecognizerClient();
            var trainingClient = TrainingClient();
            var trainedModel = await trainingClient.StartTrainingAsync(new Uri(trainingUri), true).WaitForCompletionAsync();
            var models = trainingClient.GetCustomModels();
            var model = trainingClient.GetCustomModel(modelId);

        }
    }
}
