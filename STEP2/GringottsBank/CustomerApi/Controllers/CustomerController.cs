using CustomerApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public CustomerController(IntToNetCoreContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<string> ListCustomers() {
            return _context.Customer.Select(x=>x.Name).ToList() ;
        }
    }
}
