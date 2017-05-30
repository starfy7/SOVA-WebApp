using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webservice.Models
{
    public class PostModel : LinkedResourceModel
    {
        public int PostTypeId { get; set; }
        public int ParentId { get; set; }
        public int AcceptedAnswerId { get; set; }
        public string Body { get; set; }
        public string Title { get; set; }
    }
}
