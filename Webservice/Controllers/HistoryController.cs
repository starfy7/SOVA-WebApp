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
    [Route("api/histories")]
    public class HistoryController : Controller
    {
        private readonly IDataService _dataService;
        private readonly IUrlHelper _urlHelper;

        public HistoryController(IDataService dataService, IUrlHelper urlHelper)
        {
            _dataService = dataService;
            _urlHelper = urlHelper;
        }

        [HttpGet(Name = nameof(GetHistories))]
        public IActionResult GetHistories(ResourceParameters resourceParameters)
        {
            var data = _dataService.GetHistories(resourceParameters);

            return Ok(CreateLinkedResult(data));
        }


        [HttpGet("{id}", Name = nameof(GetHistory))]
        public IActionResult GetHistory(int userId, int postId)
        {
            var history = _dataService.GetHistory(userId, postId);
            if (history == null) return NotFound();
            return Ok(CreateLinks<HistoryModel>(history));
        }

        [HttpPost(Name = nameof(CreateHistory))]
        public IActionResult CreateHistory([FromBody]HistoryModel model)
        {
            if (model == null) return BadRequest();

            var history = Mapper.Map<History>(model);
            _dataService.CreateHistory(history);
            return CreatedAtRoute(nameof(GetHistory), new { userId = history.UserId, postId = history.PostId }, Mapper.Map<HistoryModel>(history));
        }

        [HttpPut("{id}", Name = nameof(UpdateHistory))]
        public IActionResult UpdateHistory(int userId, int postId, [FromBody] HistoryModel model)
        {
            if (model == null) return BadRequest();
            var history = _dataService.GetHistory(userId, postId);
            if (history == null) return NotFound();
            Mapper.Map(model, history);
            _dataService.UpdateHistory(history);
            return NoContent();
        }

        [HttpDelete("{id}", Name = nameof(DeleteHistory))]
        public IActionResult DeleteHistory(int userId, int postId)
        {
            var history = _dataService.GetHistory(userId, postId);
            if (history == null) return NotFound();
            _dataService.DeleteHistory(history);
            return NoContent();
        }


        ////////////////////
        /// 
        /// Helper Methods
        ///
        ////////////////////

        private object CreateLinkedResult(PagedList<History> data)
        {
            return new
            {
                Values = data.Select(CreateLinks<HistoryModel>),
                Links = CreateLinks(data)
            };
        }

        private List<LinkModel> CreateLinks(PagedList<History> data)
        {
            var links = new List<LinkModel>
            {
                CreateLinkModel(nameof(GetHistories), new {data.CurrentPage, data.PageSize}, "self", "GET")
            };

            if (data.HasPrev)
            {
                links.Add(CreateLinkModel(nameof(GetHistories), new { PageNumber = data.CurrentPage - 1, data.PageSize }, "prev_page", "GET"));
            }

            if (data.HasNext)
            {
                links.Add(CreateLinkModel(nameof(GetHistories), new { PageNumber = data.CurrentPage + 1, data.PageSize }, "next_page", "GET"));
            }

            return links;
        }
        private T CreateLinks<T>(History history) where T : LinkedResourceModel
        {
            var routeValues = new { history.UserId, history.PostId };
            var model = Mapper.Map<T>(history);
            model.Url = CreateUrl(nameof(GetHistory), routeValues);

            model.Links.Add(CreateLinkModel(nameof(UpdateHistory), routeValues, "update", "PUT"));

            model.Links.Add(CreateLinkModel(nameof(DeleteHistory), routeValues, "delete", "DELETE"));

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
