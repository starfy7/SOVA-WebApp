using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SovaDatabase;
using DataAccessLayer;
using Webservice.Models;
using AutoMapper;
using DomainModel;

namespace Webservice.Controllers
{
    [Route("api/posts")]
    public class PostController : Controller
    {
        private readonly IDataService _dataService;
        private readonly IUrlHelper _urlHelper;

        public PostController(IDataService dataService, IUrlHelper urlHelper)
        {
            _dataService = dataService;
            _urlHelper = urlHelper;
        }
        
        [HttpGet(Name = nameof(GetPosts))]
        public IActionResult GetPosts(ResourceParameters resourceParameters)
        {
            var data = _dataService.GetPosts(resourceParameters);
            return Ok(CreateLinkedResult(data));
        }

        [HttpGet("{id}", Name = nameof(GetPost))]
        public IActionResult GetPost(int id)
        {
            var post = _dataService.GetPost(id);
            if (post == null) return NotFound();
            return Ok(CreateLinks<PostModel>(post));
        }

        private object CreateLinkedResult(PagedList<Post> data)
        {
            return new
            {
                Values = data.Select(CreateLinks<PostModel>),
                Links = CreateLinks(data)
            };
        }

        private List<LinkModel> CreateLinks(PagedList<Post> data)
        {
            var links = new List<LinkModel>
            {
                CreateLinkModel(nameof(GetPosts), new {data.CurrentPage, data.PageSize}, "self", "GET")
            };

            if (data.HasPrev)
            {
                links.Add(CreateLinkModel(nameof(GetPosts), new { PageNumber = data.CurrentPage - 1, data.PageSize }, "prev_page", "GET"));
            }

            if (data.HasNext)
            {
                links.Add(CreateLinkModel(nameof(GetPosts), new { PageNumber = data.CurrentPage + 1, data.PageSize }, "next_page", "GET"));
            }

            return links;
        }
        private T CreateLinks<T>(Post post) where T : LinkedResourceModel
        {
            var routeValues = new { post.Id };
            var model = Mapper.Map<T>(post);
            model.Url = CreateUrl(nameof(GetPost), routeValues);

            
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
