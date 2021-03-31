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


        private FormRecognizerClient authenticateFormrecognizer() => new(new Uri(endpoint), new AzureKeyCredential(apiKey));
        private FormTrainingClient authenticateTrainingClient() => new(new Uri(endpoint), new AzureKeyCredential(apiKey));

        [HttpGet]
        public IActionResult Index()
        {
            var formRecogniserClient = authenticateFormrecognizer();

        }
    }
}
