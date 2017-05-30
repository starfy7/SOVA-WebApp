using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;
using DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using Webservice.Models;

namespace Webservice.Controllers
{
    [Route("api/posttypes")]
    public class PosttypeController : Controller
    {
        private readonly IDataService _dataService;

        public PosttypeController(IDataService dataService)
        {
            _dataService = dataService;
        }

        [HttpGet]
        public IActionResult GetPosttypes()
        {
            var data = _dataService.GetPosttypes();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public IActionResult GetPosttype(int id)
        {
            var posttype = _dataService.GetPosttype(id);
            if (posttype == null) return NotFound();

            return Ok(posttype);
        }
    }
}
