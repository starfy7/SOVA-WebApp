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
    [Route("api/tags")]
    public class TagController : Controller
    {
        private readonly IDataService _dataService;
        private readonly IUrlHelper _urlHelper;

        public TagController(IDataService dataService, IUrlHelper urlHelper)
        {
            _dataService = dataService;
            _urlHelper = urlHelper;
        }

        [HttpGet(Name = nameof(GetTags))]
        public IActionResult GetTags(ResourceParameters resourceParameters)
        {
            var data = _dataService.GetTags(resourceParameters);
            return Ok(CreateLinkedResult(data));
        }

        [HttpGet("{id}", Name = nameof(GetTag))]
        public IActionResult GetTag(int id)
        {
            var tag = _dataService.GetTag(id);
            if (tag == null) return NotFound();
            return Ok(CreateLinks<TagModel>(tag));
        }

        private object CreateLinkedResult(PagedList<Tag> data)
        {
            return new
            {
                Values = data.Select(CreateLinks<TagModel>),
                Links = CreateLinks(data)
            };
        }

        private List<LinkModel> CreateLinks(PagedList<Tag> data)
        {
            var links = new List<LinkModel>
            {
                CreateLinkModel(nameof(GetTags), new {data.CurrentPage, data.PageSize}, "self", "GET")
            };

            if (data.HasPrev)
            {
                links.Add(CreateLinkModel(nameof(GetTags), new { PageNumber = data.CurrentPage - 1, data.PageSize }, "prev_page", "GET"));
            }

            if (data.HasNext)
            {
                links.Add(CreateLinkModel(nameof(GetTags), new { PageNumber = data.CurrentPage + 1, data.PageSize }, "next_page", "GET"));
            }

            return links;
        }
        private T CreateLinks<T>(Tag tag) where T : LinkedResourceModel
        {
            var routeValues = new { tag.Id };
            var model = Mapper.Map<T>(tag);
            model.Url = CreateUrl(nameof(GetTag), routeValues);


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
