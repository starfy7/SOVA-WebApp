using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SovaDatabase;
using DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using Webservice.Models;
using AutoMapper;
using DomainModel;

namespace Webservice.Controllers
{
    [Route("api/users")]
    public class UserController : Controller
    {
        private readonly IDataService _dataService;
        private readonly IUrlHelper _urlHelper;

        public UserController(IDataService dataService, IUrlHelper urlHelper)
        {
            _dataService = dataService;
            _urlHelper = urlHelper;
        }

        [HttpGet(Name = nameof(GetUsers))]
        public IActionResult GetUsers(ResourceParameters resourceParameters)
        {
            var data = _dataService.GetUsers(resourceParameters);

            return Ok(CreateLinkedResult(data));
        }


        [HttpGet("{id}", Name = nameof(GetUser))]
        public IActionResult GetUser(int id)
        {
            var user = _dataService.GetUser(id);
            if (user == null) return NotFound();
            return Ok(CreateLinks<UserModel>(user));
        }

        


        ////////////////////
        /// 
        /// Helper Methods
        ///
        ////////////////////

        private object CreateLinkedResult(PagedList<User> data)
        {
            return new
            {
                Values = data.Select(CreateLinks<UserModel>),
                Links = CreateLinks(data)
            };
        }

        private List<LinkModel> CreateLinks(PagedList<User> data)
        {
            var links = new List<LinkModel>
            {
                CreateLinkModel(nameof(GetUsers), new {data.CurrentPage, data.PageSize}, "self", "GET")
            };

            if (data.HasPrev)
            {
                links.Add(CreateLinkModel(nameof(GetUsers), new { PageNumber = data.CurrentPage - 1, data.PageSize }, "prev_page", "GET"));
            }

            if (data.HasNext)
            {
                links.Add(CreateLinkModel(nameof(GetUsers), new { PageNumber = data.CurrentPage + 1, data.PageSize }, "next_page", "GET"));
            }

            return links;
        }
        private T CreateLinks<T>(User user) where T : LinkedResourceModel
        {
            var routeValues = new { user.Id };
            var model = Mapper.Map<T>(user);
            model.Url = CreateUrl(nameof(GetUser), routeValues);

            return model;
        }

        private LinkModel CreateLinkModel(string routeString, object routeValues, string rel, string method)
        {
            return new LinkModel { Href = CreateUrl(routeString, routeValues), Rel = rel, Method = method };
        }

        private string CreateUrl(string routeString, object routeValues)
        {
            return _urlHelper.Link(routeString, routeValues);
        }
    }
}
