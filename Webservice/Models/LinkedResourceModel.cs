using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webservice.Models
{
    public class LinkedResourceModel
    {
        public string Url { get; set; }
        public IList<LinkModel> Links { get; set; } = new List<LinkModel>();
    }
    
}
