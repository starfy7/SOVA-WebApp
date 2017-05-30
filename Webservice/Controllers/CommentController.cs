using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SovaDatabase;
using DataAccessLayer;
using Webservice.Models;
using AutoMapper;
using DomainModel;
using Microsoft.AspNetCore.Mvc;

namespace Webservice.Controllers
{
    [Route("api/comments")]
    public class CommentController : Controller
    {
        private readonly IDataService _dataService;
        private readonly IUrlHelper _urlHelper;

        public CommentController(IDataService dataService, IUrlHelper urlHelper)
        {
            _dataService = dataService;
            _urlHelper = urlHelper;
        }

        [HttpGet(Name = nameof(GetComments))]
        public IActionResult GetComments(ResourceParameters resourceParameters)
        {
            var data = _dataService.GetComments(resourceParameters);
            return Ok(CreateLinkedResult(data));
        }

        [HttpGet("{id}", Name = nameof(GetComment))]
        public IActionResult GetComment(int id)
        {
            var comment = _dataService.GetComment(id);
            if (comment == null) return NotFound();
            return Ok(CreateLinks<CommentModel>(comment));
        }

        private object CreateLinkedResult(PagedList<Comment> data)
        {
            return new
            {
                Values = data.Select(CreateLinks<CommentModel>),
                Links = CreateLinks(data)
            };
        }

        private List<LinkModel> CreateLinks(PagedList<Comment> data)
        {
            var links = new List<LinkModel>
            {
                CreateLinkModel(nameof(GetComments), new {data.CurrentPage, data.PageSize}, "self", "GET")
            };

            if (data.HasPrev)
            {
                links.Add(CreateLinkModel(nameof(GetComments), new { PageNumber = data.CurrentPage - 1, data.PageSize }, "prev_page", "GET"));
            }

            if (data.HasNext)
            {
                links.Add(CreateLinkModel(nameof(GetComments), new { PageNumber = data.CurrentPage + 1, data.PageSize }, "next_page", "GET"));
            }

            return links;
        }
        private T CreateLinks<T>(Comment comment) where T : LinkedResourceModel
        {
            var routeValues = new { comment.Id };
            var model = Mapper.Map<T>(comment);
            model.Url = CreateUrl(nameof(GetComment), routeValues);


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