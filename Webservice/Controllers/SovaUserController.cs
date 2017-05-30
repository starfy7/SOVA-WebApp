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
    [Route("api/sovausers")]
    public class SovaUserController : Controller
    {
        private readonly IDataService _dataService;
        private readonly IUrlHelper _urlHelper;

        public SovaUserController(IDataService dataService, IUrlHelper urlHelper)
        {
            _dataService = dataService;
            _urlHelper = urlHelper;
        }
        
        [HttpGet(Name = nameof(GetSovaUsers))]
        public IActionResult GetSovaUsers(ResourceParameters resourceParameters)
        {
            var data = _dataService.GetSovaUsers(resourceParameters);
            
            return Ok(CreateLinkedResult(data));
        }
        

        [HttpGet("{id}", Name = nameof(GetSovaUser))]
        public IActionResult GetSovaUser(int id)
        {
            var sovaUser = _dataService.GetSovaUser(id);
            if (sovaUser == null) return NotFound();
            return Ok(CreateLinks<SovaUserModel>(sovaUser));
        }

        [HttpPost(Name = nameof(CreateSovaUser))]
        public IActionResult CreateSovaUser([FromBody]SovaUserCreateModel model)
        {
            if (model == null) return BadRequest();

            var sovaUser = Mapper.Map<SovaUser>(model);
            _dataService.CreateSovaUser(sovaUser);
            return CreatedAtRoute(nameof(GetSovaUser), new { id = sovaUser.Id }, Mapper.Map<SovaUserModel>(sovaUser));
        }

        [HttpPut("{id}", Name = nameof(UpdateSovaUser))]
        public IActionResult UpdateSovaUser(int id, [FromBody] SovaUserCreateModel model)
        {
            if (model == null) return BadRequest();
            var sovaUser = _dataService.GetSovaUser(id);
            if (sovaUser == null) return NotFound();
            Mapper.Map(model, sovaUser);
            _dataService.UpdateSovaUser(sovaUser);
            return NoContent();
        }

        [HttpDelete("{id}", Name = nameof(DeleteSovaUser))]
        public IActionResult DeleteSovaUser(int id)
        {
            var sovaUser = _dataService.GetSovaUser(id);
            if (sovaUser == null) return NotFound();
            _dataService.DeleteSovaUser(sovaUser);
            return NoContent();
        }


        ////////////////////
        /// 
        /// Helper Methods
        ///
        ////////////////////

        private object CreateLinkedResult(PagedList<SovaUser> data)
        {
            return new
            {
                Values = data.Select(CreateLinks<SovaUserListModel>),
                Links = CreateLinks(data)
            };
        }

        private List<LinkModel> CreateLinks(PagedList<SovaUser> data)
        {
            var links = new List<LinkModel>
            {
                CreateLinkModel(nameof(GetSovaUsers), new {data.CurrentPage, data.PageSize}, "self", "GET")
            };

            if (data.HasPrev)
            {
                links.Add(CreateLinkModel(nameof(GetSovaUsers), new {PageNumber = data.CurrentPage -1, data.PageSize}, "prev_page", "GET"));
            }

            if (data.HasNext)
            {
                links.Add(CreateLinkModel(nameof(GetSovaUsers), new { PageNumber = data.CurrentPage +1, data.PageSize }, "next_page", "GET"));
            }

            return links;
        }
        private T CreateLinks<T>(SovaUser sovaUser) where T: LinkedResourceModel
        {
            var routeValues = new {sovaUser.Id};
            var model = Mapper.Map<T>(sovaUser);
            model.Url = CreateUrl(nameof(GetSovaUser), routeValues);

            model.Links.Add(CreateLinkModel(nameof(UpdateSovaUser), routeValues, "update", "PUT"));

            model.Links.Add(CreateLinkModel(nameof(DeleteSovaUser), routeValues, "delete", "DELETE"));

            return model;
        }

        private LinkModel CreateLinkModel(string routeString, object routeValues, string rel, string method)
        {
            return new LinkModel {Href = CreateUrl(routeString, routeValues), Rel = rel, Method = method};
        }

        private string CreateUrl(string routeString, object routeValues)
        {
            return _urlHelper.Link(routeString, routeValues);
        }
    }
}
