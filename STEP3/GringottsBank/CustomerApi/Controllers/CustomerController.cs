using CustomerApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IntToNetCoreContext _context;
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(IntToNetCoreContext context, ILogger<CustomerController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public List<string> ListCustomers() {

            List<string> customerList = _context.Customer.Select(x => x.Name).ToList();

            //Log customer count
            _logger.LogInformation($"There are {customerList.Count} customers in the bank. ");

            return customerList;
        }
    }
}
