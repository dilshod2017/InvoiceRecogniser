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
        [HttpGet]
        public IActionResult Index()
        {
            using var db = Database.NewDatabaseInstance.Value;
            return Ok();
        }
    }
}
